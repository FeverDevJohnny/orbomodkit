using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using UnityEditor.SceneManagement;
using Steamworks;
using System.Threading.Tasks;

public class OrboWorkshopBuilderHelper : EditorWindow
{
    public Mod currentMod;
    int status;
    int uploadingStatus = 0;

    static bool initializedClient = false;

    ModProgress m_modUploadProgress;
public static void ActivateWorkshopBuilder(Mod targetMod)
    {
        OrboWorkshopBuilderHelper window = (OrboWorkshopBuilderHelper)EditorWindow.GetWindow(typeof(OrboWorkshopBuilderHelper));

        window.minSize = new Vector2(600, 400);
        window.maxSize = new Vector2(600, 400);

        window.currentMod = targetMod;

        //SteamAPI.Init();

        window.titleContent = new GUIContent("Workshop Uploading Utility");
        window.Show();
        window.AttemptWorkshopBuild();
    }

    public void AttemptWorkshopBuild()
    {
        status = TryUpload();

        if (status == 0)
            UploadBegin();
    }

    public async void UploadBegin()
    {
        Debug.Log(currentMod.ModDirectory);
        Debug.Log(currentMod.ModCore);
        Debug.Log(currentMod.ModScreenshot);

        uploadingStatus = 1;

        m_modUploadProgress = new ModProgress();

        Steamworks.Ugc.PublishResult result;

        if (!currentMod.hasKnownFileID)
        {
            Steamworks.Ugc.Editor upload = Steamworks.Ugc.Editor.NewCommunityFile.WithTitle(currentMod.modName).WithDescription(currentMod.modDesc).WithContent(currentMod.ModDirectory);

            switch (currentMod.workshopVisiblity)
            {
                case WorkshopVisiblity.Private:
                    upload = upload.WithPrivateVisibility();
                    break;

                case WorkshopVisiblity.FriendsOnly:
                    upload = upload.WithFriendsOnlyVisibility();
                    break;

                case WorkshopVisiblity.Public:
                    upload = upload.WithPublicVisibility();
                    break;
            }


            if (currentMod.modIcon != null)
                upload = upload.WithPreviewFile(currentMod.ModScreenshot);

            result = await upload.SubmitAsync(m_modUploadProgress);
        }
        else
        {
            Steamworks.Ugc.Editor update = new Steamworks.Ugc.Editor(currentMod.GetFileID()).WithTitle(currentMod.modName).WithDescription(currentMod.modDesc).WithContent(currentMod.ModDirectory);

            switch (currentMod.workshopVisiblity)
            {
                case WorkshopVisiblity.Private:
                    update = update.WithPrivateVisibility();
                    break;

                case WorkshopVisiblity.FriendsOnly:
                    update = update.WithFriendsOnlyVisibility();
                    break;

                case WorkshopVisiblity.Public:
                    update = update.WithPublicVisibility();
                    break;
            }

            if (currentMod.modIcon != null)
                update = update.WithPreviewFile(currentMod.ModScreenshot);

            result = await update.SubmitAsync(m_modUploadProgress); //Steamworks.Ugc.Editor.NewCommunityFile.WithTitle(currentMod.modName).WithDescription(currentMod.modDesc).WithPreviewFile(currentMod.ModScreenshot).WithContent(currentMod.ModDirectory).SubmitAsync(m_modUploadProgress);
        }

        if (result.Success)
        {
            currentMod.UpdateModWorkshopAddress(result.FileId);
            uploadingStatus = 2;
        }
        else
        {
            if (result.NeedsWorkshopAgreement)
            {
                Application.OpenURL("https://steamcommunity.com/sharedfiles/workshoplegalagreement");
                status = 4;
            }
            else
            {
                
                if(currentMod.hasKnownFileID && result.Result == Result.FileNotFound)
                {
                    currentMod.RemoveWorkshopAddress();
                    UploadBegin();
                    return;
                }

                status = 5;
            }
        }
    }

    public void OnGUI()
    {
        GUILayout.Space(32f);
        if (status == 0)
        {
            switch (uploadingStatus)
            {
                case 0:
                    CenterText("Constructing workshop item...");
                    break;

                case 1:
                    if (currentMod.hasKnownFileID)
                    {
                        CenterText($"Updating mod {currentMod.modName} on the workshop...");
                        CenterText($"{Mathf.RoundToInt(m_modUploadProgress.progress)}% Complete...");
                    }
                    else
                    {
                        CenterText($"Uploading new mod {currentMod.modName} to the workshop...");
                        CenterText($"{Mathf.RoundToInt(m_modUploadProgress.progress)}% Complete...");
                    }
                    break;

                case 2:
                    CenterText("Mod upload complete! Congratulations!");
                    break;
            }


            GUILayout.Space(16f);

            if (uploadingStatus == 2)
            {
                if (CenterButton("OK"))
                    Close();
            }
            else
            {
                if (CenterButton("Cancel"))
                    CancelUpload();
            }
        }
        else
        {
            switch (status)
            {
                case 1:
                    CenterText("ERROR! COULD NOT CONNECT TO STEAM TO BEGIN UPLOAD!");
                    CenterText("Please confirm that you are logged in and have the app up and running.");
                    break;

                case 2:
                    CenterText("ERROR! BUILD MISSING!");
                    CenterText("You need to hit \"Build Mod\" first before you can upload to the steam workshop!");
                    break;

                case 3:
                    CenterText("ERROR! FAILED TO BUILD ITEM!");
                    CenterText("Something went wrong during the upload process! Please try again.");
                    break;

                case 4:
                    CenterText("ERROR! NEED TO ACCEPT WORKSHOP AGREEMENT!");
                    CenterText("Please accept the workshop upload agreement before proceeding to upload your mod.");
                    break;

                case 5:
                    CenterText("ERROR! FAILED TO UPLOAD WORKSHOP CONTENT!");
                    CenterText("This can be caused by several things, but a common culprit is trying to upload a mod with an icon that's larger than 1MB.");
                    CenterText("Try exporting your icon as a jpeg, or otherwise reduce the resolution, then re-build your mod and try to upload again.");
                    CenterText("If that fails to work, try asking for help in the steam discussions.");
                    break;
            }

            GUILayout.Space(16f);

            if (CenterButton("Retry"))
                RetryConnection();
            if (CenterButton("Cancel"))
                CancelUpload();
        }
    }

    void RetryConnection()
    {
        AttemptWorkshopBuild();
    }
    void CancelUpload()
    {
        this.Close();
    }

    public void CenterText(string text)
    {
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label(text);
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
    }

    public bool CenterButton(string text)
    {
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        bool m_ret = GUILayout.Button(text, GUILayout.Height(64), GUILayout.Width(256));
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        return m_ret;
    }

    public int TryUpload()
    {
        if (!Directory.Exists(currentMod.ModDirectory))
            return 2;
        else
            if (!File.Exists(currentMod.ModCore))
                return 2;

        try
        {
            if (!initializedClient)
            {
                SteamClient.Init(2539960, true);
                initializedClient = true;
            }
        }
        catch
        {
            return 1;
        }

        return 0;
    }
}

public class ModProgress : System.IProgress<float>
{
    public float progress;
    public void Report(float value)
    {
        progress = Mathf.Max(value * 100f, progress);
    }
}
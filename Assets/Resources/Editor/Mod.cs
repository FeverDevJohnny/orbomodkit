using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.IO;
using Steamworks.Data;
using MyBox;
//using Steamworks;

public class Mod : ScriptableObject
{
    [LimitString(30)]
    public string modName;
    [LimitStringTextArea(200, 8)]
    public string modDesc;
    [LimitString(20)]
    public string modAuthor;
    public Texture2D modIcon;
    public string version;
    [Space]
    public string initialSceneToLoad;
    public Vector3 spawnPosition;
    [Range(0f,360f)] public float spawnYaw;
    [Space]
    public SceneAsset[] scenes = new SceneAsset[0];

    [HideInInspector]
    public ulong associatedFileID;
    [HideInInspector]
    public bool hasKnownFileID = false;
    [Space]
    public WorkshopVisiblity workshopVisiblity = WorkshopVisiblity.Private;

    public void UpdateModWorkshopAddress(PublishedFileId newID)
    {
        hasKnownFileID = true;
        associatedFileID = newID.Value;
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    public PublishedFileId GetFileID()
    {
        return new PublishedFileId() { Value = associatedFileID };
    }

    public void RemoveWorkshopAddress()
    {
        hasKnownFileID = false;
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
    }
    

    public string ModDirectory => Path.GetFullPath($"{Path.Combine(Application.dataPath, "../")}/ModExports/{name.Replace("Mod_", "")}").Replace("\\","/");
    public string ModScreenshot => $"{ModDirectory}/{name}_icon.png";
    public string ModCore => $"{ModDirectory}/{name}_core.json";

}

[System.Serializable]
public class ModData
{
    public string modName;
    public string modDesc;
    public string modAuthor;
    public string modIconAddress;
    public string version;
    public Vector3 spawnPosition;
    public float spawnYaw;
    public string initialSceneToLoad;
    public string sceneAssetBundle;
    public string coreAssetBundle;
    public string modkitVersion;
    public string folderName;
}

public enum WorkshopVisiblity
{ 
    Private = 0,
    FriendsOnly = 1,
    Public = 2
}


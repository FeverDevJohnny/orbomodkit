using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEditor.SceneManagement;
using UnityEditor.Build.Pipeline;

[CustomEditor(typeof(Mod))]
public class ModEditor : Editor
{
    SerializedObject m_target;
    SerializedProperty m_sceneArray, m_version, m_modName, m_modDesc, m_modIcon, m_modInitScene, m_spawnYaw, m_spawnPos;
    Mod targetMod;
    int buildValidity = 0;
    int m_lastCount;

    public void OnEnable()
    {
        targetMod = target as Mod;
        SceneView.duringSceneGui += DrawHandlesInScene;
        UpdateTargetModSerializables();


        buildValidity = 0;

        if (string.IsNullOrEmpty(targetMod.modName))
            buildValidity |= 1 << 1;

        if (string.IsNullOrEmpty(targetMod.version))
            buildValidity |= 1 << 2;

        if (targetMod.scenes.Length == 0)
            buildValidity |= 1 << 3;

        if (string.IsNullOrEmpty(targetMod.initialSceneToLoad))
            buildValidity |= 1 << 4;
        else
        {
            if (targetMod.scenes.Length != 0)
            {
                buildValidity |= 1 << 5;

                for (int i = 0; i < targetMod.scenes.Length; i++)
                    if (targetMod.scenes[i] != null)
                    {
                        if (targetMod.scenes[i].name == targetMod.initialSceneToLoad)
                            buildValidity &= ~(1 << 5);
                    }
                    else
                        buildValidity |= 1 << 6;
            }
        }
    }
    public void OnDisable()
    {
        SceneView.duringSceneGui -= DrawHandlesInScene;
    }

    public void CenterText(string text)
    {
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label(text);//EditorGUILayout.LabelField("Welcome to the Orbo modding toolset!");
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

    public override void OnInspectorGUI()
    {
        if (targetMod != null)
        {
            CenterText($"Modifying: {targetMod.name}");
            CenterText("Your assets should be stored in the ModContent sub-folder.");
            CenterText("Place your mod icon in the same folder as this mod asset.");
            EditorGUIUtility.labelWidth = EditorGUIUtility.currentViewWidth - 332;
            GUILayout.Space(16);

            EditorGUI.BeginChangeCheck();
            m_lastCount = targetMod.scenes.Length;

            DrawDefaultInspector();

            if (EditorGUI.EndChangeCheck() || m_lastCount != targetMod.scenes.Length)
            {
                //Validity sweep
                m_target.ApplyModifiedProperties();

                buildValidity = 0;

                if (string.IsNullOrEmpty(targetMod.modName))
                    buildValidity |= 1 << 1;

                if (string.IsNullOrEmpty(targetMod.version))
                    buildValidity |= 1 << 2;

                if (targetMod.scenes.Length == 0)
                    buildValidity |= 1 << 3;

                if (string.IsNullOrEmpty(targetMod.initialSceneToLoad))
                    buildValidity |= 1 << 4;
                else
                {
                    if (targetMod.scenes.Length != 0)
                    {
                        buildValidity |= 1 << 5;

                        for (int i = 0; i < targetMod.scenes.Length; i++)
                            if (targetMod.scenes[i] != null)
                            {
                                if (targetMod.scenes[i].name == targetMod.initialSceneToLoad)
                                    buildValidity &= ~(1 << 5);
                            }
                            else
                                buildValidity |= 1 << 6;
                    }
                }

            }

            GUILayout.Space(16);
            GUILayout.Space(16);

            if (buildValidity == 0)
            {
                if (CenterButton("Build Mod"))
                    BuildMod();
                if (CenterButton((targetMod.hasKnownFileID) ? "Update On Steam Workshop" : "Upload To Steam Workshop"))
                    OpenSteamWorkshopBuilder();
            }
            else
            {
                CenterText("Invalid mod configuration!");

                if ((buildValidity & 1 << 1) != 0)
                    CenterText("Please provide a name for your mod!");

                if ((buildValidity & 1 << 2) != 0)
                    CenterText("Version should not be empty!");

                if ((buildValidity & 1 << 3) != 0)
                    CenterText("You must have at least one scene in your scenes array!");

                if ((buildValidity & 1 << 4) != 0)
                    CenterText("You must define a starting scene!");

                if ((buildValidity & 1 << 5) != 0)
                    CenterText("Starting scene must match one of the scenes in your scenes array!");

                if ((buildValidity & 1 << 6) != 0)
                    CenterText("Null scenes cannot be in the scenes array!");

                CenterButton("INVALID");
            }

            if (Selection.activeObject != targetMod)
                targetMod = null;
        }
    }

    public void DrawHandlesInScene(SceneView obj)
    {
        if (targetMod != null)
        {
            if (targetMod.initialSceneToLoad != string.Empty)
            {
                if (targetMod.initialSceneToLoad == SceneManager.GetActiveScene().name)
                {
                    Handles.color = Color.yellow;
                    Handles.DrawWireDisc(targetMod.spawnPosition, Vector3.up, 1f);

                    Quaternion rot = Quaternion.Euler(0f, targetMod.spawnYaw, 0f);
                    Vector3 a = targetMod.spawnPosition + Vector3.up * 1f, b = a + rot * Vector3.forward * 1f;

                    Handles.DrawLine(targetMod.spawnPosition, a);
                    Handles.DrawLine(a, b);
                    Handles.DrawLine(b, b - rot * Vector3.forward * 0.5f + rot * Vector3.Cross(Vector3.forward, Vector3.up) * 0.2f);
                    Handles.DrawLine(b, b - rot * Vector3.forward * 0.5f - rot * Vector3.Cross(Vector3.forward, Vector3.up) * 0.2f);
                }
            }
        }    
    }

    public void UpdateTargetModSerializables()
    {
        m_target = new SerializedObject(target);

        m_version = m_target.FindProperty("version");
        m_modName = m_target.FindProperty("modName");
        m_modDesc = m_target.FindProperty("modDesc");
        m_sceneArray = m_target.FindProperty("scenes");
        m_modIcon = m_target.FindProperty("modIcon");
        m_modInitScene = m_target.FindProperty("initialSceneToLoad");
        m_spawnYaw = m_target.FindProperty("spawnYaw");
        m_spawnPos = m_target.FindProperty("spawnPosition");
    }

    public void OpenSteamWorkshopBuilder()
    {
        OrboWorkshopBuilderHelper.ActivateWorkshopBuilder(targetMod);
    }

    public void ValidateBuild()
    {

    }

    public void BuildMod()
    {
        string bundleDir = "ModExports";

        if (SceneManager.GetActiveScene().IsValid())
            EditorSceneManager.SaveScene(SceneManager.GetActiveScene());

        if (!Directory.Exists(bundleDir))
            Directory.CreateDirectory(bundleDir);

        if (!Directory.Exists(bundleDir + $"/{targetMod.name.Replace("Mod_", "")}"))
            Directory.CreateDirectory(bundleDir + $"/{targetMod.name.Replace("Mod_", "")}");

        List<string> standardAssets = new List<string>(0);
        List<string> scenes = new List<string>(0);

        Object[] unsorted = Resources.LoadAll($"Mods/{targetMod.name.Replace("Mod_", "")}/ModContent");

        for (int i = 0; i < unsorted.Length; i++)
            if (unsorted[i] as SceneAsset == null)
            {
                string str = AssetDatabase.GetAssetPath(unsorted[i]);
                if (!standardAssets.Contains(str))
                    standardAssets.Add(str);
            }

        for (int i = 0; i < targetMod.scenes.Length; i++)
        {
            scenes.Add(AssetDatabase.GetAssetPath(targetMod.scenes[i]));
            AssetImporter scn = AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(targetMod.scenes[i]));

            if (scn.assetBundleName != targetMod.name + "_scenes")
                scn.assetBundleName = targetMod.name + "_scenes";
        }

        for (int i = 0; i < standardAssets.Count; i++)
        {
            AssetImporter obj = AssetImporter.GetAtPath(standardAssets[i]);

            if (obj.assetBundleName != targetMod.name)
                obj.assetBundleName = targetMod.name;
        }

        AssetBundleBuild[] builds = new AssetBundleBuild[] { new AssetBundleBuild() { assetBundleName = targetMod.name, assetNames = standardAssets.ToArray() }, new AssetBundleBuild() { assetBundleName = targetMod.name + "_scenes", assetNames = scenes.ToArray() } };

        File.WriteAllText(bundleDir + $"/{targetMod.name.Replace("Mod_", "")}/{targetMod.name}_core.json", JsonUtility.ToJson(new ModData { folderName = $"{targetMod.name.Replace("Mod_", "")}", modIconAddress = ((targetMod.modIcon != null) ? $"{targetMod.name}_icon.png" : "NULL"), coreAssetBundle = targetMod.name.ToLower(), sceneAssetBundle = targetMod.name.ToLower() + "_scenes", modAuthor = targetMod.modAuthor, modName = ((targetMod.modName.Length < 30) ? targetMod.modName : targetMod.modName.Substring(0,30)), modDesc = ((targetMod.modDesc.Length < 200) ? targetMod.modDesc : targetMod.modDesc.Substring(0,200)), spawnPosition = targetMod.spawnPosition, spawnYaw = targetMod.spawnYaw, version = targetMod.version, initialSceneToLoad = targetMod.initialSceneToLoad, modkitVersion = OrboModdingHelper.MODKITVER }));

        if (targetMod.modIcon != null)
        {
            string iconpath = AssetDatabase.GetAssetPath(targetMod.modIcon);
            TextureImporter icon = (AssetImporter.GetAtPath(iconpath) as TextureImporter);
            icon.textureType = TextureImporterType.Default;
            icon.npotScale = TextureImporterNPOTScale.None;
            icon.isReadable = true;
            icon.crunchedCompression = false;
            icon.textureCompression = TextureImporterCompression.Uncompressed;
            AssetDatabase.ImportAsset(iconpath);
            AssetDatabase.Refresh();

            File.WriteAllBytes(bundleDir + $"/{targetMod.name.Replace("Mod_", "")}/{targetMod.name}_icon.png", targetMod.modIcon.EncodeToPNG());
        }

        CompatibilityBuildPipeline.BuildAssetBundles(bundleDir + $"/{targetMod.name.Replace("Mod_", "")}", builds, BuildAssetBundleOptions.ForceRebuildAssetBundle, BuildTarget.StandaloneWindows);

        //BuildPipeline.BuildAssetBundles(bundleDir + $"/{targetMod.name.Replace("Mod_", "")}", builds, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
        EditorUtility.RevealInFinder(bundleDir + $"/{targetMod.name.Replace("Mod_", "")}");

    }

}

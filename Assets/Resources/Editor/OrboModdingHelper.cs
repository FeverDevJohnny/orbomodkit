using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using UnityEditor.SceneManagement;

public class OrboModdingHelper : EditorWindow
{
    public static readonly string MODKITVER = "1.0.0";

    string newModName = "";
    string oldModName = "";
    int nameValidity = 1<<3;


    [MenuItem("Modding/Modkit Helper")]
    static void Init()
    {
        OrboModdingHelper window = (OrboModdingHelper)EditorWindow.GetWindow(typeof(OrboModdingHelper));

        window.minSize = new Vector2(550,350);
        window.maxSize = new Vector2(550,350);

        window.titleContent = new GUIContent("Orbo Modkit Window");
        window.Show();
    }

    [MenuItem("GameObject/Modkit/Items/Gear Part", priority = 10)]
    static void CreateGearPart()
    {
        CreateObjectAtSceneViewPosition("Item - Gear Part", typeof(Shell_GearPart));
    }

    [MenuItem("GameObject/Modkit/Items/Pip Placer", priority = 10)]
    static void CreatePip()
    {
        CreateObjectAtSceneViewPosition("Item - Pip", typeof(Shell_Pips));
    }

    [MenuItem("GameObject/Modkit/Management/Pip Manager", priority = 10)]
    static void CreatePipManager()
    {
        CreateObjectAtSceneViewPosition("Management - Pips", typeof(Shell_PipManager));
    }

    [MenuItem("GameObject/Modkit/Dynamic/Trial Ring", priority = 10)]
    static void CreateTimeTrialRing()
    {
        CreateObjectAtSceneViewPosition("Trial Ring", typeof(Shell_TimeTrialRing));
    }

    [MenuItem("GameObject/Modkit/Management/Trial Manager", priority = 10)]
    static void CreateTimeTrialManager()
    {
        CreateObjectAtSceneViewPosition("Management - Trial Manager", typeof(Shell_TimeTrialManager));
    }

    [MenuItem("GameObject/Modkit/Dynamic/Slab Door", priority = 10)]
    static void CreateSlabDoor()
    {
        CreateObjectAtSceneViewPosition("Slab Door", typeof(Shell_DoorSlab));
    }
    [MenuItem("GameObject/Modkit/Items/Key Orb", priority = 10)]
    static void CreateKeyOrb()
    {
        CreateObjectAtSceneViewPosition("Key Orb", typeof(Shell_KeyOrb));
    }
    [MenuItem("GameObject/Modkit/Event/Button", priority = 10)]
    static void CreateButton()
    {
        CreateObjectAtSceneViewPosition("Button - New", typeof(Shell_Button));
    }
    [MenuItem("GameObject/Modkit/Movement/Cannon", priority = 10)]
    static void CreateCannon()
    {
        GameObject m_can = CreateObjectAtSceneViewPosition("Movement - Cannon", typeof(Shell_Cannon));
        GameObject head = new GameObject("Cannon Head");
        head.transform.SetParent(m_can.transform);
        head.transform.localPosition = Vector3.up;
        m_can.GetComponent<Shell_Cannon>().cannonHead = head.transform;
    }
    [MenuItem("GameObject/Modkit/Movement/Gust Plate", priority = 10)]
    static void CreateGustPlate()
    {
        CreateObjectAtSceneViewPosition("Movement - Gust Plate", typeof(Shell_GustPlate));
    }
    [MenuItem("GameObject/Modkit/Dynamic/Checkpoint", priority = 10)]
    static void CreateCheckpoint()
    {
        Shell_Checkpoint m_check = CreateObjectAtSceneViewPosition("Checkpoint", typeof(Shell_Checkpoint)).GetComponent<Shell_Checkpoint>();
        GameObject spawn = new GameObject("Checkpoint");
        spawn.transform.SetParent(m_check.transform);
        spawn.transform.localPosition = Vector3.forward * 2f;
        m_check.spawnPoint = spawn.transform;
    }
    [MenuItem("GameObject/Modkit/Event/Interact Prompt", priority = 10)]
    static void CreateInteractPrompt()
    {
        CreateObjectAtSceneViewPosition("Interact Prompt", typeof(Shell_InteractPrompt));
    }
    [MenuItem("GameObject/Modkit/Visual/Post Processing", priority = 10)]
    static void CreatePostProcessing()
    {
        CreateObjectAtSceneViewPosition("Visual - Post Processing", typeof(Shell_PostProcessing));
    }
    [MenuItem("GameObject/Modkit/Event/Interactable Event", priority = 10)]
    static void CreateInteractableEvent()
    {
        GameObject m_obj = CreateObjectAtSceneViewPosition("Event - Interactable", typeof(Interactable_Events), typeof(SphereCollider));
        if (m_obj.TryGetComponent<SphereCollider>(out SphereCollider col))
        {
            col.isTrigger = true;
            col.radius = 5f;
        }
        if(m_obj.TryGetComponent<Interactable>(out Interactable inter))
        {
            inter.isInvincible = true;
        }
    }
    [MenuItem("GameObject/Modkit/Audio/BGM Volume", priority = 10)]
    static void CreateBGMVolume()
    {
        CreateObjectAtSceneViewPosition("BGM Volume", typeof(JTools.Audio.BGMVolume));
    }
    [MenuItem("GameObject/Modkit/Damageable/Events", priority = 10)]
    static void CreateDamageableEvents()
    {
        CreateObjectAtSceneViewPosition("Damageable - Events", typeof(Damageable_Events));
    }
    [MenuItem("GameObject/Modkit/Pain/Hurt Volume", priority = 10)]
    static void CreateHurtVolume()
    {
        GameObject m_obj = CreateObjectAtSceneViewPosition("Hurt Volume", typeof(HurtVolume), typeof(BoxCollider));
        m_obj.GetComponent<BoxCollider>().isTrigger = true;
    }
    [MenuItem("GameObject/Modkit/Event/JComposer", priority = 10)]
    static void CreateJComposer()
    {
        CreateObjectAtSceneViewPosition("Sequence - New", typeof(JComposer));
    }
    [MenuItem("GameObject/Modkit/Event/JSwitch", priority = 10)]
    static void CreateJSwitch()
    {
        CreateObjectAtSceneViewPosition("JSwitch - New", typeof(JSwitch));
    }
    [MenuItem("GameObject/Modkit/Event/JTrigger", priority = 10)]
    static void CreateJTrigger()
    {
        GameObject m_obj = CreateObjectAtSceneViewPosition("JTrigger - New", typeof(JTrigger), typeof(Rigidbody));
        m_obj.GetComponent<Rigidbody>().isKinematic = true;
        m_obj.GetComponent<JTrigger>().context = EventTriggerContext.OnDemand;
    }
    [MenuItem("GameObject/Modkit/Pain/Player Kill Trigger", priority = 10)]
    static void CreateKillTrigger()
    {
        GameObject m_obj = CreateObjectAtSceneViewPosition("Player Kill Trigger", typeof(PlayerKillTrigger), typeof(BoxCollider));
        m_obj.GetComponent<BoxCollider>().isTrigger = true;
    }
    [MenuItem("GameObject/Modkit/Event/Scene Transition Volume", priority = 10)]
    static void CreateSceneTransitionVolume()
    {
        GameObject m_obj = CreateObjectAtSceneViewPosition("Scene Transition Volume", typeof(SceneTransitionVolume), typeof(BoxCollider));
        m_obj.GetComponent<BoxCollider>().isTrigger = true;
    }
    [MenuItem("GameObject/Modkit/Visual/Screen Shaker", priority = 10)]
    static void CreateScreenShaker()
    {
        CreateObjectAtSceneViewPosition("Screen Shaker", typeof(ScreenShaker));
    }
    [MenuItem("GameObject/Modkit/Management/Level Binder", priority = 10)]
    static void CreateLevelBinder()
    {
        CreateObjectAtSceneViewPosition("Management - Level Binder", typeof(LevelBinder));
    }
    [MenuItem("GameObject/Modkit/Misc/Size Reference")]
    static void CreateSizeReference()
    {
        CreateObjectAtSceneViewPosition("Size Reference", typeof(SizeRef));
    }

    static GameObject CreateObjectAtSceneViewPosition(string name, params System.Type[] type)
    {
        Camera m_cam = SceneView.lastActiveSceneView.camera;

        GameObject m_obj = new GameObject(name, type);
        m_obj.transform.position = (m_cam?.transform.position ?? Vector3.zero) + (m_cam?.transform.forward * 10f ?? Vector3.zero);
        Selection.activeObject = m_obj;

        Undo.RegisterCreatedObjectUndo(m_obj, $"Created {name}");
        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());

        return m_obj;
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


    private void OnGUI()
    {
        GUILayout.Space(16);

        CenterText("== Welcome to the Orbo's Odyssey modding toolset ==");//EditorGUILayout.LabelField("Welcome to the Orbo modding toolset!");
        CenterText("Please refer to the documentation at [LINK HERE] for help!");

        GUILayout.Space(16);

        ModCreationMenu();        
    }

    public void ModCreationMenu()
    {
        CenterText("You can either select a pre-existing mod in your project folder to edit it,");
        CenterText("or use the configuration system below to make a new one!");

        GUILayout.Space(16);

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label("New Mod Name:");
        newModName = GUILayout.TextField(newModName, GUILayout.Width(200)).Replace(' ', '_');
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.Space(8);

        if (oldModName != newModName)
        {
            nameValidity = 0;

            Regex r = new Regex(@"^[a-zA-Z0-9_-]+$");

            Mod[] mods = Resources.LoadAll<Mod>("Mods/");

            if (!r.IsMatch(newModName))
                nameValidity |= 1<<1;

            if (string.IsNullOrEmpty(newModName))
                nameValidity = 1 << 3; //This error code overwrites the whole thing, because the illegal character regex match will also complain if there's no text.

            foreach (Mod m in mods)
            {
                if ((m.name == "Mod_" + newModName.Replace(' ', '_')))
                    nameValidity |= 1 << 2; 
            }
        }

        if(nameValidity == 0)
        {
            if (CenterButton($"Create Mod {newModName}"))
            {
                if (!AssetDatabase.IsValidFolder("Assets/Resources/Mods"))
                    AssetDatabase.CreateFolder("Assets/Resources", "Mods");

                if (!AssetDatabase.IsValidFolder($"Assets/Resources/Mods/{newModName}"))
                    AssetDatabase.CreateFolder("Assets/Resources/Mods", newModName);

                if (!AssetDatabase.IsValidFolder($"Assets/Resources/Mods/{newModName}/ModContent"))
                    AssetDatabase.CreateFolder($"Assets/Resources/Mods/{newModName}", "ModContent");

                Mod newMod = ScriptableObject.CreateInstance<Mod>();
                newMod.name = "Mod_" + newModName;

                newMod.modName = newModName.Replace('_', ' ');
                newMod.modDesc = "This is where you tell people about your mod! Make sure to be concise!";
                newMod.version = "1.0.0";
                newMod.initialSceneToLoad = "PLEASE ADD A DEFAULT SCENE.";
                
                AssetDatabase.CreateAsset(newMod, $"Assets/Resources/Mods/{newModName}/Mod_{newModName}.asset");
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                Selection.activeObject = newMod;

                newModName = string.Empty;
                oldModName = string.Empty;
                nameValidity = 1;
            }
        }
        else
        {

            CenterText("ERROR! Name invalid.");
            
            if ((nameValidity & (1 << 2)) != 0)
                CenterText("Name already taken. Please write a new one.");

            if ((nameValidity & (1 << 1)) != 0)
                CenterText("Name has illegal characters.");

            if ((nameValidity & (1 << 3)) != 0)
                CenterText("Please provide a name for your new mod.");

            GUILayout.Space(8);

            CenterButton("INVALID NAME");
        }

        GUILayout.Space(16);

        oldModName = newModName;
    }
}

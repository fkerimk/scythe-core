using scythe;
using System.IO;
using UnityEngine;

public class Loader : MonoBehaviour {

    public string devMod;
    
    private void Awake() {
        
        Screen.SetResolution(640, 380, false);
        
        #if UNITY_EDITOR
        var devModPath = Path.Join(Application.dataPath, devMod);
        
        if (Directory.Exists(devModPath)) {
            
            Mod.Load(devModPath);
            return;
        }
        #endif
        
        CommandLine.Initialize();
        
        if (!CommandLine.TryGetArgument("mod", out var modPath)) {

            Debug.LogError("Missing required argument: -mod <path>");
            Application.Quit(1);
            return;
        }

        if (!Directory.Exists(modPath))  {
            
            Debug.LogError($"Invalid mod path: {modPath}");
            Application.Quit(1);
            return;
        }

        var modConfig = Path.Combine(modPath, "mod.ini");
        
        if (!File.Exists(modConfig)) {
            
            Debug.LogError($"Missing mod.ini in {modPath}");
            Application.Quit(1);
            return;
        }

        Mod.Load(modPath);
    }
}

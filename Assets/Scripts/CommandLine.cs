using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using JetBrains.Annotations;

public class CommandLine : MonoBehaviour {
    
    private static Dictionary<string, string> _arguments;

    private void Awake() {
        
        Initialize();
        ProcessArguments(_arguments);
    }

    public static void Initialize() {
        
        if (_arguments != null) return;

        var args = Environment.GetCommandLineArgs();
        _arguments = ParseArguments(args);
    }

    private static Dictionary<string, string> ParseArguments(string[] args) {
        
        var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        for (var i = 1; i < args.Length; i++) {
            
            var currentArg = args[i];

            if ((currentArg.StartsWith("-") || currentArg.StartsWith("+")) && i + 1 < args.Length) {
                
                var key = currentArg[1..];
                var value = args[i + 1];

                if (!(value.StartsWith("-") || value.StartsWith("+"))) {
                    
                    dict[key] = value;
                    i++;

                    continue;
                }
            }

            if (!currentArg.StartsWith("-") && !currentArg.StartsWith("+")) continue; {
                
                var key = currentArg[1..];
                dict[key] = "true";
            }
        }

        return dict;
    }

    public static bool TryGetArgument(string argument, [CanBeNull] out string value) {
        
        if (_arguments != null && _arguments.TryGetValue(argument, out value))
            return true;
        
        value = null;
        return false;
    }

    private static void ProcessArguments(Dictionary<string, string> args) {
        
        if (!TryGetArgument("mod", out var modPath)) {

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

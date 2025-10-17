using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using JetBrains.Annotations;

public class CommandLine : MonoBehaviour {
    
    private static Dictionary<string, string> _arguments;
    
    private void Awake() {

        Initialize();
    }

    public static void Initialize() {
        
        if (_arguments != null) return;
        
        var args = System.Environment.GetCommandLineArgs();
        _arguments = ParseArguments(args);
        
        ProcessArguments(_arguments);
    }
    
    private static Dictionary<string, string> ParseArguments(string[] args) {
        
        var dict = new Dictionary<string, string>();
        
        for (var i = 1; i < args.Length; i++) {
            
            var currentArg = args[i].ToLowerInvariant();

            if ((currentArg.StartsWith("-") || currentArg.StartsWith("+")) && i + 1 < args.Length) {
                
                var value = args[i + 1];
                
                if (!(value.StartsWith("-") || value.StartsWith("+"))) {
                    
                    dict[currentArg[1..]] = value;
                    i++; 
                    
                    continue; 
                }
            }

            if (currentArg.StartsWith("-") || currentArg.StartsWith("+"))
                dict[currentArg[1..]] = "true"; 
        }
        
        return dict;
    }

    public static bool TryGetArgument(string argument, [CanBeNull] out string value, string defaultValue = null) {

        return _arguments.TryGetValue(argument, out value);
    }
    
    private static void ProcessArguments(Dictionary<string, string> args) {
        
        if (TryGetArgument("mod", out var modPath)) {
            
            if (Directory.Exists(modPath)) {
                
                var modConfig = Path.Join(modPath, "mod.ini");

                if (File.Exists(modConfig))
                    Mod.Load(modPath);
                
                else Application.Quit(1);
            }
            
            else Application.Quit(1);
            
        } else Application.Quit(1);
    }
}
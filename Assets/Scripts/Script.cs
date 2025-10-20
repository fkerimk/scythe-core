using System.IO;
using System.Linq;
using System.Collections;
using UnityEngine;

public class Script : MonoBehaviour {

    public static void Run(string path) {

        var relativePath = Mod.RelativePath(path);
        
        var name = Path.GetFileNameWithoutExtension(relativePath);
        var lines = File.ReadAllLines(relativePath);

        if (!File.Exists(relativePath)) {
            
            Logger.Log("Script not found: " + path);
            return;
        }
        
        Logger.Log("Loading script: " + path);
        
        var obj = new GameObject(name);
        var script = obj.AddComponent<Script>();

        script.StartCoroutine(script.Runner(lines));
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator Runner(string[] lines) {

        // ReSharper disable once ForCanBeConvertedToForeach
        for (var i = 0; i < lines.Length; i++) {

            var split = lines[i].Split(' ');

            // ReSharper disable once InvertIf
            if (split.Length >= 2) {
                
                var args = split.Skip(2).ToArray();
                Reflector.Call(split[0], split[1], args);
            }
        }
        
        yield return null;
    }
}

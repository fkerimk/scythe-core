using UnityEditor;
using UnityEngine;

public class BuildScript  {
    
    public static void Build() {
        
        CommandLine.Initialize();
        
        CommandLine.TryGetArgument("buildLinux", out var buildLinux);
        CommandLine.TryGetArgument("buildWindows", out var buildWindows);

        var scenes = new[] { "Assets/Scenes/Main.unity" };
        
        Debug.Log("Linux is good: " + buildLinux);
        Debug.Log("Windows is good: " + buildWindows);
        
        if (buildLinux == "true") {
            
            BuildPipeline.BuildPlayer(
            
                scenes,
                "Builds/linux-x64/scythe-core.x86_64",
                BuildTarget.StandaloneLinux64,
                BuildOptions.None
            );
        }

        if (buildWindows == "true") {
            
            BuildPipeline.BuildPlayer(
            
                scenes,
                "Builds/win-x64/scythe-core.exe",
                BuildTarget.StandaloneWindows64,
                BuildOptions.None
            );
        }
    }
}

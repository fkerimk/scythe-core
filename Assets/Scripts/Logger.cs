using System.IO;
using UnityEngine;

public static class Logger {

    private static string _logPath;

    public static void Init(string path) {

        _logPath = Mod.RelativePath(path);
        Clear();
    }
    
    public static void Log(string message) {
        
        try  { File.AppendAllText(_logPath, $"[{System.DateTime.Now:HH:mm:ss}] {message}\n"); }
        
        catch (System.Exception e) {
            
            Debug.LogError($"Logger is fucked: {e.Message}");
        }
    }

    private static void Clear() {
        
        try { if (File.Exists(_logPath)) File.WriteAllText(_logPath, ""); }
        
        catch (System.Exception e) {
            
            Debug.LogError($"Logger is fucked: {e.Message}");
        }
    }
}
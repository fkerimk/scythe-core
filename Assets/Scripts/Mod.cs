using scythe;
using System.IO;
using UnityEngine;

public static class Mod {
    
    private static string _path;
    private static string _configPath;
    private static IniFile _config;
    private static string _name;

    public static string RelativePath(string path) {
        
        return Path.Join(_path, path);
    }
    
    public static void Load(string modPath) {

        _path = modPath;
        _configPath = Path.Join(_path, "mod.ini");
        _config = new IniFile(_configPath);
        _name = _config.Read("mod", "name", "null");
        
        if (_name == "null") Application.Quit(1);
        
        Logger.Init("../../latest.log");
        
        var init = _config.Read("Mod", "init", "null");

        if (init != "null")
             Script.Run(init);
        else Logger.Log("The init script hasn’t been set up");
    }
}
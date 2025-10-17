using scythe;
using System.IO;
using UnityEngine;
using SysPath = System.IO.Path;

public static class Mod {

    public static string Path;
    public static string ConfigPath;
    public static IniFile Config;
    public static string Name;
    
    public static void Load(string modPath) {

        Path = modPath;
        ConfigPath = SysPath.Join(Path, "mod.ini");
        Config = new IniFile(ConfigPath);
        Name = Config.Read("mod", "name", "null");
        
        File.WriteAllText(SysPath.Join(Application.dataPath, "log.log.log"), Path + "\n" + ConfigPath + "\n" + Name);
        
        //if (Name == "null") Application.Quit(1);
    }
}
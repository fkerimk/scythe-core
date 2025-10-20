using System;
using System.Reflection;

public static class Reflector {
    
    public static void Call(string module, string function, params object[] parameters) {

        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        
        Type type = null;

        foreach (var asm in assemblies) {
            
            type = asm.GetType("mod_" + module);
            if (type != null) break;
        }
        
        //var type = Type.GetType("mod_" + module);
        
        if (type == null) {
            
            Logger.Log("Module not found: " + module);
            return;
        }

        var method = type.GetMethod(function, BindingFlags.Public | BindingFlags.Static);
        
        if (method == null) {
            
            Logger.Log("Function not found: " + function);
            return;
        }

        method.Invoke(null, parameters);
    }
}
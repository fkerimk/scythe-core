using System.IO;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once InconsistentNaming
public class mod_splash : MonoBehaviour {

    public Image background;
    public Image art;

    // ReSharper disable once InconsistentNaming
    public static void show(string path) {

        var instance = FindAnyObjectByType<mod_splash>();
        
        instance.background.enabled = true;
        
        var relativePath = Mod.RelativePath(path);
        
        if (!File.Exists(relativePath)) {
            
            Logger.Log("File not found: " + path);
            return;
        }
        
        Logger.Log("Loading splash screen: " + path);
        
        var bytes = File.ReadAllBytes(relativePath);
            
        var tex = new Texture2D(2, 2);
        tex.LoadImage(bytes);

        var sprite = Sprite.Create(
            tex,
            new Rect(0, 0, tex.width, tex.height),
            new Vector2(0.5f, 0.5f)
        );

        instance.art.sprite = sprite;
        instance.art.enabled = true;
    }
}
using UnityEngine;
using System.Collections;
using System.IO;

public class MainStartScript : StartScript
{
    public static string SAVE_PATH = "..\\saveGames\\Test.txt";
    
    public void Start()
    {
        string  path_to_check = Path.GetDirectoryName(SAVE_PATH);
        if(!Directory.Exists(path_to_check)) Directory.CreateDirectory(path_to_check);

        base.Level = "GameIntro";
        if (File.Exists(SAVE_PATH))
            base.Level = "LevelMenu";
    }
}

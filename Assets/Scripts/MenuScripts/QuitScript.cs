using UnityEngine;
using System.Collections;

public class QuitScript : MonoBehaviour 
{
    public void OnAction()//OnMouseDown
    {
        if (Application.loadedLevelName == "MainMenu")
        {
            Application.Quit();
        }
        else
        {
            Time.timeScale = 1;
            Application.LoadLevel("LevelMenu");
        }
    }
}

using UnityEngine;
using System.Collections;

public class StartScript : MonoBehaviour 
{
    public string Level = "";

    void OnMouseDown()
    {
        if (renderer.enabled)
        {
            LoadLevel();
        }
    }

    public void LoadLevel()
    {
        Time.timeScale = 1;
        Application.LoadLevel(Level);
    }
}

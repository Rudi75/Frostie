using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Text;

public class MoveOnLevelMenu : MonoBehaviour
{
  public Transform Player;
  public Transform LevelButtons;

  public static int currentLevel = 1;
  public int levelCount = 5;
  public static int availableLevels = 1;
  public static bool start = true; 

  
	// Use this for initialization
	void Start ()
  {
      string path_to_check = Path.GetDirectoryName(MainStartScript.SAVE_PATH);
     
        if (!Directory.Exists(path_to_check)) 
          Directory.CreateDirectory(path_to_check);

        if(File.Exists(MainStartScript.SAVE_PATH))
        {
            TextReader reader = File.OpenText(MainStartScript.SAVE_PATH);
            availableLevels = int.Parse(reader.ReadLine());
            if (start)
                availableLevels--;
            reader.Close();
        }

        availableLevels++;
        if (LevelButtons.GetChild(currentLevel))
        {
          Player.position = LevelButtons.GetChild(currentLevel).position + new Vector3(0, 0.3f, -1);
        }

        TextWriter writer = new StreamWriter(MainStartScript.SAVE_PATH, false);
        writer.Write(availableLevels);
        writer.Close();
	}

    void OnLevelWasLoaded(int level)
    {
        var savedData = FindObjectOfType<SavedDataManager>();
        if (savedData != null) savedData.ResetCurrentState();
    }
	
	// Update is called once per frame
	void Update ()
  {
    if(Input.GetKeyDown(KeyCode.RightArrow))
    {
      currentLevel++;
      if (currentLevel > availableLevels)
          currentLevel = availableLevels;

      if (currentLevel > levelCount)
        currentLevel = levelCount;
    }
    else if (Input.GetKeyDown(KeyCode.LeftArrow))
    {
      currentLevel--;
      if (currentLevel < 0)
        currentLevel = 0;
    }
    else if (Input.GetKeyDown(KeyCode.Return))
    {
      if (LevelButtons.GetChild(currentLevel))
      {
          if (LevelButtons.GetChild(currentLevel).GetComponent<StartScript>())
        {
            start = false;
            LevelButtons.GetChild(currentLevel).GetComponent<StartScript>().LoadLevel();
        }
      }
    }


    if (LevelButtons.GetChild(currentLevel))
    {
      Player.position = LevelButtons.GetChild(currentLevel).position + new Vector3(0, 0.3f, -1);
    }
  }

}

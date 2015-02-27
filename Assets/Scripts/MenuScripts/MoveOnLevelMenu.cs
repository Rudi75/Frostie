using UnityEngine;
using System.Collections;

public class MoveOnLevelMenu : MonoBehaviour
{

  public Transform Player;
  public Transform LevelButtons;

  public int currentLevel = 0;
  public int levelCount = 5;

	// Use this for initialization
	void Start ()
  {
    if (LevelButtons.GetChild(currentLevel))
    {
      Player.position = LevelButtons.GetChild(currentLevel).position + new Vector3(0, 0.3f, -1);
    }
	}
	
	// Update is called once per frame
	void Update ()
  {
    if(Input.GetKeyDown(KeyCode.RightArrow))
    {
      currentLevel++;
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
        if (LevelButtons.GetChild(currentLevel).GetComponent<start>())
        {
          LevelButtons.GetChild(currentLevel).GetComponent<start>().LoadLevel();
        }
      }
    }


    if (LevelButtons.GetChild(currentLevel))
    {
      Player.position = LevelButtons.GetChild(currentLevel).position + new Vector3(0, 0.3f, -1);
    }
  }

}

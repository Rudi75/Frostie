using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Utils;

public abstract class SaveableScript : MonoBehaviour
{
    public string ID = "";
    public abstract void saveData(SavedDataContainer dataContainer);
    public abstract void loadData(SavedDataContainer dataContainer);

    public void Awake()
    {
        if (String.IsNullOrEmpty(ID))
            ID = name + transform.position.ToString();
    }
}

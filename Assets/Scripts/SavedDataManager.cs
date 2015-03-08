using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Utils;

public class SavedDataManager : MonoBehaviour
{
    public Dictionary<string, SavedDataContainer> SavedData { get; set; }

    private static SavedDataManager Instance;

    void Awake()
    {
        if (Instance)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            SavedData = new Dictionary<string, SavedDataContainer>();
        }
    }

    void OnLevelWasLoaded(int level)
    {
        LoadCurrentState();
    }

    public void SaveCurrentState()
    {
        var saveable = FindObjectsOfType<SaveableScript>();
        foreach (var item in saveable)
        {
            if (!SavedData.ContainsKey(item.ID))
            {
                var data = new SavedDataContainer(item.ID);
                SavedData.Add(item.ID, data);
            }
            item.saveData(SavedData[item.ID]);
        }
    }

    public void LoadCurrentState()
    {
        var saveable = FindObjectsOfType<SaveableScript>();
        foreach (var item in saveable)
        {
            if (SavedData.ContainsKey(item.ID))
            {
                item.loadData(SavedData[item.ID]);
            }
        }
    }

    public void ResetCurrentState()
    {
        SavedData.Clear();
    }
}

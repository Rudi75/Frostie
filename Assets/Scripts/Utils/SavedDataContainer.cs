using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Utils
{
    public class SavedDataContainer
    {
        public string UniqueID { get; set; }
        public Dictionary<string, object> SavedData { get; set; }

        public SavedDataContainer(string ID)
        {
            UniqueID = ID;
            SavedData = new Dictionary<string, object>();
        }

        public bool AddData(string key, object value)
        {
            if (!SavedData.ContainsKey(key))
            {
                SavedData.Add(key, value);
                return true;
            }
            return false;
        }

        public object retrieveData(string key)
        {
            object value = null;
            if (SavedData.TryGetValue(key, out value))
            {
                return value;
            }
            return null;
        }
    }
}

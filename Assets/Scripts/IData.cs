using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class IData : MonoBehaviour
{
    #region PLAYER PREFS
    // PLAYER PREFS
    int currentLevel = 0;

    void SavePP() => PlayerPrefs.SetInt("currentLevel", currentLevel);

    void LoadPP() => currentLevel = PlayerPrefs.GetInt("currentLevel");

    void DeletePP(string s) => PlayerPrefs.DeleteKey(s);

    void DeleteAllPP() => PlayerPrefs.DeleteAll();
    #endregion

    #region JSON
    public Inventory inventory;

    void LoadJSON()
    {
        string data = PlayerPrefs.GetString("saveData");
        inventory = JsonUtility.FromJson<Inventory>(data);
    }

    void SaveJSON()
    {
        inventory.objects.Add("A");
        inventory.objects.Add("B");
        inventory.objects.Add("C");
        inventory.objects.Add("D");

        string saveData = JsonUtility.ToJson(inventory);
        PlayerPrefs.SetString("saveData", saveData);
    }

    [System.Serializable]
    public class Inventory
    {
        public List<string> objects;
    }
    #endregion
}
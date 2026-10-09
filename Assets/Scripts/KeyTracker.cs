using UnityEngine;
using System.Collections.Generic;
using System;


public class KeyTracker : MonoBehaviour
{
    public string levelName;
    public List<Key> keysInLevel;
    public PlayerController playerController;

    private void Awake()
    {
        string saveKey = "LevelSave_" + levelName;

        if (PlayerPrefs.HasKey(saveKey))
        {
            string json = PlayerPrefs.GetString(saveKey);

            LevelSaveData loadedData = JsonUtility.FromJson<LevelSaveData>(json);

            Debug.Log($"Successfully loaded save data for: {loadedData.levelName}");

            if(loadedData.keyInfo != null){

                Debug.Log(loadedData.keyInfo.Count.ToString());

                foreach (KeyIdPairing pairing in loadedData.keyInfo)
                {
                    foreach (Key k in keysInLevel) { if (k.keyID == pairing.keyID) { k.gameObject.SetActive(!pairing.isCollected); } }
                }
            }
            else Debug.Log("keyInfo is NULL");

            playerController.SetKeys(loadedData.currentKeysHeld);


        }
        else
        {
            Debug.LogWarning($"No save data found for key: {saveKey}");
        }

    }


    public void SaveKeyData()
    {
        LevelSaveData saveData = new LevelSaveData();
        saveData.levelName = levelName;
        saveData.keyInfo = new List<KeyIdPairing>();
        foreach (Key key in keysInLevel)
        {
            KeyIdPairing pairing = new KeyIdPairing();
            pairing.keyID = key.keyID;
            pairing.isCollected = !key.gameObject.activeSelf;
            saveData.keyInfo.Add(pairing);
        }
        saveData.currentKeysHeld = playerController.KeysCollectedCount();
        string json = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString("LevelSave_" + levelName, json);
        PlayerPrefs.Save();
    }


}

[System.Serializable]
public class KeyIdPairing
{
    public string keyID;
    public bool isCollected;
}

[System.Serializable]
public class LevelSaveData
{
    public string levelName;
    public int currentKeysHeld;
    public List<KeyIdPairing> keyInfo;

}

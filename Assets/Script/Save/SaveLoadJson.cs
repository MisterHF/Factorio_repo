using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoadJson : MonoBehaviour
{
    public static SaveLoadJson Instance;

    public GameData _gameData;
    string _saveFilePath;

    /// PlayerData

    //public float _playerSpeed;
    //public float _playerMiningSpeed;
    //public int _playerMiningRange;

    public Dictionary<string, string> items;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            _gameData = new GameData();
            _saveFilePath = Application.dataPath + "/PlayerData.json";
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SaveToJson();
        }

    }

    public void SaveToJson()
    {
        SaveInformations();

        string savePlayerData = JsonUtility.ToJson(_gameData);
        File.WriteAllText(_saveFilePath, savePlayerData);

        Debug.Log("Save file created at: " + _saveFilePath);

    }

    public void LoadGame()
    {
        if (File.Exists(_saveFilePath))
        {
            string loadPlayerData = File.ReadAllText(_saveFilePath);
            _gameData = JsonUtility.FromJson<GameData>(loadPlayerData);
            LoadInformationsInventory();
        }
    }

    public void SaveBuilding()
    {

    }

    public void SaveInventory(Dictionary<string, string> _items)
    {
        items = _items;
        
    }

    private void LoadInformationsInventory()
    {
        /*string[] IDInventoryItem = _gameData.IDITEM.Split('/');
        string[] CountInventoryItem = _gameData.COUNT.Split('/');

        items = new Dictionary<string, string>();
        for (int i = 0; i < IDInventoryItem.Length; i++)
        {
            items.Add(IDInventoryItem[i], CountInventoryItem[i]);
        }*/
    }
    /// <summary>
    /// Met les valeurs des variables de SaveAndLoad dans => GameData
    /// </summary>
    private void SaveInformations()
    {
        //Inventory dictionary
        if (items != null) 
        {
            _gameData.COUNT = "";
            _gameData.IDITEM = "";

            foreach (string IDCount in items.Keys)
            {
                _gameData.IDITEM += IDCount + "/";
                _gameData.COUNT += items[IDCount] + "/";
            }
        }
    }

    public void DeleteSaveFile()
    {
        if (File.Exists(_saveFilePath))
        {
            File.Delete(_saveFilePath);
            Debug.Log("Save delete !");
        }
    }
}

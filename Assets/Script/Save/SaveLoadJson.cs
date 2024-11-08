using System.IO;
using UnityEngine;

public class SaveLoadJson : MonoBehaviour
{
    public static SaveLoadJson Instance;

    public GameData _gameData;
    string _saveFilePath;

    /// PlayerData

    public float _playerSpeed;
    public float _playerMiningSpeed;
    public int _playerMiningRange;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            _gameData = new GameData();
            _saveFilePath = Application.persistentDataPath + "/PlayerData.json";
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
        }
    }

    //public void RegisterInformations(bool alteratePlayerStats, float speed, int mining_range, int mining_speed)
    //{
    //    if (alteratePlayerStats)
    //    {
    //        _playerSpeed = speed;
    //        _playerMiningSpeed = mining_speed;
    //        _playerMiningRange = mining_range;
    //    }

    //}
}

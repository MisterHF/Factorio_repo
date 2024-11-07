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
            _saveFilePath = Application.dataPath + "/GameData.json";
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Scene Play Load");
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}

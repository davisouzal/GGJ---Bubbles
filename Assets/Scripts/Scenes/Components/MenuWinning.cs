using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuWinning : MonoBehaviour
{
    public string menuSceneName = "Introducao";
    // Método para ir ao menu principal
    public void LoadMenu()
    {
        SceneManager.LoadScene(menuSceneName);
    }

    // Método para ir para a próxima fase
    public void LoadNextScene()
    {
        if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(menuSceneName);
        }
    }
}

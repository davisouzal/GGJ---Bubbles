using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuWinning : MonoBehaviour
{
    public string menuSceneName = "Cena 0";
    // Método para ir ao menu principal
    public void LoadMenu()
    {
        SceneManager.LoadScene(menuSceneName);
    }

    // Método para ir para a próxima fase
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

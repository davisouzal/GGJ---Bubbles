using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            // Carrega a próxima cena
            SceneManager.LoadScene("Menu Inicial"); // Substitua "GameScene" pelo nome da sua cena do jogo
        }
    }
}

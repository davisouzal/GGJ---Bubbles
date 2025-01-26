using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // Update is called once per frame
    [SerializeField]private string nomedoleveldojogo;
    [SerializeField]private GameObject painelMenuInicial;
    [SerializeField]private GameObject painelOpcoes;
    public void Jogar()
    {
        SceneManager.LoadScene(nomedoleveldojogo);
    }

    public void AbrirCreditos()
    {
        painelMenuInicial.SetActive(false);
        painelOpcoes.SetActive(true);
    }
    public void FecharCreditos()
    {
        painelMenuInicial.SetActive(true);
        painelOpcoes.SetActive(false);
    }


    public void Exit()
    {
        Application.Quit();
        Debug.Log("Sai do jogo!");
    }
}

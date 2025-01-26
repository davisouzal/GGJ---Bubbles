using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public string nextSceneName = "House";
    public void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }

}

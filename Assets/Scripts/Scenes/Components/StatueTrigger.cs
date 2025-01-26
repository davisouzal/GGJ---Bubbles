using UnityEngine;
using UnityEngine.SceneManagement;

public class StatueTrigger : MonoBehaviour
{
    public GameObject canvas;
    public bool isPlayerClose = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvas.SetActive(false);
    }

    void Update()
    {
        if (isPlayerClose && Input.GetKeyDown(KeyCode.E))
        {
            canvas.SetActive(false);
            SceneManager.LoadScene("Fase");
        }
    }

    // Update is called once per frame
   private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.gameObject.GetComponent<IPlayer>() != null)
        {
            canvas.SetActive(true);
            isPlayerClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.gameObject.GetComponent<IPlayer>() != null)
        {
            canvas.SetActive(false);
            isPlayerClose = false;
        }
    }
}

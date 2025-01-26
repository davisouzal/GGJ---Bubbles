using UnityEngine;

public class LevelWinning : MonoBehaviour
{
    public GameObject canvas;

    private void Start()
    {
        canvas.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.gameObject.GetComponent<IPlayer>() != null)
        {
            canvas.SetActive(true);
        }
    } 
}

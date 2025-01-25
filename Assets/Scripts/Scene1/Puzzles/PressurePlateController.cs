using UnityEngine;

public class PressurePlateController : MonoBehaviour
{
    public Vector3 originalPlatePosition;
    public Vector3 originalDoorPosition;
    public GameObject Door;
    bool moveBack = false;

    // Start é chamado uma vez antes da primeira execução de Update após o MonoBehaviour ser criado
    void Start()
    {
        originalPlatePosition = transform.position;
        originalDoorPosition = Door.transform.position;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.transform.name == "Player")
        {
            transform.Translate(0, -0.015f, 0);
            moveBack = false;
            Door.transform.Translate(0, 0.01f, 0); // Move a porta para cima
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.name == "Player")
        {
            collision.transform.parent = transform;
            GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.transform.name == "Player")
        {
            moveBack = true;
            collision.transform.parent = null;
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    // Update é chamado uma vez por frame
    void Update()
    {
        if(moveBack)
        {
            if (transform.position.y < originalPlatePosition.y)
            {
                transform.Translate(0, 0.01f, 0);
            }
            if (Door.transform.position.y > originalDoorPosition.y)
            {
                Door.transform.Translate(0, -0.01f, 0);
            }
            else
            {
                moveBack = false;
            }
        }
    }
}

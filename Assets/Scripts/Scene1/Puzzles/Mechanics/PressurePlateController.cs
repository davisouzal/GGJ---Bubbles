using UnityEngine;

public class PressurePlateController : MonoBehaviour
{
    public Vector3 originalPlatePosition;
    public Vector3 originalDoorPosition;
    public Vector2 maxPlateOffset;
    public Vector2 maxDoorOffset;

    public GameObject Door;
    bool inTriggerContact = false;

    // Start é chamado uma vez antes da primeira execução de Update após o MonoBehaviour ser criado
    void Start()
    {
        originalPlatePosition = transform.position;
        originalDoorPosition = Door.transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.gameObject.GetComponent<IActivatable>() != null)
        {
            collision.transform.parent = transform;
            GetComponent<SpriteRenderer>().color = Color.red;
            inTriggerContact = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.transform.name == "Player")
        {
            inTriggerContact = false;
            collision.transform.parent = null;
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    // Update é chamado uma vez por frame
    void Update()
    {
        if (inTriggerContact)
        {
            transform.Translate(0, -0.015f, 0);
            Door.transform.Translate(0, 0.01f, 0);
            if (transform.position.y < maxPlateOffset.y)
            {
                transform.position = new Vector2(transform.position.x, maxPlateOffset.y);
            }
            if (Door.transform.position.y > maxDoorOffset.y)
            {
                Door.transform.position = new Vector2(Door.transform.position.x, maxDoorOffset.y);
            }
        }
        else
        {
            if(transform.position != originalPlatePosition)
            {
                if (transform.position.y < originalPlatePosition.y)
                {
                    transform.Translate(0, 0.01f, 0);
                }
                if (Door.transform.position.y > originalDoorPosition.y)
                {
                    Door.transform.Translate(0, -0.01f, 0);
                }
            }
        }
    }
}

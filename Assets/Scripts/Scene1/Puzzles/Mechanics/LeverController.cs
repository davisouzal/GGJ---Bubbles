using UnityEngine;

public class LeverController : MonoBehaviour
{
    private enum DoorStatus
    {
        Closed,
        Closing,
        Opening,
        Opened
    }
    public KeyCode interactionKey = KeyCode.E;

    public float interactionDistance = 2f;
    private bool isPlayerNearby = false;

    public GameObject Door;
    public Vector3 originalDoorPosition;
    private DoorStatus doorStatus = DoorStatus.Closed;
    public Vector2 maxDoorOffset;

    private void Start()
    {
        originalDoorPosition = Door.transform.position;
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(interactionKey) &&
        (doorStatus == DoorStatus.Closed || doorStatus == DoorStatus.Opened))
        {
            switch (doorStatus)
            {
                case DoorStatus.Closed:
                    doorStatus = DoorStatus.Opening;
                    break;

                case DoorStatus.Opened:
                    doorStatus = DoorStatus.Closing;
                    break;
            }
        }

        if(doorStatus == DoorStatus.Opening)
        {
            Door.transform.Translate(0, 0.01f, 0);

            if (Door.transform.position.y > maxDoorOffset.y)
            {
                doorStatus = DoorStatus.Opened;
            }
        }

        if (doorStatus == DoorStatus.Closing)
        {
            Door.transform.Translate(0, -0.01f, 0);

            if (Door.transform.position.y < originalDoorPosition.y)
            {
                doorStatus = DoorStatus.Closed;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.gameObject.GetComponent<IPlayer>() != null)
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.gameObject.GetComponent<IPlayer>() != null)
        {
            isPlayerNearby = false;
        }
    }
}

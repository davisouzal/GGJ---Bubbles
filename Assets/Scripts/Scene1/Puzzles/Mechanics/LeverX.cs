using UnityEngine;

public class LeverX : MonoBehaviour
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
    public Vector3 originalDoorPosition; // A posi��o original da porta no eixo X
    private DoorStatus doorStatus = DoorStatus.Closed;
    public Vector2 maxDoorOffset; // O deslocamento m�ximo no eixo X.
    public Sprite leverActiveSprite; // Sprite ativado.
    public Sprite leverInactiveSprite;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalDoorPosition = Door.transform.position; // Posi��o inicial no eixo X
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(interactionKey) &&
        (doorStatus == DoorStatus.Closed || doorStatus == DoorStatus.Opened))
        {
            spriteRenderer.sprite = doorStatus == DoorStatus.Closed ? leverActiveSprite : leverInactiveSprite;

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

        if (doorStatus == DoorStatus.Opening)
        {
            // Movimenta��o da porta no eixo X
            Door.transform.Translate(0, -0.01f, 0);

            // Verificar se ultrapassou o limite de abertura no eixo X
            if (Door.transform.position.x > originalDoorPosition.x + maxDoorOffset.x)
            {
                doorStatus = DoorStatus.Opened;
            }
        }

        if (doorStatus == DoorStatus.Closing)
        {
            // Movimenta��o da porta no eixo X
            Door.transform.Translate(0, 0.01f, 0);

            // Verificar se a porta voltou � posi��o original no eixo X
            if (Door.transform.position.x < originalDoorPosition.x)
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
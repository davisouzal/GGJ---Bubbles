using UnityEngine;

public class PlayerController: Entity, IPlayer
{
    // Declara��es para o proj�til
    public GameObject projectilePrefab;
    private Vector3 projectileOriginalPosition;
    public float projectileSpeed = 10f;
    public float projChargeTime = 1f;
    public float projCooldownTime = 5f;
    private float projChargeTimer = 0f; // Temporizador para o carregamento
    private bool projIsCharging = false; // Verifica se o jogador est� carregando a habilidade
    private bool projIsOnCooldown = false; // Verifica se a habilidade est� em recarga

    // Declarações para a bolha
    public GameObject bubblePrefab; // Prefab da bolha
    public float bubbleCooldownTime = 5f; // Tempo de recarga da bolha
    private bool bubbleIsOnCooldown = false; // Verifica se a habilidade da bolha está em recarga

    // Update is called once per frame
    void Update()
    {
        playerMovement();
        castAbilityProjectile();
        castAbilityBubble();
    }

    void playerMovement()
    {
        GetComponent<Rigidbody2D>().linearVelocity = new Vector2(Input.GetAxis("Horizontal") * 5, GetComponent<Rigidbody2D>().linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 300f);
        }
    }

    void castAbilityProjectile()
    {
        if (Input.GetMouseButton(0) && !projIsOnCooldown)
        {
            projIsCharging = true;
            projChargeTimer += Time.deltaTime;

            if (projChargeTimer >= projChargeTime)
            {
                LaunchProjectile();
                projChargeTimer = 0f;
                projIsCharging = false;
                projIsOnCooldown = true;
                Invoke(nameof(ResetProjectileCooldown), projCooldownTime); // Inicia o cooldown
            }
        }

        if (Input.GetMouseButtonUp(0) && projIsCharging)
        {
            projIsCharging = false;
            projChargeTimer = 0f;
        }
    }

    private void LaunchProjectile()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Posição do mouse no mundo
        mousePosition.z = 0;
        projectileOriginalPosition = transform.position;
        projectileOriginalPosition.y += 0.2f;
        projectileOriginalPosition.x = (mousePosition.x < transform.position.x) ? projectileOriginalPosition.x - 1 : projectileOriginalPosition.x + 1;
        GameObject projectileObject = Instantiate(projectilePrefab, projectileOriginalPosition, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();

        if (projectile != null)
        {
            projectile.Owner = gameObject;
            Vector2 direction = (mousePosition.x < transform.position.x) ? Vector2.left : Vector2.right;
            projectile.setProjectile(direction, 10f, gameObject);
        }
    }

    private void ResetProjectileCooldown()
    {
        projIsOnCooldown = false;
    }

    void castAbilityBubble()
    {
        if (Input.GetMouseButtonDown(1) && !bubbleIsOnCooldown) // Botão direito do mouse
        {
            ActivateBubble();
            bubbleIsOnCooldown = true;
            Invoke(nameof(ResetBubbleCooldown), bubbleCooldownTime); // Inicia o cooldown da bolha
        }
    }

    private void ActivateBubble()
    {
        GameObject bubble = Instantiate(bubblePrefab, transform.position, Quaternion.identity);
        bubble.transform.parent = transform;
    }
    private void ResetBubbleCooldown()
    {
        bubbleIsOnCooldown = false;
    }
}

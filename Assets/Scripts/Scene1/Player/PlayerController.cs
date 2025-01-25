using UnityEngine;

public class PlayerController : MonoBehaviour, IPlayer
{
    // Declara��es para o proj�til
    public GameObject projectilePrefab;
    private Vector3 projectileOriginalPosition;
    public float projectileSpeed = 10f;
    public float chargeTime = 1f;
    public float cooldownTime = 5f;

    // Declara��es para os timers
    private float chargeTimer = 0f; // Temporizador para o carregamento
    private bool isCharging = false; // Verifica se o jogador est� carregando a habilidade
    private bool isOnCooldown = false; // Verifica se a habilidade est� em recarga

    // Update is called once per frame
    void Update()
    {
        playerMovement();
        castAbilityProjectile();
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
        if (Input.GetMouseButton(0) && !isOnCooldown)
        {
            isCharging = true;
            chargeTimer += Time.deltaTime;

            if (chargeTimer >= chargeTime)
            {
                LaunchProjectile();
                chargeTimer = 0f;
                isCharging = false;
                isOnCooldown = true;
                Invoke(nameof(ResetCooldown), cooldownTime); // Inicia o cooldown
            }
        }

        if (Input.GetMouseButtonUp(0) && isCharging)
        {
            isCharging = false;
            chargeTimer = 0f;
        }
    }

    private void LaunchProjectile()
    {
        projectileOriginalPosition = transform.position;
        projectileOriginalPosition.x += 1;
        projectileOriginalPosition.y += 0.2f;
        GameObject projectile = Instantiate(projectilePrefab, projectileOriginalPosition, Quaternion.identity);

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.right * projectileSpeed; // Altere a dire��o conforme necess�rio
        }
    }

    private void ResetCooldown()
    {
        isOnCooldown = false;
    }

}

using UnityEngine;
using UnityEngine.SceneManagement;

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
    private Animator anim;
    private Rigidbody2D rb;
    public bool inGround = true;
    
    public int playerHealth = 2;

    void Start()
    {
        // Obter referências ao Rigidbody2D e ao Animator
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        playerMovement();
        castAbilityProjectile();
        castAbilityBubble();    
        Jump();
        if(playerHealth <= 0)
        {
            Die();
        }
    }
     
    void playerMovement()
    {
        float move = Input.GetAxis("Horizontal");

        // Atualizar a velocidade no Rigidbody2D
        rb.linearVelocity = new Vector2(move * 5f, rb.linearVelocity.y);
        if (Mathf.Abs(move) > 0.01f)
        {
            anim.SetInteger("transition", 1); // Andando
        }
        else
        {
            anim.SetInteger("transition", 0); // Parado
        }
        if(move > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }if(move < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
    }



    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.linearVelocity.y) < 0.01f && inGround == true)
        {
            rb.AddForce(Vector2.up * 350f);
            inGround = false;
            // Atualizar a animação de pulo, se necessário
            anim.SetTrigger("Jump");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Ground")
        {
            inGround = true;
        }
        if (collision.gameObject.TryGetComponent(out IProjectile projectile))
        {
            TakeDamage(1);
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

            Vector2 direction = (mousePosition.x < transform.position.x) ? Vector2.right : Vector2.left;
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

    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
        if (playerHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
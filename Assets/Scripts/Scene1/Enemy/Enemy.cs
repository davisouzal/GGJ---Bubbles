using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy
{
    public GameObject projectilePrefab; // Prefab do projétil
    public float projSpeed = 5f;        // Velocidade do projétil
    public bool playerInSight = false;
    
    public float shootCooldown = 5f;
    private float shootChargeTimer = 0f;

    public void Update()
    {
        if (playerInSight)
        {
            ShootProjectile(transform.position);
        }
    }

    private void ShootProjectile(Vector2 direction)
    {
        shootChargeTimer += Time.deltaTime;
        if (shootCooldown < shootChargeTimer)
        {
            Vector3 projStartPosition = transform.position;
            projStartPosition.x += 2;
            GameObject projectileObject = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Projectile projectile = projectileObject.GetComponent<Projectile>();

            if (projectile != null)
            {
                projectile.setProjectile(direction, projSpeed, gameObject);
            }

            shootChargeTimer = 0f;
        }
    }

    public void setPlayerSight(bool inSight)
    {
        playerInSight = inSight;
    }
}

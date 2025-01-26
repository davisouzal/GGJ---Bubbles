using UnityEngine;

public class Projectile : MonoBehaviour, IProjectile
{
    public Vector2 startDirection;
    public float projSpeed = 1f;
    public GameObject Owner;

    void Start()
    {
        Destroy(gameObject, 5f);
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = -startDirection * projSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Owner.name != collision.gameObject.name)
        {
            if (collision.transform.gameObject.TryGetComponent(out IEnemy enemy))
            {
                    Destroy(collision.gameObject);

            }
            if (collision.transform.gameObject.TryGetComponent(out IBubble bubble))
            {
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.linearVelocity = -startDirection * projSpeed;
                }
            }
            Debug.Log("Collision with " + collision.gameObject.name);
            Destroy(gameObject);
        }
    }

    public void setProjectile(Vector2 direction, float speed, GameObject owner )
    {
        Owner = owner;
        startDirection = direction;
        projSpeed = speed;
    }
}

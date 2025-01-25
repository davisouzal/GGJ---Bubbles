using UnityEngine;

public class Projectile : MonoBehaviour, IProjectile
{
    //Start is called once before the first execution of Update after the MonoBehaviour is created
    public Vector2 startDirection;
    public float projSpeed = 1f;

    public GameObject Owner { get; set; }
    void Start()
    {
        Destroy(gameObject, 5f);
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = - startDirection * projSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Owner != null)
        {
            return;
        }
        if (Owner != collision.gameObject &&(collision.transform.gameObject.TryGetComponent(out IEnemy enemy)))
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
        } else
        {
            Destroy(gameObject);
        }
    }

    public void setProjectile(Vector2 direction, float speed, GameObject owner)
    {
        Owner = owner;
        startDirection = direction;
        projSpeed = speed;
    }
}

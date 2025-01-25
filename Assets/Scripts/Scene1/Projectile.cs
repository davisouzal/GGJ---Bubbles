using UnityEngine;

public class Projectile : MonoBehaviour, IProjectile
{
    //Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Pegou no caba");
        if (collision.transform.gameObject.TryGetComponent(out IEnemy enemy))
        {
            Destroy(collision.gameObject);
        }
        Destroy(gameObject);
    }
}

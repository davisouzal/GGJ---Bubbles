using UnityEngine;

public class EnemySensor : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemyParent = GetComponentInParent<Enemy>();
        if (collision.gameObject.TryGetComponent(out IPlayer player))
        {
            enemyParent.setPlayerSight(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Enemy enemyParent = GetComponentInParent<Enemy>();
        if (collision.gameObject.TryGetComponent(out IPlayer player))
        {
            enemyParent.setPlayerSight(false);
        }
    }
}

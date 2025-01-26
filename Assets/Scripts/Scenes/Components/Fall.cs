using UnityEngine;

public class Fall : MonoBehaviour
{
    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.gameObject.TryGetComponent(out IPlayer player))
        {
            player.Die();
        }
    }
}

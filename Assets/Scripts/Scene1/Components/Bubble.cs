using UnityEngine;

public class Bubble : MonoBehaviour, IBubble
{
    public GameObject projectilePrefab;
    void Start()
    {
        Destroy(gameObject, 2f);
    }
}

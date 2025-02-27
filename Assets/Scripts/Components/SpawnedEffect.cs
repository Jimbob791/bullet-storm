using UnityEngine;

public class SpawnedEffect : MonoBehaviour
{
    public float lifetime;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }
}

using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    public PlayerStats stats;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        stats = GetComponent<PlayerStats>();
    }
}

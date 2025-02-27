using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "PlayerHurtbox")
        {
            Player.instance.GetComponent<PlayerHealth>().Damage(1);
        }
    }
}

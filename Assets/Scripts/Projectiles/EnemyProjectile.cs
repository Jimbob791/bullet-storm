using UnityEngine;

public class EnemyProjectile : Projectile
{
    bool ownedByPlayer = false;

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Parrybox")
        {
            if (!canBeParried || timeSinceParried < 0.2f) { return; }
            if (Player.instance.GetComponent<PlayerBlocking>().AttemptParry())
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                direction = mousePos - (Vector2)transform.position;
                speed *= Player.instance.GetComponent<PlayerStats>().parryCoeff;
                rb.linearVelocity = direction.normalized * speed;
                RotateSprite(direction);

                timeSinceParried = 0;
                ownedByPlayer = true;
            }
        }

        if (col.gameObject.name == "HurtBox" && ownedByPlayer)
        {
            col.transform.parent.GetComponent<Health>().Damage(speed / 10f);

            CheckDestroy();
        }

        if (col.gameObject.tag == "GiftCharm" && ownedByPlayer)
        {
            col.GetComponent<GiftCharm>().Open();

            CheckDestroy();
        }
    }
}

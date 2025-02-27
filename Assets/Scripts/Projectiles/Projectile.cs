using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Projectile : MonoBehaviour
{
    public float speed;
    public int pierce = 1;
    public Vector2 direction;
    public bool canBeParried = false;
    public GameObject hitEffect;
    public GameObject destroyEffect;
    public int bounces = 200;
    public GameObject bounceSFX;

    protected float timeSinceParried = 0;

    protected Rigidbody2D rb; 

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction.normalized * speed;
        RotateSprite(direction);
    }

    private void Update()
    {
        timeSinceParried += Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<Bounceable>() != null)
        {
            ContactPoint2D firstContact = col.contacts[0];
            direction = Vector2.Reflect(direction.normalized, firstContact.normal);

            speed *= col.gameObject.GetComponent<Bounceable>().bounceCoeff;
            rb.linearVelocity = direction.normalized * speed;
            RotateSprite(direction);

            bounces -= 1;
            Instantiate(bounceSFX);
            if (bounces < 0) { pierce = -1; CheckDestroy(); }
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
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
            }
        }

        if (col.gameObject.name == "HurtBox")
        {
            col.transform.parent.GetComponent<Health>().Damage(speed / 10f);

            CheckDestroy();
        }

        if (col.gameObject.tag == "GiftCharm")
        {
            col.GetComponent<GiftCharm>().Open();

            CheckDestroy();
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "PlayerHurtbox" && timeSinceParried >= 0.2f)
        {
            Player.instance.GetComponent<Health>().Damage(1);

            CheckDestroy();
        }
    }

    public virtual void CheckDestroy()
    {
        pierce -= 1;
        if (pierce < 0) 
        { 
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
            Destroy(gameObject); 
        }
        else
        {
            Instantiate(hitEffect, transform.position, Quaternion.identity);
        }
    }

    protected void RotateSprite(Vector2 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.GetChild(0).rotation = Quaternion.Euler(0, 0, angle);
    } 
}

using UnityEngine;

public class AirShootProjectile : State
{
    public GameObject projectilePrefab;
    public float projectileSpeed;
    bool shot = false;
    public Vector2 setDirection;
    public bool useSetDirection = false;

    public override void OnEnter(Controller controller)
    {
        base.OnEnter(controller);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, Vector3.zero, Time.deltaTime);

        if (scaledTime > 3f/12f && !shot)
        {
            Vector2 shootDir = useSetDirection ? setDirection : Player.instance.transform.position - transform.position;
            shootDir.Normalize();
            GameObject newProjectile = GameObject.Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            newProjectile.GetComponent<Projectile>().direction = shootDir;
            newProjectile.GetComponent<Projectile>().speed = projectileSpeed;
            
            shot = true;
        }
    }
}

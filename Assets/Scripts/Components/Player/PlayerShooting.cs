using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerShooting : MonoBehaviour
{
    [Header("Ammo")]
    public int ammoMax = 1;
    public int ammo;

    [Header("GunStats")]
    public float fireSpeedCoeff = 1;
    public int burstSize = 1;

    private InputAction shootAction;
    private Vector2 shootDirection;

    bool canShoot = true;
    public bool stopInput = false;

    [SerializeField] GameObject pulloutSFX;
    [SerializeField] GameObject shootSFX;
    [SerializeField] GameObject gunArm;
    [SerializeField] SpriteRenderer gunArmSR;
    [SerializeField] Animator anim;
    [SerializeField] IconBar ammoBar;

    private void Start()
    {
        ammo = ammoMax;
        shootAction = InputSystem.actions.FindAction("Shoot");
        UpdateAmmoBar();
    }

    private void Update()
    {
        if (HelperFunctions.tutorial) { return; }

        // Rotate arm
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        shootDirection = mousePos - (Vector2)transform.position;
        float angle = (Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg);
        gunArm.transform.eulerAngles = new Vector3(0, 0, angle);

        gunArm.transform.localScale = new Vector3(shootDirection.x < 0 ? -1 : 1, 1, 1);
        gunArmSR.flipY = shootDirection.x < 0;

        if (shootAction.WasPressedThisFrame() && canShoot && ammo > 0 && !stopInput)
        {
            canShoot = false;
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        gunArmSR.enabled = true;
        anim.SetTrigger("shoot");
        Instantiate(pulloutSFX);
        yield return new WaitForSeconds(1f/12f);

        // Shoot bullet(s)
        for (int i = 0; i < burstSize; i++)
        {
            yield return new WaitForSeconds(4f / 12f / burstSize);
            GameObject newProjectile = GameObject.Instantiate(Player.instance.stats.bullet, Player.instance.stats.barrelPos.position, Quaternion.identity);
            newProjectile.GetComponent<Projectile>().direction = shootDirection;
            Instantiate(shootSFX);
            ammo -= 1;
            UpdateAmmoBar();
            if (ammo <= 0) 
            {
                break;
            }
        }

        canShoot = true;
    }

    public void UpdateAmmoBar()
    {
        ammoBar.UpdateBar(ammoMax, ammo);
    }
}

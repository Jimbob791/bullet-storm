using System.Threading;
using UnityEngine;

public class BasicBullet : Projectile
{
    public override void CheckDestroy()
    {
        if (pierce - 1 < 0) { 
            Player.instance.GetComponent<PlayerShooting>().ammo += 1;
            Player.instance.GetComponent<PlayerShooting>().UpdateAmmoBar();
        }
        
        base.CheckDestroy();
    }
}

using UnityEngine;

[System.Serializable]
public abstract class Item
{
    public abstract string GetName();

    public virtual void Update(GameObject player, int stacks)
    {

    }

    public virtual void OnShoot(int stacks)
    {

    }

    public virtual void OnPickup(int stacks)
    {

    }

    public virtual void OnParry(int stacks)
    {

    }

    public virtual void OnReload(int stacks)
    {

    }

    public virtual void OnHit(GameObject enemy, int stacks)
    {

    }

    public virtual void OnKill(GameObject enemy, int stacks)
    {

    }
}

// ----------------------------------------------------- ITEMS ----------------------------------------------------- //
public class RegenerativeCore : Item
{
    public override string GetName()
    {
        return "RegenerativeCore";
    }

    public override void OnPickup(int stacks)
    {
        Player.instance.GetComponent<PlayerHealth>().regenDelay = 60/stacks;
    }
}

public class ReinforcedArmor : Item
{
    public override string GetName()
    {
        return "ReinforcedArmor";
    }

    public override void OnPickup(int stacks)
    {
        Player.instance.GetComponent<PlayerHealth>().maxHealth += 1;
        Player.instance.GetComponent<PlayerHealth>().Heal(1);
    }
}

public class AmmoPack : Item
{
    public override string GetName()
    {
        return "AmmoPack";
    }

    public override void OnPickup(int stacks)
    {
        Player.instance.GetComponent<PlayerShooting>().ammoMax += 2;
        Player.instance.GetComponent<PlayerShooting>().ammo += 2;
        Player.instance.GetComponent<PlayerShooting>().UpdateAmmoBar();
    }
}

public class BurstCore : Item
{
    public override string GetName()
    {
        return "BurstCore";
    }

    public override void OnPickup(int stacks)
    {
        Player.instance.GetComponent<PlayerShooting>().burstSize += 1;
        Player.instance.GetComponent<PlayerShooting>().ammoMax += 1;
        Player.instance.GetComponent<PlayerShooting>().ammo += 1;
    }
}

public class TimeStorage : Item
{
    public override string GetName()
    {
        return "TimeStorage";
    }

    public override void OnPickup(int stacks)
    {
        Player.instance.GetComponent<PlayerBulletTime>().maxSlowSeconds += 0.25f;
    }
}

public class RapidRecharge : Item
{
    public override string GetName()
    {
        return "RapidRecharge";
    }

    public override void OnPickup(int stacks)
    {
        Player.instance.GetComponent<PlayerBulletTime>().pointRecoveryRate += 0.1f;
    }
}

public class CyberLegs : Item
{
    public override string GetName()
    {
        return "CyberLegs";
    }

    public override void OnPickup(int stacks)
    {
        Player.instance.GetComponent<PlayerStats>().jumpStrength += 3f;
    }
}

public class TurboBoots : Item
{
    public override string GetName()
    {
        return "TurboBoots";
    }

    public override void OnPickup(int stacks)
    {
        Player.instance.GetComponent<PlayerStats>().runSpeed += 2f;
    }
}

public class HonedBlade : Item
{
    public override string GetName()
    {
        return "HonedBlade";
    }

    public override void OnPickup(int stacks)
    {
        Player.instance.GetComponent<PlayerStats>().parryCoeff *= 1.2f;
    }
}

public class BallisticRounds : Item
{
    public override string GetName()
    {
        return "BallisticRounds";
    }

    public override void OnPickup(int stacks)
    {
        Player.instance.GetComponent<PlayerShooting>().fireSpeedCoeff *= 1.1f;
    }
}
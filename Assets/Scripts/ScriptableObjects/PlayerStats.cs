using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Movement")]
    public float runSpeed = 5; // Maximum running speed in units/second
    public float runAccel = 20; // Accel to reach maximum running speed
    public float jumpStrength = 1;

    [Header("Gun")]
    public GameObject bullet;
    public Transform barrelPos;

    [Header("Sword")]
    public float parryWindow = 1f;
    public float parryCoeff = 1.5f;

    //[Header("Effects")]
}
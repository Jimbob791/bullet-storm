using UnityEngine;

public class GroundedCheck : MonoBehaviour
{
    public bool grounded = false;
    [SerializeField] float checkRadius;
    [SerializeField] LayerMask platformLayer;
    [SerializeField] Transform checkPos;

    private void Update()
    {
        grounded = Physics2D.CircleCast(checkPos.position, checkRadius, Vector2.zero, 0, platformLayer);
    }
}

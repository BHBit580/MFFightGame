using UnityEngine;

public class Feet : MonoBehaviour
{
    [SerializeField] private float distance = 0.5f;
    public bool onGround;
    private void Update()
    {
        onGround = IsGrounded();
    }
    
    public bool IsGrounded()
    {
        Ray ray = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, distance))
        {
            return true;
        }
        
        Debug.DrawRay(transform.position, Vector3.down * distance, Color.red);
        
        return false;
    }
}

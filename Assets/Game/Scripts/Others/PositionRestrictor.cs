using UnityEngine;

public class PositionRestrictor : MonoBehaviour
{
    [SerializeField] private float minX;
    [SerializeField] private float maxX;

    void Update()
    { 
        Vector3 position = transform.position;
        
        if (minX > maxX)
        {
            (minX, maxX) = (maxX, minX);
        }
        
        position.x = Mathf.Clamp(position.x, minX, maxX);
        
        transform.position = position;
    }
}
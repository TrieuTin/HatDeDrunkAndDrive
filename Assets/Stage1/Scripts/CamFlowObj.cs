using UnityEngine;

public class CamFlowObj : MonoBehaviour
{
    public Transform target;   
    
    public float smoothSpeed = 5f;
    void LateUpdate()
    {
        if (target == null) return;

        Vector3 currentPos = transform.position;
        
        Vector3 targetPos = new Vector3(currentPos.x, currentPos.y, target.position.z +3);
        
        transform.position = Vector3.Lerp(currentPos, targetPos, smoothSpeed * Time.deltaTime);
    }
}

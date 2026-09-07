using UnityEngine;
using UnityEngine.InputSystem;


public class CharacterLock : MonoBehaviour, ICharacterComponent
{
    [SerializeField] private Camera camera;
    [SerializeField] private float detectedRadius;
    [SerializeField] private LayerMask detectionMask;
    [SerializeField] private float detectionAngle;
    private float nearestAngle;
    private float nearestDistance;
    public Character ParentCharacter { get; set; }

    public void OnCharacterLock(InputAction.CallbackContext ctx)
    {
        if(!ctx.started)return;
        if(ParentCharacter.LockTarget != null) 
        {
            ParentCharacter.LockTarget=null;
            return;
        }
    
        Collider[] detectedObjects = Physics.OverlapSphere(transform.position, detectedRadius, detectionMask);
        if(detectedObjects.Length==0) return;
        

        int closeObjects = 0;

        Vector3 cameraForward = camera.transform.forward;

        for(int i=0; i<detectedObjects.Length; i++)
        {
           Collider obj = detectedObjects[i];
           Vector3 objectviewDirection = obj.transform.position - camera.transform.position;
           float dot = Vector3.Dot(cameraForward.normalized, objectviewDirection.normalized); 

           float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
           
           if(angle > detectionAngle) continue;

           float distance = Vector3.Distance(obj.transform.position, transform.position);
           
           if(distance < nearestDistance && angle < nearestAngle)
           {
                closeObjects=i;
           }

           nearestAngle = Mathf.Min(angle, nearestAngle);
           nearestDistance = Mathf.Min(distance, nearestDistance);

        }
        ParentCharacter.LockTarget = detectedObjects[closeObjects].transform;
    }
    #if UNITY_EDITOR
        private void OnDrawGizmos(){
            Gizmos.DrawWireSphere(transform.position, detectedRadius);
        }
    #endif
}

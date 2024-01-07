using UnityEngine;

public class FollowVRCamera : MonoBehaviour
{
    private Transform originalParent;
    public Transform vrCamera;

    void Start()
    {
       
        originalParent = transform.parent;
    }

    void LateUpdate()
    {
       
        if (vrCamera != null)
        {
           
            transform.position = vrCamera.position + vrCamera.forward * 1.0f + vrCamera.up * 0.5f + vrCamera.right * 0.5f;

          
            transform.rotation = vrCamera.rotation;
        }
    }

    
    public void ResetParent()
    {
        transform.parent = originalParent;
    }
}

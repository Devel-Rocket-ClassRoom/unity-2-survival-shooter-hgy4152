using UnityEngine;


public class TargetingCamera : MonoBehaviour
{
    public GameObject target; 
    public Vector3 offset = new Vector3(0, 1, -10); 

    private void FixedUpdate()
    {
        if (target != null)
        {

            Vector3 pos = target.transform.position + offset;
            transform.position = pos;
        }
    }
}
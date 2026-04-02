using UnityEngine;

public class AttackArea : MonoBehaviour
{
    public bool isAttack;
    public GameObject target;
    public void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isAttack = true;
            target = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isAttack = false;
        }
    }
}

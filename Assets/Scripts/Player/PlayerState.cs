using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public int Health = 100;

    public bool isDead;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnDamage(int damage)
    {
        Health -= damage;

        if(Health <= 0 && !isDead)
        {
            Health = 0;
            isDead = true;
            animator.SetTrigger("Dead");

        }
    }
}

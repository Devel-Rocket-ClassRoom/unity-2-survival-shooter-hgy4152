using System.Collections;
using UnityEngine;

public abstract class AtkPattern : MonoBehaviour
{
    public abstract bool coAtk { get; set; }
    private Enemy Zombie;
    protected PlayerState playerState;

    public void Attack(GameObject player, GameObject zombie)
    {
        if(!coAtk)
        {
            Zombie = zombie.GetComponent<Enemy>();
            playerState = player.GetComponent<PlayerState>();
            StartCoroutine(CoAttack(player, Zombie));
        }
    }
    public abstract IEnumerator CoAttack(GameObject player, Enemy zombie);
    
}

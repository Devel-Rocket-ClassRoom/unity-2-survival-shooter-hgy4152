using System.Collections;
using UnityEngine;

public class Zombunny : AtkPattern
{
    public override bool coAtk { get; set; }

    public override IEnumerator CoAttack(GameObject player, Enemy zombie)
    {

        coAtk = true;
        zombie.animator.speed = 3;
        
        // 체력 감소
        playerState.Health -= zombie.damage;

        yield return new WaitForSeconds(zombie.atkInterval);

        zombie.animator.speed = 1;
        coAtk = false;
    }

}

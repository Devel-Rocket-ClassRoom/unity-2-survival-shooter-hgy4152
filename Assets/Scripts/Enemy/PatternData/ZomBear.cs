using System.Collections;
using UnityEngine;

public class ZomBear : AtkPattern
{
    public override bool coAtk { get; set; }

    public override IEnumerator CoAttack(GameObject player, Enemy zombie)
    {

        coAtk = true;
        zombie.animator.speed = 3;
        playerState.OnDamage(zombie.damage);



        yield return new WaitForSeconds(zombie.atkInterval);

        zombie.animator.speed = 1;
        coAtk = false;
    }

}

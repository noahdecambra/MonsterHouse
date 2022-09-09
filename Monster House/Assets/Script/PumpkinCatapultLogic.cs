using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinCatapultLogic : BulletController
{
    private int _damage = 5;
    private float _aoeRange = 5;
    public LayerMask enemies;

    public override void HitEffect()
    {
        Collider[] enemiesInAOE = Physics.OverlapSphere(transform.position, _aoeRange, enemies);
        foreach (var enemy in enemiesInAOE)
        {
            _enemyScript = enemy.GetComponent<EnemyBase>();
            if (_enemyScript == null)
            {
                return;
            }
            _enemyScript.DamageMonitor(_damage);
        }
        base.HitEffect();
    }



    
}

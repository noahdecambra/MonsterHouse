using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinCatapultLogic : BulletController
{
    private int _damage = 5;
    private float _aoeRange = 5;
    public LayerMask enemies;
    private int _superEffectiveMultiplier = 2;
    private float _resistedMultiplier = .5f;
    private string _superEffectiveType = "Beast";
    private string _resistedType = "Unholy";

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
            if (_target.tag == _superEffectiveType)
            {
                _damage *= _superEffectiveMultiplier;
            }

            if (_target.tag == _resistedType)
            {
                _damage *= (int)_resistedMultiplier;
            }
            _enemyScript.DamageMonitor(_damage);
        }
        base.HitEffect();
    }



    
}

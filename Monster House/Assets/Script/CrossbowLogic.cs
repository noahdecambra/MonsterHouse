using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CrossbowLogic : BulletController
{
    private int _damage = 10;
    private int _superEffectiveMultiplier = 2;
    public override void HitEffect()
    {
        _enemyScript = _target.GetComponent<EnemyBase>();
        if (_enemyScript == null)
        {
            return;
        }
        
        if (_target.tag == "Unholy")
        {
            _damage *= _superEffectiveMultiplier;
        }
        _enemyScript.DamageMonitor(_damage);
        base.HitEffect();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyBlasterLogic : BulletController
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

        if (_target.tag == "Magical")
        {
            _damage *= _superEffectiveMultiplier;
        }
        _enemyScript.DamageMonitor(_damage);
        base.HitEffect();
    }
}

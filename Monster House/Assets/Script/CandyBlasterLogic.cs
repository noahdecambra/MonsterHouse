using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyBlasterLogic : BulletController
{
    private int _damage = 10;
    private int _superEffectiveMultiplier = 2;
    private float _resistedMultiplier = .5f;
    private string _superEffectiveType = "Magical";
    private string _resistedType = "Beast";
    public override void HitEffect()
    {
        _enemyScript = _target.GetComponent<EnemyBase>();
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
        base.HitEffect();
    }
}

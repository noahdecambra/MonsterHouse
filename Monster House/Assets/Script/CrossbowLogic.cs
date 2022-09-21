using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CrossbowLogic : BulletController
{
    private int _damage = 10;
    private int _superEffectiveMultiplier = 2;
    private float _resistedMultiplier = .5f;
    private string _superEffectiveType = "Unholy" ;
    private string _resistedType = "Magical";
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
            _damage *=(int)_resistedMultiplier;
        }
        _enemyScript.DamageMonitor(_damage);
        base.HitEffect();
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class CrossbowLogic : BulletController
{
    public int _damage = 5;
    private int _superEffectiveMultiplier = 2;
    private float _resistedMultiplier = .5f;
    private string _superEffectiveType = "Unholy" ;
    private string _resistedType = "Magical";
    private bool _pierce = false;

    void Start()
    {
       TurretLevel parenTurretLevel = gameObject.transform.parent.GetComponent<TurretController>().currentTurretLevel;
       if (parenTurretLevel == TurretLevel.Level3|| parenTurretLevel== TurretLevel.Level4)
       {
           _damage += 3;
       }
       if (parenTurretLevel== TurretLevel.Level4)
       {
           
            _pierce = true;
       }
    }

    public override void TargetHit()
    {
        var pierceCount = 0;
        var pierceMax = 2;
        if (_pierce)
        {
            pierceCount++;
            if (pierceCount == pierceMax)
            {
                base.TargetHit();
            }
        }
    }

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

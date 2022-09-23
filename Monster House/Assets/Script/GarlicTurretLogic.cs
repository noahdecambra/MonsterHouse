using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicTurretLogic : BulletController
{
    private int _damage = 3;
    private int _duration = 10;
    private int _superEffectiveMultiplier = 2;
    private float _resistedMultiplier = .5f;
    private string _superEffectiveType = "Beast";
    private string _resistedType = "Unholy";

     void Start()
    {
        TurretLevel parenTurretLevel = gameObject.transform.parent.GetComponent<TurretController>().currentTurretLevel;

        if(parenTurretLevel== TurretLevel.Level2|| parenTurretLevel == TurretLevel.Level3 || parenTurretLevel == TurretLevel.Level4)
        {
            _duration += 3;
        }
        if (parenTurretLevel == TurretLevel.Level3 || parenTurretLevel == TurretLevel.Level4)
        {
            _damage += 2;
        }
        if (parenTurretLevel == TurretLevel.Level4)
        {
            _damage += 2;
            //should be cloud 
            
        }
    }
    public override void HitEffect()
    {
        _enemyScript = _target.GetComponent<EnemyBase>();
       // StartCoroutine(_enemyScript.takeTickDamage(_damage,_duration));
       _enemyScript.damageOverTime = true;
       if (_target.tag == _superEffectiveType)
       {
           _damage *= _superEffectiveMultiplier;
       }

       if (_target.tag == _resistedType)
       {
           _damage *= (int)_resistedMultiplier;
       }
        _enemyScript.DamageMonitor(_damage,_duration);
        base.HitEffect();
    }

  
}

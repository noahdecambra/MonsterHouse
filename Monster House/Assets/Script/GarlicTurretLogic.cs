using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicTurretLogic : BulletController
{
    private int _damage = 1;
    private int _duration = 10;
    public override void HitEffect()
    {
        _enemyScript = _target.GetComponent<EnemyBase>();
       // StartCoroutine(_enemyScript.takeTickDamage(_damage,_duration));
       _enemyScript.damageOverTime = true;
       _enemyScript.DamageMonitor(_damage,_duration);
        base.HitEffect();
    }

  
}

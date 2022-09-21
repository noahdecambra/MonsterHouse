using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebLauncherLogic : BulletController
{
    [SerializeField] private float _duration, _amount;

    public override void HitEffect()
    {
        _enemyScript = _target.GetComponent<EnemyBase>();
        _enemyScript.SlowEffect(_amount,_duration);
        base.HitEffect();
    }
}

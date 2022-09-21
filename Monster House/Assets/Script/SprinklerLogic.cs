using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SprinklerLogic : BuildableBase
{
    private int _damage= 3;
    private EnemyBase currentEnemyHealthManager;
    private int _superEffectiveMultiplier = 2;
    private float _resistedMultiplier = .5f;
    private string _superEffectiveType = "Unholy";
    private string _resistedType = "Magical";
    // Start is called before the first frame update
    public override void Effect()
    {
        currentEnemyHealthManager = enemyHealthManager;
        currentEnemyHealthManager.damageOverTime = true;
        currentEnemyHealthManager.giveTickDamage = true;
        if (currentEnemyHealthManager.tag == _superEffectiveType)
        {
            _damage *= _superEffectiveMultiplier;
        }

        if (currentEnemyHealthManager.tag == _resistedType)
        {
            _damage *= (int)_resistedMultiplier;
        }
        currentEnemyHealthManager.DamageMonitor(_damage, 0, currentEnemyHealthManager.giveTickDamage);
    }

    void OnTriggerStay()
    {
        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<EnemyBase>())
        {
            other.GetComponent<EnemyBase>().giveTickDamage = false;
        }
    }

 


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprinklerLogic : BuildableBase
{
    
    private int _originalDamage = 3;
    private EnemyBase currentEnemyHealthManager;
    private int _superEffectiveMultiplier = 2;
    private float _resistedMultiplier = .5f;
    private string _superEffectiveType = "Unholy";
    private string _resistedType = "Magical";
    // Start is called before the first frame update
    
    public override void Effect()
    {
        int _damage=_originalDamage;
        currentEnemyHealthManager = enemyHealthManager;
        currentEnemyHealthManager.damageOverTime = true;
        currentEnemyHealthManager.giveTickDamage = true;
        if (currentEnemyHealthManager.gameObject.tag == _superEffectiveType)
        {
            _damage = _originalDamage * _superEffectiveMultiplier;
        }

        else if (currentEnemyHealthManager.gameObject.tag == _resistedType)
        {
            _damage = (int)(_originalDamage * _resistedMultiplier);
        }

        currentEnemyHealthManager.DamageMonitor(_damage, 0, currentEnemyHealthManager.giveTickDamage);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<EnemyBase>())
        {
            other.GetComponent<EnemyBase>().giveTickDamage = false;
        }
    }

 


}

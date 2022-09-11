using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class BuildableBase : MonoBehaviour
{
    public float health;
    public LayerMask enemies;
    protected enemymov enemyMovement;
    protected EnemyBase enemyHealthManager;
    protected List<enemymov> currentEnemyMoves;
    protected List<EnemyBase> CurrentEnemyBases;
   
    public virtual void Effect()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (Physics.CheckSphere(gameObject.transform.position, 2, enemies))
        {
             enemyMovement = other.GetComponent<enemymov>();
             currentEnemyMoves.Add(enemyMovement);
             enemyHealthManager = other.GetComponent<EnemyBase>();
             CurrentEnemyBases.Add(enemyHealthManager);
              
             Effect();
        }
    }

    public IEnumerator TakeDamage()
    {
        var damage = enemyHealthManager.damage;
        var attackRate = enemyHealthManager.attackRate;
        while(health>0)
        {
            health -= damage;
            
            if (health<=0)
        {
            Destroy(gameObject);
        }
            yield return new WaitForSeconds(attackRate);
        }
        
        
    }

}

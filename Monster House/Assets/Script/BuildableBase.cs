using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class BuildableBase : MonoBehaviour
{
    public float health;
    public LayerMask enemies;
    public LayerMask buildable;
    protected enemymov enemyMovement;
    protected EnemyBase enemyHealthManager;
    protected List<enemymov> currentEnemyMoves = new List<enemymov>();
    protected List<EnemyBase> CurrentEnemyBases = new List<EnemyBase>();
    
    public virtual void Effect()
    {
        
    }


   
    void OnTriggerEnter(Collider other)
    {
        
       // if (Physics.CheckSphere(gameObject.transform.position, 2, enemies))
        //{
             Debug.Log("Enemy In Trigger");
             enemyMovement = other.gameObject.GetComponent<enemymov>();
            
                currentEnemyMoves.Add(enemyMovement);
             
             enemyHealthManager = other.gameObject.GetComponent<EnemyBase>();
            
             
                CurrentEnemyBases.Add(enemyHealthManager);
             

            Effect();
       // }
    }

  /*   void OnTriggerExit(Collider other)
    {
        enemyHealthManager = other.gameObject.GetComponent<EnemyBase>();
        enemyMovement = other.gameObject.GetComponent<enemymov>();
        if (currentEnemyMoves.Contains(enemyMovement))
        {
            currentEnemyMoves.Remove(enemyMovement);
        }
        if (CurrentEnemyBases.Contains(enemyHealthManager))
        {
            CurrentEnemyBases.Remove(enemyHealthManager);
        }

    } */

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

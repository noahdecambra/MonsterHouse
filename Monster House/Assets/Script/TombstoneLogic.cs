using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TombstoneLogic : BuildableBase
{
     void Start()
    {
       var towerscipt = gameObject.transform.parent.GetComponent<TowerController>();
       
    }
    public override void Effect()
    {
        if (enemyMovement.gameObject.name=="Ghost")
        {
            return;
        }
        enemyMovement.canMove = false;
        base.Effect();
        StartCoroutine(TakeDamage());
    }

    void OnDestroy()
   {
       foreach (var script in currentEnemyMoves)
       {
           script.canMove = true;
       }
   }
}

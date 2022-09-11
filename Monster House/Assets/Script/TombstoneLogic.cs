using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TombstoneLogic : BuildableBase
{
    public override void Effect()
    {
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

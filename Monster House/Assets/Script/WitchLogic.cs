using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchLogic : MonoBehaviour
{
    public LayerMask enemyLayer;
    [SerializeField] private float radius;
    private List<GameObject> nearbyEnemies;
    private int _healAmount = 5;
    private EnemyBase enemyScript;
    private float countdown;
    private float healTimer=3;
    void Start()
    {
        enemyScript = gameObject.GetComponent<EnemyBase>();
        
    }


    void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(Heal());
            countdown = healTimer;
        }

        countdown -= Time.deltaTime;
    }
    

    private IEnumerator Heal()
    {
        
            Collider[] enemiesInAOE = getHealArray();
            foreach (var enemy in enemiesInAOE)
            {
                if (Physics.CheckSphere(gameObject.transform.position, enemyLayer))
                {
                    enemy.gameObject.GetComponent<EnemyBase>().health += _healAmount;
                    Debug.Log(enemy.name + " Health: " + enemy.gameObject.GetComponent<EnemyBase>().health);
                }
            }

            yield return new WaitForSeconds(1f);
        
    }

    private Collider[] getHealArray()
    {
        List<Collider> temp = new List<Collider>();
       Collider[] initialColliders= Physics.OverlapSphere(transform.position, radius, enemyLayer);
       foreach (var val in initialColliders)
       {
           if (val.gameObject != gameObject)
           {
               temp.Add(val);
           }
       }

       Collider[] retVal = temp.ToArray();
       return retVal;
    }
}

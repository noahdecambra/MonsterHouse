using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public EnemySO enemyStats;
    private enemymov enemyMovement;

    public int health;
    public bool giveTickDamage;
    public bool damageOverTime;
    public float damage;
    public float attackRate;

   // [SerializeField]private float slowDuration;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.name = enemyStats.enemyName;
        if (gameObject.GetComponent<enemymov>()==null)
        {
            gameObject.AddComponent<enemymov>();
        }

        //Instantiate(enemyStats.enemyPrefab, gameObject.transform);
        enemyMovement = gameObject.GetComponent<enemymov>();
        health = enemyStats.health;
        damage = enemyStats.damage;
        attackRate = enemyStats.attackRate;
        damageOverTime = false;
        giveTickDamage = false;
        enemyMovement.baseSpeed = enemyStats.speed;
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void takeDamage(int damage)
    {
        health -= damage;
        Debug.Log(gameObject.name + ": " + health);
    }

    
   public IEnumerator takeTickDamage(int damage, float duration, bool tickDamage = false)
   {
       giveTickDamage = tickDamage;
        for (int i = 0; i < duration; i++)
        {
            takeDamage(damage);
            yield return new WaitForSeconds(1f);
        }

        while (giveTickDamage)
        {
            takeDamage(damage);
            yield return new WaitForSeconds(1f);
            
        }

        damageOverTime = false;
    }

   public void DamageMonitor(int damage, float duration = 1, bool tickDamage = false)
   {
       giveTickDamage = tickDamage;
       if (damageOverTime)
       {
           StartCoroutine(takeTickDamage(damage, duration, giveTickDamage));
       }
       else
       {
           takeDamage(damage);
       }
    }

   public void SlowEffect(float amount, float duration)
   {
      // slowDuration += duration;
       StartCoroutine(SlowCountdown(amount, duration));
   }
   
   IEnumerator SlowCountdown(float amount, float duration)
   {
       var movementScript = gameObject.GetComponent<enemymov>();
       if (movementScript != null)
       {

           if (movementScript.speed>= movementScript.baseSpeed*.25)
           {
               movementScript.speed /= amount;
           }
           yield return new WaitForSeconds(duration);

           movementScript.speed *= amount;
       }
   }
}

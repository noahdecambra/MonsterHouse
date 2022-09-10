using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public int health = 100;

    public bool damageOverTime;

   // [SerializeField]private float slowDuration;

    // Start is called before the first frame update
    void Start()
    {
        damageOverTime = false;
       
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

    
   public IEnumerator takeTickDamage(int damage, float duration)
    {
        for (int i = 0; i < duration; i++)
        {
            takeDamage(damage);
            yield return new WaitForSeconds(.25f);
        }

        damageOverTime = false;
    }

   public void DamageMonitor(int damage, float duration = 0)
   {
       if (damageOverTime)
       {
           StartCoroutine(takeTickDamage(damage, duration));
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

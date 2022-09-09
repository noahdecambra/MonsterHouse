using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public int health = 100;

    public bool damageOverTime;
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
}

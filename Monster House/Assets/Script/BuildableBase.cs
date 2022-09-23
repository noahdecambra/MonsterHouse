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
    public GameObject[] buildablePrefabs;
    public GameObject currentBuildable;

     

    public virtual void Effect()
    {
        
    }

    public GameObject GetCurrentBuildable()
    {
        return currentBuildable;
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

    public void Upgrade()
    {
        //check if can upgrade first, then proceed
        UpgradeEffects();
    }

    private void UpgradeEffects()
    {
        var currentLevel = gameObject.transform.parent.GetComponent<TowerController>().currentLevel;
        var currentTowerLevel = gameObject.transform.parent.GetComponent<TowerController>().currentTowerLevel;
        string turretName = gameObject.name;
        switch (turretName)
        {
            case "Tombstone":
                switch (currentLevel)
                {
                    case towerLevel.Level1:
                        currentTowerLevel++;
                        health += 10;
                        buildablePrefabs[0].SetActive(true);
                        buildablePrefabs[1].SetActive(false);
                        buildablePrefabs[2].SetActive(false);
                        currentBuildable = buildablePrefabs[0];
                        currentLevel++;
                        break;
                    case towerLevel.Level2:
                        currentTowerLevel++;
                        health += 10;
                        buildablePrefabs[0].SetActive(false);
                        buildablePrefabs[1].SetActive(true);
                        buildablePrefabs[2].SetActive(false);
                        currentBuildable = buildablePrefabs[1];
                        currentLevel++;
                        break;
                    case towerLevel.Level3:
                        currentTowerLevel++;
                        health += 10;
                        buildablePrefabs[0].SetActive(false);
                        buildablePrefabs[1].SetActive(false);
                        buildablePrefabs[2].SetActive(true);
                        currentBuildable = buildablePrefabs[2];
                        currentLevel++;
                        break;
                }
                break;
            case "Sprinkler":
                switch (currentLevel)
                {
                    case towerLevel.Level1:
                        currentTowerLevel++;
                        health += 10;
                        buildablePrefabs[0].SetActive(true);
                        buildablePrefabs[1].SetActive(false);
                        buildablePrefabs[2].SetActive(false);
                        currentBuildable = buildablePrefabs[0];
                        currentLevel++;
                        break;
                    case towerLevel.Level2:
                        currentTowerLevel++;
                        health += 10;
                        buildablePrefabs[0].SetActive(false);
                        buildablePrefabs[1].SetActive(true);
                        buildablePrefabs[2].SetActive(false);
                        currentBuildable = buildablePrefabs[1];
                        currentLevel++;
                        break;
                    case towerLevel.Level3:
                        currentTowerLevel++;
                        health += 10;
                        buildablePrefabs[0].SetActive(false);
                        buildablePrefabs[1].SetActive(false);
                        buildablePrefabs[2].SetActive(true);
                        currentBuildable = buildablePrefabs[2];
                        currentLevel++;
                        break;
                }
                break;
        }
    }




}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class EnemySO : ScriptableObject
{
    public int health;
    public int damage;
    public float attackRate;
    public string enemyName;
    public float speed;
    public GameObject enemyPrefab;
}

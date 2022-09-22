using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Turret", menuName = "Turrets")]
public class TurretSO : ScriptableObject
{
    public float range;
    public float rateOfFire;
    public GameObject bulletPrefab;
    public int damage;
    public string turretName;
}

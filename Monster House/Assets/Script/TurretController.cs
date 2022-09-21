using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [Header("Universal Attributes")]
    [SerializeField] private Transform _target;

    public float range = 15f;

   // public string enemyTag = "enemy";
    public string[] enemyTags;
    #region Firing vars
    [Header("Shooting")]
    [SerializeField] private float _fireRate = 1f;
    private float _fireCountdown = 0f;
    public GameObject bulletPrefab;
    [SerializeField] private Transform _firePoint;

    #endregion

    #region Rotation vars
    [Header("Rotation")]
    public Transform rotationPoint;

    private Quaternion _startRotation;

    [SerializeField] private float _rotationSpeed = 10f;


    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _startRotation = rotationPoint.rotation;
        InvokeRepeating("UpdateTarget", 0, .5f);
    }

    // Update is called once per frame
    void Update()
    {
      RotationController();

      if (_fireCountdown<=0f)
      {
          Shoot();
          _fireCountdown = 1f / _fireRate;
      }

      _fireCountdown -= Time.deltaTime;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    void UpdateTarget()
    {
        GameObject[] enemyGroups = null;
        List<GameObject> enemies = new List<GameObject>();
        foreach (var tag in enemyTags)
        {
            enemyGroups = GameObject.FindGameObjectsWithTag(tag);
            foreach (var enemy in enemyGroups)
            {
                enemies.Add(enemy);
            }
        }

        var nearestEnemyDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (var enemy in enemies)
        {
            var distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy<nearestEnemyDistance)
            {
                nearestEnemyDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy!=null && nearestEnemyDistance <= range)
        {
            _target = nearestEnemy.transform;
        }
        else
        {
            _target = null;
        }

    
    }
    void RotationController()
    {
        if (_target == null)
        {
            if (rotationPoint.rotation != _startRotation)
            {
                rotationPoint.rotation = Quaternion.Lerp(rotationPoint.rotation, _startRotation, Time.deltaTime);
            }
            return;
        }

        Vector3 dir = _target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(rotationPoint.rotation, lookRotation, Time.deltaTime*_rotationSpeed).eulerAngles;
        rotationPoint.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Shoot()
    { 
       GameObject bullet =(GameObject) Instantiate(bulletPrefab, _firePoint.position, _firePoint.rotation);
       BulletController bulletController = bullet.GetComponent<BulletController>();
       if (bulletController != null)
       {
           bulletController.TargetAquisition(_target);
       }
    }
}

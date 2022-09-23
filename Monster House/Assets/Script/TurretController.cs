using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public enum TurretLevel
{
    Level1, Level2, Level3, Level4
}
public class TurretController : MonoBehaviour
{
    public TurretSO turretStats;
    private int _turretLevel;
    [Header("Universal Attributes")]
    [SerializeField] private Transform _target;

    public GameObject[] turretPrefabs;
    public Transform[] rotationPoints;
    //public int currentPrefab;
    private int _upgradeCost;
    public int upgradeCost { get; private set; }
    private float _range = 15f;

   // public string enemyTag = "enemy";
    private string[] _enemyTags = new string[]{"Magical", "Beast", "Unholy"};
    #region Firing vars
    [Header("Shooting")]
    [SerializeField] private float _fireRate = 1f;
    private float _fireCountdown = 0f;
    private GameObject _bulletPrefab;
    [SerializeField] private Transform _firePoint;

    #endregion

    #region Rotation vars
    [Header("Rotation")]
    public Transform rotationPoint;

    private Quaternion _startRotation;

    [SerializeField] private float _rotationSpeed = 10f;

  

    public TurretLevel currentTurretLevel;
    #endregion

    void Awake()
    {
        foreach (var turret in turretPrefabs)
        {
            turret.SetActive(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        _turretLevel = 1;
        turretPrefabs[0].SetActive(true);
        _firePoint.transform.parent = turretPrefabs[0].transform;
        gameObject.name = turretStats.turretName;
        _bulletPrefab = turretStats.bulletPrefab;
        _range = turretStats.range;
        _fireRate = turretStats.rateOfFire;
        _startRotation = rotationPoint.rotation;
        InvokeRepeating("UpdateTarget", 0, .5f);
    }

    // Update is called once per frame
    void Update()
    {
      RotationController();

      if (_fireCountdown<=0f && _target !=null)
      {
          Shoot();
          _fireCountdown = 1f / _fireRate;
      }

      _fireCountdown -= Time.deltaTime;

     
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _range);
    }

    void UpdateTarget()
    {
        GameObject[] enemyGroups = null;
        List<GameObject> enemies = new List<GameObject>();
        foreach (var tag in _enemyTags)
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

        if (nearestEnemy!=null && nearestEnemyDistance <= _range)
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
       GameObject bullet =(GameObject) Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
       bullet.transform.parent = gameObject.transform;
       BulletController bulletController = bullet.GetComponent<BulletController>();
       if (bulletController != null)
       {
           bulletController.TargetAquisition(_target);
       }
    }

    public void Upgrade()
    {
        //check if can upgrade first, then proceed
        UpgradeEffects();
    }

    private void UpgradeEffects( )
    {
        string turretName = gameObject.name;
        switch (turretName)
        {
            case "Crossbow":
                switch (currentTurretLevel)
                {
                    case TurretLevel.Level1:
                        _turretLevel++;
                        _range += 3;
                        turretPrefabs[0].SetActive(true);
                        turretPrefabs[1].SetActive(false);
                        turretPrefabs[2].SetActive(false);
                        rotationPoint = rotationPoints[0];
                        _firePoint.transform.parent = turretPrefabs[0].transform;
                        currentTurretLevel++;
                        break;
                    case TurretLevel.Level2:
                        _turretLevel++;
                        turretPrefabs[0].SetActive(false);
                        turretPrefabs[1].SetActive(true);
                        turretPrefabs[2].SetActive(false);
                        rotationPoint = rotationPoints[1];
                        _firePoint.transform.parent = turretPrefabs[1].transform;
                        currentTurretLevel++;
                        break;
                    case TurretLevel.Level3:
                        _turretLevel++;
                        turretPrefabs[0].SetActive(false);
                        turretPrefabs[1].SetActive(false);
                        turretPrefabs[2].SetActive(true);
                        rotationPoint = rotationPoints[2];
                        _firePoint.transform.parent = turretPrefabs[2].transform;
                        currentTurretLevel++;
                        break;
                }
                break;
            case "Garlic Cannon":
                switch (currentTurretLevel)
                {
                    case TurretLevel.Level1:
                        _turretLevel++;
                        _range += 3;
                        turretPrefabs[0].SetActive(true);
                        turretPrefabs[1].SetActive(false);
                        turretPrefabs[2].SetActive(false);
                        rotationPoint = rotationPoints[0];
                        _firePoint.transform.parent = turretPrefabs[0].transform;
                        currentTurretLevel++;
                        break;
                    case TurretLevel.Level2:
                        _turretLevel++;
                        turretPrefabs[0].SetActive(false);
                        turretPrefabs[1].SetActive(true);
                        turretPrefabs[2].SetActive(false);
                        rotationPoint = rotationPoints[1];
                        _firePoint.transform.parent = turretPrefabs[1].transform;
                        currentTurretLevel++;
                        break;
                    case TurretLevel.Level3:
                        _turretLevel++;
                        turretPrefabs[0].SetActive(false);
                        turretPrefabs[1].SetActive(false);
                        turretPrefabs[2].SetActive(true);
                        rotationPoint = rotationPoints[2];
                        _firePoint.transform.parent = turretPrefabs[2].transform;
                        currentTurretLevel++;
                        break;
                }
                break;
        }
    }
}

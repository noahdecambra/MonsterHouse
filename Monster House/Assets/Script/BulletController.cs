using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    protected Transform _target;
    protected EnemyBase _enemyScript;
    [SerializeField] private float _speed = 70f;
    public GameObject impactEffect;
    [SerializeField] private float _destroyDelay;
    public void TargetAquisition(Transform target)
    {
        _target = target;
    }
    // Update is called once per frame
    void Update()
    {
        if (_target == null)
        {
            Destroy(gameObject);
            return;
        }
        Tracking();
    }

    void Tracking()
    {
        Vector3 dir = _target.position - transform.position;
        float distanceInFrame = _speed * Time.deltaTime;

        if (dir.magnitude <= distanceInFrame)
        {
            TargetHit();
            return;
        }

        transform.Translate(dir.normalized * distanceInFrame,Space.World);

    }

    public virtual void TargetHit()
    {
        Debug.Log("target reached");
        HitEffect();
       
        Destroy(gameObject,_destroyDelay);
    }

    public virtual void HitEffect()
    {
        if (impactEffect != null)
        {
            GameObject effect = Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(effect, 1);
        }
    }
}

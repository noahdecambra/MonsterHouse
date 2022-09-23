using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemymov : MonoBehaviour
{
    public float speed;
    public float baseSpeed = 5f;
    private Transform target;
    private int wavePointIndex = 0;
    public bool canMove;
    public Animator anim;
    public Transform rotationPoint;
    void Start()
    {
        if (anim == null)
        {
            if (gameObject.GetComponent<Animator>()==null)
            {
                gameObject.AddComponent<Animator>();
            }
            anim= gameObject.GetComponent<Animator>();
        }
        speed = baseSpeed;
        target = wavePoints.points[0];
        canMove = true;
    }

    void Update()
    {
        anim.SetBool("Walk", canMove);
        if (canMove)
        {
           
            Move();
        }
        else
        {
           
            anim.SetTrigger("Attack");
            
        }

    }

    void GetNextWaypoint()
    {
        if (wavePointIndex >= wavePoints.points.Length - 1)
        {
            Destroy(gameObject);
        }
        else
        {
            wavePointIndex++;
            target = wavePoints.points[wavePointIndex];
        }

    }

    void Move()
    {
        
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        LookController();
        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }
    }

    void LookController()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(rotationPoint.rotation, lookRotation, Time.deltaTime*speed).eulerAngles;
        rotationPoint.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

}


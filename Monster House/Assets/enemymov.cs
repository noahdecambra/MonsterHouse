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

    void Start()
    {
        speed = baseSpeed;
        target = wavePoints.points[0];
        canMove = true;
    }

    void Update()
    {
        if (canMove)
        {
            Move();
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

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }
    }

}


using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Patrol : FishController
{
    [SerializeField] private Transform patrolPointsParent;
    [SerializeField] private float waitTimeMin;
    [SerializeField] private float waitTimeMax;
    [SerializeField] private float acceleration;
    [SerializeField] private float deacceleration;

    private List<Transform> patrolPoints = new List<Transform>();
    private Transform targetPos;
    private float randomWaitTime;
    private Vector2 velocity = Vector2.zero;
    
    private Transform lastPos;

    private void OnEnable()
    {
        patrolPointsParent.GetComponentsInChildren(patrolPoints);
        patrolPoints.RemoveAt(0);
        targetPos = transform;
        
        SetUp();
    }


    protected override void Move()
    {


        rb.velocity = velocity;
        if (!(Vector2.Distance(transform.position, targetPos.position) < 0.3f)) return;
        rb.velocity = Vector2.zero;
        if (randomWaitTime <= 0)
        {

            SetUp();
        }
        else
        {
            randomWaitTime -= Time.deltaTime;
        }
    }

    private void SetUp()
    {
        lastPos = targetPos;
        

        randomWaitTime = Random.Range(waitTimeMin, waitTimeMax);

        var validPoints = patrolPoints.Where(go => go.gameObject != targetPos.gameObject).ToArray();

        if (validPoints.Length < 0)
        {
            return;
        }

        targetPos = validPoints[Random.Range(0, validPoints.Length)];
        
        velocity = (targetPos.position - lastPos.position) / (swimSpeed * Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingLine : MonoBehaviour
{
    private class LineParticle
    {
        public Vector3 Pos;
        public Vector3 OldPos;
        public Vector3 Acceleration;

        public LineParticle(Vector3 startPosition)
        {
            Pos = OldPos = startPosition;
            Acceleration = Physics.gravity;
        }
    }

    [SerializeField] private Transform anchorPoint;
    [SerializeField] private GameObject bobber;
    [SerializeField] private int iterations = 5;
    [SerializeField] private float length = 5f;
    [SerializeField] private float maxDistBetweenPoints;

    private List<LineParticle> points;
    private LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = this.GetComponent<LineRenderer>();
        points = new List<LineParticle>();

        var spacing = 0f;

        for (int i = 0; i < iterations; i++)
        {
            points.Add(new LineParticle(new Vector3(anchorPoint.position.x, anchorPoint.position.y - spacing, anchorPoint.position.z)));
            spacing += length / iterations;
        }

        lineRenderer.positionCount = points.Count;
        bobber.transform.position = (Vector2)lineRenderer.GetPosition(iterations - 1);
    }

    private void Update()
    {
        for (int i = 0; i < points.Count; i++)
        {
            lineRenderer.SetPosition(i, points[i].Pos);
        }
    }

    void FixedUpdate()
    {
        Verlet();

        for (int i = 0; i < points.Count - 1; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                PoleConstraint(points[i], points[i + 1], length / points.Count);
            }
        }

        bobber.transform.localPosition = (Vector2)lineRenderer.GetPosition(iterations - 1);
    }

    private void Verlet()
    {
        Anchor(points[0]);
        for (int i = 1; i < points.Count; i++)
        {
            var tmp = points[i].Pos;
            var dir = points[i].Pos - points[i].OldPos;
            var dist = Mathf.Clamp(Vector3.Distance(points[i].OldPos, points[i].Pos), 0, maxDistBetweenPoints);

            var dt = Time.deltaTime;
            points[i].Pos += (dir.normalized * dist) + points[i].Acceleration * (dt * dt);
            points[i].OldPos = tmp;
        }
    }

    private void PoleConstraint(LineParticle p1, LineParticle p2, float restLength)
    {
        var dir = p2.Pos - p1.Pos;
        var lineLength = dir.magnitude;
        var diff = (lineLength - restLength) / lineLength;

        p1.Pos += dir * (diff * 0.5f);
        p2.Pos -= dir * (diff * 0.5f);
    }

    private void Anchor(LineParticle topPoint)
    {
        topPoint.Pos = topPoint.OldPos = anchorPoint.position;
    }
}

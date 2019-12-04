﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    public float fieldOfViewAngle;
    public int rayCount;
    public float viewDistance;

    public bool playerInSight;
    public Vector2 personalLastSighting;

    private float angle;

    private EnemyMovement em;
    private Vector2 currentPos;
    private Quaternion currentRotate;

    [SerializeField]
    private LayerMask layerMask;

    private Vector3 origin = Vector3.zero;
    private Mesh mesh;

    private void Start()
    {
        //currentPos = em.transform.position;
        //currentRotate = em.transform.rotation;
        angle = fieldOfViewAngle / 2f;
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    private void Update()
    {
        float angleIncrease = fieldOfViewAngle / rayCount;
        //currentPos = em.transform.position;
        //currentRotate = em.transform.rotation;
        var tempAngle = angle;
        Vector3[] verts = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[verts.Length];
        int[] tris = new int[rayCount * 3];

        verts[0] = origin;

        int vertexIndex = 1;
        int trisIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;
            RaycastHit2D rayHit = Physics2D.Raycast(origin, GetVectorFromAngle(tempAngle), viewDistance, layerMask);

            if (rayHit.collider == null)
            {
                vertex = origin + GetVectorFromAngle(tempAngle) * viewDistance;
            }
            else
            {
                vertex = rayHit.point;
            }

            verts[vertexIndex] = vertex;

            if (i > 0)
            {
                tris[trisIndex + 0] = 0;
                tris[trisIndex + 1] = vertexIndex - 1;
                tris[trisIndex + 2] = vertexIndex;

                trisIndex += 3;
            }
            vertexIndex++;
            tempAngle -= angleIncrease;
        }

        mesh.vertices = verts;
        mesh.uv = uv;
        mesh.triangles = tris;

    }

    public void SetOrigin(Vector3 origin)
    {
        this.origin = origin;
    }

    private Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    private void ChangeViewDirection(float viewDirection)
    {
       // GetVectorFromAngle(viewDirection);
    }

    public void setAngle(float newAngle)
    {
        angle = newAngle;
        angle += fieldOfViewAngle / 2f;
    }

    private float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }
}

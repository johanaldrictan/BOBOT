using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
   
    public float fieldOfViewAngle;
    public int rayCount;
    public float viewDistance;


    public bool playerInSight;
    public Vector2 personalLastSighting;

    [SerializeField]
    private LayerMask layerMask;

    private Vector3 origin = Vector3.zero;
    private Mesh mesh;

    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    private void Update()
    {
        float angle = 0f;
        float angleIncrease = fieldOfViewAngle / rayCount;

        Vector3[] verts = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[verts.Length];
        int[] tris = new int[rayCount * 3];

        verts[0] = origin;

        int vertexIndex = 1;
        int trisIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;
            RaycastHit2D rayHit = Physics2D.Raycast(origin, GetVectorFromAngle(angle), viewDistance, layerMask);

            if (rayHit.collider == null)
            {
                vertex = origin + GetVectorFromAngle(angle) * viewDistance;
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

            angle -= angleIncrease;
        }

        mesh.vertices = verts;
        mesh.uv = uv;
        mesh.triangles = tris;

    }

    private static Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }
}

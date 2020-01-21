using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class sizeCollider : MonoBehaviour
{
    private TextMeshPro m_mesh;
    private EdgeCollider2D col;

    public List<Vector2> flatPoints;
    public Vector3[] vertices;

    private void Start()
    {
        col = GetComponent<EdgeCollider2D>();
    }

    private void Update()
    {
        if(col.pointCount != GetComponent<MeshFilter>().mesh.vertexCount)
        {
           
            for(int i = 0; i < GetComponent<MeshFilter>().mesh.vertexCount; i++)
            {
                col.points[i] = new Vector2(GetComponent<MeshFilter>().mesh.vertices[i].x, GetComponent<MeshFilter>().mesh.vertices[i].y);
            }
        }
    }
}

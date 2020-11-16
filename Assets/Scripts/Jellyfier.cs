using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Jellyfier : MonoBehaviour
{
    //How fast it will jiggle
    public float bounceSpeed;
    
    //Emulate the mass when dropping
    public float fallForce;
    
    public float stiffness;

    public bool mouseControl;

    private MeshFilter _meshFilter;
    private Mesh _mesh;
    
    JellyVertex[] _jellyVertices;
    Vector3[] _currentMeshVertices;

    private int mousePressure;

    // Start is called before the first frame update
    void Start()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _mesh = _meshFilter.mesh;

        GetVertices();
    }

    private void GetVertices()
    {
        _jellyVertices = new JellyVertex[_mesh.vertices.Length];
        _currentMeshVertices = new Vector3[_mesh.vertices.Length];
        for (int i = 0; i < _mesh.vertices.Length; i++)
        {
            _jellyVertices[i] = new JellyVertex(i, _mesh.vertices[i], _mesh.vertices[i], Vector3.zero);
            _currentMeshVertices[i] = _mesh.vertices[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateVertices();
    }
    
    void OnMouseDrag()
    {
        mousePressure += 1;
        Debug.Log(mousePressure);
    }
    
    void OnMouseUp()
    {
        if (mouseControl)
        {
            ApplyPressureToPoint(
                Camera.main.ScreenToWorldPoint((new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f))),
                mousePressure);
        }
        mousePressure = 0;
    }


    //Update vertices and apply it to the mesh
    private void UpdateVertices()
    {
        for (int i = 0; i < _jellyVertices.Length; i++)
        {
            _jellyVertices[i].UpdateVelocity(bounceSpeed);
            _jellyVertices[i].Settle(stiffness);

            _jellyVertices[i].CurrentVertexPosition += _jellyVertices[i].CurrentVelocity * Time.deltaTime;
            _currentMeshVertices[i] = _jellyVertices[i].CurrentVertexPosition;
        }

        _mesh.vertices = _currentMeshVertices;
        _mesh.RecalculateBounds();
        _mesh.RecalculateNormals();
        _mesh.RecalculateTangents();
    }
    
    //Applying offset and pressure when colliding
    public void OnCollisionEnter(Collision other)
    {
        ContactPoint[] collisionPoints = other.contacts;
        for (int i = 0; i < collisionPoints.Length; i++)
        {
            Vector3 inputPoint = collisionPoints[i].point + (collisionPoints[i].point * .1f);
            ApplyPressureToPoint(inputPoint, fallForce);
        }
    }
    
    //Mouse click pressure
    public void ApplyPressureToPoint(Vector3 _point, float _pressure)
    {
        for (int i = 0; i < _jellyVertices.Length; i++)
        {
            _jellyVertices[i].ApplyPressureToVertex(transform, _point, _pressure);
        }
    }
}

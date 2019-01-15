using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GGR/Location")]
public class GGR_Location : ScriptableObject
{
    public Vector3 position;
    public Mesh pictureMesh;
    public Material pictureMaterial;

    public void Render()
    {
        Graphics.DrawMesh(pictureMesh, Vector3.zero, Quaternion.identity, pictureMaterial, 0);
    }

}

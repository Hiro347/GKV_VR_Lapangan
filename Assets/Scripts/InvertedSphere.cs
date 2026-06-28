using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(MeshFilter))]
public class InvertedSphere : MonoBehaviour
{
    void Start()
    {
        InvertMesh();
    }

    void OnValidate()
    {
        InvertMesh();
    }

    [ContextMenu("Invert Mesh")]
    public void InvertMesh()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null) return;

        Mesh mesh = meshFilter.sharedMesh;
        if (mesh == null) return;

        // Skip if already inverted
        if (mesh.name.Contains("_inverted")) return;

        Mesh newMesh = Instantiate(mesh);
        newMesh.name = mesh.name + "_inverted";

        // Reverse triangles (flips normal directions)
        int[] triangles = newMesh.triangles;
        for (int i = 0; i < triangles.Length; i += 3)
        {
            int temp = triangles[i];
            triangles[i] = triangles[i + 2];
            triangles[i + 2] = temp;
        }
        newMesh.triangles = triangles;

        // Reverse normals
        Vector3[] normals = newMesh.normals;
        for (int i = 0; i < normals.Length; i++)
        {
            normals[i] = -normals[i];
        }
        newMesh.normals = normals;

        meshFilter.sharedMesh = newMesh;

        // Update MeshCollider if present
        MeshCollider meshCollider = GetComponent<MeshCollider>();
        if (meshCollider != null)
        {
            meshCollider.sharedMesh = newMesh;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinnedMeshColliderBaker : MonoBehaviour
{
    [field: SerializeField]
    private SkinnedMeshRenderer TargetObjectRenderer { get; set; }
    [field: SerializeField]
    private MeshCollider TargetObjectCollider { get; set; }
    // Start is called before the first frame update
    public virtual void Start()
    {
        BakeMesh();
    }

    public void BakeMesh ()
    {
        Mesh mesh = new Mesh();
        TargetObjectRenderer.BakeMesh(mesh, true);
        mesh.RecalculateBounds();
        TargetObjectCollider.sharedMesh = null;
        TargetObjectCollider.sharedMesh = mesh;
    }
}

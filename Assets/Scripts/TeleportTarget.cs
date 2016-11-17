using UnityEngine;
using System.Collections;

public class TeleportTarget : MonoBehaviour {
    public static bool show { get; set; }

    public MeshRenderer mesh { get; private set; }

    public bool alignToTerrain = true;

	// Use this for initialization
	void Start () {
        mesh = GetComponent<MeshRenderer>();

        Mesh model = GetComponent<MeshFilter>().mesh;

        if (alignToTerrain) {
            int layerMask = 1 << LayerMask.NameToLayer("Terrain");

            Vector3[] verts = model.vertices;
            for (int i = 0; i < verts.Length; i++) {
                RaycastHit hit;
                if (Physics.Raycast(transform.TransformPoint(verts[i]) + Vector3.down * 0.02f, Vector3.down, out hit, layerMask)) {
                    verts[i] = transform.InverseTransformPoint(hit.point) + Vector3.up * 0.04f;
                }
            }

            model.vertices = verts;
            model.RecalculateNormals();
            model.RecalculateBounds();

            GetComponent<MeshCollider>().sharedMesh = model;
        }
	}
	
	// Update is called once per frame
	void Update () {
        mesh.enabled = show;
	}
}

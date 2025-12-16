using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace CGRust.Samples
{
    public static class SampleUtil
    {
    
        public static GameObject CreateEmptyMeshGO(Transform parent, string name, out MeshFilter mf, out MeshRenderer mr)
        {
            var go = new GameObject(name);
            go.transform.parent = parent;

            mf = go.AddComponent<MeshFilter>();
            mr = go.AddComponent<MeshRenderer>();

            return go;
        }

        public static List<GameObject> CreateDebugMeshGO(SampleScene scene, Transform parent, string name, Mesh sharedMesh, out List<MeshFilter> mf, out List<MeshRenderer> mr)
        {
            var go = new GameObject(name);
            go.transform.parent = parent;

            var regularGO = new GameObject($"{name}_rendering");
            var wireframeGO = new GameObject($"{name}_wireframe");
            var pointGO = new GameObject($"{name}_points");

            regularGO.transform.parent = go.transform;
            wireframeGO.transform.parent = go.transform;
            pointGO.transform.parent = go.transform;

            mf = new List<MeshFilter>();
            mr = new List<MeshRenderer>();

            mf.Add(regularGO.AddComponent<MeshFilter>());
            mf.Add(wireframeGO.AddComponent<MeshFilter>());

            mr.Add(regularGO.AddComponent<MeshRenderer>());
            mr.Add(wireframeGO.AddComponent<MeshRenderer>());

            var wireRenderer = mr[(int)SampleRenderDebugType.WIREFRAME];
            wireRenderer.sharedMaterial = scene.Materials.WireframeMaterial;
            wireRenderer.shadowCastingMode = ShadowCastingMode.Off;
            
            for(int i = 0; i < mf.Count; i++)
            {
                mf[i].sharedMesh = sharedMesh;
            }

            var vertexRenderer = pointGO.AddComponent<VertexRenderer>();
            vertexRenderer.Init(scene, sharedMesh, parent);

            var gos = new List<GameObject>();
            gos.Add(regularGO);
            gos.Add(wireframeGO);
            gos.Add(pointGO);

            return gos;
        }

    }
}

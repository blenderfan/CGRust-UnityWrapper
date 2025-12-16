using UnityEngine;

namespace CGRust.Samples
{
    public class VertexRenderer : MonoBehaviour
    {

        private Mesh renderMesh;
        private Mesh vertexMesh;

        private SampleScene scene;

        private Matrix4x4[] baseVertexMatrices;
        private Matrix4x4[] vertexMatrices;

        private RenderParams rp;

        private Transform meshParent;

        public void Init(SampleScene scene, Mesh renderMesh, Transform meshParent)
        {
            this.scene = scene;
            this.renderMesh = renderMesh;

            var vertices = renderMesh.vertices;

            this.baseVertexMatrices = new Matrix4x4[vertices.Length];
            this.vertexMatrices = new Matrix4x4[vertices.Length];

            for (int i = 0; i < vertices.Length; i++)
            {
                this.baseVertexMatrices[i] = Matrix4x4.Translate(vertices[i]);
            }

            var vertexPrefab = scene.Prefabs.Vertex;
            var mf = vertexPrefab.GetComponentInChildren<MeshFilter>();

            if(mf != null)
            {
                this.vertexMesh = mf.sharedMesh;
            }

            this.rp = new RenderParams(this.scene.Materials.PointMaterial);
            this.meshParent = meshParent;
        }


        private void Update()
        {
            for(int i = 0; i < this.vertexMatrices.Length; i++)
            {
                this.vertexMatrices[i] = this.baseVertexMatrices[i] * this.meshParent.localToWorldMatrix;
            }
            Graphics.RenderMeshInstanced(this.rp, this.vertexMesh, 0, this.vertexMatrices);
        }

    }
}

using CGRust.Runtime;
using CGRust.Samples;
using Unity.Mathematics;
using UnityEngine;

namespace CGRust.Samples.FanTriangulation
{

    public class FanTriangulation : MonoBehaviour
    {

        public Gradient polygonGradient;

        public Material material;

        public SampleScene scene;

        void Start()
        {

            var polygon = Polygon2D.CreateRegular(new float2(0.0f, 0.0f), 1.0f, 6);
            var mesh = polygon.ToMesh(CardinalDirection.Z);

            var hardEdgeMesh = MeshUtil.ToHardEdgesMesh(mesh);
            MeshVisualizationUtil.AddTriangleGradientColors(ref hardEdgeMesh, this.polygonGradient);

            SampleUtil.CreateDebugMeshGO(this.scene, this.transform, "polygon", hardEdgeMesh, out var mf, out var mr);

            mr[(int)SampleRenderDebugType.TRIANGLES].sharedMaterial = this.material;

            polygon.Dispose();
        }
    }

}
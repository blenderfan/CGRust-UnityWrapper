using CGRust.Runtime;
using Unity.Mathematics;
using UnityEngine;

namespace CGRust.Samples.FanTriangulation
{

    public class FanTriangulation : MonoBehaviour
    {

        public Gradient polygonGradient;

        public Material material;

        void Start()
        {

            var polygon = Polygon2D.CreateRegular(new float2(0.0f, 0.0f), 1.0f, 6);
            var mesh = polygon.ToMesh(CardinalDirection.Z);

            var hardEdgeMesh = MeshUtil.ToHardEdgesMesh(mesh);
            MeshVisualizationUtil.AddTriangleGradientColors(ref hardEdgeMesh, this.polygonGradient);

            var mf = this.GetComponent<MeshFilter>();
            mf.sharedMesh = hardEdgeMesh;

            var mr = this.GetComponent<MeshRenderer>();
            mr.sharedMaterial = this.material;

            polygon.Dispose();
        }
    }

}
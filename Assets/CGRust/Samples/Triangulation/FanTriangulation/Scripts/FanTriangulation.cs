using CGRust.Runtime;
using CGRust.Wrapper;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

namespace CGRust.Samples.FanTriangulation
{

    public class FanTriangulation : MonoBehaviour
    {

        public Gradient polygonGradient;

        public Material material;

        void Start()
        {

            var pointsArray = PolygonMethods.CreateRegularPolygon(new float2(0.0f, 0.0f), 1.0f, 6);
            var polygon = new Polygon2D(pointsArray);

            var mesh = polygon.ToMesh();

            var hardEdgeMesh = MeshUtil.ToHardEdgesMesh(mesh);
            MeshVisualizationUtil.AddTriangleGradientColors(ref hardEdgeMesh, this.polygonGradient);

            var mf = this.GetComponent<MeshFilter>();
            mf.sharedMesh = hardEdgeMesh;

            var mr = this.GetComponent<MeshRenderer>();
            mr.sharedMaterial = this.material;

            pointsArray.Dispose();
            polygon.Dispose();
        }
    }

}
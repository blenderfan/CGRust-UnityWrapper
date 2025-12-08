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
        public Material material;

        void Start()
        {

            var pointsArray = PolygonMethods.CreateRegularPolygon(new float2(0.0f, 0.0f), 1.0f, 6);
            var triangulationArray = PolygonMethods.TriangulatePolygon(pointsArray);
            var mesh = new Mesh();

            var points = pointsArray.data;
            var triangulation = triangulationArray.data;

            Vector3[] vertices = new Vector3[points.Length];
            for(int i = 0; i < vertices.Length; i++)
            {
                vertices[i] = new Vector3(points[i].x, points[i].y, 0.0f);
            }
            int[] triangles = new int[triangulation.Length];
            for(int i = 0; i < triangulation.Length; i++)
            {
                triangles[i] = (int)triangulation[i];
            }

            mesh.SetVertices(vertices);
            mesh.SetTriangles(triangles, 0);
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();

            var mf = this.GetComponent<MeshFilter>();
            mf.sharedMesh = mesh;

            var mr = this.GetComponent<MeshRenderer>();
            mr.sharedMaterial = this.material;

            pointsArray.Dispose();
            triangulationArray.Dispose();
        }
    }

}
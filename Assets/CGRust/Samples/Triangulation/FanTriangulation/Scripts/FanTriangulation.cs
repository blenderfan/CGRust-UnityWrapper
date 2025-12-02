using CGRust.Wrapper;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace CGRust.Samples.FanTriangulation
{

    public class FanTriangulation : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

            NativeArray<float2> points = new NativeArray<float2>(new float2[]
            {
                new float2(0.0f, 0.0f),
                new float2(1.0f, 0.0f),
                new float2(1.0f, 1.0f),
                new float2(0.0f, 1.0f),
            }, Allocator.TempJob);

            var triangulation = PolygonTriangulation.TriangulatePolygon(points);

            Debug.Log(triangulation.Length);

            points.Dispose();
            triangulation.Dispose();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
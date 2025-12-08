using System;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;

namespace CGRust.Wrapper
{
    public static class PolygonMethods
    {
        /// <summary>
        /// Creates a regular polygon where each line segment has the same length and the angle
        /// between all edges are equal as well.
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        /// <param name="corners"></param>
        /// <returns></returns>
        public unsafe static CGRustArray<float2> CreateRegularPolygon(float2 center, float radius, int corners)
        {
            var polyPtr = cg_rust_polygon_regular(center, radius, corners);

            PArray<float2> nativeArray = new PArray<float2>();
            nativeArray = Marshal.PtrToStructure<PArray<float2>>(polyPtr);
            return new CGRustArray<float2>(new UnsafeList<float2>(nativeArray.data, (int)nativeArray.length));
        }

        /// <summary>
        /// Splits the polygon into triangles and returns a list of integers, where each triple
        /// forms a triangle as part of the triangulation of the polygon.
        /// 
        /// The method will return an empty array if the polygon is self-intersecting
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public unsafe static CGRustArray<long> TriangulatePolygon(CGRustArray<float2> points) 
        {
            PArray<float2> pPoints = new PArray<float2>()
            {
                data = points.data.Ptr,
                length = points.data.Length
            };

            var listPtr = cg_rust_polygon_triangulate(ref pPoints);

            if(listPtr != IntPtr.Zero)
            {
                PArray<long> nativeArray = new PArray<long>();
                nativeArray = Marshal.PtrToStructure<PArray<long>>(listPtr);
                var unsafeList = new UnsafeList<long>(nativeArray.data, (int)nativeArray.length);
                unsafeList.Reverse();

                return new CGRustArray<long>(unsafeList);
            }
            return new CGRustArray<long>();
        }


        #region Import

        [DllImport("cg_rust", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        private static extern IntPtr cg_rust_polygon_regular(float2 center, float radius, long corners);

        [DllImport("cg_rust", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        private static extern IntPtr cg_rust_polygon_triangulate(ref PArray<float2> points);

        #endregion


    }
}

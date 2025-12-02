using System;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;

namespace CGRust.Wrapper
{
    public static class PolygonTriangulation
    {

        public unsafe static NativeArray<int> TriangulatePolygon(NativeArray<float2> points, Allocator alloc = Allocator.TempJob) 
        {
            PArray<float2> pPoints = new PArray<float2>()
            {
                data = (float2*)points.GetUnsafePtr(),
                length = points.Length
            };

            var listPtr = cg_rust_polygon_triangulate(ref pPoints);


            if(listPtr != IntPtr.Zero)
            {
                PArray<int> nativeArray = new PArray<int>();
                nativeArray = Marshal.PtrToStructure<PArray<int>>(listPtr);

                NativeArray<int> triangulation = new NativeArray<int>((int)nativeArray.length, alloc);
                UnsafeUtility.MemCpy(triangulation.GetUnsafePtr(), nativeArray.data, nativeArray.length);

                return triangulation;
            }
            return new NativeArray<int>();
        }


        #region Import

        [DllImport("cg_rust", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        private static extern Int32 cg_rust_simple_test(Int32 t);

        [DllImport("cg_rust", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        private static extern IntPtr cg_rust_polygon_triangulate(ref PArray<float2> points);

        #endregion


    }
}

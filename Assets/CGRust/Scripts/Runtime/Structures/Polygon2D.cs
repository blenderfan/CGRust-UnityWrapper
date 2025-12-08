using CGRust.Wrapper;
using System;
using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

namespace CGRust.Runtime
{
    /// <summary>
    /// A 2D polygon, that can be used within Unity's Job System
    /// </summary>
    public unsafe struct Polygon2D : IDisposable, IGLTFExportable
    {
        #region Public Fields

        CGRustArray<float2> points;

        #endregion

        public Polygon2D(CGRustArray<float2> points)
        {
            this.points = points;
        }

        public bool IsCreated => this.points.data.IsCreated;

        [BurstDiscard]
        public void Dispose()
        {
            this.points.Dispose();
        }

        [BurstDiscard]
        public Mesh ToMesh(string name = "")
        {
            var triangulationArray = PolygonMethods.TriangulatePolygon(this.points);

            var dataArray = Mesh.AllocateWritableMeshData(1);
            var data = dataArray[0];

            IndexFormat idxFormat = triangulationArray.data.Length < ushort.MaxValue ? IndexFormat.UInt16 : IndexFormat.UInt32;

            data.SetVertexBufferParams(this.points.data.Length,
                new VertexAttributeDescriptor(VertexAttribute.Position),
                new VertexAttributeDescriptor(VertexAttribute.Normal, stream:1));

            data.SetIndexBufferParams(triangulationArray.data.Length, idxFormat);

            var positions = data.GetVertexData<Vector3>();
            for(int i = 0; i < positions.Length; i++)
            {
                positions[i] = new Vector3(this.points.data[i].x, this.points.data[i].y, 0.0f);
            }

            if (idxFormat == IndexFormat.UInt16)
            {
                var indices = data.GetIndexData<ushort>();
                UnsafeUtility.MemCpyStride(indices.GetUnsafePtr(), sizeof(ushort), triangulationArray.data.Ptr, sizeof(long), sizeof(ushort), indices.Length);
            }
            else
            {
                var indices = data.GetIndexData<uint>();
                UnsafeUtility.MemCpyStride(indices.GetUnsafePtr(), sizeof(uint), triangulationArray.data.Ptr, sizeof(long), sizeof(uint), indices.Length);
            }

            data.subMeshCount = 1;
            data.SetSubMesh(0, new SubMeshDescriptor(0, triangulationArray.data.Length));

            var mesh = new Mesh();
            mesh.name = name;
            Mesh.ApplyAndDisposeWritableMeshData(dataArray, mesh);
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();

            triangulationArray.Dispose();

            return mesh;
        }

        #region IGLTFExportable

        #endregion
    }
}

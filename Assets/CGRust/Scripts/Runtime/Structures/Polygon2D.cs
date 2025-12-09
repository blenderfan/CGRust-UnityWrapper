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

        /// <summary>
        /// Transforms the Polygons into a Unity Mesh
        /// </summary>
        /// <param name="name">The name assigned to the new mesh</param>
        /// <returns></returns>
        [BurstDiscard]
        public Mesh ToMesh(CardinalDirection up, string name = "")
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
            var planeIndices = up.GetOrthogonalIndices();
            for(int i = 0; i < positions.Length; i++)
            {
                var pos = Vector3.zero;
                pos[planeIndices.x] = this.points.data[i].x;
                pos[planeIndices.y] = this.points.data[i].y;
                positions[i] = pos;
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

        /// <summary>
        /// Creates a regular polygon where each line segment has the same length and the angle
        /// between all edges are equal as well.
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        /// <param name="corners"></param>
        /// <returns></returns>
        public static Polygon2D CreateRegular(float2 center, float radius, int corners)
        {
            var pointsArray = PolygonMethods.CreateRegularPolygon(center, radius, corners);
            return new Polygon2D(pointsArray);
        }

        #region IGLTFExportable

        #endregion
    }
}

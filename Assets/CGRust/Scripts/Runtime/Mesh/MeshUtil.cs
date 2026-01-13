using Codice.CM.Common;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Rendering;

namespace CGRust.Runtime
{
    public static class MeshUtil
    {
        /// <summary>
        /// Turns a mesh with shared vertices into a mesh were each triangle has its own vertices (producing a hard-edged mesh)
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public unsafe static Mesh ToHardEdgesMesh(Mesh original, string name = "")
        {
            
            var originalArray = Mesh.AcquireReadOnlyMeshData(original);
            var originalData = originalArray[0];

            var dataArray = Mesh.AllocateWritableMeshData(1);
            var data = dataArray[0];

            IndexFormat idxFormat = original.indexFormat;

            int indices = original.triangles.Length;

            var vertexAttributes = original.GetVertexAttributes();

            data.SetVertexBufferParams(indices, vertexAttributes);
            data.SetIndexBufferParams(indices, idxFormat);

            NativeArray<uint> triangleCopy = new NativeArray<uint>(indices, Allocator.TempJob);
            if (idxFormat == IndexFormat.UInt16)
            {
                var indexArr = data.GetIndexData<ushort>();
                var originalTriangles = originalData.GetIndexData<ushort>();
                for (int j = 0; j < indices; j++)
                {
                    indexArr[j] = (ushort)j;
                }
                UnsafeUtility.MemCpyStride(triangleCopy.GetUnsafePtr(), UnsafeUtility.SizeOf<uint>(), originalTriangles.GetUnsafeReadOnlyPtr(), UnsafeUtility.SizeOf<ushort>(),
                    UnsafeUtility.SizeOf<ushort>(), indices);
            }
            else
            {
                var indexArr = data.GetIndexData<uint>();
                var originalTriangles = originalData.GetIndexData<uint>();
                for (int j = 0; j < indices; j++)
                {
                    indexArr[j] = (uint)j;
                }
                UnsafeUtility.MemCpy(triangleCopy.GetUnsafePtr(), originalTriangles.GetUnsafeReadOnlyPtr(), UnsafeUtility.SizeOf<uint>() * indices);
            }

            for (int i = 0; i < vertexAttributes.Length; i++)
            {

                var attribute = vertexAttributes[i];

                var length = attribute.dimension;
                if (attribute.format == VertexAttributeFormat.SInt16
                    || attribute.format == VertexAttributeFormat.Float16
                    || attribute.format == VertexAttributeFormat.SNorm16
                    || attribute.format == VertexAttributeFormat.UInt16
                    || attribute.format == VertexAttributeFormat.UNorm16)
                {
                    length *= 2;
                }
                else if (attribute.format == VertexAttributeFormat.Float32
                    || attribute.format == VertexAttributeFormat.SInt32
                    || attribute.format == VertexAttributeFormat.UInt32)
                {
                    length *= 4;
                }

                var vertexData = data.GetVertexData<byte>();
                var originalVertexData = originalData.GetVertexData<byte>();

                var vertexDataPtr = (byte*)vertexData.GetUnsafePtr();
                var originalVertexDataPtr = (byte*)originalVertexData.GetUnsafeReadOnlyPtr();

                for (int j = 0; j < indices; j++)
                {
                    uint idx = triangleCopy[j];
                    UnsafeUtility.MemCpy(vertexDataPtr + j * length, originalVertexDataPtr + idx * length, length);
                }
            }

            data.subMeshCount = originalData.subMeshCount;
            
            for(int i = 0; i < originalData.subMeshCount; i++)
            {
                var subMeshDescriptor = originalData.GetSubMesh(i);
                data.SetSubMesh(i, subMeshDescriptor);
            }

            originalArray.Dispose();
            triangleCopy.Dispose();

            var mesh = new Mesh();
            mesh.name = name;
            Mesh.ApplyAndDisposeWritableMeshData(dataArray, mesh);
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();

            return mesh;
        }

    }
}

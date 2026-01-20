using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Rendering;

namespace CGRust.Runtime
{
    public static class MeshUtilJobs
    {

        [BurstCompile]
        public unsafe struct ToHardEdgeJob : IJob
        {
            public int indices;

            public Mesh.MeshData data;

            public Mesh.MeshData original;

            public NativeArray<VertexAttributeDescriptor> attributes;

            public void Execute()
            {
                IndexFormat idxFormat = this.original.indexFormat;

                this.data.SetVertexBufferParams(this.indices, this.attributes);
                this.data.SetIndexBufferParams(this.indices, idxFormat);
                
                NativeArray<uint> triangleCopy = new NativeArray<uint>(this.indices, Allocator.Temp);
                if (idxFormat == IndexFormat.UInt16)
                {
                    var indexArr = this.data.GetIndexData<ushort>();
                    var originalTriangles = this.original.GetIndexData<ushort>();
                    for (int j = 0; j < this.indices; j++)
                    {
                        indexArr[j] = (ushort)j;
                    }
                    UnsafeUtility.MemCpyStride(triangleCopy.GetUnsafePtr(), UnsafeUtility.SizeOf<uint>(), originalTriangles.GetUnsafeReadOnlyPtr(), UnsafeUtility.SizeOf<ushort>(),
                        UnsafeUtility.SizeOf<ushort>(), this.indices);
                }
                else
                {
                    var indexArr = this.data.GetIndexData<uint>();
                    var originalTriangles = this.original.GetIndexData<uint>();
                    for (int j = 0; j < indices; j++)
                    {
                        indexArr[j] = (uint)j;
                    }
                    UnsafeUtility.MemCpy(triangleCopy.GetUnsafePtr(), originalTriangles.GetUnsafeReadOnlyPtr(), UnsafeUtility.SizeOf<uint>() * this.indices);
                }

                for (int i = 0; i < this.attributes.Length; i++)
                {

                    var attribute = this.attributes[i];

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

                    var vertexData = this.data.GetVertexData<byte>();
                    var originalVertexData = this.original.GetVertexData<byte>();

                    var vertexDataPtr = (byte*)vertexData.GetUnsafePtr();
                    var originalVertexDataPtr = (byte*)originalVertexData.GetUnsafeReadOnlyPtr();

                    for (int j = 0; j < this.indices; j++)
                    {
                        uint idx = triangleCopy[j];
                        UnsafeUtility.MemCpy(vertexDataPtr + j * length, originalVertexDataPtr + idx * length, length);
                    }
                }

                this.data.subMeshCount = this.original.subMeshCount;

                for (int i = 0; i < this.original.subMeshCount; i++)
                {
                    var subMeshDescriptor = this.original.GetSubMesh(i);
                    data.SetSubMesh(i, subMeshDescriptor);
                }
            }
        }

    }
}

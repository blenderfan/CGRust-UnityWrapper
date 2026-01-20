using Unity.Collections;
using Unity.Jobs;
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

            var vertexAttributes = new NativeArray<VertexAttributeDescriptor>(original.GetVertexAttributes(), Allocator.TempJob);

            var toHardEdgeJob = new MeshUtilJobs.ToHardEdgeJob()
            {
                attributes = vertexAttributes,
                indices = indices,
                data = data,
                original = originalData
            };
            toHardEdgeJob.Schedule().Complete();

            vertexAttributes.Dispose();
            originalArray.Dispose();

            var mesh = new Mesh();
            mesh.name = name;
            Mesh.ApplyAndDisposeWritableMeshData(dataArray, mesh);
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();

            return mesh;
        }

    }
}

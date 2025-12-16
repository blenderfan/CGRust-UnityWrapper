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

            data.SetVertexBufferParams(indices, original.GetVertexAttributes());
            data.SetIndexBufferParams(indices, idxFormat);

            var positions = data.GetVertexData<Vector3>();
            var originalPositions = originalData.GetVertexData<Vector3>();  


            if (idxFormat == IndexFormat.UInt16)
            {
                var indexArr = data.GetIndexData<ushort>();
                var originalTriangles = originalData.GetIndexData<ushort>();

                for (int i = 0; i < indices; i++)
                {
                    uint idx = originalTriangles[i];
                    var vertex = originalPositions[(int)idx];
                    positions[i] = vertex;
                    indexArr[i] = (ushort)i;
                }
            }
            else
            {
                var indexArr = data.GetIndexData<uint>();
                var originalTriangles = originalData.GetIndexData<uint>();

                for (int i = 0; i < indices; i++)
                {
                    uint idx = originalTriangles[i];
                    var vertex = originalPositions[(int)idx];
                    positions[i] = vertex;
                    indexArr[i] = (uint)i;
                }
            }

            originalArray.Dispose();

            data.subMeshCount = 1;
            data.SetSubMesh(0, new SubMeshDescriptor(0, original.triangles.Length));

            var mesh = new Mesh();
            mesh.name = name;
            Mesh.ApplyAndDisposeWritableMeshData(dataArray, mesh);
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();

            return mesh;
        }

    }
}

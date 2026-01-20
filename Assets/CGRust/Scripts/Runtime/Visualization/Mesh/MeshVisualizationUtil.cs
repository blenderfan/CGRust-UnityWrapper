using UnityEngine;

namespace CGRust.Runtime
{
    public static class MeshVisualizationUtil
    {
    
        /// <summary>
        /// Generates and adds vertex colors to a mesh for each triangle. This will only produce good results, if the mesh
        /// only contains hard-edges!
        /// </summary>
        public static void AddTriangleGradientColors(ref Mesh mesh, Gradient gradient)
        {
            var vertices = mesh.vertices;
            var colors = new Color[vertices.Length];

            var triangles = mesh.triangles;

            for (int i = 0; i < triangles.Length / 3; i++)
            {

                int a = triangles[i * 3 + 0];
                int b = triangles[i * 3 + 1];
                int c = triangles[i * 3 + 2];

                float rnd = Random.value;
                var color = gradient.Evaluate(rnd);

                colors[a] = color;
                colors[b] = color;
                colors[c] = color;
            }

            mesh.SetColors(colors);
        }

    }
}

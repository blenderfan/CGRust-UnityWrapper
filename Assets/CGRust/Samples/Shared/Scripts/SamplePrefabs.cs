using UnityEngine;

namespace CGRust.Samples
{
    [CreateAssetMenu(fileName = "SamplePrefabs", menuName = "Scriptable Objects/CGRust/Samples/SamplePrefabs")]
    public class SamplePrefabs : ScriptableObject
    {
        [SerializeField]
        private GameObject vertexPrefab;

        public GameObject Vertex => this.vertexPrefab;
    }
}

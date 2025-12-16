using UnityEngine;

namespace CGRust.Samples
{
    public class SampleScene : MonoBehaviour
    {
        [SerializeField]
        private SampleMaterials sampleMaterials;

        [SerializeField]
        private SamplePrefabs samplePrefabs;

        public SampleMaterials Materials => this.sampleMaterials;

        public SamplePrefabs Prefabs => this.samplePrefabs;
        

    }
}

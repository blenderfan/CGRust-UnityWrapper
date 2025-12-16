using UnityEngine;

namespace CGRust.Samples
{
    [CreateAssetMenu(fileName = "SampleShaders", menuName = "Scriptable Objects/CGRust/Samples/SampleShaders")]
    public class SampleMaterials : ScriptableObject
    {
        [SerializeField]
        private Material wireframeMaterial;

        [SerializeField]
        private Material pointMaterial;

        public Material WireframeMaterial => this.wireframeMaterial;

        public Material PointMaterial => this.pointMaterial;
    }
}

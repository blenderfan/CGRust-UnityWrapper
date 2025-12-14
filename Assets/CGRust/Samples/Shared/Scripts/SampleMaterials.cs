using UnityEngine;

namespace CGRust.Samples
{
    [CreateAssetMenu(fileName = "SampleShaders", menuName = "Scriptable Objects/CGRust/Samples/SampleShaders")]
    public class SampleMaterials : ScriptableObject
    {
        [SerializeField]
        private Material wireframeMaterial;


        public Material WireframeMaterial => this.wireframeMaterial;

    }
}

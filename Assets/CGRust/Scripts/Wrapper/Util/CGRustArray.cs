using NUnit.Framework;
using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace CGRust.Wrapper
{
    /// <summary>
    /// Helper class for handling arrays allocated in Rust. The Container can be used in
    /// Burst Jobs! However, disposing has to be done outside the job.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct CGRustArray<T> where T : unmanaged
    {

        public UnsafeList<T> data;

        public CGRustArray(UnsafeList<T> list) 
        {
            this.data = list;
        }

        [BurstDiscard]
        public unsafe void Dispose()
        {
            PArray<T> parr = new PArray<T>()
            {
                data = data.Ptr,
                length = data.Length
            };
            FreeMemory.FreeArray(ref parr);
            
        }
    }
}

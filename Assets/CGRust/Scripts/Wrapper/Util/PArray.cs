using System;
using System.Runtime.InteropServices;

namespace CGRust.Wrapper
{
    /// <summary>
    /// A helper struct for transferring data via P/Invoke from the Rust Library to managed C#
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PArray<T> where T : unmanaged
    {
        public T* data;
        public long length;
    }
}

using System;
using System.Runtime.InteropServices;

namespace CGRust.Wrapper
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PArray<T> where T : unmanaged
    {
        public T* data;
        public long length;
    }
}

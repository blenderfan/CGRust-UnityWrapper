using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using Unity.Burst;
using Unity.Mathematics;

namespace CGRust.Wrapper
{
    public static class FreeMemory
    {

        private static Dictionary<Type, MethodInfo> freeMethods = new Dictionary<Type, MethodInfo>();

        static FreeMemory() {

            freeMethods.Clear();
            var methods = typeof(FreeMemory).GetMethods(BindingFlags.Static | BindingFlags.Public);

            foreach (var method in methods)
            {
                if (!method.Name.StartsWith("cg_rust")) continue;
                var pars = method.GetParameters();
                if(pars.Length > 0)
                {
                    var par = pars[0];
                    var generics = par.ParameterType.GetGenericArguments();
                    if (generics.Length > 0)
                    {
                        var generic = generics[0];
                        freeMethods.Add(generic, method);
                    }
                }
            }
        }

        [BurstDiscard]
        public static void FreeArray<T>(ref PArray<T> arr) where T : unmanaged
        {
            var type = typeof(T);
            if (freeMethods.ContainsKey(type))
            {
                var method = freeMethods[type];
                method.Invoke(null, new object[] { arr });
            }
        }

        #region Import

        [DllImport("cg_rust", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void cg_rust_free_array_int(ref PArray<int> intArray);

        [DllImport("cg_rust", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void cg_rust_free_array_long(ref PArray<long> longArray);

        [DllImport("cg_rust", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void cg_rust_free_array_float2(ref PArray<float2> float2Array);

        #endregion

    }
}

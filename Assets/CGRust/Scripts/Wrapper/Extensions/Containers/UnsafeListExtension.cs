using Unity.Collections.LowLevel.Unsafe;

namespace CGRust.Wrapper
{

    public static class UnsafeListExtension
    {
        /// <summary>
        /// Reverses the UnsafeList
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="unsafeList"></param>
        public static void Reverse<T>(ref this UnsafeList<T> unsafeList) where T : unmanaged
        {
            var length = unsafeList.Length;
            for(int i = 0; i < length / 2; i++)
            {
                var tmp = unsafeList[i];
                unsafeList[i] = unsafeList[length - i - 1];
                unsafeList[length - i - 1] = tmp;
            }
        }

    }

}
using Unity.Mathematics;

namespace CGRust.Runtime
{
    public enum CardinalDirection : byte
    {
        X = 0,
        Y = 1,
        Z = 2,
    }

    public static class CardinalDirectionExtensions {

        public static int GetDirectionIndex(this CardinalDirection direction)
        {
            return (int)direction;
        }

        public static int2 GetOrthogonalIndices(this CardinalDirection direction)
        {
            switch (direction)
            {
                case CardinalDirection.X:
                    return new int2(1, 2);
                case CardinalDirection.Y:
                    return new int2(0, 2);
                case CardinalDirection.Z:
                    return new int2(0, 1);
            }
            return int2.zero;
        }

    }

}

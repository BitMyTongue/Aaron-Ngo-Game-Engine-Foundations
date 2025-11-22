using OpenTK.Mathematics;

namespace MidtermGame.Physics
{
    // AABB
    // Centered at some point, extends each direction half a unit
    public struct BoundingBox
    {
        public Vector3 Center;
        public Vector3 HalfSize;

        public BoundingBox(Vector3 center, Vector3 halfSize)
        {
            Center = center;
            HalfSize = halfSize;
        }

        // Each BoundingBox has a function to check for intersection against another BoundingBox
        public bool Intersects(BoundingBox other)
        {
            return
                MathF.Abs(Center.X - other.Center.X) <= (HalfSize.X + other.HalfSize.X) &&
                MathF.Abs(Center.Y - other.Center.Y) <= (HalfSize.Y + other.HalfSize.Y) &&
                MathF.Abs(Center.Z - other.Center.Z) <= (HalfSize.Z + other.HalfSize.Z);
        }
    }
}

using OpenTK.Mathematics;

namespace MidtermGame.Graphics
{
    public class Camera
    {
        public Vector3 Position { get; set; }
        public Vector3 Front { get; private set; } = -Vector3.UnitZ;
        public Vector3 Up { get; private set; } = Vector3.UnitY;
        public Vector3 Right => Vector3.Normalize(Vector3.Cross(Front, Up));
        
        public float AspectRatio;


        private float yaw = -90f;    // start facing -Z
        private float pitch = 0f;    // no vertical tilt
        private float fov = 60f;     // field of view

        private const float sensitivity = 0.1f;
        private const float minFov = 30f;
        private const float maxFov = 90f;

        public Camera(Vector3 startPosition)
        {
            Position = startPosition;
        }
        public Camera(Vector3 position, float aspectRatio)
        {
            Position = position;
            AspectRatio = aspectRatio;
            Front = -Vector3.UnitZ;
            Up = Vector3.UnitY;
        }

        // Move camera forward/back/left/right
        public void Move(Vector3 direction, float amount)
        {
            Position += direction * amount;
        }

        // Adjust camera rotation from mouse input
        public void Rotate(float deltaX, float deltaY)
        {
            yaw += deltaX * sensitivity;
            pitch -= deltaY * sensitivity;

            // Clamp pitch to avoid flipping
            if (pitch > 89f) pitch = 89f;
            if (pitch < -89f) pitch = -89f;

            Vector3 front;
            front.X = MathF.Cos(MathHelper.DegreesToRadians(yaw)) * MathF.Cos(MathHelper.DegreesToRadians(pitch));
            front.Y = MathF.Sin(MathHelper.DegreesToRadians(pitch));
            front.Z = MathF.Sin(MathHelper.DegreesToRadians(yaw)) * MathF.Cos(MathHelper.DegreesToRadians(pitch));
            Front = Vector3.Normalize(front);
        }

        // Adjust zoom
        public void AdjustFov(float offset)
        {
            fov -= offset;
            if (fov < minFov) fov = minFov;
            if (fov > maxFov) fov = maxFov;
        }

        // Create the view matrix
        public Matrix4 GetViewMatrix()
        {
            return Matrix4.LookAt(Position, Position + Front, Up);
        }

        // Create the projection matrix
        public Matrix4 GetProjectionMatrix(float aspectRatio)
        {
            return Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(fov), aspectRatio, 0.1f, 100f);
        }
    }
}

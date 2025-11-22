using MidtermGame.Graphics;
using MidtermGame.Physics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace MidtermGame
{
    public class Game : GameWindow
    {
        // GL
        private Shader shader;
        private Camera camera;

        // private Mesh cubeMesh;       // temp placeholder for assets

        // Game world objs and textures, positions
        private Mesh groundMesh;
        private Texture groundTexture;

        private Mesh lanternMesh;
        private Texture lanternTexture;
        private Vector3 lanternPosition = new Vector3(1.85f, -0.15f, 2f);       // Mannually adjusted

        private Mesh keyMesh;
        private Texture keyTexture;
        Vector3 keyPosition;

        private Mesh tableMesh;
        private Texture tableTexture;
        private Vector3 tablePosition = new Vector3(1.5f, -0.35f, 2f);          // Mannually adjusted

        private Mesh chairMesh;
        private Texture chairTexture;
        private Vector3 chair1Position = new Vector3(1.2f, -0.25f, 1.7f);       // Mannually adjusted
        private Vector3 chair2Position = new Vector3(1.8f, -0.25f, 1.7f);       // Mannually adjusted

        private Mesh spoonMesh;
        private Texture spoonTexture;
        private Vector3 spoonPosition = new Vector3(1.5f, -0.15f, 2.0f);        // Mannually adjusted

        private Mesh houseMesh;
        private Texture houseTexture;
        private Vector3 housePosition = new Vector3(0, 0.45f, -7.4f);           // Mannually adjusted
        
        private Mesh doorMesh;
        private Texture doorTexture;
        private Vector3 doorPosition = new Vector3(0.04f, -0.1f, -7.035f);      // Mannually adjusted

        // Collision
        private BoundingBox playerBox, tableBox, chair1Box, chair2Box, houseBox, doorBox;

        // World boundaries
        private const float worldMinX = -8f;
        private const float worldMaxX = 8f;
        private const float worldMinZ = -8f;
        private const float worldMaxZ = 8f;

        // Flags for game to function
        private bool lanternHeld = false;
        bool hasKey = false;
        bool nearKey = false;
        bool nearDoor = false;

        public Game()
            : base(GameWindowSettings.Default, NativeWindowSettings.Default)
        {

        }
        public Game(int width, int height)
            : base(
                new GameWindowSettings()
                {
                    UpdateFrequency = 60.0  // 60 fps
                },
                new NativeWindowSettings()
                {
                    Size = new Vector2i(width, height),
                    Title = "Aaron Midterm Game",
                    WindowBorder = WindowBorder.Resizable,
                    StartVisible = true,
                    StartFocused = true
                })
        {
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            // If I have time, replace with on-screen text?
            Console.WriteLine("Oh no! You seem to have dropped your keys. Guess we have to look for them.");
            Console.WriteLine("WASD for movement, E to interact with objects.");
            Console.WriteLine("Once you've found the key, interact with the door.");

            // Lock the cursor inside the window
            CursorState = CursorState.Grabbed;

            //GL.ClearColor(0.3f, 0.4f, 0.6f, 1.0f);    // day sky
            GL.ClearColor(0.15f, 0.2f, 0.3f, 1.0f);     // night sky
            GL.Enable(EnableCap.DepthTest);

            // Create camera
            camera = new Camera(new Vector3(0, 0, 3), Size.X / (float)Size.Y);

            // Player collision box
            playerBox = new BoundingBox(
                camera.Position,                        // box is centered at the camera position
                new Vector3(0.3f, 0.5f, 0.3f)           // represents half size of player hitbox
            );

            // Load shaders
            shader = new Shader("Shaders/vertex.glsl", "Shaders/fragment.glsl");

            //// Temp cube to get something rendered into the world
            // texture = new Texture("Assets/Textures/dirt.jpg");
            // cubeMesh = Mesh.CreateCube();

            // ------------------------------- //
            // Load game assets into the world //
            // ------------------------------- //

            // KEY
            keyMesh = Mesh.LoadFromObj("Assets/key.obj");
            keyTexture = new Texture("Assets/Textures/key.png");
            
            // position the key randomly everytime onLoad
            Random rng = new Random();
            float x = (float)(rng.NextDouble() * 10.0 - 5.0); // between -5 and +5
            float z = (float)(rng.NextDouble() * 10.0 - 5.0); // between -5 and +5
            keyPosition = new Vector3(x, 0.2f, z);

            // Grass
            groundMesh = Mesh.CreateGround();
            groundTexture = new Texture("Assets/Textures/grass.png");

            // Lantern
            lanternMesh = Mesh.LoadFromObj("Assets/lantern.obj");
            lanternTexture = new Texture("Assets/Textures/lantern.png");

            // Table
            tableMesh = Mesh.LoadFromObj("Assets/table.obj");
            tableTexture = new Texture("Assets/Textures/table.png");
            // Table collision box
            tableBox = new BoundingBox(
                tablePosition,
                new Vector3(0.4f, 0.5f, 0.12f)
            );

            // House
            houseMesh = Mesh.LoadFromObj("Assets/house.obj");
            houseTexture = new Texture("Assets/Textures/house.png");
            // House collision box
            houseBox = new BoundingBox(
                housePosition,
                new Vector3(1.05f, 1.0f, 0.5f)
            );

            // Door
            doorMesh = Mesh.LoadFromObj("Assets/door.obj");
            doorTexture = new Texture("Assets/Textures/door.png");
            // Door collision box
            doorBox = new BoundingBox(
                doorPosition,
                new Vector3(0.3f, 1f, 0.1f)
            );

            // Chair
            chairMesh = Mesh.LoadFromObj("Assets/Chair.obj");
            chairTexture = new Texture("Assets/Textures/chair.png");
            // Chair collision boxes
            chair1Box = new BoundingBox(
                chair1Position,
                new Vector3(0.1f, 0.5f, 0.1f)
            );
            chair2Box = new BoundingBox(
            chair2Position,
            new Vector3(0.1f, 0.5f, 0.1f)
            );

            // Spoon
            spoonMesh = Mesh.LoadFromObj("Assets/Spoon.obj");
            spoonTexture = new Texture("Assets/Textures/spoon.png");

            shader.Use();
            shader.SetInt("ourTexture", 0);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Matrix4 view = camera.GetViewMatrix();
            Matrix4 projection = camera.GetProjectionMatrix(Size.X / (float)Size.Y);


            //Activate Shader, send light/camera info
            shader.Use();
            shader.SetMatrix4("uView", view);
            shader.SetMatrix4("uProj", projection);
            shader.SetVector3("viewPos", camera.Position);
            shader.SetVector3("lightPos", lanternPosition);     // light follows the lantern
            shader.SetVector3("lightColor", new Vector3(1.5f, 1.5f, 1f));

            // Ground
            shader.Use();
            groundTexture.Use(TextureUnit.Texture0);
            shader.SetInt("ourTexture", 0);
            shader.SetVector3("objectColor", new Vector3(1f, 1f, 1f));
            shader.SetMatrix4("uModel", Matrix4.CreateTranslation(0f, -0.51f, 0f));
            groundMesh.Draw();

            // Key
            Matrix4 keyTransform =
                Matrix4.CreateScale(0.01f) *                 // way smaller than the blendr model
                Matrix4.CreateTranslation(
                    keyPosition.X,
                    -0.5f,                                   // Lower the key to the ground
                    keyPosition.Z
                );

            // if key is not held, it should be rendered. If it IS held, it shouldn't be rendered (picked up by player)
            if (!hasKey)
            {
                shader.Use();
                keyTexture.Use(TextureUnit.Texture0);
                shader.SetInt("ourTexture", 0);
                shader.SetVector3("objectColor", new Vector3(1f, 1f, 1f));
                shader.SetMatrix4("uModel", keyTransform);
                keyMesh.Draw();
            }

            // Lantern
            shader.Use();
            lanternTexture.Use(TextureUnit.Texture0);
            shader.SetInt("ourTexture", 0);
            shader.SetVector3("objectColor", new Vector3(1f, 1f, 1f));
            shader.SetMatrix4("uModel",
                Matrix4.CreateScale(0.1f) *
                Matrix4.CreateTranslation(lanternPosition)
            );
            lanternMesh.Draw();

            // Table
            shader.Use();
            tableTexture.Use(TextureUnit.Texture0);
            shader.SetInt("ourTexture", 0);
            shader.SetVector3("objectColor", new Vector3(1f, 1f, 1f));
            shader.SetMatrix4("uModel",
                Matrix4.CreateScale(0.2f) *
                Matrix4.CreateTranslation(tablePosition)
            );
            tableMesh.Draw();

            // House
            shader.Use();
            houseTexture.Use(TextureUnit.Texture0);
            shader.SetInt("ourTexture", 0);
            shader.SetVector3("objectColor", new Vector3(1f, 1f, 1f));
            shader.SetMatrix4("uModel",
                Matrix4.CreateScale(0.2f) *
                Matrix4.CreateTranslation(housePosition)
            );
            houseMesh.Draw();

            // Door
            shader.Use();
            doorTexture.Use(TextureUnit.Texture0);
            shader.SetInt("ourTexture", 0);
            shader.SetVector3("objectColor", new Vector3(1f, 1f, 1f));
            shader.SetMatrix4("uModel",
                Matrix4.CreateScale(0.2f) *
                Matrix4.CreateTranslation(doorPosition)
            );
            doorMesh.Draw();

            shader.Use();
            chairTexture.Use(TextureUnit.Texture0);
            shader.SetInt("ourTexture", 0);
            shader.SetVector3("objectColor", new Vector3(1f, 1f, 1f));
            shader.SetMatrix4("uModel",
                Matrix4.CreateScale(0.15f) *
                Matrix4.CreateTranslation(chair1Position)
            );
            chairMesh.Draw();

            shader.Use();
            chairTexture.Use(TextureUnit.Texture0);
            shader.SetInt("ourTexture", 0);
            shader.SetVector3("objectColor", new Vector3(1f, 1f, 1f));
            shader.SetMatrix4("uModel",
                Matrix4.CreateScale(0.15f) *
                Matrix4.CreateTranslation(chair2Position)
            );
            chairMesh.Draw();

            shader.Use();
            spoonTexture.Use(TextureUnit.Texture0);
            shader.SetInt("ourTexture", 0);
            shader.SetVector3("objectColor", new Vector3(1f, 1f, 1f));
            shader.SetMatrix4("uModel",
                Matrix4.CreateScale(0.01f) *
                Matrix4.CreateTranslation(spoonPosition)
            );
            spoonMesh.Draw();

            SwapBuffers();
        }

        private Vector2 _lastMousePos;
        private bool _firstMove = true;

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            // ESC closes the game
            if (KeyboardState.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.Escape))
                Close();

            float moveSpeed = 2.5f * (float)e.Time;

            // Prevents player from moving up or down (Y-axis) with just WASD
            Vector3 flatFront = new Vector3(camera.Front.X, 0f, camera.Front.Z);
            flatFront = Vector3.Normalize(flatFront);
            Vector3 flatRight = Vector3.Normalize(Vector3.Cross(flatFront, camera.Up));

            // WASD movement
            if (KeyboardState.IsKeyDown(Keys.W))
                TryMove(flatFront * moveSpeed);
            if (KeyboardState.IsKeyDown(Keys.S))
                TryMove(-flatFront * moveSpeed);
            if (KeyboardState.IsKeyDown(Keys.A))
                TryMove(-flatRight * moveSpeed);
            if (KeyboardState.IsKeyDown(Keys.D))
                TryMove(flatRight * moveSpeed);

            // Distance for interacting with objects
            float keyDist = Vector3.Distance(camera.Position, keyPosition);
            float doorDist = Vector3.Distance(camera.Position, doorPosition);
            float lampDist = Vector3.Distance(camera.Position, lanternPosition);

            bool nearKey = keyDist < 1.0f;
            bool nearDoor = doorDist < 1.0f;
            bool nearLantern = lampDist < 1.0f;

            // ------------------- //
            // Interact Button (E) //
            // ------------------- //

            if (KeyboardState.IsKeyPressed(OpenTK.Windowing.GraphicsLibraryFramework.Keys.E))
            {
                // Pick up Lantern
                if (!lanternHeld && nearLantern)    // if not already held & near
                {
                    lanternHeld = true;             // pick up
                }

                // Pick up Key
                if (!hasKey && nearKey)             // if not already held & near
                {
                    hasKey = true;                  // pick up
                }
                
                // Open Door
                if (hasKey && nearDoor)             // if holding key and near door
                {
                    Console.WriteLine("You win!!! idk LOL");    // win
                    Close();
                }
            }

            // Lantern follows in front of camera if held
            if (lanternHeld)
            {
                lanternPosition = camera.Position + camera.Front * 0.5f + new Vector3(0f, -0.2f, 0f);
            }
        }

        protected override void OnMouseMove(OpenTK.Windowing.Common.MouseMoveEventArgs e)
        {
            base.OnMouseMove(e);

            if (_firstMove)
            {
                _lastMousePos = new Vector2(e.X, e.Y);
                _firstMove = false;
                return;
            }

            float deltaX = e.X - _lastMousePos.X;
            float deltaY = e.Y - _lastMousePos.Y;
            _lastMousePos = new Vector2(e.X, e.Y);

            camera.Rotate(deltaX, deltaY);
        }
        
        // Re-grab the mouse cursor if alt-tabbed and tabbed back in
        protected override void OnFocusedChanged(FocusedChangedEventArgs e)
        {
            base.OnFocusedChanged(e);

            if (e.IsFocused)
                CursorState = CursorState.Grabbed;
        }

        protected override void OnMouseWheel(OpenTK.Windowing.Common.MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);
            camera.AdjustFov(e.OffsetY);
        }
        private void TryMove(Vector3 movement)
        {
            // Calculate where camera wants to move
            Vector3 newPosition = camera.Position + movement;

            // Create bounding box at the new position to check for collisions
            BoundingBox newPlayerBox = new BoundingBox(
                newPosition,
                playerBox.HalfSize
            );

            // check for collisions
            if (newPlayerBox.Intersects(tableBox)) return;
            if (newPlayerBox.Intersects(chair1Box)) return;
            if (newPlayerBox.Intersects(chair2Box)) return;
            if (newPlayerBox.Intersects(houseBox)) return;
            if (newPlayerBox.Intersects(doorBox)) return;

            // World boundary collision
            if (newPosition.X < worldMinX || newPosition.X > worldMaxX)
                return;

            if (newPosition.Z < worldMinZ || newPosition.Z > worldMaxZ)
                return;

            // If we're here in the code, there are no collisions, so we allow the movement
            camera.Position = newPosition;
            playerBox.Center = newPosition;
        }

    }
}

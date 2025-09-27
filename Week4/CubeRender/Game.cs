using System;
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace CubeDemo
{
    public class Game : GameWindow
    {
        private int vertexBufferHandle;
        private int shaderProgramHandle;
        private int vertexArrayHandle;
        private int elementBufferHandle;
        private int textureHandle;              // Adding texture handle for Assignment 4

        private int modelLoc, viewLoc, projLoc;

        private float rotation = 0f;

        // Position (x,y,z) and position on texture (u, v)
        // 4 corners x 6 faces of the cube
        // Basically mapping every corner of the cube to an associating corner of the texture
        private float[] vertices =
        {
            // Position            (u, v)                                       indices
            
            // Front face
            -0.5f, -0.5f,  0.5f,    0f, 0f,     // bottom left of texture       0
            0.5f, -0.5f,  0.5f,     1f, 0f,     // bottom right of texture      1
            0.5f,  0.5f,  0.5f,     1f, 1f,     // top right of texture         2
            -0.5f,  0.5f,  0.5f,    0f, 1f,     // top left of texture          3

            // Back face
            -0.5f, -0.5f, -0.5f,    1f, 0f,     // bottom right of texture      4
            0.5f, -0.5f, -0.5f,     0f, 0f,     // bottom left of texture       5
            0.5f,  0.5f, -0.5f,     0f, 1f,     // top left of texture          6
            -0.5f,  0.5f, -0.5f,    1f, 1f,     // top right of texture         7

            // Left face
            -0.5f, -0.5f, -0.5f,    0f, 0f,     // bottom left of texture       8
            -0.5f, -0.5f,  0.5f,    1f, 0f,     // bottom right of texture      9
            -0.5f,  0.5f,  0.5f,    1f, 1f,     // top right of texture         10
            -0.5f,  0.5f, -0.5f,    0f, 1f,     // top left of texture          11

            // Right face
            0.5f, -0.5f, -0.5f,     1f, 0f,     // bottom right of texture      12
            0.5f, -0.5f,  0.5f,     0f, 0f,     // bottom left of texture       13
            0.5f,  0.5f,  0.5f,     0f, 1f,     // top left of texture          14
            0.5f,  0.5f, -0.5f,     1f, 1f,     // top right of texture         15

            // Top face
            -0.5f,  0.5f,  0.5f,    0f, 0f,     // bottom left of texture       16
            0.5f,  0.5f,  0.5f,     1f, 0f,     // bottom right of texture      17
            0.5f,  0.5f, -0.5f,     1f, 1f,     // top right of texture         18
            -0.5f,  0.5f, -0.5f,    0f, 1f,     // top left of texture          19

            // Bottom face
            -0.5f, -0.5f,  0.5f,    0f, 1f,     // top left of texture          20
            0.5f, -0.5f,  0.5f,     1f, 1f,     // top right of texture         21
            0.5f, -0.5f, -0.5f,     1f, 0f,     // bottom right of texture      22
            -0.5f, -0.5f, -0.5f,    0f, 0f      // bottom left of texture       23
        };

        // This tells OpenGL how to connect the above vertices
        // 12 triangles, 2 per face
        private int[] indices =
        {
            // Front face
            0, 1, 2,
            2, 3, 0,

            // Back face
            4, 5, 6,
            6, 7, 4,

            // Left face
            8, 9,10,
            10,11, 8,

            // Right face
            12,13,14,
            14,15,12,

            // Top face
            16,17,18,
            18,19,16,

            // Bottom face
            20,21,22,
            22,23,20
        };


        public Game()
            : base(GameWindowSettings.Default, NativeWindowSettings.Default)
        {
            this.Size = new Vector2i(1280, 768);
            this.CenterWindow(this.Size);
        }
        protected override void OnResize(ResizeEventArgs e)
        {
            GL.Viewport(0, 0, e.Width, e.Height);
            base.OnResize(e);
        }

        private readonly string vertexShaderCode = @"
            #version 330 core
            layout (location = 0) in vec3 aPos;
            layout (location = 1) in vec2 aTexCoord;

            out vec2 TexCoord;

            uniform mat4 uModel;
            uniform mat4 uView;
            uniform mat4 uProj;

            void main()
            {
                TexCoord = aTexCoord;
                gl_Position = uProj * uView * uModel * vec4(aPos, 1.0);
            }
        ";

        private readonly string fragmentShaderCode = @"
            #version 330 core
            out vec4 FragColor;
            in vec2 TexCoord;

            uniform sampler2D ourTexture;

            void main()
            {
                FragColor = texture(ourTexture, TexCoord);
            }
        ";

        protected override void OnLoad()
        {
            base.OnLoad();

            // Background colour
            GL.ClearColor(new Color4(0.5f, 0.7f, 0.8f, 1f));

            // Make cube look not flat
            GL.Enable(EnableCap.DepthTest);

            vertexBufferHandle = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferHandle);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            elementBufferHandle = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferHandle);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);

            vertexArrayHandle = GL.GenVertexArray();
            GL.BindVertexArray(vertexArrayHandle);

            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferHandle);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferHandle);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);

            GL.BindVertexArray(0); int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, vertexShaderCode);
            GL.CompileShader(vertexShader);

            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, fragmentShaderCode);
            GL.CompileShader(fragmentShader);

            shaderProgramHandle = GL.CreateProgram();
            GL.AttachShader(shaderProgramHandle, vertexShader);
            GL.AttachShader(shaderProgramHandle, fragmentShader);
            GL.LinkProgram(shaderProgramHandle);

            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);

            // Get uniform locations
            modelLoc = GL.GetUniformLocation(shaderProgramHandle, "uModel");
            viewLoc = GL.GetUniformLocation(shaderProgramHandle, "uView");
            projLoc = GL.GetUniformLocation(shaderProgramHandle, "uProj");

            // Load the dirt texture
            textureHandle = LoadTexture("Assets/dirt.jpg");

            GL.UseProgram(shaderProgramHandle);
            int texLoc = GL.GetUniformLocation(shaderProgramHandle, "ourTexture");
            GL.Uniform1(texLoc, 0);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            rotation += (float)args.Time;
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.UseProgram(shaderProgramHandle);

            GL.BindVertexArray(vertexArrayHandle);

            // Create transformation matrices
            Matrix4 model = Matrix4.CreateRotationY(rotation) * Matrix4.CreateRotationX(rotation * 0.5f);

            // View matrix (camera looking at origin)
            Matrix4 view = Matrix4.LookAt(new Vector3(0, 0, 5), Vector3.Zero, Vector3.UnitY);

            // Projection matrix (perspective)
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(
                MathHelper.DegreesToRadians(60f),
                (float)Size.X / Size.Y,
                0.1f,
                100f
            );

            // Send model matrix to shader
            GL.UniformMatrix4(modelLoc, false, ref model);
            GL.UniformMatrix4(viewLoc, false, ref view);
            GL.UniformMatrix4(projLoc, false, ref projection);

            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, textureHandle);

            // Draw cube using the indices
            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);

            GL.BindVertexArray(0);
            SwapBuffers();
        }

        protected override void OnUnload()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(vertexBufferHandle);

            GL.BindVertexArray(0);
            GL.DeleteVertexArray(vertexArrayHandle);

            GL.UseProgram(0);
            GL.DeleteProgram(shaderProgramHandle);

            base.OnUnload();
        }

        private int LoadTexture(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException($"Could not find texture file: {path}");

            int texId = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, texId);

            // Wrapping and filtering
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)All.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)All.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)All.LinearMipmapLinear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)All.Linear);

            using (Bitmap bmp = new Bitmap(path))
            {
                bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);

                var data = bmp.LockBits(
                    new Rectangle(0, 0, bmp.Width, bmp.Height),
                    ImageLockMode.ReadOnly,
                    System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                GL.TexImage2D(TextureTarget.Texture2D,
                    0,
                    PixelInternalFormat.Rgba,
                    data.Width,
                    data.Height,
                    0,
                    OpenTK.Graphics.OpenGL.PixelFormat.Bgra,
                    PixelType.UnsignedByte,
                    data.Scan0);

                bmp.UnlockBits(data);
            }

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            return texId;
        }


        private void CheckShaderCompile(int shaderHandle, string shaderName)
        {
            GL.GetShader(shaderHandle, ShaderParameter.CompileStatus, out int success);
            if (success == 0)
            {
                string infoLog = GL.GetShaderInfoLog(shaderHandle);
                Console.WriteLine($"Error compiling {shaderName}: {infoLog}");
            }
        }
    }
}

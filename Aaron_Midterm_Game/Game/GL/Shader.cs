using OpenTK.Graphics.OpenGL4;
using System.IO;

namespace MidtermGame.Graphics
{
    public class Shader
    {
        public int Handle { get; private set; }

        public Shader(string vertexPath, string fragmentPath)
        {
            string vertexCode = File.ReadAllText(vertexPath);
            string fragmentCode = File.ReadAllText(fragmentPath);

            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, vertexCode);
            GL.CompileShader(vertexShader);

            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, fragmentCode);
            GL.CompileShader(fragmentShader);

            Handle = GL.CreateProgram();
            GL.AttachShader(Handle, vertexShader);
            GL.AttachShader(Handle, fragmentShader);
            GL.LinkProgram(Handle);

            GL.DetachShader(Handle, vertexShader);
            GL.DetachShader(Handle, fragmentShader);
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);
        }

        public void Use() => GL.UseProgram(Handle);

        public void SetMatrix4(string name, OpenTK.Mathematics.Matrix4 matrix)
        {
            int loc = GL.GetUniformLocation(Handle, name);
            GL.UniformMatrix4(loc, false, ref matrix);
        }

        public void SetVector3(string name, OpenTK.Mathematics.Vector3 vector)
        {
            int loc = GL.GetUniformLocation(Handle, name);
            GL.Uniform3(loc, vector);
        }

        public void SetInt(string name, int value)
        {
            int loc = GL.GetUniformLocation(Handle, name);
            GL.Uniform1(loc, value);
        }
    }
}

using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.Globalization;

namespace MidtermGame.Graphics
{
    public class Mesh
    {
        private readonly int _vao;
        private readonly int _vbo;
        private readonly int _ebo;
        private readonly int _indexCount;

        public Mesh(float[] vertices, int[] indices)
        {
            _indexCount = indices.Length;

            _vao = GL.GenVertexArray();
            _vbo = GL.GenBuffer();
            _ebo = GL.GenBuffer();

            GL.BindVertexArray(_vao);

            // Vertex data to GPU
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            // Index data (Defines how traingles are made)
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _ebo);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(int), indices, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);

            GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 5 * sizeof(float));
            GL.EnableVertexAttribArray(2);

            GL.BindVertexArray(0);
        }

        public void Draw()
        {
            GL.BindVertexArray(_vao);
            GL.DrawElements(PrimitiveType.Triangles, _indexCount, DrawElementsType.UnsignedInt, 0);
        }
        public static Mesh CreateGround()
        {
            float[] vertices = {
                // position         uv          normal
                -10f, 0f, -10f,     0f, 10f,    0f, 1f, 0f,
                 10f, 0f, -10f,     10f, 10f,   0f, 1f, 0f,
                 10f, 0f, 10f,      10f, 0f,    0f, 1f, 0f,
                -10f, 0f, 10f,      0f, 0f,     0f, 1f, 0f
            };

            int[] indices = { 0, 1, 2, 2, 3, 0 };

            return new Mesh(vertices, indices);
        }

        public static Mesh CreateCube()
        {
            float[] vertices =
            {
                // position         uv      normal
                // front
                -0.5f,-0.5f, 0.5f,  0f,0f,  0f,0f,1f,
                 0.5f,-0.5f, 0.5f,  1f,0f,  0f,0f,1f,
                 0.5f, 0.5f, 0.5f,  1f,1f,  0f,0f,1f,
                -0.5f, 0.5f, 0.5f,  0f,1f,  0f,0f,1f,

                // back
                -0.5f,-0.5f,-0.5f,  1f,0f,  0f,0f,-1f,
                 0.5f,-0.5f,-0.5f,  0f,0f,  0f,0f,-1f,
                 0.5f, 0.5f,-0.5f,  0f,1f,  0f,0f,-1f,
                -0.5f, 0.5f,-0.5f,  1f,1f,  0f,0f,-1f,

                // left
                -0.5f,-0.5f,-0.5f,  0f,0f, -1f,0f,0f,
                -0.5f,-0.5f, 0.5f,  1f,0f, -1f,0f,0f,
                -0.5f, 0.5f, 0.5f,  1f,1f, -1f,0f,0f,
                -0.5f, 0.5f,-0.5f,  0f,1f, -1f,0f,0f,

                // right
                 0.5f,-0.5f,-0.5f,  1f,0f,  1f,0f,0f,
                 0.5f,-0.5f, 0.5f,  0f,0f,  1f,0f,0f,
                 0.5f, 0.5f, 0.5f,  0f,1f,  1f,0f,0f,
                 0.5f, 0.5f,-0.5f,  1f,1f,  1f,0f,0f,

                // top
                -0.5f, 0.5f, 0.5f,  0f,0f,  0f,1f,0f,
                 0.5f, 0.5f, 0.5f,  1f,0f,  0f,1f,0f,
                 0.5f, 0.5f,-0.5f,  1f,1f,  0f,1f,0f,
                -0.5f, 0.5f,-0.5f,  0f,1f,  0f,1f,0f,

                // top
                -0.5f,-0.5f, 0.5f,  0f,1f,  0f,-1f,0f,
                 0.5f,-0.5f, 0.5f,  1f,1f,  0f,-1f,0f,
                 0.5f,-0.5f,-0.5f,  1f,0f,  0f,-1f,0f,
                -0.5f,-0.5f,-0.5f,  0f,0f,  0f,-1f,0f
            };

            int[] indices =
            {
                0,1,2,  2,3,0,      
                4,5,6,  6,7,4,      
                8,9,10, 10,11,8,    
                12,13,14, 14,15,12, 
                16,17,18, 18,19,16, 
                20,21,22, 22,23,20
            };

            return new Mesh(vertices, indices);
        }

        //Load data from obj file to create a mesh
        public static Mesh LoadFromObj(string path)
        {
            List<Vector3> positions = new();
            List<Vector2> texCoords = new();
            List<Vector3> normals = new();
            List<float> vertexData = new();
            List<int> indices = new();
            Dictionary<string, int> vertexCache = new();
            int nextIndex = 0;

            foreach (var line in File.ReadLines(path))
            {
                // Vertex pos
                if (line.StartsWith("v "))                                              
                {
                    var p = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    positions.Add(new Vector3(
                        float.Parse(p[1], CultureInfo.InvariantCulture),
                        float.Parse(p[2], CultureInfo.InvariantCulture),
                        float.Parse(p[3], CultureInfo.InvariantCulture)
                    ));
                }
                // Texture Coordinates
                else if (line.StartsWith("vt "))
                {
                    var p = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    texCoords.Add(new Vector2(
                        float.Parse(p[1], CultureInfo.InvariantCulture),
                        float.Parse(p[2], CultureInfo.InvariantCulture)
                    ));
                }
                // Normal Vector
                else if (line.StartsWith("vn "))
                {
                    var p = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    normals.Add(new Vector3(
                        float.Parse(p[1], CultureInfo.InvariantCulture),
                        float.Parse(p[2], CultureInfo.InvariantCulture),
                        float.Parse(p[3], CultureInfo.InvariantCulture)
                    ));
                }
                // Face
                else if (line.StartsWith("f "))
                {
                    var verts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries)[1..];

                    foreach (var v in verts)
                    {
                        if (!vertexCache.TryGetValue(v, out int index))
                        {
                            var ids = v.Split('/');
                            int vi = int.Parse(ids[0]) - 1;
                            int ti = ids.Length > 1 && ids[1] != "" ? int.Parse(ids[1]) - 1 : 0;
                            int ni = ids.Length > 2 && ids[2] != "" ? int.Parse(ids[2]) - 1 : 0;

                            var pos = positions[vi];
                            var uv = texCoords.Count > ti ? texCoords[ti] : Vector2.Zero;
                            var norm = normals.Count > ni ? normals[ni] : Vector3.UnitY;

                            vertexData.AddRange(new float[] {
                                pos.X, pos.Y, pos.Z,
                                uv.X, uv.Y,
                                norm.X, norm.Y, norm.Z
                            });

                            index = nextIndex++;
                            vertexCache[v] = index;
                        }
                        indices.Add(index);
                    }
                }
            }
            return new Mesh(vertexData.ToArray(), indices.ToArray());
        }
    }
}

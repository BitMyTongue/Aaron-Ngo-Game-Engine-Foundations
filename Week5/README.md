# Fourth Assignment - Apply Texture to Cube

## Libraries Used
- OpenTK.Mathematics for vector and matrix operations
- OpenTK.Graphics for rendering with OpenGL
- System.Drawing.Common for loading img texture

## How the Cube was Rendered

For each vertex of every face of the cube, a (u, v) was assigned mapping to a corner of the texture. 

For example, this is the first vertex of the front face (index 0):
            // Position             (u, v)                                      indices
                        
            -0.5f, -0.5f,  0.5f,    0f, 0f,     // bottom left of texture       0

We then used indicies to specify to OpenGL how the vertices connect into triangles to create our cube shape. 

For example, this creates our front face. We connect vertex 0, 1, 2 to make one triangle, and 2, 3, 0 to create another: 
            
            // Front face
            0, 1, 2,
            2, 3, 0,

The vertex shader now passes texture coordinates to the fragment shader:
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

I the LoadTexture(string path) method found in the example repository to load my dirt.jpg texture.

## How to Run
1. Open CubeRender.sln in Visual Studio
2. Build and Run the project

## Output: 
The a 3D cube with minecraft's dirt block texture rotating around the X and Y axis

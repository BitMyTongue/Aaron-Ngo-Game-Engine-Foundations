# Third Assignment - Rotating Cube

## Libraries Used
- OpenTK.Mathematics for vector and matrix operations

## How the Cube was Rendered

8 vertices, each with an x, y, and z position were assigned different colours. 

For example, this is the first vertex (index 0):
// Position             Colour
-0.5f, -0.5f, -0.5f,    1f, 0f, 0f, // red, 0

We then used indicies to specify to OpenGL how the vertices connect into triangles to create our cube shape. 

For example, this creates our first face. We connect vertex 4, 5, 6 to make one triangle, and 6, 7, 4 to create another: 
4,5,6, 6,7,4,   // front face

This line compares "potential pixels" when they overlap. The pixel further away (z value) is thrown away and the one closer is displayed:
GL.Enable(EnableCap.DepthTest);

This line groups the vertices as described by our indices array and renders them on screen as triangles, forming the cube:
GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);

## How to Run
1. Open CubeRender.sln in Visual Studio
2. Build and Run the project

## Output: 
Coloured 3D cube that rotates around the X and Y axis

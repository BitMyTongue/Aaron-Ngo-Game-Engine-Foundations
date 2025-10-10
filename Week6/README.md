# Sixth Assignment - Implement a Controllable 3D Camera in OpenTK

## Libraries Used
- OpenTK.Mathematics for vector and matrix operations
- OpenTK.Graphics for rendering with OpenGL
- System.Drawing.Common for loading img texture

## Addition from Assignment 5:

For this assignment, I implemented FPS-style camera movement for the minecraft 
dirt block from assignment 5. The WASD keys now move the camera instead of the 
lighting. Space moves the camera up, Left-Shift moves the camera down along the 
Y-Axis. The scroll wheel now affects the camera field of view to allow zooming 
in/out. This now simulates a first person perspective, as if you are moving in 
relation to the cube position.

## How to Run
1. Open CubeRender.sln in Visual Studio
2. Build and Run the project

## Controls
WASD to move camera position Foward, Backward, Left, Right. (+/- X or Z Axis)
Space to move camera up, Left-Shift to move camera down. (+/- Y axis)
Mouse movement to rotate camera.
Scroll Wheel adjusts the field of view. (zoom-in/zoom-out)

## Output: 
The Minecraft dirt block is now rendered in a 3D space, with a controllable 
first-person style camera.

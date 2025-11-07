# Seventh Assignment - Advanced Sprite Animation in OpenTK

## Libraries Used
- OpenTK.Mathematics for vector and matrix operations
- OpenTK.Graphics for rendering with OpenGL

## Newly Implemented
Holding shift while walking sprints (speeds up walking animation by 1.5x)
Press up to jump

## State Machine
On every frame, I have code to check, and then set the player's state 
depending on what the player is doing. 

If player's position is above ground, they're jumping.
If a direction isn't pressed, they're idle.
Otherwise, if a direction is pressed, and shift is held down, they're running.
If shift isn't held down, they're walking.

## How to Run
1. Open SpriteGameOpenTk.sln in Visual Studio
2. Build and Run the project

## Controls
Left or Right to walk
Up to jump
Hold Shift while walking to sprint

## Output: 
The Minecraft dirt block is now rendered in a 3D space, with a controllable 
first-person style camera.
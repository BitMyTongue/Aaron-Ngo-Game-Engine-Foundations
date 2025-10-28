# Midterm Assignment - Find Your House Keys Game

## Libraries Used
- OpenTK.Mathematics for vector and matrix operations
- OpenTK.Graphics for rendering with OpenGL
- System.Drawing.Common for loading img texture

## Description
The day this project was announced, when I got home, I'd dropped the keys to my unit somewhere. 
I had to retrace my steps and search for them. Luckily enough, they weren't too far from my 
appartment building. This is where the idea for my project came from.

In this mini game, it's dark out, and you've dropped your keys somewhere. Conveniently enough, 
you're provided a lantern to help you search for them. Once you find the keys, pick them up 
and head on home!

## Feature List
First Person Movement
Interactive Objects (Lamntern, Keys, Door)
Dynamic Lighting
Custom 3D Models made in blender. (My .blend files are provided on BlackBoard if interested)
Key Spawns in random locations.

## Known Issues
If I have time, I'd like to come back to this project in the future to implement some improvements.

1. There's a good chance the key spawns too close to the player, making the search way too easy.
2. Collision is not yet implemented, meaning the player can walk through the table, house, and 
even outside the rendered ground.
3. I'm not sure how to fix the UV Wrapping for my objects, some of the textures are oddly stretched.
4. Instructions are not found anywhere in the game. I'd like to have the controls and some basic instructions 
rendered on the screen as 2D text.

## How to Run
1. Open Game.sln in Visual Studio
2. Install OpenTK Package, and System.Drawing.Common.
3. Build and Run the project.

## Controls
WASD for movement
Mouse-movement to look around
Scroll-Wheel to change FOV
E to interact with objects (Lantern, Keys, Door) 

## Output: 
An interactive 3D Environment with custom objects modeled in blender.
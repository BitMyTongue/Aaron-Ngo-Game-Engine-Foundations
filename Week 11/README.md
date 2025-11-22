# Assignment 9 - 3D Object Collision Detection in OpenTK

## Libraries Used
- OpenTK.Mathematics for vector and matrix operations
- OpenTK.Graphics for rendering with OpenGL
- System.Drawing.Common for loading img texture

## Description
In this mini game, it's dark out, and you've dropped your keys somewhere. Conveniently enough, 
you're provided a lantern to help you search for them. Once you find the keys, pick them up 
and head on home!

*** For Assignment 9, I'm adding collision to my midterm game, as that was its biggest issue.
This update features two new assets modeled in blender. Chair for more collision objects, and spoon.
The spoon was modeled originally because I had planned on using it in a different game, which 
isn't currently in development, and I didnt want it to go to waste.

## Feature List
Midterm Submission:

First Person Movement

Interactive Objects (Lamntern, Keys, Door)

Dynamic Lighting

Custom 3D Models made in blender. (My .blend files are provided on BlackBoard if interested)

Key Spawns in random locations.


Assignment 9 Update: 

Collision against objects + World Borders

## Known Issues
If I have time, I'd like to come back to this project in the future to implement some improvements.

1. There's a good chance the key spawns too close to the player, making the search way too easy.
2. I'm not sure how to fix the UV Wrapping for my objects, some of the textures are oddly stretched.
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

## Textures used:

- Grass005 from AmbientCG for ground texture
    https://ambientcg.com/a/Grass005

- Wood058 from AmbientCG for table texture
    https://ambientcg.com/a/Wood058

- Wood066 from AmbientCG for door texture
    https://ambientcg.com/a/Wood066

-  Metal048C from AmbientCG for key texture
    https://ambientcg.com/a/Metal048C

- Metal046B from AmbientCG for lantern texture
    https://ambientcg.com/a/Metal046B

- Plaster001 from AmbientCG for house texture
    https://ambientcg.com/a/Plaster001

- Metal009 from AmbientCG for spoon texture
    https://ambientcg.com/a/Metal009

- Wood049 from AmbientCG for chair texture
    https://ambientcg.com/a/Wood049

## Output: 
An interactive 3D Environment with custom objects modeled in blender.
# Second Assignment - Vector and Matrix Operations

## Libraries Used
- OpenTK.Mathematics for vector and matrix operations

## Features
- Vector addition, subtraction, dot and cross product
- Matrix Identity, Scaling, Rotation (Z-axis 45degree), and Multiplication (scaling * rotation)
- Applying Transformation to a Vector and displaying result

## How to Run
1. Open WindowEngine.sln in Visual Studio
2. Build and Run the project

## Output: 
Vector Operations
Vector 1: (1, 2, 3)
Vector 2: (4, 5, 6)
Vector 1 + Vector 2 = (5, 7, 9)
Vector 1 - Vector 2 = (-3, -3, -3)
Dot Product (Vector 1, Vector 2) = 32
Cross Product (Vector 1, Vector 2) = (-3, 6, -3)

MAtrix Operations
Identity =
(1, 0, 0, 0)
(0, 1, 0, 0)
(0, 0, 1, 0)
(0, 0, 0, 1)

Scale =
(2, 0, 0, 0)
(0, 2, 0, 0)
(0, 0, 2, 0)
(0, 0, 0, 1)

Rotation =
(0.70710677, 0.70710677, 0, 0)
(-0.70710677, 0.70710677, 0, 0)
(0, 0, 1, 0)
(0, 0, 0, 1)

Multiply =
(1.4142135, 1.4142135, 0, 0)
(-1.4142135, 1.4142135, 0, 0)
(0, 0, 2, 0)
(0, 0, 0, 1)

Starting Point: (1, 0, 0, 1)
Transformed Point: (1.4142135, 1.4142135, 0, 1)

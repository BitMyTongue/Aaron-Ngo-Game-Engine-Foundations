using System;
using System.Diagnostics;
using OpenTK.Mathematics;

class Program
{
    static void Main()
    {
        // ----------------- //
        // Vector Operations //
        // ----------------- //

        // Create two 3D vectors
        Vector3 vector1 = new Vector3(1, 2, 3);
        Vector3 vector2 = new Vector3(4, 5, 6);


        Vector3 add = vector1 + vector2; // Addition
        Vector3 subtract = vector1 - vector2; // Subtraction
        float dot = Vector3.Dot(vector1, vector2); // Dot Product
        Vector3 cross = Vector3.Cross(vector1, vector2); // Cross Product

        // Printing results
        Console.WriteLine("Vector Operations");
        Console.WriteLine($"Vector 1: {vector1}\nVector 2: {vector2}");
        Console.WriteLine($"Vector 1 + Vector 2 = {add}");
        Console.WriteLine($"Vector 1 - Vector 2 = {subtract}");
        Console.WriteLine($"Dot Product (Vector 1, Vector 2) = {dot}");
        Console.WriteLine($"Cross Product (Vector 1, Vector 2) = {cross}\n");

        // ----------------- //
        // Matrix Operations //
        // ----------------- //

        Matrix4 identity = Matrix4.Identity; // Neutral 4x4 Matrix
        Matrix4 scale = Matrix4.CreateScale(2f); // Scale by 2
        Matrix4 rotation = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(45f)); // 45 degrees Rotation Around Z-axis
        Matrix4 transform = scale * rotation; // Matrix Multiplication

        // Printing results
        Console.WriteLine("MAtrix Operations");
        Console.WriteLine($"Identity = \n{identity}\n");
        Console.WriteLine($"Scale = \n{scale}\n");
        Console.WriteLine($"Rotation = \n{rotation}\n");
        Console.WriteLine($"Multiply = \n{transform}\n");

        // Transforming a point
        Vector4 point = new Vector4(1, 0, 0, 1);
        Vector4 transformedPoint = point * transform;

        Console.WriteLine($"Starting Point: {point}\nTransformed Point: {transformedPoint}");
    }

}
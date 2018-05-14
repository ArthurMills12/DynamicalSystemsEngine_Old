using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace Calculator
{
    static partial class Mathematics
    {
        //given a translation, rotation, and scale, create a transformation matrix that does all of these things.
        public static Matrix4 CreateTransformationMatrix(Vector3 translation, float xAxisRotationDegrees, float yAxisRotationDegrees, float zAxisRotationDegrees, float scale)
        {
            Matrix4 r1 = Matrix4.CreateRotationX(xAxisRotationDegrees);
            Matrix4 r2 = Matrix4.CreateRotationY(yAxisRotationDegrees);
            Matrix4 r3 = Matrix4.CreateRotationZ(zAxisRotationDegrees);
            Matrix4 t1 = Matrix4.CreateTranslation(translation);
            Matrix4 s1 = Matrix4.CreateScale(scale);

            Matrix4 transformationMatrix = r1 * r2 * r3 * t1 * s1;

            return transformationMatrix;
        }
        public static Matrix4 CreateTransformationMatrix(Vector3 translation, float xScale, float yScale)
        {
            Matrix4 t1 = Matrix4.CreateTranslation(translation);
            Matrix4 s1 = Matrix4.CreateScale(xScale, yScale, 0);

            Matrix4 transformationMatrix = s1 * t1;//Matrix4.Transpose(Matrix4.Transpose(t1) * s1);

            return transformationMatrix;
        }
        public static Matrix4d CreateTransformationMatrix(Vector3d translation, double xScale, double yScale)
        {
            Matrix4d t1 = Matrix4d.CreateTranslation(translation);
            Matrix4d s1 = Matrix4d.Scale(xScale, yScale, 0);

            Matrix4d transformationMatrix = s1 * t1;//Matrix4.Transpose(Matrix4.Transpose(t1) * s1);

            return transformationMatrix;
        }

        //get a view matrix, so the world moves when the camera moves.
        public static Matrix4 CreateViewMatrix(Camera camera)
        {
            Matrix4 pitch = Matrix4.CreateRotationX(DegreesToRadians(camera.pitch));
            Matrix4 yaw = Matrix4.CreateRotationY(DegreesToRadians(camera.yaw));
            Matrix4 negativeCameraPosition = Matrix4.CreateTranslation(-camera.position);

            Matrix4 viewMatrix = pitch * yaw * negativeCameraPosition;
            return viewMatrix;
        }

        public static float DegreesToRadians(float degrees)
        {
            return degrees * Convert.ToSingle(Math.PI / 180f);
        }

        //methods used to calculate the transformations.
        public static float Lerp(float t, float v0, float v1) //returns a value between v0 and v1, corresponding to the value that is t percent between v0 and v1.
        {
            return v0 * (1 - t) + v1 * t;
        }
        public static Vector3 Lerp(float t, Vector3 v0, Vector3 v1)
        {
            float x = Lerp(t, v0.X, v1.X);
            float y = Lerp(t, v0.Y, v1.Y);
            float z = Lerp(t, v0.Z, v1.Z);
            return new Vector3(x, y, z);
        }
        public static Vector2 Lerp(float t, Vector2 v0, Vector2 v1)
        {
            float x = Lerp(t, v0.X, v1.X);
            float y = Lerp(t, v0.Y, v1.Y);
            return new Vector2(x, y);
        }
        public static Vector2d Lerp(float t, Vector2d v0, Vector2d v1)
        {
            double x = Lerp(t, v0.X, v1.X);
            double y = Lerp(t, v0.Y, v1.Y);
            return new Vector2d(x, y);
        }
        public static float InverseLerp(float v, float v0, float v1) //returns a value betwen 0 and 1, corresponding to the percent that v is between v0 and v1.
        {
            return (v - v0) / (v1 - v0);
        }
        public static double Lerp(double t, double v0, double v1) //returns a value between v0 and v1, corresponding to the value that is t percent between v0 and v1.
        {
            return v0 * (1 - t) + v1 * t;
        }
        public static double InverseLerp(double v, double v0, double v1) //returns a value betwen 0 and 1, corresponding to the percent that v is between v0 and v1.
        {
            return (v - v0) / (v1 - v0);
        }
        public static float EuclideanDistance1D(float x0, float x1) //1D Euclidean distance between any two points.
        {
            float distance = x1 - x0;
            return Math.Abs(distance);
        }

        public static float Sin(float t)
        {
            return Convert.ToSingle(Math.Sin(t));
        }
        public static float Cos(float t)
        {
            return Convert.ToSingle(Math.Cos(t));
        }
    }
}

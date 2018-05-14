using System;
using System.Collections.Generic;
using OpenTK;

namespace Calculator
{
    public static partial class Mathematics
    {
        public static partial class DifferentialEquations
        {
            public static class LorenzSystem
            {
                /* METHODS */
                public static float FunctionX(float x, float y, float z, float t, float s, float p, float b)
                {
                    return (s * (y - x));
                }
                public static float FunctionY(float x, float y, float z, float t, float s, float p, float b)
                {
                    return (x * (p - z) - y);
                }
                public static float FunctionZ(float x, float y, float z, float t, float s, float p, float b)
                {
                    return ((x * y) - (b * z));
                }


                public static List<Vector3> Integrate(float x0, float y0, float z0, float t0, float tf, int steps, float s, float p, float b)
                {
                    float tOld = t0;
                    float xOld = x0;
                    float yOld = y0;
                    float zOld = z0;
                    float dt = tf / steps;

                    List<Vector3> solutions = new List<Vector3>();

                    while (tOld <= tf)
                    {
                        Vector3 currentPoint = new Vector3(xOld, yOld, zOld);
                        solutions.Add(currentPoint);

                        float kx0 = dt * FunctionX(xOld, yOld, zOld, tOld, s, p, b);
                        float ky0 = dt * FunctionY(xOld, yOld, zOld, tOld, s, p, b);
                        float kz0 = dt * FunctionZ(xOld, yOld, zOld, tOld, s, p, b);

                        float kx1 = dt * FunctionX(xOld + (0.5f * kx0), yOld + (0.5f * ky0), zOld + (0.5f * kz0), tOld + (0.5f * dt), s, p, b);
                        float ky1 = dt * FunctionY(xOld + (0.5f * kx0), yOld + (0.5f * ky0), zOld + (0.5f * kz0), tOld + (0.5f * dt), s, p, b);
                        float kz1 = dt * FunctionZ(xOld + (0.5f * kx0), yOld + (0.5f * ky0), zOld + (0.5f * kz0), tOld + (0.5f * dt), s, p, b);

                        float kx2 = dt * FunctionX(xOld + (0.5f * kx1), yOld + (0.5f * ky1), zOld + (0.5f * kz1), tOld + (0.5f * dt), s, p, b);
                        float ky2 = dt * FunctionY(xOld + (0.5f * kx1), yOld + (0.5f * ky1), zOld + (0.5f * kz1), tOld + (0.5f * dt), s, p, b);
                        float kz2 = dt * FunctionZ(xOld + (0.5f * kx1), yOld + (0.5f * ky1), zOld + (0.5f * kz1), tOld + (0.5f * dt), s, p, b);

                        float kx3 = dt * FunctionX(xOld + kx2, yOld + ky2, zOld + kz2, tOld + dt, s, p, b);
                        float ky3 = dt * FunctionY(xOld + kx2, yOld + ky2, zOld + kz2, tOld + dt, s, p, b);
                        float kz3 = dt * FunctionZ(xOld + kx2, yOld + ky2, zOld + kz2, tOld + dt, s, p, b);

                        float xNew = xOld + ((1f / 6f) * (kx0 + (2 * kx1) + (2 * kx2) + (3 * kx3)));
                        float yNew = yOld + ((1f / 6f) * (ky0 + (2 * ky1) + (2 * ky2) + (3 * ky3)));
                        float zNew = zOld + ((1f / 6f) * (kz0 + (2 * kz1) + (2 * kz2) + (3 * kz3)));
                        float tNew = tOld + dt;

                        xOld = xNew;
                        yOld = yNew;
                        zOld = zNew;
                        tOld = tNew;
                    }
                    return solutions;
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using OpenTK;

namespace Calculator
{
    public static partial class Mathematics
    {
        public static partial class DifferentialEquations
        {
            public static class DoublePendulum
            {
                public static float D_AngularVelocity0(float theta0, float theta1, float angularVelocity0, float angularVelocity1, float alpha, float beta, float gamma)
                {
                    float term0 = (1 + alpha) * gamma * Sin(theta0);
                    float term1 = alpha * beta * angularVelocity1 * angularVelocity1 * Sin(theta0 - theta1);
                    float term2 = alpha * Cos(theta0 - theta1) * (angularVelocity0 * angularVelocity0 * Sin(theta0 - theta1) - gamma * Sin(theta1));
                    float term3 = 1 + alpha * Sin(theta0 - theta1) * Sin(theta0 - theta1);
                    return -(term0 + term1 + term2) / term3;
                }
                public static float D_AngularVelocity1(float theta0, float theta1, float angularVelocity0, float angularVelocity1, float alpha, float beta, float gamma)
                {
                    float term0 = (1 + alpha) * (angularVelocity0 * angularVelocity0 * Sin(theta0 - theta1) - gamma * Sin(theta1));
                    float term1 = Cos(theta0 - theta1) * (1 + alpha) * gamma * Sin(theta0) + alpha * beta * angularVelocity1 * angularVelocity1 * Sin(theta0 - theta1);
                    float term2 = beta * (1 + alpha * Sin(theta0 - theta1) * Sin(theta0 - theta1));
                    return (term0 + term1) / term2;
                }
                public static float D_Theta0(float theta0, float theta1, float angularVelocity0, float angularVelocity1, float alpha, float beta, float gamma)
                {
                    return angularVelocity0;
                }
                public static float D_Theta1(float theta0, float theta1, float angularVelocity0, float angularVelocity1, float alpha, float beta, float gamma)
                {
                    return angularVelocity1;
                }


                public static List<Vector4> Integrate(float theta0, float theta1, float angularVelocity0, float angularVelocity1, float t0, float tf, int steps, float mass0, float mass1, float length0, float length1, float g)
                {
                    float tOld = t0;
                    float theta0Old = theta0;
                    float theta1Old = theta1;
                    float angularVelocity0Old = angularVelocity0;
                    float angularVelocity1Old = angularVelocity1;
                    float dt = tf / steps;

                    float alpha = mass1 / mass0;
                    float beta = length1 / length0;
                    float gamma = g / length0;

                    List<Vector4> solutions = new List<Vector4>();

                    while (angularVelocity1Old <= tf)
                    {
                        Vector4 currentPoint = new Vector4(theta0Old, theta1Old, angularVelocity0Old, angularVelocity1);

                        float kx0 = dt * D_AngularVelocity0(theta0Old, theta1Old, angularVelocity0Old, angularVelocity1Old, alpha, beta, gamma);
                        float ky0 = dt * D_AngularVelocity1(theta0Old, theta1Old, angularVelocity0Old, angularVelocity1Old, alpha, beta, gamma);
                        float kz0 = dt * D_Theta0(theta0Old, theta1Old, angularVelocity0Old, angularVelocity1Old, alpha, beta, gamma);
                        float kw0 = dt * D_Theta1(theta0Old, theta1Old, angularVelocity0Old, angularVelocity1Old, alpha, beta, gamma);

                        float kx1 = dt * D_AngularVelocity0(theta0Old + (0.5f * kx0), theta1Old + (0.5f * ky0), angularVelocity0Old + (0.5f * kz0), angularVelocity1Old + (0.5f * kw0), alpha, beta, gamma);
                        float ky1 = dt * D_AngularVelocity1(theta0Old + (0.5f * kx0), theta1Old + (0.5f * ky0), angularVelocity0Old + (0.5f * kz0), angularVelocity1Old + (0.5f * kw0), alpha, beta, gamma);
                        float kz1 = dt * D_Theta0(theta0Old + (0.5f * kx0), theta1Old + (0.5f * ky0), angularVelocity0Old + (0.5f * kz0), angularVelocity1Old + (0.5f * kw0), alpha, beta, gamma);
                        float kw1 = dt * D_Theta1(theta0Old + (0.5f * kx0), theta1Old + (0.5f * ky0), angularVelocity0Old + (0.5f * kz0), angularVelocity1Old + (0.5f * kw0), alpha, beta, gamma);

                        float kx2 = dt * D_AngularVelocity0(theta0Old + (0.5f * kx1), theta1Old + (0.5f * ky1), angularVelocity0Old + (0.5f * kz1), angularVelocity1Old + (0.5f * kw1), alpha, beta, gamma);
                        float ky2 = dt * D_AngularVelocity1(theta0Old + (0.5f * kx1), theta1Old + (0.5f * ky1), angularVelocity0Old + (0.5f * kz1), angularVelocity1Old + (0.5f * kw1), alpha, beta, gamma);
                        float kz2 = dt * D_Theta0(theta0Old + (0.5f * kx1), theta1Old + (0.5f * ky1), angularVelocity0Old + (0.5f * kz1), angularVelocity1Old + (0.5f * kw1), alpha, beta, gamma);
                        float kw2 = dt * D_Theta1(theta0Old + (0.5f * kx1), theta1Old + (0.5f * ky1), angularVelocity0Old + (0.5f * kz1), angularVelocity1Old + (0.5f * kw1), alpha, beta, gamma);

                        float kx3 = dt * D_AngularVelocity0(theta0Old + kx2, theta1Old + ky2, angularVelocity0Old + kz2, angularVelocity1Old + kw2, alpha, beta, gamma);
                        float ky3 = dt * D_AngularVelocity1(theta0Old + kx2, theta1Old + ky2, angularVelocity0Old + kz2, angularVelocity1Old + kw2, alpha, beta, gamma);
                        float kz3 = dt * D_Theta0(theta0Old + kx2, theta1Old + ky2, angularVelocity0Old + kz2, angularVelocity1Old + kw2, alpha, beta, gamma);
                        float kw3 = dt * D_Theta1(theta0Old + kx2, theta1Old + ky2, angularVelocity0Old + kz2, angularVelocity1Old + kw2, alpha, beta, gamma);

                        float theta0New = theta0Old + ((1f / 6f) * (kx0 + (2 * kx1) + (2 * kx2) + (3 * kx3)));
                        float theta1New = theta1Old + ((1f / 6f) * (ky0 + (2 * ky1) + (2 * ky2) + (3 * ky3)));
                        float angularVelocity0New = angularVelocity0Old + ((1f / 6f) * (kz0 + (2 * kz1) + (2 * kz2) + (3 * kz3)));
                        float angularVelocity1New = angularVelocity1Old + ((1f / 6f) * (kw0 + (2 * kw1) + (2 * kw2) + (3 * kw3)));
                        float tNew = tOld + dt;
                        
                        theta0Old = theta0New;
                        theta1Old = theta1New;
                        angularVelocity0Old = angularVelocity0New;
                        angularVelocity1Old = angularVelocity1New;
                        tOld = tNew;
                    }
                    return solutions;
                }
            }
        }
    }
}

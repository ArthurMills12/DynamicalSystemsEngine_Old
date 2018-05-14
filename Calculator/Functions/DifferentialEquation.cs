using System;
using System.Collections.Generic;
using OpenTK;

namespace Calculator
{
    abstract class DifferentialEquation
    {
        public List<Vector3> coordinates;

        public abstract List<Vector3> Integrator(float x0, float y0, float z0, float t0, float tf, int n);
    }
}

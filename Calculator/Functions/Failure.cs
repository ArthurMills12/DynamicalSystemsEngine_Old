using System;
using System.Collections.Generic;
using OpenTK;

namespace Calculator
{
    class Failure : QuadraticFunction
    {
        /* FIELDS AND PROPERTIES */
        private float _mu;
        public float mu { get => _mu; set => _mu = value; }


        /* CONSTRUCTORS */
        public Failure(float startTime, float finalTime, int steps, float scale, Vector3 position, Vector3 color, int pointSize, float mu) : base(startTime, finalTime, steps, scale, position, color, pointSize, mu)
        {
            this.mu = mu;
        }


        /* METHODS */
        public override float Image(float x) //standard one-parameter family of quadratic functions. the parameter is mu. 
        {
            return mu * x * x * (1 - x);
        }
    }
}

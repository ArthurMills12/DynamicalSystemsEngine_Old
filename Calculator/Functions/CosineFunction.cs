using System;
using System.Collections.Generic;
using OpenTK;

namespace Calculator
{
    class CosineFunction : Function
    {
        /* FIELDS AND PROPERTIES */
        private float _omega;
        public float omega { get => _omega; set => _omega = value; }


        public CosineFunction(float startTime, float finalTime, int steps, float scale, Vector3 position, Vector3 color, int pointSize, float omega) : base(startTime, finalTime, steps, scale, position, color, pointSize)
        {
            this.omega = omega;
        }

        public override float Image(float x)
        {
            return Convert.ToSingle(Math.Cos(omega * x));
        }
    }
}

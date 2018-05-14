using System;
using System.Collections.Generic;
using OpenTK;

namespace Calculator
{
    class ExponentialFunction : Function
    {
        /* FIELDS AND PROPERTIES */
        private float _alpha;
        public float alpha { get => _alpha; set => _alpha = value; }
        private float _shift;
        public float shift { get => _shift; set => _shift = value; }

        /* CONSTRUCTORS */
        public ExponentialFunction(float startTime, float finalTime, int steps, float scale, Vector3 position, Vector3 color, int pointSize, float alpha, float shift) : base(startTime, finalTime, steps, scale, position, color, pointSize)
        {
            this.alpha = alpha;
            this.shift = shift;
        }


        /* METHODS */
        public override float Image(float x) //standard one-parameter family of quadratic functions. the parameter is mu. 
        {
            return Convert.ToSingle(Math.Exp(x*alpha) - shift);
        }
    }
}

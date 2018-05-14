using System;
using System.Collections.Generic;
using OpenTK;

namespace Calculator
{
    //general linear function.
    class LinearFunction : Function
    {
        /* FIELDS AND PROPERTIES */
        private float _slope;
        public float slope { get => _slope; set => _slope = value; }

        private float _yIntercept;
        public float yIntercept { get => _yIntercept; set => _yIntercept = value; }


        /* CONSTRUCTORS */
        public LinearFunction(float startTime, float finalTime, int steps, float scale, Vector3 position, Vector3 color, int pointSize, float slope, float yIntercept) : base(startTime, finalTime, steps, scale, position, color, pointSize)
        {
            this.slope = slope;
            this.yIntercept = yIntercept;
        }


        /* METHODS */
        public override float Image(float x) //standard one-parameter family of quadratic functions. the parameter is mu. 
        {
            return slope * x + yIntercept;
        }
    }
}

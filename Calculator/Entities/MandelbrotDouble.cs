using System;
using System.Collections.Generic;
using OpenTK;

namespace Calculator
{
    class MandelbrotDouble : FractalDouble
    {
        /* PROPERTIES */



        /* CONSTRUCTORS */
        public MandelbrotDouble(Loader loader, Vector3 position, float xDistance, float yDistance, double minReal, double maxReal, double minIm, double maxIm) : base(loader, position, xDistance, yDistance, minReal, maxReal, minIm, maxIm)
        {

        }


        /* METHODS */
        public void Move()
        {
            double velocity = 0.001f;
            for (int i = 0; i < domain.Length; i++)
            {
                domain[0] += new Vector2d(velocity, velocity);
                domain[1] += new Vector2d(-velocity, velocity);
                domain[2] += new Vector2d(-velocity, -velocity);
                domain[3] += new Vector2d(velocity, -velocity);
            }
        }
    }
}

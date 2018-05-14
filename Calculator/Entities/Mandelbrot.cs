using System;
using System.Collections.Generic;

using OpenTK;

namespace Calculator
{
    class Mandelbrot : Fractal
    {
        /* PROPERTIES */



        /* CONSTRUCTORS */
        public Mandelbrot(Loader loader, Vector3 position, float xDistance, float yDistance, float minReal, float maxReal, float minIm, float maxIm) : base(loader, position, xDistance, yDistance, minReal, maxReal, minIm, maxIm)
        {

        }


        /* METHODS */
        public void Move()
        {
            float velocity = 0.001f;
            for (int i = 0; i < domain.Length; i++)
            {
                domain[0] += new Vector2(velocity, velocity);
                domain[1] += new Vector2(-velocity, velocity);
                domain[2] += new Vector2(-velocity, -velocity);
                domain[3] += new Vector2(velocity, -velocity);
            }
        }
    }
}

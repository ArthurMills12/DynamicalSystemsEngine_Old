using System;
using System.Collections.Generic;
using OpenTK;

namespace Calculator
{
    static partial class Mathematics
    {
        public static class BifurcationDiagram
        {
            /* METHODS */
            //we will plot the parameter along the horizontal axis and the orbit values along the vertical.
            public static List<Vector3> GetOrbit(QuadraticFunction quadraticFunction, float muMin, float muMax, int steps, float orbitPoint, int minOrbits, int maxOrbits, float scale)
            {
                List<Vector3> coordinates = new List<Vector3>();

                float dmu = (muMax - muMin) / steps;

                //loop through mu's:
                for (float mu = muMin; mu < muMax; mu += dmu)
                {
                    float orbitValue = orbitPoint;
                    quadraticFunction.mu = mu;

                    //loop through number of orbits to graph:
                    for (int i = 0; i < maxOrbits; i++)
                    {
                        float currentOrbit = quadraticFunction.Image(orbitValue);

                        if (i > minOrbits)
                        {
                            coordinates.Add(new Vector3(scale * mu, scale * currentOrbit, quadraticFunction.position.Z));
                        }

                        orbitValue = currentOrbit;
                    }
                }

                return coordinates;
            }
        }
    }
}

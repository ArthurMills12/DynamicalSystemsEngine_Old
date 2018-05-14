using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace Calculator
{
    //data points to be rendered.
    class Data : IRenderable
    {
        /* PROPERTIES */
        public Vector3 position;
        public Vector3 rotation;
        public List<Vector3> coordinates;
        public List<Vector3> colors;
        public RawModel rawModel { get; set; }
        public PrimitiveType primitiveType;

        /* CONSTRUCTORS */
        public Data(Loader loader, Vector3 position, Vector3 color, PrimitiveType primitiveType, List<Vector3> data)
        {
            coordinates = new List<Vector3>();
            colors = new List<Vector3>();

            this.position = position;
            this.coordinates = data;
            this.colors = GetColorArray(color, coordinates.Count);
            this.primitiveType = primitiveType;

            GetRawModel(loader);
        }

        /* METHODS */
        /*
        public void GetData(Loader loader, Vector3 position, DifferentialEquation differentialEquation)
        {
            //initialize:
            this.position = position;
            this.rotation = new Vector3(0.5f * Convert.ToSingle(Math.PI), 0, Convert.ToSingle(Math.PI));
            coordinates = differentialEquation.coordinates;
            colors = GetColorArray(new Vector3(0, 0, 0), coordinates.Count);
            this.primitiveType = PrimitiveType.LineStrip;

            GetRawModel(loader);
        }

        public void GetData(Loader loader, Vector3 position, QuadraticFunction quadraticFunction, float muMin, float muMax, int steps, float orbitPoint, int minOrbits, int maxOrbits, float scale)
        {
            this.position = position;
            coordinates = BifurcationDiagram.GetOrbit(quadraticFunction, muMin, muMax, steps, orbitPoint, minOrbits, maxOrbits, scale);
            colors = GetColorArray(new Vector3(0, 0, 0), coordinates.Count);
            this.primitiveType = PrimitiveType.Points;

            GetRawModel(loader);
        }

        public void GetData(Loader loader, Vector3 position, DoublePendulum doublePendulum)
        {
            this.position = position;
            coordinates = doublePendulum.coordinates;
            colors = GetColorArray(new Vector3(0, 0, 0), coordinates.Count);
            this.primitiveType = PrimitiveType.LineStrip;
            GetRawModel(loader);
        }
        */

        public List<Vector3> GetColorArray(Vector3 color, int length)
        {
            List<Vector3> colorArray = new List<Vector3>();
            for (int i = 0; i < length; i++)
            {
                colorArray.Add(color);
            }
            return colorArray;
        }

        public void GetRawModel(Loader loader)
        {
            rawModel = loader.LoadToVAO(coordinates.ToArray(), colors.ToArray());
        }

        public void Clear()
        {
            coordinates.Clear();
            colors.Clear();
        }
    }
}

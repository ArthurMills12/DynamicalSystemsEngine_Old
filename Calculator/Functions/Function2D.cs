using System;
using System.Collections.Generic;
using OpenTK;

namespace Calculator
{
    //two-parameter function that will be graphed in 3D-space.
    abstract class Function2D : IRenderable
    {
        /* PROPERTIES */
        //position in world space.
        private Vector3 _position;
        public Vector3 position { get => _position; set => _position = value; }

        //coordinate positions that make up the function.
        private List<Vector3> _coordinates;
        public List<Vector3> coordinates { get => _coordinates; set => _coordinates = value; }

        //colors for the vertices.
        private List<Vector3> _colors;
        public List<Vector3> colors { get => _colors; set => _colors = value; }

        //triangles to figure out how the model should be stitched together.
        private List<int> _triangles;
        public List<int> triangles { get => _triangles; set => _triangles = value; }

        //domain in R^3 to be specified using vertices of a prism.
        public float xMin { get; private set; }
        public float xMax { get; private set; }
        public float yMin { get; private set; }
        public float yMax { get; private set; }
        public float zMin { get; private set; }
        public float zMax { get; private set; }

        //raw model for rendering.
        public RawModel rawModel { get; set; }


        /* CONSTRUCTORS */
        public Function2D(Vector3 position, float xMin, float xMax, float yMin, float yMax, float zMin, float zMax)
        {
            //initialization:
            coordinates = new List<Vector3>();
            colors = new List<Vector3>();
            triangles = new List<int>();

            this.position = position;

            //figure out domain:
            this.xMin = xMin;
            this.xMax = xMax;
            this.yMin = yMin;
            this.yMax = yMax;
            this.zMin = zMin;
            this.zMax = zMax;
        }


        /* METHODS */
        public abstract float Image(float x, float y);

        public virtual void GetCoordinates(float steps)
        {
            float dx = Math.Abs(xMax - xMin) / steps;
            float dy = Math.Abs(yMax - yMin) / steps;

            for (float x = xMin; x < xMax; x += dx)
            {
                for (float y = yMin; y < yMax; y += dy)
                {
                    Vector3 xyPlaneCoordinate = new Vector3(x, y, 0); //so EVEN indexed coordinates will be these base coordinates in the xy-plane.
                    Vector3 currentCoordinate = new Vector3(x, y, Image(x, y));

                    coordinates.Add(xyPlaneCoordinate);
                    coordinates.Add(currentCoordinate);
                }
            }
        }

        public virtual void FormTriangles()
        {

        }

        public virtual void GetRawModel(Loader loader) //get a rawModel using the coordinates list.
        {
            //rawModel = loader.LoadToVAO(coordinates, colors);
        }

        public virtual void GetColorArray(Vector3 color)
        {
            for (int i = 0; i < coordinates.Count; i++)
            {
                colors.Add(color);
            }
        }
    }
}

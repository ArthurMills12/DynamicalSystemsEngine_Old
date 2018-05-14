using System;
using System.Collections.Generic;
using OpenTK;

namespace Calculator
{
    public abstract class Function : IRenderable
    {
        /* FIELDS AND PROPERTIES */
        //position in world space.
        private Vector3 _position;
        public Vector3 position { get => _position; set => _position = value; }

        //coordinates that make up the function.
        private List<Vector3> _coordinates;
        public List<Vector3> coordinates { get => _coordinates; set => _coordinates = value; }

        public Vector3 color { get; private set; }

        public int pointSize { get; private set; }

        private float _startTime;
        public float startTime { get => _startTime; set => _startTime = value; }

        private float _finalTime;
        public float finalTime { get => _finalTime; set => _finalTime = value; }

        private int _steps;
        public int steps { get => _steps; set => _steps = value; }
        public float timeStep { get => Math.Abs(finalTime - startTime) / steps; }

        private float _scale;
        public float scale { get => _scale; set => _scale = value; }

        //allow the function to be rendered.
        public RawModel rawModel { get; set; }

        public Function(float startTime, float finalTime, int steps, float scale, Vector3 position, Vector3 color, int pointSize)
        {
            this.startTime = startTime;
            this.finalTime = finalTime;
            this.steps = steps;
            this.scale = scale;
            this.position = position;
            this.color = color;
            this.pointSize = pointSize;
            coordinates = new List<Vector3>();
        }


        /* METHODS */
        public abstract float Image(float x); //this is the rule f(x) for the function. 
        
        public void GetCoordinates()
        {
            for (float x = startTime; x < finalTime; x += timeStep)
            {
                Vector3 currentCoordinate = new Vector3(scale * x, scale * Image(x), 0);
                coordinates.Add(currentCoordinate);
            }
        }

        public virtual void GetRawModel(Loader loader) //get a rawModel using the coordinates list.
        {
            GetCoordinates();
            rawModel = loader.LoadToVAO(coordinates.ToArray(), GetColorArray());
        }

        public virtual Vector3[] GetColorArray()
        {
            Vector3[] colorArray = new Vector3[coordinates.Count];
            for (int i = 0; i < colorArray.Length; i++)
            {
                colorArray[i] = color;
            }
            return colorArray;
        }
        public virtual int[] GetPointSizeArray()
        {
            int[] pointSizeArray = new int[coordinates.Count];
            for (int i = 0; i < pointSizeArray.Length; i++)
            {
                pointSizeArray[i] = pointSize;
            }
            return pointSizeArray;
        }

    }
}

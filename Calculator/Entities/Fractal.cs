using System;
using System.Collections.Generic;
using OpenTK;

namespace Calculator
{
    //a quad that will have a fractal rendered onto it.
    abstract class Fractal
    {
        /* FIELDS AND PROPERTIES */
        //position of the bottom left vertex in world space.
        private Vector3 _position;
        public Vector3 position { get => _position; set => _position = value; }

        /*
        private double _xDistance;
        public double xDistance { get => _xDistance; set => _xDistance = value; }
        private double _yDistance;
        public double yDistance { get => _yDistance; set => _yDistance = value; }
        */

        private Vector3[] _vertices;
        public Vector3[] vertices { get => _vertices; set => _vertices = value; }

        private Vector2[] _domain;
        public Vector2[] domain { get => _domain; set => _domain = value; }

        public RawModel rawModel;

        /* CONSTRUCTORS */
        public Fractal(Loader loader, Vector3 position, float xDistance, float yDistance, float minReal, float maxReal, float minIm, float maxIm)
        {
            this.position = position;
            vertices = GetVertices(xDistance, yDistance);
            
            domain = GetUnitSquareDomain();
            rawModel = loader.LoadToVAO(vertices, domain);
            domain = GetDomain(minReal, maxReal, minIm, maxIm);
        }

        /* METHODS */
        private Vector3[] GetVertices(float xDistance, float yDistance)
        {
            Vector3 bottomLeft = position + new Vector3(0, 0, 0);
            Vector3 bottomRight = position + new Vector3(xDistance, 0, 0);
            Vector3 topRight = position + new Vector3(xDistance, yDistance, 0);
            Vector3 topLeft = position + new Vector3(0, yDistance, 0);
            return new Vector3[4] { bottomLeft, bottomRight, topRight, topLeft}; //yes, they must be entered this order: counterclockwise from bottom left.
        }

        private Vector2[] GetUnitSquareDomain()
        {
            Vector2 bottomLeft = new Vector2(0, 0);
            Vector2 bottomRight = new Vector2(1, 0);
            Vector2 topRight = new Vector2(1, 1);
            Vector2 topLeft = new Vector2(0, 1);
            return new Vector2[4] { bottomLeft, bottomRight, topRight, topLeft };
        }
        
        private Vector2[] GetDomain(float minReal, float maxReal, float minIm, float maxIm)
        {
            Vector2 bottomLeft = new Vector2(minReal, minIm);
            Vector2 bottomRight = new Vector2(maxReal, minIm);
            Vector2 topRight = new Vector2(maxReal, maxIm);
            Vector2 topLeft = new Vector2(minReal, maxIm);
            return new Vector2[4] { bottomLeft, bottomRight, topRight, topLeft };
        }


    }
}

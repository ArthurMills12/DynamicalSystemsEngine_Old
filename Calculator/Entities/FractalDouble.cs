using System;
using System.Collections.Generic;
using OpenTK;

namespace Calculator
{
    abstract class FractalDouble
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

        private Vector2d[] _domain;
        public Vector2d[] domain { get => _domain; set => _domain = value; }

        public RawModel rawModel;

        /* CONSTRUCTORS */
        public FractalDouble(Loader loader, Vector3 position, float xDistance, float yDistance, double minReal, double maxReal, double minIm, double maxIm)
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
            return new Vector3[4] { bottomLeft, bottomRight, topRight, topLeft }; //yes, they must be entered this order: counterclockwise from bottom left.
        }

        private Vector2d[] GetUnitSquareDomain()
        {
            Vector2d bottomLeft = new Vector2d(0, 0);
            Vector2d bottomRight = new Vector2d(1, 0);
            Vector2d topRight = new Vector2d(1, 1);
            Vector2d topLeft = new Vector2d(0, 1);
            return new Vector2d[4] { bottomLeft, bottomRight, topRight, topLeft };
        }

        private Vector2d[] GetDomain(double minReal, double maxReal, double minIm, double maxIm)
        {
            Vector2d bottomLeft = new Vector2d(minReal, minIm);
            Vector2d bottomRight = new Vector2d(maxReal, minIm);
            Vector2d topRight = new Vector2d(maxReal, maxIm);
            Vector2d topLeft = new Vector2d(minReal, maxIm);
            return new Vector2d[4] { bottomLeft, bottomRight, topRight, topLeft };
        }


    }
    }

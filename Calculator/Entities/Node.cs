using System;
using System.Collections.Generic;
using OpenTK;

namespace Calculator
{
    //represents a single point to be rendered to the screen.
    class Node : IRenderable
    {
        /* PROPERTIES */
        private Vector3 _position;
        public Vector3 position { get => _position; set => _position = value; }

        private Vector3 _color;
        public Vector3 color { get => _color; set => _color = value; }

        private int _pointSize;
        public int pointSize { get => _pointSize; set => _pointSize = value; }
        
        private RawModel _rawModel;
        public RawModel rawModel { get => _rawModel; set => _rawModel = value; }


        /* CONSTRUCTORS */
        public Node(Vector3 position, Vector3 color, int pointSize)
        {
            this.position = position;
            this.color = color;
            this.pointSize = pointSize;
        }


        /* METHODS */
        public void GetRawModel(Loader loader)
        {
            rawModel = loader.LoadToVAO(new Vector3(0, 0, 0), color); //this is a point, so its relative position to itself is zero.
        }
    }
}

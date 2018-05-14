using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace Calculator
{
    //store raw data about a model in a VAO.
    public class RawModel
    {
        /* FIELDS AND PROPERTIES */
        private int _vaoID;
        private int _vertexCount;
        public int vaoID { get { return _vaoID; } private set { _vaoID = value; } }
        public int vertexCount { get { return _vertexCount; } private set { _vertexCount = value; } }

        /* CONSTRUCTORS */
        public RawModel(int vaoID, int vertexCount)
        {
            this.vaoID = vaoID;
            this.vertexCount = vertexCount;
        }
    }
}

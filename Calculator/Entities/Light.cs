using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace Calculator
{
    //entity that will emit light. other classes that will be light up will refer to the properties of this entity for lighting information.
    public class Light
    {
        /* FIELDS AND PROPERTIES */
        private Vector3 _position;
        private Vector3 _color;
        public Vector3 position { get => _position; set => _position = value; }
        public Vector3 color { get => _color; set => _color = value; }


        /* CONSTRUCTORS */
        public Light(Vector3 position, Vector3 color)
        {
            this.position = position;
            this.color = color;
        }
    }
}

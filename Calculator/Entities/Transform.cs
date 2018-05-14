using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace Calculator
{
    //basically just copying Unity here.
    public class Transform
    {
        /* FIELDS AND PROPERTIES */
        private Vector3 _position;
        private float _xAxisRotation;
        private float _yAxisRotation;
        private float _zAxisRotation;
        private float _xScale;
        private float _yScale;
        private float _zScale;
        private float _scale;

        public Vector3 position { get => _position; set => _position = value; }
        public float xAxisRotation { get => _xAxisRotation; set => _xAxisRotation = value; }
        public float yAxisRotation { get => _yAxisRotation; set => _yAxisRotation = value; }
        public float zAxisRotation { get => _zAxisRotation; set => _zAxisRotation = value; }
        public float xScale { get => _xScale; set => _xScale = value; }
        public float yScale { get => _yScale; set => _yScale = value; }
        public float zScale { get => _zScale; set => _zScale = value; }
        public float scale { get => _scale; set => _scale = value; }


        /* CONSTRUCTORS */
        public Transform(Vector3 position, float xAxisRotation, float yAxisRotation, float zAxisRotation, float xScale, float yScale, float zScale)
        {
            this.position = position;
            this.xAxisRotation = xAxisRotation;
            this.yAxisRotation = yAxisRotation;
            this.zAxisRotation = zAxisRotation;
            this.xScale = xScale;
            this.yScale = yScale;
            this.zScale = zScale;
        }
        public Transform(float xPosition, float yPosition, float zPosition, float xAxisRotation, float yAxisRotation, float zAxisRotation, float xScale, float yScale, float zScale)
        {
            position = new Vector3(xPosition, yPosition, zPosition);
            this.xAxisRotation = xAxisRotation;
            this.yAxisRotation = yAxisRotation;
            this.zAxisRotation = zAxisRotation;
            this.xScale = xScale;
            this.yScale = yScale;
            this.zScale = zScale;
        }
        public Transform(Vector3 position, float xAxisRotation, float yAxisRotation, float zAxisRotation, float scale)
        {
            this.position = position;
            this.xAxisRotation = xAxisRotation;
            this.yAxisRotation = yAxisRotation;
            this.zAxisRotation = zAxisRotation;
            this.scale = scale;
        }
        public Transform(float xPosition, float yPosition, float zPosition, float xAxisRotation, float yAxisRotation, float zAxisRotation, float scale)
        {
            position = new Vector3(xPosition, yPosition, zPosition);
            this.xAxisRotation = xAxisRotation;
            this.yAxisRotation = yAxisRotation;
            this.zAxisRotation = zAxisRotation;
            this.scale = scale;
        }
    }
}

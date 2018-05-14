using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;

namespace Calculator
{
    //moves around the world and everything in front of it will be rendered to the screen.
    public class Camera
    {
        /* FIELDS AND PROPERTIES */
        private Vector3 _position = new Vector3(0, 0, 0);
        private float _pitch; //how high or low the camera is aimed: rotation about the x-axis.
        private float _yaw; //rotation about the y-axis.
        private float _roll; //rotation about the z-axis.
        public Vector3 position { get => _position; set => _position = value; }
        public float pitch { get => _pitch; set => _pitch = value; }
        public float yaw { get => _yaw; set => _yaw = value; }
        public float roll { get => _roll; set => _roll = value; }
        //private float _cameraSpeed = 0.005f;
        private float _cameraSpeed = 0.2f;


        /* METHODS */
        //move the camera using the keyboard.
        public void Move()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            //translation:
            if (keyboardState.IsKeyDown(Key.W))
            {
                position = new Vector3(position.X, position.Y, position.Z - _cameraSpeed);
            }
            if (keyboardState.IsKeyDown(Key.S))
            {
                position = new Vector3(position.X, position.Y, position.Z + _cameraSpeed);
            }

            if (keyboardState.IsKeyDown(Key.D))
            {
                position = new Vector3(position.X + _cameraSpeed, position.Y, position.Z);
            }
            if (keyboardState.IsKeyDown(Key.A))
            {
                position = new Vector3(position.X - _cameraSpeed, position.Y, position.Z);
            }

            if (keyboardState.IsKeyDown(Key.ShiftLeft))
            {
                position = new Vector3(position.X, position.Y - _cameraSpeed, position.Z);
            }
            if (keyboardState.IsKeyDown(Key.Enter))
            {
                position = new Vector3(position.X, position.Y + _cameraSpeed, position.Z);
            }

            //rotation:
            if (keyboardState.IsKeyDown(Key.Q))
            {
                yaw += _cameraSpeed;
            }
            if (keyboardState.IsKeyDown(Key.E))
            {
                yaw -= _cameraSpeed;
            }
        }
    }
}

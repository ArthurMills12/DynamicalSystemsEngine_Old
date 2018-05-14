using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Input;
using System.Drawing;

namespace Calculator
{
    class FractalViewer : Environment
    {
        /* PROPERTIES */
        Loader loader;
        Mandelbrot mandelbrot;
        Vector2 mouseGLCoordinates;
        float mouseWheelDelta;
        float width;
        float height;


        /* CONSTRUCTORS */
        public FractalViewer(Loader loader, float width, float height)
        {
            this.loader = loader;
            this.width = width;
            this.height = height;
        }

        
        /* METHODS */
        //overrides:
        public override void CloseEnvironment()
        {
            
        }

        public override void ManageInput(KeyboardState keyboardState, MouseState mouseState, float mouseWheelDelta, Point mousePosition)
        {
            if (mousePosition != null)
            {
                mouseGLCoordinates = GetMouseUnitCoordinates(mousePosition);
            }
            this.mouseWheelDelta = mouseWheelDelta;
        }

        public override void OnLoad()
        {
            mandelbrot = new Mandelbrot(loader, new Vector3(-1f, -1f, 0), 2, 2, -2.5f, 1, -1, 1);
        }

        public override void OnRenderFrame(MasterRenderer masterRenderer)
        {
            masterRenderer.ProcessFractal(mouseGLCoordinates, mouseWheelDelta, mandelbrot);
        }

        public override void OnUpdateFrame(KeyboardState keyboardState, MouseState mouseState, float mouseWheelDelta, Point mousePosition)
        {
            ManageInput(keyboardState, mouseState, mouseWheelDelta, mousePosition);
        }


        private Vector2 GetMouseGLCoordinates(Point mouseWindowCoordinates)
        {
            float xGLCoordinate = Mathematics.InverseLerp(mouseWindowCoordinates.X, 0.5f * width, width);
            float yGLCoordinate = Mathematics.InverseLerp(mouseWindowCoordinates.Y, 0.5f * height, height);
            return new Vector2(xGLCoordinate, yGLCoordinate);
        }
        private Vector2 GetMouseUnitCoordinates(Point mouseWindowCoordinates)
        {
            float xGLCoordinate = Mathematics.InverseLerp(mouseWindowCoordinates.X, 0, width);
            float yGLCoordinate = Mathematics.InverseLerp(mouseWindowCoordinates.Y, 0, height);

            return new Vector2(xGLCoordinate, yGLCoordinate);
        }
    }
}

using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace Calculator
{
    //our wrappper for the GameWindow class.
    public sealed class Display : GameWindow
    {
        /* FIELDS AND PROPERTIES */

        //the display window:
        //private static int _width = 1280;
        //private static int _height = 720;
        private static int _width = 1920;
        private static int _height = 1080;
        public static int width { get => _width; private set => _width = value; }
        public static int height { get => _height; private set => _height = value; }

        /* CONSTRUCTORS */
        public Display()
            : base
            (
                 width, //width
                 height, //height
                 GraphicsMode.Default, //GraphicsMode: can use a new GraphicsMode(bits of color, bits of depth buffer, stenciling, anti-aliasing).
                 "Tutorial", //window name 
                 GameWindowFlags.FixedWindow, //windowed mode, fixed window, fullscreen. 
                 DisplayDevice.Default,
                 4, //major version
                 5, //minor version
                 GraphicsContextFlags.ForwardCompatible
             )
        {
            //extra initialization, if needed.
        }
    }
}

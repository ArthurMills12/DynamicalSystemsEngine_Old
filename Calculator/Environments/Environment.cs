using System;
using System.Collections.Generic;
using System.Drawing;
using OpenTK.Input;

namespace Calculator
{
    //the MainGameLoop will choose an environment to render. different environments render different things to the screen.
    abstract class Environment
    {
        /* PROPRETIES */


        /* CONSTRUCTORS */


        /* METHODS */
        //initialize the necessary things when the environment is created.
        public abstract void OnLoad();

        //when the frame is updated.
        public abstract void OnUpdateFrame(KeyboardState keyboardState, MouseState mouseState, float mouseWheelDelta, Point mousePosition);

        //when the frame is rendered.
        public abstract void OnRenderFrame(MasterRenderer masterRenderer);

        //choose how input will be handled.
        public abstract void ManageInput(KeyboardState keyboardState, MouseState mouseState, float mouseWheelDelta, Point mousePosition);

        //close down everything and reset the screen.
        public abstract void CloseEnvironment();
    }
}

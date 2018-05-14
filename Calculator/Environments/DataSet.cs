using System;
using System.Collections.Generic;
using System.Drawing;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics.OpenGL4;

namespace Calculator
{
    //a bunch of points to be rendered.
    class DataSet : Environment
    {
        /* PROPERTIES */
        public Data data;
        public Vector3 position { get; private set; }
        public Vector3 color { get; private set; }
        public List<Vector3> coordinates { get; private set; }
        public PrimitiveType primitiveType { get; private set; }
        /*
        public DifferentialEquation differentialEquation;
        public DoublePendulum doublePendulum;
        public QuadraticFunction quadraticFunction;
        */
        private Loader loader;
        private bool begin = false;
        private bool rotate = false;
        float muMin;
        float muMax;
        int steps;
        float orbitPoint;
        int minOrbits;
        int maxOrbits;
        float scale;

        /* CONSTRUCTORS */
        /*
        public DataSet(Loader loader, DifferentialEquation differentialEquation, Vector3 position)
        {
            //initialize:
            this.loader = loader;
            this.data = new Data();
            this.differentialEquation = differentialEquation;
            this.position = position;
            rotate = true;
        }
        public DataSet(Loader loader, DoublePendulum doublePendulum, Vector3 position)
        {
            //initialize:
            this.loader = loader;
            this.data = new Data();
            this.doublePendulum = doublePendulum;
            this.position = position;
            rotate = false;
        }
        public DataSet(Loader loader, QuadraticFunction quadraticFunction, float muMin, float muMax, int steps, float orbitPoint, int minOrbits, int maxOrbits, float scale)
        {
            //initialize:
            this.loader = loader;
            this.data = new Data();
            this.quadraticFunction = quadraticFunction;
            this.muMin = muMin;
            this.muMax = muMax;
            this.steps = steps;
            this.orbitPoint = orbitPoint;
            this.minOrbits = minOrbits;
            this.maxOrbits = maxOrbits;
            this.scale = scale;
        }
        */
        public DataSet(Loader loader, Vector3 position, Vector3 color, PrimitiveType primitiveType, List<Vector3> coordinates)
        {
            //initialize:
            this.loader = loader;
            this.position = position;
            this.color = color;
            this.primitiveType = primitiveType;
            this.coordinates = coordinates;
        }



        /* METHODS */


        //inherited:
        public override void CloseEnvironment()
        {
            
        }

        public void Reset()
        {
            data.Clear();
            OnLoad();
        }

        public override void ManageInput(KeyboardState keyboardState, MouseState mouseState, float mouseWheelDelta, Point mousePosition)
        {
            if (keyboardState.IsKeyDown(Key.Space))
            {
                begin = true;
            }
            if (keyboardState.IsKeyDown(Key.BackSpace))
            {
                Reset();
            }
        }

        public override void OnLoad()
        {
            /*
            if (differentialEquation != null)
            {
                data.GetData(loader, position, differentialEquation);
            }

            if (quadraticFunction != null)
            {
                data.GetData(loader, quadraticFunction.position, quadraticFunction, muMin, muMax, steps, orbitPoint, minOrbits, maxOrbits, scale);
            }

            if (doublePendulum != null)
            {
                data.GetData(loader, position, doublePendulum);
            }
            */

            this.data = new Data(loader, position, color, primitiveType, coordinates);
        }

        public override void OnRenderFrame(MasterRenderer masterRenderer)
        {
            Move(rotate);
            masterRenderer.ProcessDataSet(this);
        }

        public override void OnUpdateFrame(KeyboardState keyboardState, MouseState mouseState, float mouseWheelDelta, Point mousePosition)
        {
            ManageInput(keyboardState, mouseState, mouseWheelDelta, mousePosition);
        }

        public void Move(bool rotate)
        {
            if (rotate)
            {
                data.rotation += new Vector3(0, 0.005f, 0);
            }
        }
    }
}

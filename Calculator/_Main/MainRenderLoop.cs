using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using System.Drawing;

namespace Calculator
{
    //where the rendering logic goes: actually runs the program here.
    class MainRenderLoop
    {
        /* FIELDS AND PROPERTIES */
        //the display window:
        private Display display;
        private static int width;
        private static int height;

        //things needed to render stuff:
        Loader loader;
        MasterRenderer masterRenderer;
        Camera camera;
        Light light;
        
        //input:
        float mouseWheelDelta;
        KeyboardState keyboardState;
        MouseState mouseState;
        Point mousePosition;

        //environments:
        Environment[] environments;
        Environment currentEnvironment;

        /* CONSTRUCTORS */
        public MainRenderLoop()
        {
            //initialization:
            display = new Display();
            width = display.Width;
            height = display.Height;

            //subscribe to display events:
            display.RenderFrame += OnRenderFrame;
            display.Load += OnLoad;
            display.UpdateFrame += OnUpdateFrame;
            display.Closed += OnClosed;
            display.Resize += OnResize;
            display.MouseWheel += MouseWheel;

            //run the display:
            display.Run(60);
        }
        

        /* EVENT METHODS */
        //when the window starts.
        private void OnLoad(object sender, EventArgs e)
        {
            //initialize the things we need to render stuff:
            masterRenderer = new MasterRenderer();
            loader = new Loader();

            light = new Light(new Vector3(0, 0, -20), new Vector3(1, 1, 1));
            camera = new Camera();

            //LoadGraphicalAnalysis();
            
            
            environments = GetScholarsDayEnvironments();

            currentEnvironment = environments[13];
            currentEnvironment.OnLoad();
            
        }
        
        private Environment[] GetScholarsDayEnvironments()
        {
            Environment[] scholarsDayEnvironments = new Environment[23];

            //set environments:
            Vector3 graphicalAnalysisPosition = new Vector3(0, 0, -10);
            Vector3 lorenzPosition = new Vector3(0, -29, -55);
            Vector3 white = new Vector3(1, 1, 1);
            Vector3 black = new Vector3(0, 0, 0);
            int numberOfNodes = 1000;
            float nodeVelocity = 0.03f;
            int pointSize = 0;
            int functionPointSize = 1;
            float scale = 5;

            //first environment: demonstrate how graphical analysis is a great representation of iterating functions.
            CosineFunction cosineFunction = new CosineFunction(-6f, 6f, 1000, scale, graphicalAnalysisPosition, white, functionPointSize, 1);
            scholarsDayEnvironments[0] = LoadGraphicalAnalysis(cosineFunction, scale, 1000, nodeVelocity, false, pointSize);

            //second through ninth environments: quadratic function. change the mu values around.
            //QuadraticFunction quadraticFunction;
            QuadraticFunction quadraticFunction;
            QuadraticSquareFunction quadraticSquareFunction;
            float startTime = 0;
            float finalTime = 1;
            int steps = 1000;

            //change mu around:
            
            scholarsDayEnvironments[1] = LoadGraphicalAnalysis(quadraticFunction = new QuadraticFunction(startTime, finalTime, steps, scale, graphicalAnalysisPosition, white, functionPointSize, 1f), scale, numberOfNodes, nodeVelocity, false, pointSize);
            scholarsDayEnvironments[2] = LoadGraphicalAnalysis(quadraticFunction = new QuadraticFunction(startTime, finalTime, steps, scale, graphicalAnalysisPosition, white, functionPointSize, 1.5f), scale, numberOfNodes, nodeVelocity, false, pointSize);
            scholarsDayEnvironments[3] = LoadGraphicalAnalysis(quadraticFunction = new QuadraticFunction(startTime, finalTime, steps, scale, graphicalAnalysisPosition, white, functionPointSize, 2), scale, numberOfNodes, nodeVelocity, false, pointSize);
            scholarsDayEnvironments[4] = LoadGraphicalAnalysis(quadraticFunction = new QuadraticFunction(startTime, finalTime, steps, scale, graphicalAnalysisPosition, white, functionPointSize, 2.5f), scale, numberOfNodes, nodeVelocity, false, pointSize);
            scholarsDayEnvironments[5] = LoadGraphicalAnalysis(quadraticFunction = new QuadraticFunction(startTime, finalTime, steps, scale, graphicalAnalysisPosition, white, functionPointSize, 3), scale, numberOfNodes, nodeVelocity, false, pointSize);
            scholarsDayEnvironments[6] = LoadGraphicalAnalysis(quadraticFunction = new QuadraticFunction(startTime, finalTime, steps, scale, graphicalAnalysisPosition, white, functionPointSize, 3.2f), scale, numberOfNodes, nodeVelocity, false, pointSize);
            scholarsDayEnvironments[7] = LoadGraphicalAnalysis(quadraticFunction = new QuadraticFunction(startTime, finalTime, steps, scale, graphicalAnalysisPosition, white, functionPointSize, 3.5f), scale, numberOfNodes, nodeVelocity, false, pointSize);
            scholarsDayEnvironments[8] = LoadGraphicalAnalysis(quadraticFunction = new QuadraticFunction(startTime, finalTime, steps, scale, graphicalAnalysisPosition, white, functionPointSize, 3.839f), scale, numberOfNodes, nodeVelocity, false, pointSize);
            scholarsDayEnvironments[9] = LoadGraphicalAnalysis(quadraticFunction = new QuadraticFunction(startTime, finalTime, steps, scale, graphicalAnalysisPosition, white, functionPointSize, 4), scale, numberOfNodes, nodeVelocity, false, pointSize);
            
            scholarsDayEnvironments[14] = LoadGraphicalAnalysis(quadraticSquareFunction = new QuadraticSquareFunction(startTime, finalTime, steps, scale, graphicalAnalysisPosition, white, functionPointSize, 1), scale, numberOfNodes, nodeVelocity, false, pointSize);
            scholarsDayEnvironments[15] = LoadGraphicalAnalysis(quadraticSquareFunction = new QuadraticSquareFunction(startTime, finalTime, steps, scale, graphicalAnalysisPosition, white, functionPointSize, 1.5f), scale, numberOfNodes, nodeVelocity, false, pointSize);
            scholarsDayEnvironments[16] = LoadGraphicalAnalysis(quadraticSquareFunction = new QuadraticSquareFunction(startTime, finalTime, steps, scale, graphicalAnalysisPosition, white, functionPointSize, 2), scale, numberOfNodes, nodeVelocity, false, pointSize);
            scholarsDayEnvironments[17] = LoadGraphicalAnalysis(quadraticSquareFunction = new QuadraticSquareFunction(startTime, finalTime, steps, scale, graphicalAnalysisPosition, white, functionPointSize, 2.5f), scale, numberOfNodes, nodeVelocity, false, pointSize);
            scholarsDayEnvironments[18] = LoadGraphicalAnalysis(quadraticSquareFunction = new QuadraticSquareFunction(startTime, finalTime, steps, scale, graphicalAnalysisPosition, white, functionPointSize, 3), scale, numberOfNodes, nodeVelocity, false, pointSize);
            scholarsDayEnvironments[19] = LoadGraphicalAnalysis(quadraticSquareFunction = new QuadraticSquareFunction(startTime, finalTime, steps, scale, graphicalAnalysisPosition, white, functionPointSize, 3.2f), scale, numberOfNodes, nodeVelocity, false, pointSize);
            scholarsDayEnvironments[20] = LoadGraphicalAnalysis(quadraticSquareFunction = new QuadraticSquareFunction(startTime, finalTime, steps, scale, graphicalAnalysisPosition, white, functionPointSize, 3.5f), scale, numberOfNodes, nodeVelocity, false, pointSize);
            scholarsDayEnvironments[21] = LoadGraphicalAnalysis(quadraticSquareFunction = new QuadraticSquareFunction(startTime, finalTime, steps, scale, graphicalAnalysisPosition, white, functionPointSize, 3.839f), scale, numberOfNodes, nodeVelocity, false, pointSize);
            scholarsDayEnvironments[22] = LoadGraphicalAnalysis(quadraticSquareFunction = new QuadraticSquareFunction(startTime, finalTime, steps, scale, graphicalAnalysisPosition, white, functionPointSize, 4.5f), scale, numberOfNodes, nodeVelocity, false, pointSize);


            //watch points escape without the chaos tracker:
            scholarsDayEnvironments[10] = LoadGraphicalAnalysis(quadraticFunction = new QuadraticFunction(startTime, finalTime, steps, scale, graphicalAnalysisPosition, white, functionPointSize, 5), scale, 1000, nodeVelocity, false, pointSize);

            //watch points escape with the chaos tracker:
            scholarsDayEnvironments[11] = LoadGraphicalAnalysis(quadraticFunction = new QuadraticFunction(startTime, finalTime, steps, scale, graphicalAnalysisPosition, white, functionPointSize, 5), scale, 10000, nodeVelocity, true, pointSize);

            //Mandelbrot set:
            scholarsDayEnvironments[12] = LoadMandelbrotFractal();

            //Lorenz:
            //scholarsDayEnvironments[13] = LoadLorenzDataSet(lorenzPosition, 10, 10, 10, 0, 100, 30000, 10, 30, 8f / 3f);
            //scholarsDayEnvironments[13] = LoadDoublePendulumDataSet(new Vector3(0, 0, -10), 0.5f, 0.5f, 2, 1, 0, 10, 1000, 1, 1, 2, 2);
            //scholarsDayEnvironments[11] = LoadGraphicalAnalysis(quadraticFunction = new QuadraticFunction(startTime, finalTime, steps, scale, graphicalAnalysisPosition, white, functionPointSize, 4), scale, numberOfNodes, nodeVelocity, false, pointSize);
            scholarsDayEnvironments[13] = LoadBifurcationDiagramDataSet(new Vector3(-3, 0, -1), new Vector3(0, 0, 0), new QuadraticFunction(0, 1, 1000, 5, new Vector3(-3, 0, -1), new Vector3(0, 0, 0), 1, 1), 1f, 4f, 10000, 0.75f, 400, 500, 1);

            return scholarsDayEnvironments;
        }




        private Environment LoadGraphicalAnalysis(Function iterativeFunction, float scale, int numberOfNodes, float nodeVelocity, bool trackChaos, int pointSize)
        {
            iterativeFunction.GetRawModel(loader);
            GraphicalAnalysis graphicalAnalysis = new GraphicalAnalysis(loader, iterativeFunction, numberOfNodes, nodeVelocity, trackChaos, pointSize);
            graphicalAnalysis.GetProjectionInformation(camera, MasterRenderer.fieldOfView, width, height);
            return graphicalAnalysis;
        }

        private Environment LoadMandelbrotFractal()
        {
            return new FractalViewer(loader, width, height);
        }

        private Environment LoadMandelbrotFractalDouble()
        {
            return new FractalViewerDouble(loader, width, height);
        }

        private Environment LoadLorenzDataSet(Vector3 position, Vector3 color, float x0, float y0, float z0, float t0, float tf, int steps, float s, float p, float b)
        {
            return new DataSet(loader, position, color, PrimitiveType.LineStrip, Mathematics.DifferentialEquations.LorenzSystem.Integrate(x0, y0, z0, t0, tf, steps, s, p, b));
        }

        private Environment LoadDoublePendulumDataSet(Vector3 position, Vector3 color, float theta0, float theta1, float angularVelocity0, float angularVelocity1, float t0, float tf, int steps, float mass0, float mass1, float length0, float length1, float g)
        {
            List<Vector4> coordinates = Mathematics.DifferentialEquations.DoublePendulum.Integrate(theta0, theta1, angularVelocity0, angularVelocity1, t0, tf, steps, mass0, mass1, length0, length1, g);
            List<Vector3> coordinatesSwizzled = new List<Vector3>();
            for (int i = 0; i < coordinates.Count; i++)
            {
                Vector3 coordinateSwizzled = new Vector3(coordinates[i].X, coordinates[i].Y, position.Z);
                coordinatesSwizzled.Add(coordinateSwizzled);
            }

            return new DataSet(loader, position, color, PrimitiveType.LineStrip, coordinatesSwizzled);
        }

        private Environment LoadBifurcationDiagramDataSet(Vector3 position, Vector3 color, QuadraticFunction quadraticFunction, float muMin, float muMax, int steps, float orbitPoint, int minOrbits, int maxOrbits, float scale)
        {
            return new DataSet(loader, position, color, PrimitiveType.Points, Mathematics.BifurcationDiagram.GetOrbit(quadraticFunction, muMin, muMax, steps, orbitPoint, minOrbits, maxOrbits, scale));
        }

        private void MouseWheel(object sender, MouseWheelEventArgs e)
        {
            mouseWheelDelta = e.Delta;
        }



        //when the window is resized.
        private void OnResize(object sender, EventArgs e)
        {
            GL.Viewport(0, 0, width, height); //set the Viewport to the same size of the window.
        }
        
        //when the frame updates, based upon framerate.
        private void OnUpdateFrame(object sender, FrameEventArgs e)
        {
            camera.Move();
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetCursorState();
            mousePosition = display.PointToClient(new Point(mouseState.X, mouseState.Y)); //gets the position of the mouse with (0, 0) being the top left of the window.
            
            ManageInput(keyboardState, mouseState);

            currentEnvironment.OnUpdateFrame(keyboardState, mouseState, mouseWheelDelta, mousePosition);
        }
        
        //when the frame is rendered, fixed timestep.
        private void OnRenderFrame(object sender, FrameEventArgs e)
        {
            //game logic:

            //render stuff:
            currentEnvironment.OnRenderFrame(masterRenderer);

            masterRenderer.Render(light, camera);

            //finish up rendering:
            display.SwapBuffers();
            mouseWheelDelta = 0;
        }

        //when the window is closed.
        private void OnClosed(object sender, EventArgs e)
        {
            CleanUp(); //clean up everything.

            display.Dispose(); //dispose of the display.
        }


        /* METHODS */
        //call all objects' CleanUp() methods here.
        private void CleanUp()
        {
            masterRenderer.CleanUp();
            loader.CleanUp();
        }

        

        private void ManageInput(KeyboardState keyboardState, MouseState mouseState)
        {
            //we cannot expect everyone that uses this to have quick fingers, so a key press is normally registered for multiple frame updates--resulting in multiple calculations for no reason.
            //avoid this issue by keeping track of the last key pressed.
            Key lastKeyPressed = Key.Escape;

            //environment switcher:
            if (keyboardState.IsKeyDown(Key.Number1) && lastKeyPressed != Key.Number1 && environments.Length > 0)
            {
                lastKeyPressed = Key.Number1;
                currentEnvironment.CloseEnvironment();
                currentEnvironment = environments[0];
                currentEnvironment.OnLoad();
            }
            else if (keyboardState.IsKeyDown(Key.Number2) && lastKeyPressed != Key.Number2 && environments.Length > 1)
            {
                lastKeyPressed = Key.Number2;
                currentEnvironment.CloseEnvironment();
                currentEnvironment = environments[1];
                currentEnvironment.OnLoad();
            }
            else if (keyboardState.IsKeyDown(Key.Number3) && lastKeyPressed != Key.Number3 && environments.Length > 2)
            {
                lastKeyPressed = Key.Number3;
                currentEnvironment.CloseEnvironment();
                currentEnvironment = environments[2];
                currentEnvironment.OnLoad();
            }
            else if (keyboardState.IsKeyDown(Key.Number4) && lastKeyPressed != Key.Number4 && environments.Length > 3)
            {
                lastKeyPressed = Key.Number4;
                currentEnvironment.CloseEnvironment();
                currentEnvironment = environments[3];
                currentEnvironment.OnLoad();
            }
            else if (keyboardState.IsKeyDown(Key.Number5) && lastKeyPressed != Key.Number5 && environments.Length > 4)
            {
                lastKeyPressed = Key.Number5;
                currentEnvironment.CloseEnvironment();
                currentEnvironment = environments[4];
                currentEnvironment.OnLoad();
            }
            else if (keyboardState.IsKeyDown(Key.Number6) && lastKeyPressed != Key.Number6 && environments.Length > 5)
            {
                lastKeyPressed = Key.Number6;
                currentEnvironment.CloseEnvironment();
                currentEnvironment = environments[5];
                currentEnvironment.OnLoad();
            }
            else if (keyboardState.IsKeyDown(Key.Number7) && lastKeyPressed != Key.Number7 && environments.Length > 6)
            {
                lastKeyPressed = Key.Number7;
                currentEnvironment.CloseEnvironment();
                currentEnvironment = environments[6];
                currentEnvironment.OnLoad();
            }
            else if (keyboardState.IsKeyDown(Key.Number8) && lastKeyPressed != Key.Number8 && environments.Length > 7)
            {
                lastKeyPressed = Key.Number8;
                currentEnvironment.CloseEnvironment();
                currentEnvironment = environments[7];
                currentEnvironment.OnLoad();
            }
            else if (keyboardState.IsKeyDown(Key.Number9) && lastKeyPressed != Key.Number9 && environments.Length > 8)
            {
                lastKeyPressed = Key.Number9;
                currentEnvironment.CloseEnvironment();
                currentEnvironment = environments[8];
                currentEnvironment.OnLoad();
            }
            else if (keyboardState.IsKeyDown(Key.Number0) && lastKeyPressed != Key.Number0 && environments.Length > 9)
            {
                lastKeyPressed = Key.Number0;
                currentEnvironment.CloseEnvironment();
                currentEnvironment = environments[9];
                currentEnvironment.OnLoad();
            }
            else if (keyboardState.IsKeyDown(Key.T) && lastKeyPressed != Key.T && environments.Length > 10)
            {
                lastKeyPressed = Key.T;
                currentEnvironment.CloseEnvironment();
                currentEnvironment = environments[10];
                currentEnvironment.OnLoad();
            }
            else if (keyboardState.IsKeyDown(Key.Y) && lastKeyPressed != Key.Y && environments.Length > 11)
            {
                lastKeyPressed = Key.Y;
                currentEnvironment.CloseEnvironment();
                currentEnvironment = environments[11];
                currentEnvironment.OnLoad();
            }
            else if (keyboardState.IsKeyDown(Key.U) && lastKeyPressed != Key.U && environments.Length > 12)
            {
                lastKeyPressed = Key.U;
                currentEnvironment.CloseEnvironment();
                currentEnvironment = environments[12];
                currentEnvironment.OnLoad();
            }
            else if (keyboardState.IsKeyDown(Key.I) && lastKeyPressed != Key.I && environments.Length > 13)
            {
                lastKeyPressed = Key.I;
                currentEnvironment.CloseEnvironment();
                currentEnvironment = environments[13];
                currentEnvironment.OnLoad();
            }


            if (keyboardState.IsKeyDown(Key.Keypad1) && lastKeyPressed != Key.Keypad1 && environments.Length > 14)
            {
                lastKeyPressed = Key.Keypad1;
                currentEnvironment.CloseEnvironment();
                currentEnvironment = environments[14];
                currentEnvironment.OnLoad();
            }
            else if (keyboardState.IsKeyDown(Key.Keypad2) && lastKeyPressed != Key.Keypad2 && environments.Length > 15)
            {
                lastKeyPressed = Key.Keypad2;
                currentEnvironment.CloseEnvironment();
                currentEnvironment = environments[15];
                currentEnvironment.OnLoad();
            }
            else if (keyboardState.IsKeyDown(Key.Keypad3) && lastKeyPressed != Key.Keypad3 && environments.Length > 16)
            {
                lastKeyPressed = Key.Keypad3;
                currentEnvironment.CloseEnvironment();
                currentEnvironment = environments[16];
                currentEnvironment.OnLoad();
            }
            else if (keyboardState.IsKeyDown(Key.Keypad4) && lastKeyPressed != Key.Keypad4 && environments.Length > 17)
            {
                lastKeyPressed = Key.Keypad4;
                currentEnvironment.CloseEnvironment();
                currentEnvironment = environments[17];
                currentEnvironment.OnLoad();
            }
            else if (keyboardState.IsKeyDown(Key.Keypad5) && lastKeyPressed != Key.Keypad5 && environments.Length > 18)
            {
                lastKeyPressed = Key.Keypad5;
                currentEnvironment.CloseEnvironment();
                currentEnvironment = environments[18];
                currentEnvironment.OnLoad();
            }
            else if (keyboardState.IsKeyDown(Key.Keypad6) && lastKeyPressed != Key.Keypad6 && environments.Length > 19)
            {
                lastKeyPressed = Key.Keypad6;
                currentEnvironment.CloseEnvironment();
                currentEnvironment = environments[19];
                currentEnvironment.OnLoad();
            }
            else if (keyboardState.IsKeyDown(Key.Keypad7) && lastKeyPressed != Key.Keypad7 && environments.Length > 20)
            {
                lastKeyPressed = Key.Keypad7;
                currentEnvironment.CloseEnvironment();
                currentEnvironment = environments[20];
                currentEnvironment.OnLoad();
            }
            else if (keyboardState.IsKeyDown(Key.Keypad8) && lastKeyPressed != Key.Keypad8 && environments.Length > 21)
            {
                lastKeyPressed = Key.Keypad8;
                currentEnvironment.CloseEnvironment();
                currentEnvironment = environments[21];
                currentEnvironment.OnLoad();
            }
            else if (keyboardState.IsKeyDown(Key.Keypad9) && lastKeyPressed != Key.Keypad9 && environments.Length > 22)
            {
                lastKeyPressed = Key.Keypad9;
                currentEnvironment.CloseEnvironment();
                currentEnvironment = environments[22];
                currentEnvironment.OnLoad();
            }


            //close the display: end the program.
            if (keyboardState.IsKeyDown(Key.Escape))
            {
                display.Close();
            }
        }
    }
}

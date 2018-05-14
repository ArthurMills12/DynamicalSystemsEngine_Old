using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace Calculator
{
    //handles rendering for everything. 
    class MasterRenderer
    {
        /* FIELDS AND PROPERTIES */
        private NodeRenderer nodeRenderer;
        
        //projection matrix:
        private Matrix4 projectionMatrix;
        public static float fieldOfView = Convert.ToSingle(Math.PI / 3f); //field of view angle for the frustrum.
        public static float nearPlane = 0.1f; //location of the nearest objects viewable by the projection matrix.
        public static float farPlane = 1000; //location of the furthest objects viewable by the projection matrix.
        
        //functions:
        private FunctionRenderer functionRenderer;
        private GraphingShader graphingShader;
        private List<Function> functions;

        //nodes:
        private List<Node> nodesToRender;

        //fractals:
        private MandelbrotRenderer mandelbrotRenderer;
        private MandelbrotShader mandelbrotShader;
        private MandelbrotShaderDouble mandelbrotShaderDouble;
        private List<Fractal> fractals;
        private List<FractalDouble> fractalDoubles;

        //data:
        private List<Data> data;

        //input:
        private Vector2 mousePosition;
        private float mouseWheelDelta;

        /* CONSTRUCTORS */
        public MasterRenderer()
        {
            //we should not bother rendering triangles whose normal vectors are pointing away from the camera. we will "cull" those faces here.
            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Back);

            //initialize shader and projection matrix:
            projectionMatrix = CreateProjectionMatrix();

            //initialize function stuff:
            this.graphingShader = new GraphingShader();
            this.functionRenderer = new FunctionRenderer(graphingShader, projectionMatrix);
            functions = new List<Function>();

            //initialize node stuff:
            this.nodeRenderer = new NodeRenderer(graphingShader, projectionMatrix);
            nodesToRender = new List<Node>();

            //initialize fractal stuff:
            this.mandelbrotShader = new MandelbrotShader();
            this.mandelbrotShaderDouble = new MandelbrotShaderDouble();
            this.mandelbrotRenderer = new MandelbrotRenderer(mandelbrotShader, mandelbrotShaderDouble);
            this.fractals = new List<Fractal>();
            this.fractalDoubles = new List<FractalDouble>();

            //initialize data stuff:
            this.data = new List<Data>();
        }


        /* METHODS */
        //prepare for rendering by clearing the colors on the screen.
        public void Prepare()
        {
            //additional rendering specifications:
            GL.Enable(EnableCap.DepthTest); //check z-values of objects and render the ones closest to the camera.
            GL.Enable(EnableCap.VertexProgramPointSize); //allows the shaders to access the gl_PointSize variable, changing the size of point primitives.

            //clear the color of the screen:
            GL.ClearColor(0, 0, 0, 1);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
        }

        private Matrix4 CreateProjectionMatrix()
        {
            //create the projection matrix using the fields above.
            return Matrix4.CreatePerspectiveFieldOfView(fieldOfView, (float)Display.width / (float)Display.height, nearPlane, farPlane);
        }

        public void Render(Light light, Camera camera)
        {
            //rendering properties:
            Prepare();

            //graphing shader: functions, nodes, data.
            graphingShader.Start();
            graphingShader.LoadViewMatrix(camera);
            functionRenderer.Render(functions);
            functionRenderer.Render(data);
            nodeRenderer.Render(nodesToRender);
            graphingShader.Stop();
            
            //mandelbrot shader: mandelbrot fractal.
            mandelbrotShader.Start();
            mandelbrotRenderer.Render(mousePosition, mouseWheelDelta, fractals);
            mandelbrotShader.Stop();

            //mandelbrot shader for double precision:
            mandelbrotShaderDouble.Start();
            mandelbrotRenderer.Render(mousePosition, mouseWheelDelta, fractalDoubles);
            mandelbrotShaderDouble.Stop();

            //CLEAR THE LISTS!
            nodesToRender.Clear();
            functions.Clear();
            fractals.Clear();
            fractalDoubles.Clear();
            data.Clear();
        }
        

        public void ProcessNode(params Node[] nodes)
        {
            foreach (Node node in nodes)
            {
                this.nodesToRender.Add(node);
            }
        }

        public void ProcessFunctions(params Function[] functions)
        {
            foreach (Function function in functions)
            {
                this.functions.Add(function);
            }
        }

        public void ProcessFractal(Vector2 mousePosition, float mouseWheelDelta, params Fractal[] fractals)
        {
            this.mousePosition = mousePosition;
            this.mouseWheelDelta = mouseWheelDelta;

            foreach (Fractal fractal in fractals)
            {
                this.fractals.Add(fractal);
            }
        }
        public void ProcessFractal(Vector2 mousePosition, float mouseWheelDelta, params FractalDouble[] fractalDoubles)
        {
            this.mousePosition = mousePosition;
            this.mouseWheelDelta = mouseWheelDelta;

            foreach (FractalDouble fractalDouble in fractalDoubles)
            {
                this.fractalDoubles.Add(fractalDouble);
            }
        }

        public void ProcessGraphicalAnalysis(params GraphicalAnalysis[] graphicalAnalyses)
        {
            foreach (GraphicalAnalysis graphicalAnalysis in graphicalAnalyses)
            {
                ProcessFunctions(graphicalAnalysis.iterativeFunction, graphicalAnalysis.linearFunction);
                ProcessNode(graphicalAnalysis.nodes.ToArray());
                if (graphicalAnalysis.trackChaos)
                {
                    ProcessNode(graphicalAnalysis.staticNodes.ToArray());
                }
                ProcessNode(graphicalAnalysis.customNodes.ToArray());
            }
        }

        public void ProcessDataSet(params DataSet[] dataSets)
        {
            foreach (DataSet dataSet in dataSets)
            {
                this.data.Add(dataSet.data);
            }
        }
        
        public void CleanUp()
        {
            graphingShader.CleanUp();
            mandelbrotShader.CleanUp();
            mandelbrotShaderDouble.CleanUp();
        }
    }
}

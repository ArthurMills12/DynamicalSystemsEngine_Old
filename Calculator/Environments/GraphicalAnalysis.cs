using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Input;
using System.Drawing;

namespace Calculator
{
    //manages an animation of graphical analysis for a given function.
    class GraphicalAnalysis : Environment
    {
        public Function iterativeFunction { get; private set; } //function to iterate.
        public Function linearFunction { get; private set; } //the line y=x.
        public List<AnimatedNode> nodes { get; private set; } //list of the nodes whose orbits will be calculated.
        public List<Node> staticNodes { get; private set; } //list of static nodes that will be used to keep track of chaos.
        public List<AnimatedNode> customNodes { get; private set; }
        private Loader loader;
        private int numberOfNodes;
        private float nodeVelocity;
        private float nodeVelocityRange;
        private float staticNodeScreenHeight = -1;
        private int pointSize = 1;
        private bool begin = false;
        public bool trackChaos = false;

        //concerning the screen/projection:
        Camera camera;
        float fieldOfView;
        int width;
        int height;

        
        public GraphicalAnalysis(Loader loader, Function iterativeFunction, int numberOfNodes, float nodeVelocity, bool trackChaos, int pointSize, float nodeVelocityRange = 0)
        {
            this.loader = loader;
            this.numberOfNodes = numberOfNodes;
            this.nodeVelocity = nodeVelocity;
            this.nodeVelocityRange = nodeVelocityRange;
            this.trackChaos = trackChaos;
            this.pointSize = pointSize;
            this.customNodes = new List<AnimatedNode>();

            linearFunction = new LinearFunction(iterativeFunction.startTime, iterativeFunction.finalTime, iterativeFunction.steps, iterativeFunction.scale, iterativeFunction.position, iterativeFunction.color, iterativeFunction.pointSize, 1, 0);
            linearFunction.GetRawModel(this.loader);

            this.iterativeFunction = iterativeFunction;
            nodes = new List<AnimatedNode>();
            
            OnLoad();
        }

        
        
        //inherited:
        public override void OnLoad()
        {
            GetNodes(this.loader, iterativeFunction.startTime, iterativeFunction.finalTime, numberOfNodes, nodeVelocity, this.nodeVelocityRange);
            if (trackChaos)
            {
                GetStaticNodes(this.loader);
            }
        }

        public void GetProjectionInformation(Camera camera, float fieldOfView, int width, int height)
        {
            this.camera = camera;
            this.fieldOfView = fieldOfView;
            this.width = width;
            this.height = height;
        }

        public override void OnUpdateFrame(KeyboardState keyboardState, MouseState mouseState, float mouseWheelDelta, Point mousePosition)
        {
            ManageInput(keyboardState, mouseState, mouseWheelDelta, mousePosition);

            Move(begin);
        }

        public override void OnRenderFrame(MasterRenderer masterRenderer)
        {
            masterRenderer.ProcessGraphicalAnalysis(this);
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
            if (mouseState.IsButtonDown(MouseButton.Left) && mousePosition != null)
            {
                Vector3 mousePositionCartesianCoordinates = GetMousePositionCartesianCoordinates(GetMousePositionUnitCoordinates(mousePosition));
                CreateNode(loader, mousePositionCartesianCoordinates, new Vector3(0, 1, 0));
            }
        }

        public override void CloseEnvironment()
        {
            nodes.Clear();
            if (staticNodes != null)
            {
                staticNodes.Clear();
            }
            customNodes.Clear();
            begin = false;
        }
        

        private void GetNodes(Loader loader, float startTime, float finalTime, int numberOfNodes, float nodeVelocity, float nodeVelocityRange = 0)
        {
            Random random = new Random();
            
            for (int i = 0; i < numberOfNodes; i++)
            {
                float velocity = nodeVelocity + (nodeVelocityRange * random.Next(-1, 2));

                AnimatedNode node = new AnimatedNode(iterativeFunction, linearFunction, (float)startTime + i * (finalTime - startTime) / numberOfNodes, velocity, i, new Vector3(1, 0, 1), pointSize);
                if (trackChaos)
                {
                    node.NodeDied += KillNode;
                }
                node.GetRawModel(loader);
                nodes.Add(node);
            }
        }
        
        private void GetStaticNodes(Loader loader)
        {
            staticNodes = new List<Node>();
            for (int i = 0; i < nodes.Count; i++)
            {
                Node node = new Node(new Vector3(nodes[i].position.X, staticNodeScreenHeight, nodes[i].position.Z), new Vector3(1, 1, 1), pointSize);
                node.GetRawModel(loader);
                staticNodes.Add(node);
            }
        }
        
        private void CreateNode(Loader loader, Vector3 mousePositionCartesianCoordinates, Vector3 color)
        {
            AnimatedNode node = new AnimatedNode(iterativeFunction, linearFunction, mousePositionCartesianCoordinates.X / iterativeFunction.scale, nodeVelocity, nodes.Count, color, pointSize);
            node.GetRawModel(loader);
            customNodes.Add(node);
        }
        
        
        private Vector3 GetMousePositionCartesianCoordinates(Vector3 mousePositionUnitCoordinates)
        {
            //we will get the mouse position by linearly interpolating between the cartesian coordinates of the frustrum edges. we will consider the plane of the frustrum at the graphical analysis location.
            float zDistance = Math.Abs(camera.position.Z - iterativeFunction.position.Z);

            //frustrum width, height:
            float frustrumHalfHeight = zDistance * Convert.ToSingle(Math.Tan(fieldOfView * 0.5f));
            float frustrumHalfWidth = frustrumHalfHeight * ((float)width / height);

            float leftScreenEdge = camera.position.X - frustrumHalfWidth;
            float rightScreenEdge = camera.position.X + frustrumHalfWidth;
            float bottomScreenEdge = camera.position.Y - frustrumHalfHeight;
            float topScreenEdge = camera.position.Y + frustrumHalfHeight;

            return new Vector3(Mathematics.Lerp(mousePositionUnitCoordinates.X, leftScreenEdge, rightScreenEdge), -Mathematics.Lerp(mousePositionUnitCoordinates.Y, bottomScreenEdge, topScreenEdge), 0);
        }




        private Vector3 GetMousePositionUnitCoordinates(Point mousePosition)
        {
            float mousePositionInverseLerpX = Mathematics.InverseLerp(mousePosition.X, 0, width);
            float mousePositionInverseLerpY = Mathematics.InverseLerp(mousePosition.Y, 0, height);
            //return new Vector3(Mathematics.Lerp(mousePositionInverseLerpX, -1, 1), -Mathematics.Lerp(mousePositionInverseLerpY, -1, 1), 0);
            return new Vector3(mousePositionInverseLerpX, mousePositionInverseLerpY, 0);
        }


        public void Move(bool begin)
        {
            if (begin)
            {
                foreach (AnimatedNode node in nodes)
                {
                    node.Move();
                }
            }
            foreach (AnimatedNode customNode in customNodes)
            {
                customNode.Move();
            }
        }

        public void Reset()
        {
            CloseEnvironment();
            /*
            GetNodes(this.loader, iterativeFunction.startTime, iterativeFunction.finalTime, numberOfNodes, nodeVelocity, this.nodeVelocityRange);
            GetStaticNodes(this.loader);*/
            OnLoad();
        }

        
        public void KillNode(int index, int orbitIndex)
        {
            //AnimatedNode deadNode = nodes[index];
            //deadNode.position = staticNodes[index].position;
            //staticNodes[index].color = new Vector3(1f / orbitIndex, Convert.ToSingle(Math.Abs(Math.Cos(orbitIndex))), Convert.ToSingle(Math.Abs(Math.Sin(orbitIndex))));
            staticNodes[index].color = GetColor(orbitIndex);
        }
        
        public Vector3 GetColor(int iterations)
        {
            Vector3 color;

            if (iterations == 1)
            {
                color = new Vector3(1, 0, 0);
            }
            else
            {
                color = new Vector3(1f / iterations, Convert.ToSingle(Math.Abs(Math.Cos(iterations))), Convert.ToSingle(Math.Abs(Math.Sin(iterations))));
            }

            return color;
        }
    }
}

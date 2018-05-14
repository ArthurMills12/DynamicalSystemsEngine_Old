using System;
using System.Collections.Generic;
using OpenTK;

namespace Calculator
{
    //each vertex that will be moving around for the graphical analysis of a map.
    class AnimatedNode : Node
    {
        /* FIELDS AND PROPERTIES */
        private Vector3 _destinationPoint;
        public Vector3 destinationPoint { get => _destinationPoint; set => _destinationPoint = value; }

        private float[] _domain;
        public float[] domain { get => _domain; private set => _domain = value; }

        private int _orbitIndex = 0;
        public int orbitIndex { get => _orbitIndex; set => _orbitIndex = value; }

        public int nodeIndex { get; }

        private float tolerance { get; set; }

        private Vector3 deadZone = new Vector3(-100, -100, 100);
        
        private bool _evaluating;
        public bool evaluating { get => _evaluating; set => _evaluating = value; }

        private float _velocity; //define this in here so that the Ray class can set this velocity to be zero, in which case this node must be still.
        public float velocity { get => _velocity; private set => _velocity = value; }

        private Function _iterativeFunction; //such as F_\mu(x) = \mu*x*(1-x).
        public Function iterativeFunction { get => _iterativeFunction; private set => _iterativeFunction = value; }

        private Function _returnFunction; //this should, in general, be set to f(x) = x.
        public Function returnFunction { get => _returnFunction; private set => _returnFunction = value; }

        private float scale { get => iterativeFunction.scale; }

        //events:
        public delegate void DelDeath(int index, int orbitIndex);
        public event DelDeath NodeDied;

        /* CONSTRUCTORS */
        public AnimatedNode(Function iterativeFunction, Function returnFunction, float x0, float velocity, int nodeIndex, Vector3 color, int pointSize) : base(new Vector3(0, 0, 0), color, pointSize)
        {
            //initialization:
            this.iterativeFunction = iterativeFunction;
            this.returnFunction = returnFunction;
            this.velocity = velocity;
            this.nodeIndex = nodeIndex;
            this.color = color;
            this.pointSize = pointSize;
            evaluating = true;
            tolerance = 0.05f;

            //set domain:
            domain = new float[2] { this.scale * this.returnFunction.Image(returnFunction.startTime), this.scale * this.returnFunction.Image(returnFunction.finalTime) };

            //get initial position and set destination.
            base.position = new Vector3(scale * x0, scale * x0, iterativeFunction.position.Z);
            this.destinationPoint = GetDestinationPoint(evaluating, position);
        }


        /* METHODS */
        private Vector3 GetDestinationPoint(bool evaluating, Vector3 currentPosition)
        {
            Vector3 destination;
            if (evaluating) //then we want the point to move towards its image at point (x, f(x)).
            {
                destination = new Vector3(currentPosition.X, scale * iterativeFunction.Image(currentPosition.X / scale), currentPosition.Z);
            }
            else //then the destination needs to be the line y=x at point (f(x), f(x)).
            {
                destination = new Vector3(scale * returnFunction.Image(currentPosition.Y / scale), currentPosition.Y, currentPosition.Z);
            }
            
            return destination;
        }
        


        public void Move()
        {
            if (evaluating) //then we are moving in the vertical direction.
            {
                float yDifference = destinationPoint.Y - position.Y;
                int direction = (yDifference >= 0) ? 1 : -1; //if positive, move upwards. if negative, move downwards so we will multiply -1 to velocity.

                if (Math.Abs(yDifference) < tolerance) //then we are close enough to the destination point to choose a new destination.
                {
                    orbitIndex++;
                    position = destinationPoint;
                    evaluating = false;
                    destinationPoint = GetDestinationPoint(evaluating, position);
                }
                else //then we need to move towards the destination point.
                {
                    position += new Vector3(0, direction * velocity, 0);
                }
            }
            else //then we are moving in the horizontal direction.
            {
                float xDifference = destinationPoint.X - position.X;
                int direction = (xDifference >= 0) ? 1 : -1; //if positive, move right. if negative, move left so we will multiply -1 to velocity.

                if (Math.Abs(xDifference) < tolerance) //then we are close enough to the destination point to choose a new destination.
                {
                    orbitIndex++;
                    position = destinationPoint;
                    evaluating = true;
                    destinationPoint = GetDestinationPoint(evaluating, position);
                }
                else //then we need to move towards the destination point.
                {
                    position += new Vector3(direction * velocity, 0, 0);

                    if (position.X > domain[1] || position.X < domain[0])
                    {
                        if (NodeDied != null)
                        {
                            NodeDied(nodeIndex, orbitIndex);
                        }
                        Die();
                    }
                }
            }
        }

        public void Die()
        {
            velocity = 0;
            position = deadZone;
        }
    }
}

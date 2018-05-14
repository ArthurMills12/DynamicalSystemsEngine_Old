using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Calculator
{
    class SineFunction : Function
    {
        /* FIELDS AND PROPERTIES */
        private float _omega;
        public float omega { get => _omega; set => _omega = value; }


        public SineFunction(float startTime, float finalTime, int steps, float scale, Vector3 position, Vector3 color, int pointSize, float omega) : base(startTime, finalTime, steps, scale, position, color, pointSize)
        {
            this.omega = omega;
        }

        public override float Image(float x)
        {
            return Convert.ToSingle(Math.Sin(omega * x));
        }
    }
}

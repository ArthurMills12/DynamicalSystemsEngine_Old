using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    //an object that implements IRenderable must have things that let the object be rendered.
    interface IRenderable
    {
        RawModel rawModel { get; set; }

        void GetRawModel(Loader loader);
    }
}

using System;
using System.Collections.Generic;
using OpenTK;

namespace Calculator
{
    class MandelbrotShaderDouble : ShaderProgram
    {
        /* PROPERTIES */
        private static string vertexShaderFile = Properties.Resources.mandelbrotVertexShaderDouble;
        private static string fragmentShaderFile = Properties.Resources.mandelbrotFragmentShaderDouble;

        private int locationRow0;
        private int locationRow1;
        private int locationRow2;
        private int locationRow3;

        /* CONSTRUCTORS */
        public MandelbrotShaderDouble() : base(vertexShaderFile, fragmentShaderFile)
        {

        }


        /* METHODS */
        //bind our desired attributes.
        protected override void BindAttributes()
        {
            BindAttribute(0, "position");
            BindAttribute(1, "complex");
        }

        //figure out where are desired uniforms are located in the shaders.
        protected override void GetAllUniformLocations()
        {
            locationRow0 = GetUniformLocation("row0");
            locationRow1 = GetUniformLocation("row1");
            locationRow2 = GetUniformLocation("row2");
            locationRow3 = GetUniformLocation("row3");
        }

        public void LoadTransformationMatrix(Matrix4d transformationMatrix)
        {
            LoadUniform(locationRow0, transformationMatrix.Row0);
            LoadUniform(locationRow1, transformationMatrix.Row1);
            LoadUniform(locationRow2, transformationMatrix.Row2);
            LoadUniform(locationRow3, transformationMatrix.Row3);

        }
    }
}

using System;
using System.Collections.Generic;
using OpenTK;

namespace Calculator
{
    class MandelbrotShader : ShaderProgram
    {
        /* PROPERTIES */
        private static string vertexShaderFile = Properties.Resources.mandelbrotVertexShader;
        private static string fragmentShaderFile = Properties.Resources.mandelbrotFragmentShader;

        private int locationTransformationMatrix;

        /* CONSTRUCTORS */
        public MandelbrotShader() : base(vertexShaderFile, fragmentShaderFile)
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
            locationTransformationMatrix = GetUniformLocation("transformationMatrix");
        }

        public void LoadTransformationMatrix(Matrix4 transformationMatrix)
        {
            LoadUniform(locationTransformationMatrix, transformationMatrix);
        }
    }
}

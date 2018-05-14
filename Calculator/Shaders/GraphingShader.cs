using System;
using System.Collections.Generic;
using OpenTK;
using System.IO;

namespace Calculator
{
    class GraphingShader : ShaderProgram
    {
        /* FIELDS AND PROPERTIES */
        //shader files. reference them from the Resources folder. note that these are pure files, not filePaths.
        private static string vertexShaderFile = Properties.Resources.graphingVertexShader;
        private static string fragmentShaderFile = Properties.Resources.graphingFragmentShader;

        //uniform variables.
        private int locationTransformationMatrix;
        private int locationProjectionMatrix;
        private int locationViewMatrix;
        private int locationDynamicColor;

        /* CONSTRUCTORS */
        public GraphingShader() : base(vertexShaderFile, fragmentShaderFile)
        {

        }


        /* METHODS */
        //bind our desired attributes.
        protected override void BindAttributes()
        {
            BindAttribute(0, "position");
            BindAttribute(1, "color");
        }

        //figure out where are desired uniforms are located in the shaders.
        protected override void GetAllUniformLocations()
        {
            locationTransformationMatrix = GetUniformLocation("transformationMatrix");
            locationProjectionMatrix = GetUniformLocation("projectionMatrix");
            locationViewMatrix = GetUniformLocation("viewMatrix");
            locationDynamicColor = GetUniformLocation("dynamicColor");
        }

        //load our desired uniforms.
        public void LoadTransformationMatrix(Matrix4 transformationMatrix)
        {
            LoadUniform(locationTransformationMatrix, transformationMatrix);
        }
        public void LoadProjectionMatrix(Matrix4 projectionMatrix)
        {
            LoadUniform(locationProjectionMatrix, projectionMatrix);
        }
        public void LoadViewMatrix(Camera camera)
        {
            Matrix4 viewMatrix = Mathematics.CreateViewMatrix(camera);
            LoadUniform(locationViewMatrix, viewMatrix);
        }
        public void LoadDynamicColor(Vector3 dynamicColor)
        {
            LoadUniform(locationDynamicColor, dynamicColor);
        }
    }
}

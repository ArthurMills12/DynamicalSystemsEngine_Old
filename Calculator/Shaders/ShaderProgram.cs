using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using System.IO;

namespace Calculator
{
    public abstract class ShaderProgram
    {
        /* FIELDS AND PROPERTIES */
        private int programID;
        private int vertexShaderID;
        private int fragmentShaderID;
        

        /* CONSTRUCTORS */
        public ShaderProgram(string vertexShaderFile, string fragmentShaderFile)
        {
            //load up the two shaders.
            vertexShaderID = LoadShader(ShaderType.VertexShader, vertexShaderFile);
            fragmentShaderID = LoadShader(ShaderType.FragmentShader, fragmentShaderFile);

            //create the total shader program and attach the shaders to it.
            programID = GL.CreateProgram();
            GL.AttachShader(programID, vertexShaderID);
            GL.AttachShader(programID, fragmentShaderID);

            //prepare the shaders for use in the GPU.
            BindAttributes(); //bind all attributes.
            GL.LinkProgram(programID);
            GL.ValidateProgram(programID);
            GetAllUniformLocations(); //get locations of all uniform variables.
        }


        /* METHODS */
        //load the shader at the filePath.
        private static int LoadShader(ShaderType type, string textFile)
        {
            int shaderID = GL.CreateShader(type);
            try
            {
                GL.ShaderSource(shaderID, textFile); //find where the GLSL script actually is and link it to the ID.
                //GL.ShaderSource(shaderID, File.ReadAllText(textFile)); //find where the GLSL script actually is and link it to the ID.
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e);
            }
            return shaderID;
        }

        //figure out which location a shader program has stored the given uniform variable.
        protected int GetUniformLocation(string uniformName)
        {
            return GL.GetUniformLocation(programID, uniformName);
        }

        //any implementation of this class must know what uniform variables it uses so it can output their locations here.
        protected abstract void GetAllUniformLocations();

        //bind the given attribute to the shader program.
        protected void BindAttribute(int attribute, string variableName)
        {
            GL.BindAttribLocation(programID, attribute, variableName);
        }

        //similarly, any implementation of this class needs a way to bind the attributes that the shader needs. 
        protected abstract void BindAttributes();

        /* LOAD UNIFORM VARIABLES */
        #region Load Uniform Overloads:
        protected void LoadUniform(int location, float value)
        {
            GL.Uniform1(location, value);
        }
        protected void LoadUniform(int location, double value)
        {
            GL.Uniform1(location, value);
        }
        protected void LoadUniform(int location, int value)
        {
            GL.Uniform1(location, value);
        }
        protected void LoadUniform(int location, bool value)
        {
            GL.Uniform1(location, value ? 1 : 0); //note that GLSL does not have booleans. so if we want to send "true", we instead send 1. if we send "false", we will send 0.
        }
        protected void LoadUniform(int location, Vector2 value)
        {
            GL.Uniform2(location, value);
        }
        protected void LoadUniform(int location, Vector3 value)
        {
            GL.Uniform3(location, value);
        }
        protected void LoadUniform(int location, Vector4 value)
        {
            GL.Uniform4(location, value);
        }
        protected void LoadUniform(int location, Vector4d value)
        {
            GL.Uniform4(location, value.X, value.Y, value.Z, value.W);
        }
        protected void LoadUniform(int location, Matrix4 value)
        {
            GL.UniformMatrix4(location, false, ref value);
        }
        #endregion


        //use the shader.
        public void Start()
        {
            GL.UseProgram(programID);
        }

        //stop using the shader.
        public void Stop()
        {
            GL.UseProgram(0);
        }

        //dispose of everything.
        public void CleanUp()
        {
            Stop();
            GL.DetachShader(programID, vertexShaderID);
            GL.DetachShader(programID, fragmentShaderID);
            GL.DeleteShader(vertexShaderID);
            GL.DeleteShader(fragmentShaderID);
            GL.DeleteProgram(programID);
        }

        
    }
}

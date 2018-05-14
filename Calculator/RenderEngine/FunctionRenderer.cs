using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace Calculator
{
    class FunctionRenderer
    {
        /* FIELDS AND PROPERTIES */
        //shaders:
        private GraphingShader graphingShader;

        /* CONSTRUCTORS */
        public FunctionRenderer(GraphingShader graphingShader, Matrix4 projectionMatrix)
        {
            //initialization.
            this.graphingShader = graphingShader;


            //initialize the shader using the projectionMatrix.
            this.graphingShader.Start();
            this.graphingShader.LoadProjectionMatrix(projectionMatrix);
            this.graphingShader.Stop();
        }


        /* METHODS */
        //batch render based upon a dictionary.
        public void Render(List<Function> functions)
        {
            //go through all of these entities in the batch and render them.
            foreach (Function function in functions)
            {
                //instantiate the entity by rendering it.
                Initialize(function);
                PrepareInstance(function);
                //GL.DrawElements(BeginMode.LineLoop, function.texturedModel.rawModel.vertexCount, DrawElementsType.UnsignedInt, 0);
                GL.DrawArrays(PrimitiveType.LineStrip, 0, function.rawModel.vertexCount);
            }

            //now we are done with this textured model, so unbind it so we are ready to render the next one.
            Unbind();

        }
        public void Render(List<Data> data)
        {
            //go through all of these entities in the batch and render them.
            foreach (Data datum in data)
            {
                //instantiate the entity by rendering it.
                Initialize(datum);
                PrepareInstance(datum);
                //GL.DrawElements(BeginMode.LineLoop, function.texturedModel.rawModel.vertexCount, DrawElementsType.UnsignedInt, 0);
                GL.DrawArrays(datum.primitiveType, 0, datum.rawModel.vertexCount);
            }

            //now we are done with this textured model, so unbind it so we are ready to render the next one.
            Unbind();
        }


        //bind a texturedmodel to the GPU.
        private void Initialize(Function function)
        {
            RawModel model = function.rawModel;
            GL.BindVertexArray(model.vaoID); //bind the VAO of the model

            //enable vertex array lists:
            GL.EnableVertexAttribArray(0); //position.
            GL.EnableVertexAttribArray(1); //color.
        }
        private void Initialize(Data data)
        {
            RawModel model = data.rawModel;
            GL.BindVertexArray(model.vaoID); //bind the VAO of the model

            //enable vertex array lists:
            GL.EnableVertexAttribArray(0); //position.
            GL.EnableVertexAttribArray(1); //color.
        }

        //unbind the current textured model.
        private void Unbind()
        {
            //disable vertex array lists:
            GL.DisableVertexAttribArray(0); //after we draw, we are done using this.
            GL.DisableVertexAttribArray(1); //after we draw, we are done using this.

            GL.BindVertexArray(0); //unbind the VAO.
        }

        //prepare the shader for the given entity.
        private void PrepareInstance(Function function)
        {
            Transform transform = new Transform(function.position, 0, 0, 0, 1);
            Matrix4 transformationMatrix = Mathematics.CreateTransformationMatrix(transform.position, transform.xAxisRotation, transform.yAxisRotation, transform.zAxisRotation, transform.scale);
            graphingShader.LoadTransformationMatrix(transformationMatrix);
            graphingShader.LoadDynamicColor(function.color);
        }
        private void PrepareInstance(Data data)
        {
            Transform transform = new Transform(data.position, data.rotation.X, data.rotation.Y, data.rotation.Z, 1);
            Matrix4 transformationMatrix = Mathematics.CreateTransformationMatrix(transform.position, transform.xAxisRotation, transform.yAxisRotation, transform.zAxisRotation, transform.scale);
            
            graphingShader.LoadTransformationMatrix(transformationMatrix);
            graphingShader.LoadDynamicColor(new Vector3(1, 1, 1));
        }
    }
}
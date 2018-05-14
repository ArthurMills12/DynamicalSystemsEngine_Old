using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace Calculator
{
    //renders a list of nodes that serve as the animation rays.
    class NodeRenderer
    {
        /* FIELDS AND PROPERTIES */
        //shaders:
        private GraphingShader nodeShader;

        /* CONSTRUCTORS */
        public NodeRenderer(GraphingShader shader, Matrix4 projectionMatrix)
        {
            //initialization.
            this.nodeShader = shader;

            //initialize the shader using the projectionMatrix.
            shader.Start();
            shader.LoadProjectionMatrix(projectionMatrix);
            shader.Stop();
        }


        /* METHODS */
        //batch render based upon a dictionary.
        public void Render(List<Node> nodes)
        {
            //go through all of these entities in the batch and render them.
            foreach (Node node in nodes)
            {
                //instantiate the entity by rendering it.
                Initialize(node);
                PrepareInstance(node);
                //GL.DrawElements(BeginMode.LineLoop, function.texturedModel.rawModel.vertexCount, DrawElementsType.UnsignedInt, 0);
                GL.DrawArrays(PrimitiveType.Points, 0, 1);
            }
            //now we are done with this textured model, so unbind it so we are ready to render the next one.
            Unbind();
            
        }

        public void Render(List<Node> nodes, PrimitiveType primitiveType)
        {
            //go through all of these entities in the batch and render them.
            foreach (Node node in nodes)
            {
                //instantiate the entity by rendering it.
                Initialize(node);
                PrepareInstance(node);
                //GL.DrawElements(BeginMode.LineLoop, function.texturedModel.rawModel.vertexCount, DrawElementsType.UnsignedInt, 0);
                GL.DrawArrays(primitiveType, 0, 1);
            }

            //now we are done with this textured model, so unbind it so we are ready to render the next one.
            Unbind();
        }

        //bind a texturedmodel to the GPU.
        private void Initialize(Node node)
        {
            RawModel model = node.rawModel;
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
        private void PrepareInstance(Node node)
        {
            Transform transform = new Transform(node.position, 0, 0, 0, 1);
            Matrix4 transformationMatrix = Mathematics.CreateTransformationMatrix(transform.position, transform.xAxisRotation, transform.yAxisRotation, transform.zAxisRotation, transform.scale);
            nodeShader.LoadTransformationMatrix(transformationMatrix);
            nodeShader.LoadDynamicColor(node.color);
        }
    }
}

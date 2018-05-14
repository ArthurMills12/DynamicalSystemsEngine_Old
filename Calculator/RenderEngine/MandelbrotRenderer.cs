using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace Calculator
{
    class MandelbrotRenderer
    {
        /* FIELDS AND PROPERTIES */
        //shaders:
        private MandelbrotShader mandelbrotShader;
        private MandelbrotShaderDouble mandelbrotShaderDouble;

        //properties for zooming:
        private float stepSize = 1 / 1000f;
        private float mouseWheelDeltaMultiplier = 15;
        private float zoomMultiplier = 1 / 10f;

        /* CONSTRUCTORS */
        public MandelbrotRenderer(MandelbrotShader mandelbrotShader, MandelbrotShaderDouble mandelbrotShaderDouble)
        {
            //initialization.
            this.mandelbrotShader = mandelbrotShader;
            this.mandelbrotShaderDouble = mandelbrotShaderDouble;
        }


        /* METHODS */
        //batch render based upon a dictionary.
        public void Render(Vector2 mousePosition, float mouseWheelDelta, List<Fractal> fractals)
        {
            //go through all of these entities in the batch and render them.
            foreach (Fractal fractal in fractals)
            {
                //instantiate the entity by rendering it.
                Initialize(fractal);
                PrepareInstance(mousePosition, mouseWheelDelta, fractal);
                GL.DrawArrays(PrimitiveType.Quads, 0, fractal.rawModel.vertexCount);
            }

            //now we are done with this textured model, so unbind it so we are ready to render the next one.
            Unbind();
        }
        public void Render(Vector2 mousePosition, float mouseWheelDelta, List<FractalDouble> fractalDoubles)
        {
            //go through all of these entities in the batch and render them.
            foreach (FractalDouble fractalDouble in fractalDoubles)
            {
                //instantiate the entity by rendering it.
                Initialize(fractalDouble);
                PrepareInstance(mousePosition, mouseWheelDelta, fractalDouble);
                GL.DrawArrays(PrimitiveType.Quads, 0, fractalDouble.rawModel.vertexCount);
            }

            //now we are done with this textured model, so unbind it so we are ready to render the next one.
            Unbind();
        }

        //bind a texturedmodel to the GPU.
        private void Initialize(Fractal fractal)
        {
            RawModel model = fractal.rawModel;
            GL.BindVertexArray(model.vaoID); //bind the VAO of the model

            //enable vertex array lists:
            GL.EnableVertexAttribArray(0); //position.
            GL.EnableVertexAttribArray(1); //complex.
        }
        private void Initialize(FractalDouble fractalDouble)
        {
            RawModel model = fractalDouble.rawModel;
            GL.BindVertexArray(model.vaoID); //bind the VAO of the model

            //enable vertex array lists:
            GL.EnableVertexAttribArray(0); //position.
            GL.EnableVertexAttribArray(1); //complex.
        }

        //unbind the current textured model.
        private void Unbind()
        {
            //disable vertex array lists:
            GL.DisableVertexAttribArray(0); //after we draw, we are done using this.
            GL.DisableVertexAttribArray(1);

            GL.BindVertexArray(0); //unbind the VAO.
        }

        //determine and load a transformation matrix that manipulates the complex coordinates of the quad.
        private void PrepareInstance(Vector2 mousePositionUnitCoordinates, float mouseWheelDelta, Fractal fractal)
        {
            float stepPercent = 1 - (2 * mouseWheelDelta / 100f);
            Vector2 mousePositionComplex = new Vector2(Mathematics.Lerp(mousePositionUnitCoordinates.X, fractal.domain[0].X, fractal.domain[1].X), -Mathematics.Lerp(mousePositionUnitCoordinates.Y, fractal.domain[0].Y, fractal.domain[3].Y));

            

            Vector2 bottomLeft = Mathematics.Lerp(stepPercent, mousePositionComplex, fractal.domain[0]);
            Vector2 bottomRight = Mathematics.Lerp(stepPercent, mousePositionComplex, fractal.domain[1]);
            Vector2 topRight = Mathematics.Lerp(stepPercent, mousePositionComplex, fractal.domain[2]);
            Vector2 topLeft = Mathematics.Lerp(stepPercent, mousePositionComplex, fractal.domain[3]);

            float xScale = Math.Abs(bottomRight.X - bottomLeft.X);
            float yScale = Math.Abs(topLeft.Y - bottomLeft.Y);

            Matrix4 transformationMatrix = Mathematics.CreateTransformationMatrix(new Vector3(bottomLeft.X, bottomLeft.Y, 0), xScale, yScale);
            mandelbrotShader.LoadTransformationMatrix(transformationMatrix);

            fractal.domain[0] = bottomLeft;
            fractal.domain[1] = bottomRight;
            fractal.domain[2] = topRight;
            fractal.domain[3] = topLeft;

        }
        private void PrepareInstance(Vector2 mousePositionUnitCoordinates, float mouseWheelDelta, FractalDouble fractalDouble)
        {
            float stepPercent = 1 - (2 * mouseWheelDelta / 100f);
            Vector2d mousePositionComplex = new Vector2d(Mathematics.Lerp(mousePositionUnitCoordinates.X, fractalDouble.domain[0].X, fractalDouble.domain[1].X), -Mathematics.Lerp(mousePositionUnitCoordinates.Y, fractalDouble.domain[0].Y, fractalDouble.domain[3].Y));



            Vector2d bottomLeft = Mathematics.Lerp(stepPercent, mousePositionComplex, fractalDouble.domain[0]);
            Vector2d bottomRight = Mathematics.Lerp(stepPercent, mousePositionComplex, fractalDouble.domain[1]);
            Vector2d topRight = Mathematics.Lerp(stepPercent, mousePositionComplex, fractalDouble.domain[2]);
            Vector2d topLeft = Mathematics.Lerp(stepPercent, mousePositionComplex, fractalDouble.domain[3]);

            double xScale = Math.Abs(bottomRight.X - bottomLeft.X);
            double yScale = Math.Abs(topLeft.Y - bottomLeft.Y);

            Matrix4d transformationMatrix = Mathematics.CreateTransformationMatrix(new Vector3d(bottomLeft.X, bottomLeft.Y, 0), xScale, yScale);
            mandelbrotShaderDouble.LoadTransformationMatrix(transformationMatrix);

            fractalDouble.domain[0] = bottomLeft;
            fractalDouble.domain[1] = bottomRight;
            fractalDouble.domain[2] = topRight;
            fractalDouble.domain[3] = topLeft;

        }







        //get the closest vertex to a given vector.
        private int GetClosestVertexIndex(Vector2 mousePositionComplex, Vector2[] vectors)
        {
            float currentDistance = (mousePositionComplex - vectors[0]).Length;
            float currentMinDistance = 0;
            int index = 0;

            for (int i = 0; i < vectors.Length; i++)
            {
                currentDistance = (mousePositionComplex - vectors[i]).Length;
                if (currentDistance < currentMinDistance)
                {
                    index = i;
                    currentMinDistance = currentDistance;
                }
            }

            return index;
        }
    }
}

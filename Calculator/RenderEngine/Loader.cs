using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using System.Drawing;
using System.Drawing.Imaging;

namespace Calculator
{
    //loads a raw model into the GPU memory.
    public class Loader
    {
        /* HOW TO LOAD SOMETHING:
         * 1. Get an array of positions of vertices.
         * 2. Get arrays of other data of the vertices.
         * 3. Store all of that into VBOs.
         * 4. Create a VAO.
         * 5. Bind the VAO.
         * 6. Store the data into the VAO attribute lists.
         * 7. Unbind the VAO.
         */

        /* FIELDS AND PROPERTIES */
        //keep track of all VAOs and VBOs that are created so that we can dispose of them when we're done using them.
        private List<int> vaos = new List<int>();
        private List<int> vbos = new List<int>();
        private List<int> textures = new List<int>();


        /* METHODS */
        #region LoadToVAO overloads
        //given an array of vertex positions, create a VAO and store VBO data into the attribute lists. 
        public RawModel LoadToVAO(float[] positions, float[] uvCoordinates, float[] normals, int[] triangles)
        {
            int vaoID = CreateVAO(); //creates and binds the VAO.
            BindTrianglesBuffer(triangles); //bind the triangles array.
            StoreDataInAttributeList(0, 3, positions); //3D coordinate so size is 3.
            StoreDataInAttributeList(1, 2, uvCoordinates); //2D coordinate so size is 2.
            StoreDataInAttributeList(2, 3, normals); //2D coordinate so size is 2.
            UnbindVAO();
            return new RawModel(vaoID, triangles.Length);
        }
        public RawModel LoadToVAO(Vector3[] positions, Vector2[] uvCoordinates, Vector3[] normals, int[] triangles)
        {
            int vaoID = CreateVAO(); //creates and binds the VAO.
            BindTrianglesBuffer(triangles); //bind the triangles array.
            StoreDataInAttributeList(0, 3, positions); //3D coordinate so size is 3.
            StoreDataInAttributeList(1, 2, uvCoordinates); //2D coordinate so size is 2.
            StoreDataInAttributeList(2, 3, normals); //2D coordinate so size is 2.
            UnbindVAO();
            return new RawModel(vaoID, triangles.Length);
        }
        public RawModel LoadToVAO(Vector3[] positions, Vector2[] uvCoordinates, int[] triangles)
        {
            int vaoID = CreateVAO(); //creates and binds the VAO.
            BindTrianglesBuffer(triangles); //bind the triangles array.
            StoreDataInAttributeList(0, 3, positions); //3D coordinate so size is 3.
            StoreDataInAttributeList(1, 2, uvCoordinates); //2D coordinate so size is 2.
            UnbindVAO();
            return new RawModel(vaoID, triangles.Length);
        }
        public RawModel LoadToVAO(Vector3[] positions)
        {
            int vaoID = CreateVAO(); //creates and binds the VAO.
            StoreDataInAttributeList(0, 3, positions); //3D coordinate so size is 3.
            UnbindVAO();
            return new RawModel(vaoID, positions.Length);
        }
        public RawModel LoadToVAO(Vector3[] positions, Vector3[] colors)
        {
            int vaoID = CreateVAO(); //creates and binds the VAO.
            StoreDataInAttributeList(0, 3, positions); //3D coordinate so size is 3.
            StoreDataInAttributeList(1, 3, colors); //3D coordinate so size is 3.
            UnbindVAO();
            return new RawModel(vaoID, positions.Length);
        }
        public RawModel LoadToVAO(Vector3[] positions, Vector3[] colors, int[] pointSizes)
        {
            int vaoID = CreateVAO(); //creates and binds the VAO.
            StoreDataInAttributeList(0, 3, positions); //3D coordinate so size is 3.
            StoreDataInAttributeList(1, 3, colors); //3D coordinate so size is 3.
            StoreDataInAttributeList(2, 1, pointSizes); //1D array of integers.
            UnbindVAO();
            return new RawModel(vaoID, positions.Length);
        }
        public RawModel LoadToVAO(Vector3 position)
        {
            int vaoID = CreateVAO(); //creates and binds the VAO.
            StoreDataInAttributeList(0, 3, new Vector3[1] { position }); //3D coordinate so size is 3.
            UnbindVAO();
            return new RawModel(vaoID, 1);
        }
        public RawModel LoadToVAO(Vector3 position, Vector3 color)
        {
            int vaoID = CreateVAO(); //creates and binds the VAO.
            StoreDataInAttributeList(0, 3, new Vector3[1] { position }); //3D coordinate so size is 3.
            StoreDataInAttributeList(1, 3, new Vector3[1] { color }); //3D coordinate so size is 3.
            UnbindVAO();
            return new RawModel(vaoID, 1);
        }
        public RawModel LoadToVAO(Vector3 position, Vector3 color, int pointSize)
        {
            int vaoID = CreateVAO(); //creates and binds the VAO.
            StoreDataInAttributeList(0, 3, new Vector3[1] { position }); //3D coordinate so size is 3.
            StoreDataInAttributeList(1, 3, new Vector3[1] { color }); //3D coordinate so size is 3.
            StoreDataInAttributeList(2, 1, new int[1] { pointSize });
            UnbindVAO();
            return new RawModel(vaoID, 1);
        }
        public RawModel LoadToVAO(Vector3d[] positions)
        {
            int vaoID = CreateVAO(); //creates and binds the VAO.
            StoreDataInAttributeList(0, 3, positions); //3D coordinate so size is 3.
            UnbindVAO();
            return new RawModel(vaoID, positions.Length);
        }
        public RawModel LoadToVAO(Vector3d position)
        {
            int vaoID = CreateVAO(); //creates and binds the VAO.
            StoreDataInAttributeList(0, 3, new Vector3d[1] { position }); //3D coordinate so size is 3.
            UnbindVAO();
            return new RawModel(vaoID, 1);
        }
        public RawModel LoadToVAO(Vector3[] positions, Vector2[] complexCoordinates)
        {
            int vaoID = CreateVAO(); //creates and binds the VAO.
            StoreDataInAttributeList(0, 3, positions); //3D coordinate so size is 3.
            StoreDataInAttributeList(1, 2, complexCoordinates); //2D coordinate so size is 2.
            UnbindVAO();
            return new RawModel(vaoID, positions.Length);
        }
        public RawModel LoadToVAO(Vector3[] positions, Vector2d[] complexCoordinates)
        {
            int vaoID = CreateVAO(); //creates and binds the VAO.
            StoreDataInAttributeList(0, 3, positions); //3D coordinate so size is 3.
            StoreDataInAttributeList(1, 2, complexCoordinates); //2D coordinate so size is 2.
            UnbindVAO();
            return new RawModel(vaoID, positions.Length);
        }
        #endregion

        //load up a texture so we can texture stuff.
        public int LoadTexture(Bitmap bitmap)
        {
            int tex;
            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);

            GL.GenTextures(1, out tex);
            GL.BindTexture(TextureTarget.Texture2D, tex);

            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                OpenTK.Graphics.OpenGL4.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            bitmap.UnlockBits(data);
            
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
            
            bitmap.Dispose();

            return tex;
        }
        
        //set up a VAO to store vertex data. get the ID of the bound VAO.
        private int CreateVAO()
        {
            int vaoID;
            GL.GenVertexArrays(1, out vaoID); //generate the VAO and get the ID.
            vaos.Add(vaoID); //keep track of the VAO so we can delete it later.
            GL.BindVertexArray(vaoID); //bind the VAO to the GPU.
            return vaoID;
        }

        //load a VBO into a attribute list.
        private void StoreDataInAttributeList(int attributeNumber, int coordinateSize, float[] data)
        {
            //manage the VBOs:
            int vboID = GL.GenBuffer(); //generate a VBO and get the ID.
            vbos.Add(vboID); //keep track of the ID so we can delete it later.
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboID); //bind the VBO as an array buffer.
            GL.BufferData(BufferTarget.ArrayBuffer, 4 * data.Length, data, BufferUsageHint.StaticDraw); //the size of a float is 4 bytes and we will have data.Length floats.

            //add the VBO data to the attribute list in the VAO:
            GL.VertexAttribPointer(attributeNumber, coordinateSize, VertexAttribPointerType.Float, false, 0, 0); //place into the attribute list.

            //finish up with the VBO:
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0); //unbind the VBO by "binding" position 0.
        }
        private void StoreDataInAttributeList(int attributeNumber, int coordinateSize, int[] data)
        {
            //manage the VBOs:
            int vboID = GL.GenBuffer(); //generate a VBO and get the ID.
            vbos.Add(vboID); //keep track of the ID so we can delete it later.
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboID); //bind the VBO as an array buffer.
            GL.BufferData(BufferTarget.ArrayBuffer, 4 * data.Length, data, BufferUsageHint.StaticDraw); //the size of an int is 4 bytes and we will have data.Length ints.

            //add the VBO data to the attribute list in the VAO:
            GL.VertexAttribPointer(attributeNumber, coordinateSize, VertexAttribPointerType.Int, false, 0, 0); //place into the attribute list.

            //finish up with the VBO:
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0); //unbind the VBO by "binding" position 0.
        }
        private void StoreDataInAttributeList(int attributeNumber, int coordinateSize, Vector3[] data)
        {
            //manage the VBOs:
            int vboID = GL.GenBuffer(); //generate a VBO and get the ID.
            vbos.Add(vboID); //keep track of the ID so we can delete it later.
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboID); //bind the VBO as an array buffer.
            GL.BufferData(BufferTarget.ArrayBuffer, 4 * 3 * data.Length, data, BufferUsageHint.StaticDraw); //the size of a float is 4 bytes and we will have data.Length floats.

            //add the VBO data to the attribute list in the VAO:
            GL.VertexAttribPointer(attributeNumber, coordinateSize, VertexAttribPointerType.Float, false, 0, 0); //place into the attribute list.

            //finish up with the VBO:
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0); //unbind the VBO by "binding" position 0.
        }
        private void StoreDataInAttributeList(int attributeNumber, int coordinateSize, Vector3d[] data)
        {
            //manage the VBOs:
            int vboID = GL.GenBuffer(); //generate a VBO and get the ID.
            vbos.Add(vboID); //keep track of the ID so we can delete it later.
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboID); //bind the VBO as an array buffer.
            GL.BufferData(BufferTarget.ArrayBuffer, 8 * 3 * data.Length, data, BufferUsageHint.StaticDraw); //the size of a double is 8 bytes and we will have data.Length floats.

            //add the VBO data to the attribute list in the VAO:
            GL.VertexAttribPointer(attributeNumber, coordinateSize, VertexAttribPointerType.Double, false, 0, 0); //place into the attribute list.

            //finish up with the VBO:
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0); //unbind the VBO by "binding" position 0.
        }
        private void StoreDataInAttributeList(int attributeNumber, int coordinateSize, Vector2[] data)
        {
            //manage the VBOs:
            int vboID = GL.GenBuffer(); //generate a VBO and get the ID.
            vbos.Add(vboID); //keep track of the ID so we can delete it later.
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboID); //bind the VBO as an array buffer.
            GL.BufferData(BufferTarget.ArrayBuffer, 4 * 2 * data.Length, data, BufferUsageHint.StaticDraw); //the size of a float is 4 bytes and we will have data.Length floats.

            //add the VBO data to the attribute list in the VAO:
            GL.VertexAttribPointer(attributeNumber, coordinateSize, VertexAttribPointerType.Float, false, 0, 0); //place into the attribute list.

            //finish up with the VBO:
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0); //unbind the VBO by "binding" position 0.
        }
        private void StoreDataInAttributeList(int attributeNumber, int coordinateSize, Vector2d[] data)
        {
            //manage the VBOs:
            int vboID = GL.GenBuffer(); //generate a VBO and get the ID.
            vbos.Add(vboID); //keep track of the ID so we can delete it later.
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboID); //bind the VBO as an array buffer.
            GL.BufferData(BufferTarget.ArrayBuffer, 8 * 2 * data.Length, data, BufferUsageHint.StaticDraw); //the size of a double is 8 bytes and we will have data.Length doubles.

            //add the VBO data to the attribute list in the VAO:
            GL.VertexAttribPointer(attributeNumber, coordinateSize, VertexAttribPointerType.Float, false, 0, 0); //place into the attribute list.

            //finish up with the VBO:
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0); //unbind the VBO by "binding" position 0.
        }

        //for greater efficiency, this will serve as the array of "triangles" that tell us how vertices are arranged in triangle formations.
        private void BindTrianglesBuffer(int[] triangles)
        { 
            // NOTE: triangles are ordered COUNTERCLOCKWISE.

            //manage the VBOs:
            int vboID = GL.GenBuffer();
            vbos.Add(vboID);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, vboID);
            GL.BufferData(BufferTarget.ElementArrayBuffer, 4 * triangles.Length, triangles, BufferUsageHint.StaticDraw); //NOTE: the indices buffer will be an ElementArrayBuffer.
        }

        //unbind the VAO when we're done using it.
        private void UnbindVAO()
        {
            GL.BindVertexArray(0); //to unbind the VAO, "bind" the VAO at position 0.
        }

        //cleaup everything when we're done.
        public void CleanUp()
        { 
            //delete all of the VBOs and VAOs that we've used.
            GL.DeleteVertexArrays(vaos.Count, vaos.ToArray());
            GL.DeleteBuffers(vbos.Count, vbos.ToArray());
            GL.DeleteTextures(textures.Count, textures.ToArray());
        }
    }
}

using System;
using OpenGL;

namespace ParticleSorting
{
    class DrawFunctions
    {
        public static void DrawSquare(Matrix4? model = null, Matrix4? view = null, Matrix4? projection = null)
        {
            DrawShape(Constants.square, Constants.squareColor, Constants.squareElements, Constants.defaultTexture, Constants.UV, BeginMode.TriangleFan, 4, model, view, projection);
        }

        public static void DrawStar(Matrix4? model = null, Matrix4? view = null, Matrix4? projection = null)
        {
            DrawShape(Constants.square, Constants.squareColor, Constants.squareElements, Constants.starTexture, Constants.UV, BeginMode.TriangleFan, 4, model, view, projection);
        }

        public static void DrawColoredCube(Matrix4? model = null, Matrix4? view = null, Matrix4? projection = null)
        {
            DrawShape(Constants.cube, Constants.cubeColor, Constants.cubeElements, Constants.defaultTexture, Constants.UVCube, BeginMode.Triangles, 36, model, view, projection);
        }

        public static void DrawWhiteHUDFrame(Matrix4? model = null, Matrix4? view = null, Matrix4? projection = null)
        {
            DrawShape(Constants.square, Constants.whiteHUDColor, Constants.squareElements, Constants.defaultTexture, Constants.UV, BeginMode.LineLoop, 4, model, view, projection);
        }

        public static void DrawRedHUDFrame(Matrix4? model = null, Matrix4? view = null, Matrix4? projection = null)
        {
            DrawShape(Constants.square, Constants.redHUDColor, Constants.squareElements, Constants.defaultTexture, Constants.UV, BeginMode.TriangleFan, 4, model, view, projection);
        }

        public static void DrawRedderHUDFrame(Matrix4? model = null, Matrix4? view = null, Matrix4? projection = null)
        {
            DrawShape(Constants.square, Constants.exceededHUDColor, Constants.squareElements, Constants.defaultTexture, Constants.UV, BeginMode.TriangleFan, 4, model, view, projection);
        }

        public static void DrawShape(VBO<Vector3> pos, VBO<Vector4> color, VBO<int> elements, Texture texture, VBO<Vector2> UV, BeginMode mode, int n, Matrix4? model, Matrix4? view, Matrix4? projection)
        {
            // Sets optional parameters to default values if they were not provided
            model = model ?? Matrix4.Identity;
            view = view ?? Matrix4.LookAt(new Vector3(0, 0, 10f), Vector3.Zero, Vector3.Up);
            projection = projection ?? Matrix4.CreatePerspectiveFieldOfView(0.45f, (float)Program.width / Program.height, 0.1f, 1000f);

            // Sends the matrices to the vertex shader
            Program.shader.Use();
            Program.shader["u_Model"].SetValue(model.Value);
            Program.shader["u_View"].SetValue(view.Value);
            Program.shader["u_Projection"].SetValue(projection.Value);

            // Binds vertex and color arrays to the vertex shader attributes
            Gl.BindBufferToShaderAttribute(pos, Program.shader, "a_Position");
            Gl.BindBufferToShaderAttribute(color, Program.shader, "a_FragColor");
            Gl.BindBufferToShaderAttribute(UV, Program.shader, "a_VertexUV");
            Gl.BindTexture(texture);
            Gl.BindBuffer(elements);
            Gl.DrawElements(mode, n, DrawElementsType.UnsignedInt, IntPtr.Zero);
        }
    }
}

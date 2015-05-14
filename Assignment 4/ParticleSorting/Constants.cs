using OpenGL;
using System;

namespace ParticleSorting
{
    class Constants
    {
        private const float PARTICLE_TRANSPARENCY = 0.05f;

        public static VBO<Vector3> square;
        public static VBO<Vector4> squareColor;
        public static VBO<int> squareElements;
        public static VBO<Vector3> cube;
        public static VBO<Vector4> cubeColor;
        public static VBO<int> cubeElements;
        public static VBO<Vector4> whiteHUDColor;
        public static VBO<Vector4> redHUDColor;
        public static VBO<Vector4> exceededHUDColor;
        public static Texture defaultTexture;
        public static Texture starTexture;
        public static VBO<Vector2> UV;
        public static VBO<Vector2> UVCube;

        public static void initBuffers()
        {
            // Square information
            square = new VBO<Vector3>(new Vector3[] { new Vector3(1, 1, 0), new Vector3(-1, 1, 0), new Vector3(-1, -1, 0), new Vector3(1, -1, 0) });
            squareColor = new VBO<Vector4>(new Vector4[] { new Vector4(1, 1, 1, PARTICLE_TRANSPARENCY), new Vector4(0, 1, 1, PARTICLE_TRANSPARENCY), new Vector4(1, 0, 1, PARTICLE_TRANSPARENCY), new Vector4(1, 1, 1, PARTICLE_TRANSPARENCY) });
            squareElements = new VBO<int>(new int[] { 0, 1, 2, 3 }, BufferTarget.ElementArrayBuffer);

            // Cube information
            Vector3[] points = {
                new Vector3(1, 1, 1), 
                new Vector3(-1, 1, 1), 
                new Vector3(-1, -1, 1), 
                new Vector3(1, -1, 1), 
                new Vector3(1, -1, -1), 
                new Vector3(1, 1, -1), 
                new Vector3(-1, 1, -1), 
                new Vector3(-1, -1, -1)};
            cube = new VBO<Vector3>(points);
            Vector4[] colors = new Vector4[8];
            for (int i = 0; i < 8; i++)
            {
                colors[i].x = i & 4;
                colors[i].y = i & 2;
                colors[i].z = i & 1;
                colors[i].w = 1;
            }
            cubeColor = new VBO<Vector4>(colors);
            int[] indices = {0, 1, 2,   0, 2, 3,
                             0, 3, 4,   0, 4, 5,
                             0, 5, 6,   0, 6, 1,
                             1, 6, 7,   1, 7, 2,
                             7, 4, 3,   7, 3, 2,
                             4, 7, 6,   4, 6, 5};
            cubeElements = new VBO<int>(indices, BufferTarget.ElementArrayBuffer);

            // HUD color information
            whiteHUDColor = new VBO<Vector4>(new Vector4[] { new Vector4(1, 1, 1, 1), new Vector4(1, 1, 1, 1), new Vector4(1, 1, 1, 1), new Vector4(1, 1, 1, 1) });
            redHUDColor = new VBO<Vector4>(new Vector4[] { new Vector4(1, 0.6, 0.6, 1), new Vector4(1, 0.6, 0.6, 1), new Vector4(1, 0.6, 0.6, 1), new Vector4(1, 0.6, 0.6, 1) });
            exceededHUDColor = new VBO<Vector4>(new Vector4[] { new Vector4(1, 0, 0, 1), new Vector4(1, 0, 0, 1), new Vector4(1, 0, 0, 1), new Vector4(1, 0, 0, 1) });

            // Load texture information
            starTexture = new Texture("smoke.bmp");
            defaultTexture = new Texture("default.bmp");
            UV = new VBO<Vector2>(new Vector2[] { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1) });
            UVCube = new VBO<Vector2>(new Vector2[] {
                new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1),
                new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1),
                new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1),
                new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1),
                new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1),
                new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1)
            });
        }
    }
}

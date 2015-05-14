using System;
using Tao.FreeGlut;
using OpenGL;

namespace ParticleSorting
{
    class Program
    {
        private const float DISTANCE_FROM_ORIGIN = 20f;
        private const float PARTICLE_SCALE = 0.4f;

        public static int width = 1200, height = 800;
        public static ShaderProgram shader;
        public static ParticleSystem particleSystem;
        private static System.Diagnostics.Stopwatch watch;
        private static float angle;
        public static float cosAngle = 1;
        public static float sinAngle = 0;

        public static BMFont font;
        public static ShaderProgram fontShaders;

        static void Main(string[] args)
        {
            particleSystem = new ParticleSystem(4000);
            watch = System.Diagnostics.Stopwatch.StartNew();

            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);
            Glut.glutInitWindowSize(width, height);
            Glut.glutCreateWindow("Particle Sorting");

            Gl.Disable(EnableCap.DepthTest);
            Gl.Enable(EnableCap.Blend);
            Gl.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.One);

            Glut.glutIdleFunc(idle);
            Glut.glutDisplayFunc(display);
            Glut.glutKeyboardFunc(keyDown);
            Glut.glutMouseFunc(mouse); 
            Glut.glutMotionFunc(mouseMove); 


            shader = new ShaderProgram(vertexShader, fragmentShader);
            shader.Use();

            font = new BMFont("font24.fnt", "font24.png");
            fontShaders = new ShaderProgram(BMFont.FontVertexSource, BMFont.FontFragmentSource);
            fontShaders.Use();
            fontShaders["ortho_matrix"].SetValue(Matrix4.CreateOrthographic(width, height, 0, 1000));
            fontShaders["color"].SetValue(new Vector3(1, 1, 1));

            Constants.initBuffers();

            Glut.glutMainLoop();
        }

        private static void idle()
        {
            watch.Stop();
            float dt = (float)watch.ElapsedTicks / System.Diagnostics.Stopwatch.Frequency;
            watch.Restart();

            Gl.Viewport(0, 0, width, height);
            Gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Sorting.DepthSort(particleSystem.particles);

            for(int i = 0; i < particleSystem.Length(); i++)
            {
                // Translate each particle according to its speed
                particleSystem.getParticle(i).setPos(particleSystem.getParticle(i).getPos() + dt*particleSystem.getParticle(i).getSpeed());

                // Check if particle exceeds the distance D given in Particle.cs
                float distance = particleSystem.getParticle(i).getPos().Length;
                if(particleSystem.getParticle(i).exceedsRange())
                {
                    // If distance is exceeded, return particle to valid position
                    particleSystem.getParticle(i).setPos(particleSystem.getParticle(i).getPos() - dt*particleSystem.getParticle(i).getSpeed());
                    // And change the speed vector to face some other random direction
                    particleSystem.getParticle(i).RandomizeSpeeds();
                }

                // Draw each particle. Note the matrix operations are performed 'backwards'
                Matrix4 model = Matrix4.Identity;
                model *= Matrix4.CreateRotationY(-angle + (float)Math.PI/2); // Finally, rotate each particle to face towards the screen
                model *= Matrix4.CreateScaling(new Vector3(PARTICLE_SCALE, PARTICLE_SCALE, PARTICLE_SCALE)); // Second, scale the particle
                model *= Matrix4.CreateTranslation(particleSystem.getParticle(i).getPos()); // First, translate the particle
                Matrix4 view = Matrix4.LookAt(new Vector3(DISTANCE_FROM_ORIGIN*Math.Cos(angle), 0, DISTANCE_FROM_ORIGIN*Math.Sin(angle)), Vector3.Zero, Vector3.Up);
                DrawFunctions.DrawStar(model, view); // Draw it
            }

            HUD.DrawHUD();

            Glut.glutSwapBuffers();
        }

        private static void display()
        {
            
        }

        public static void keyDown(byte key, int x, int y)
        {
            if (key == 'M' || key == 'm' || key == 'R' || key == 'r')
                Sorting.maxTime = 0f;
        }

        private static bool mouseDown = false; 
        private static int xPos, yPos; 

        private static void mouse(int button, int state, int x, int y)
        {
            // Determines if the left mouse button was pressed down
            if (button == Glut.GLUT_LEFT_BUTTON)
                mouseDown = (state == Glut.GLUT_DOWN);

            // If the mouse has just been clicked then we store the position 
            if (mouseDown)
            {
                xPos = x;
                yPos = y;
            }
        }

        private static void mouseMove(int x, int y)
        {
            if (mouseDown)
            {
                // Update the angle rotation of our camera if the mouse is down 
                angle += (x - xPos) * 0.002f;
                cosAngle = (float)Math.Cos(angle);
                sinAngle = (float)Math.Sin(angle);

                // And update the initial positions of the mouse for the next frame
                xPos = x;
                yPos = y;
            }
            Console.WriteLine(angle);
        }


        public static string vertexShader = @"
            attribute vec4 a_Position;
            uniform mat4 u_Model;
            uniform mat4 u_View;
            uniform mat4 u_Projection;
            varying vec4 v_FragColor;
            attribute vec4 a_FragColor;
            varying vec2 v_VertexUV;
            attribute vec2 a_VertexUV;
            void main() {
                gl_Position = u_Projection * u_View * u_Model * a_Position;
	            gl_PointSize = 10.0;
	            v_FragColor = a_FragColor;
                v_VertexUV = a_VertexUV;
            }
        ";

        public static string fragmentShader = @"
            precision mediump float;
            uniform sampler2D texture;
            varying vec4 v_FragColor;
            varying vec2 v_VertexUV;
            void main() {
	            //gl_FragColor = v_FragColor;
                gl_FragColor = v_FragColor * vec4(texture2D(texture, v_VertexUV).xyz, 1);
            }
        ";
    }
}

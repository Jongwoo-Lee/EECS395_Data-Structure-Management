using OpenGL;
using System;

namespace ParticleSorting
{
    public class Particle
    {
        public const float PARTICLE_BOUNDS = 3f;

        private float x;
        private float y;
        private float z;
        private float xSpeed;
        private float ySpeed;
        private float zSpeed;
        private Vector4 color;

        public float depth
        {
            get { return z*Program.cosAngle + x*Program.sinAngle; }  // Getter
        }

        public Particle()
        {
            // Set x,y to be a random value between [-D, D]
            x = (float)(PARTICLE_BOUNDS * (2 * ParticleSystem.random.NextDouble() - 1));
            y = (float)(PARTICLE_BOUNDS * (2 * ParticleSystem.random.NextDouble() - 1));
            z = (float)(PARTICLE_BOUNDS * (2 * ParticleSystem.random.NextDouble() - 1));

            color = new Vector4(1f, ParticleSystem.random.NextDouble(), 0, 0.5);

            RandomizeSpeeds();
        }

        // Sets a random value for the speed in each directon, in the range [-1, 1]
        public void RandomizeSpeeds()
        {
            xSpeed = (float)(2 * ParticleSystem.random.NextDouble() - 1);
            ySpeed = (float)(2 * ParticleSystem.random.NextDouble() - 1);
            zSpeed = (float)(2 * ParticleSystem.random.NextDouble() - 1);
        }

        public Vector3 getPos()
        {
            return new Vector3(x, y, z);
        }

        public void setPos(Vector3 pos)
        {
            x = pos.x;
            y = pos.y;
            z = pos.z;
        }

        public Vector3 getSpeed()
        {
            return new Vector3(xSpeed, ySpeed, zSpeed);
        }

        public Vector4 getColor()
        {
            return color;
        }

        public bool exceedsRange()
        {
            return (
                    x > PARTICLE_BOUNDS || y > PARTICLE_BOUNDS || z > PARTICLE_BOUNDS||
                    x < -PARTICLE_BOUNDS || y < -PARTICLE_BOUNDS || z < -PARTICLE_BOUNDS
                );
        }
    }
}

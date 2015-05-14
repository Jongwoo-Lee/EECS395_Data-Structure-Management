using System;

namespace ParticleSorting
{
    public class ParticleSystem
    {
        public static Random random;
        public Particle[] particles;
        private int size;

        public ParticleSystem() : this(100) { } // Default constructor creates 100 particles

        public ParticleSystem(int n)
        {
            random = new Random();
            size = n;
            particles = new Particle[n];
            for (int i = 0; i < n; i++)
                particles[i] = new Particle();
        }

        public Particle getParticle(int i)
        {
            return particles[i];
        }

        public void setParticle(int i, Particle p)
        {
            particles[i] = p;
        }

        public int Length()
        {
            return size;
        }

        public bool IsDepthSorted()
        {
            float previous = float.MinValue;
            int i = 0;
            foreach(Particle particle in particles)
            {
                if (previous > particle.depth)
                    return false;
                previous = particle.depth;
                i++;
            }
            return true;
        }
    }
}

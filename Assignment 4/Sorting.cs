using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleSorting
{
    public class Sorting
    {
        private static System.Diagnostics.Stopwatch sortingWatch = new System.Diagnostics.Stopwatch();
        private static long count = 0;
        public static float currentTime, minTime = 9999, maxTime, avgTime;

        public static void DepthSort(Particle[] particles)
        {
            sortingWatch.Restart();

            // You can select which sorting algorithm you'll be using by uncommenting one of the two function calls below
            // You can visually test both of your algorithms this way

            //QuicksortDepthSort(particles);
            InsertionDepthSort(particles);

            sortingWatch.Stop();
            UpdateTimes((float)sortingWatch.ElapsedTicks / System.Diagnostics.Stopwatch.Frequency);
        }

        public static void InsertionDepthSort(Particle[] particles)
        {
            float value;
            int i, j;
            Particle _sort_part;
            // Implement me
            for (i = 1; i < particles.Length; i++)
            {
                value = particles[i].depth;
                _sort_part = particles[i];
                j = i - 1;
                while(j >= 0 && particles[j].depth > value)
                {
                    particles[j + 1] = particles[j];
                    j--;
                }
                particles[j+1] = _sort_part;
            }
        }
        public static int partition(Particle[] particles, int pIndex, int start, int end)
        {
            // Get pivot value
            float pValue = particles[pIndex].depth;

            // Move pivot to end
            Particle _temp_part = particles[pIndex];
            particles[pIndex] = particles[end];
            particles[end] = _temp_part;

            // Move small values to left
            int nextLeft = start;
            for (int i = start; i < end; i++)
            {
                if (particles[i].depth <= pValue)
                {
                    _temp_part = particles[i];
                    particles[i] = particles[nextLeft];
                    particles[nextLeft] = _temp_part;

                    nextLeft++;
                }
            }

            // Move the pivot into place
            _temp_part = particles[nextLeft];
            particles[nextLeft] = particles[end];
            particles[end] = _temp_part;

            return nextLeft;
        }

        public static void QuicksortRecur(Particle[] particles, int start, int end)
        {
            if (end > start)
            {
                int pIndex = (start + end) / 2 ;
                int newIndex = partition(particles, pIndex, start, end);
                QuicksortRecur(particles, start, newIndex - 1);
                QuicksortRecur(particles, newIndex + 1, end);
            }
        }

        public static void QuicksortDepthSort(Particle[] particles)
        {
            // Implement me
            QuicksortRecur(particles, 0, particles.Length - 1);
        }

        public static void UpdateTimes(float time)
        {
            time *= 1000;
            count++;
            currentTime = time;
            minTime = minTime < time ? minTime : time;
            maxTime = maxTime > time ? maxTime : time;
            avgTime = avgTime == 0 ? time : ((avgTime * (count - 1)) + time) / count;
        }
    }
}

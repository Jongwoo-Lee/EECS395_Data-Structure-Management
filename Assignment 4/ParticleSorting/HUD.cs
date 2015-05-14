using OpenGL;
using System;

namespace ParticleSorting
{
    class HUD
    {
        private const float MAX_VALUE_SCALE = 20f;
        public static void DrawHUD()
        {
            // Start by defining the upper right corner
            Matrix4 model, reference = Matrix4.Identity;
            reference *= Matrix4.CreateTranslation(new Vector3(1, 40, 0));
            reference *= Matrix4.CreateScaling(new Vector3(1.2, 0.05, 1));

            // Draw first rectangle
            model = reference;
            DrawFunctions.DrawWhiteHUDFrame(model);
            if (Sorting.currentTime > MAX_VALUE_SCALE)
            {
                model *= Scale(MAX_VALUE_SCALE);
                DrawFunctions.DrawRedderHUDFrame(model);
            }
            else
            {
                model *= Scale(Sorting.currentTime);
                DrawFunctions.DrawRedHUDFrame(model);
            }

            // Draw second rectangle
            model = Matrix4.CreateTranslation(new Vector3(0, -3, 0)) * reference;
            DrawFunctions.DrawWhiteHUDFrame(model);
            if (Sorting.minTime > MAX_VALUE_SCALE)
            {
                model *= Scale(MAX_VALUE_SCALE);
                DrawFunctions.DrawRedderHUDFrame(model);
            }
            else
            {
                model *= Scale(Sorting.minTime);
                DrawFunctions.DrawRedHUDFrame(model);
            }

            // Draw third rectangle
            model = Matrix4.CreateTranslation(new Vector3(0, -6, 0)) * reference;
            DrawFunctions.DrawWhiteHUDFrame(model);
            if (Sorting.maxTime > MAX_VALUE_SCALE)
            {
                model *= Scale(MAX_VALUE_SCALE);
                DrawFunctions.DrawRedderHUDFrame(model);
            }
            else
            {
                model *= Scale(Sorting.maxTime);
                DrawFunctions.DrawRedHUDFrame(model);
            }

            // Draw fourth rectangle
            model = Matrix4.CreateTranslation(new Vector3(0, -9, 0)) * reference;
            DrawFunctions.DrawWhiteHUDFrame(model);
            if (Sorting.avgTime > MAX_VALUE_SCALE)
            {
                model *= Scale(MAX_VALUE_SCALE);
                DrawFunctions.DrawRedderHUDFrame(model);
            }
            else
            {
                model *= Scale(Sorting.avgTime);
                DrawFunctions.DrawRedHUDFrame(model);
            }

            // Draw the right-hand text boxes and right-hand side information
            string[] text = { "Current Time", "Min Time", "Max Time", "Average Time" };
            string[] times = { getCurrentTime(), getMinTime(), getMaxTime(), getAvgTime() };
            float x = -10, y = Program.height / 2 - 42;
            for(int i = 0; i < text.Length; i++)
            {
                BMFont.FontMe(text[i], x, y-=25, BMFont.Justification.Right);
                BMFont.FontMe(times[i], x+440, y, BMFont.Justification.Left);
            }
        }

        private static Matrix4 Scale(float s)
        {
            // Normalize s from [0, M] to [0, 1], where M is the highest value being displayed (this will always be Sorting.maxTime)
            s /= MAX_VALUE_SCALE;
            return Matrix4.CreateScaling(new Vector3(s, 1, 1));
        }

        private static string getCurrentTime()
        {
            return Sorting.currentTime.ToString("n6") +  " ms";
        }

        private static string getMinTime()
        {
            return Sorting.minTime.ToString("n6") + " ms";
        }

        private static string getMaxTime()
        {
            return Sorting.maxTime.ToString("n6") + " ms";
        }

        private static string getAvgTime()
        {
            return Sorting.avgTime.ToString("n6") + " ms";
        }
    }
}

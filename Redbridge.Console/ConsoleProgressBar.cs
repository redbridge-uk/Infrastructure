using System;

namespace Redbridge.Console
{
    public class ConsoleProgressBar
    {
        /// <summary>
        /// 
        /// </summary>
        private const char DefaultProgressCharacter = '\u2592';

        /// <summary>
        /// Method that renders a console progress bar.
        /// </summary>
        /// <param name="complete"></param>
        /// <param name="barSize"></param>
        /// <param name="progressCharacter"></param>
        public static void DrawProgress(int complete, int barSize, char progressCharacter)
        {
            DrawProgress(complete, 100, barSize, progressCharacter);
        }

        /// <summary>
        /// Method that renders a console progress bar.
        /// </summary>
        /// <param name="complete"></param>
        /// <param name="maxVal"></param>
        /// <param name="barSize"></param>
        /// <param name="progressCharacter"></param>
        public static void DrawProgress(int complete, int maxVal = 100, int barSize = 50, char progressCharacter = DefaultProgressCharacter)
        {
            System.Console.CursorVisible = false;
            int left = System.Console.CursorLeft;
            decimal perc = (decimal)complete / (decimal)maxVal;
            int chars = (int)Math.Floor(perc / ((decimal)1 / (decimal)barSize));
            string p1 = String.Empty, p2 = String.Empty;

            for (int i = 0; i < chars; i++) p1 += progressCharacter;
            for (int i = 0; i < barSize - chars; i++) p2 += progressCharacter;

            System.Console.ForegroundColor = ConsoleColor.Green;
            System.Console.Write(p1);
            System.Console.ForegroundColor = ConsoleColor.DarkGreen;
            System.Console.Write(p2);

            System.Console.ResetColor();
            System.Console.Write(" {0}%", (perc * 100).ToString("N2"));
            System.Console.CursorLeft = left;
        }
    }
}

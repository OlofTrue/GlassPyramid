using System;

namespace GlassPyramid
{
    class Program
    {
        static void Main() //string[] args
        {
            Input(out int rows, out int col);
            Simulation sim = new Simulation(rows);
            var sec = sim.RunUntilFull(col);
            Console.WriteLine($"Det tar {sec:N3} sekunder.");
        }
        public static void Input(out int rows, out int col)
        {
            do
            {
                Console.Clear();
                Console.Write("Rad? ");
                rows = int.TryParse(Console.ReadLine(), out rows) ? rows : 0;
                if (rows < 2 || rows > 50) continue;
                Console.Write("Glas? ");
                col = int.TryParse(Console.ReadLine(), out col) ? col : 0;
                if (col > 0 && col <= rows) break;
            } while (true);
        }
    }
}
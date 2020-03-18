using System;
using System.Collections.Generic;
using System.Text;

namespace GlassPyramid
{
    class Simulation
    {
        private int row { get; set; }
        private Pyramid pyramid { get; set; }
        public Simulation(int rows)
        {
            this.pyramid = new Pyramid(rows);
            this.row = rows;
        }
        public double elapsedSec { get; set; }
        public double RunUntilFull(int col) 
        {
            double incSec;
            var pyr = this.pyramid;
            do
            {
                incSec = pyr.SecToNextFullGlass();
                pyr.UpdateGlasses(incSec);
                elapsedSec += incSec;
            } while (!pyr.IsFullGlass(this.row, col));         
            return elapsedSec;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace GlassPyramid
{
    public class Glass
    {
        public int Row { get; set; } // 1-
        public int Col { get; set; } // or glass in row 1-
        public double CurrLevel { get; set; } // proportion of glass full 0-1
        public double EstimateSecFull { get; set; } // estimated second left until glass full
        public Boolean Isfull { get; set; } // glass is full
        public double CurrFlow { get; set; } // glass per second

    }
}

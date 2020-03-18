using System;
using System.Collections.Generic;
using System.Text;

namespace GlassPyramid
{
    public class Pyramid
    {
        private List<Glass> glasses { get; set; }
        private int inxLastRow { get; set; }
        public Pyramid(int rows)
        {
            this.glasses = InitGlasses(rows);
            this.inxLastRow = GetListIndex(rows, 1);
        }
        public List<Glass> InitGlasses(int rows)
        {
            int cols = rows;
            int i = 0; ;
            var glasses = new List<Glass>();
            for (int row = 1; row <= rows; row++)
            {
                for (int col = 1; col <= cols; col++)
                {
                    Glass glass = new Glass { Col = col, Row = row }; //CurrFlow = 0.0;CurrLevel = 0.0;Isfull = false;
                    glasses.Add(glass);
                    i += 1;
                    if (col == row) break;
                }
            }
            var glassFirst = glasses[0];
            glassFirst.CurrFlow = 0.1;
            glassFirst.EstimateSecFull = (1.0 - glassFirst.CurrLevel) / glassFirst.CurrFlow;
            return glasses;
        }
        private int GetListIndex(int row, int col)
        {
            int firstInRow = (row * row + row) / 2 - row;
            return firstInRow + col - 1; // 0-based
        }
        public double SecToNextFullGlass()
        {
            double minEstimateSecFull = Double.MaxValue;
            for (int i = 0; i < glasses.Count; i++)  // remeber last with flow !!
            {
                Glass glass = glasses[i];
                if (!glass.Isfull && glass.CurrFlow > 0) minEstimateSecFull = Math.Min(glass.EstimateSecFull, minEstimateSecFull);
            }
            return minEstimateSecFull;
        }

        public Boolean IsFullGlass(int row, int col)
        {
            return glasses[GetListIndex(row, col)].Isfull;
        }
        public void UpdateGlasses(double incSec)
        {
            // new level + reset flow
            for (int i = 0; i < glasses.Count; i++)
            {
                Glass glass = glasses[i];
                if (!glass.Isfull)
                {
                    glass.CurrLevel += incSec * glass.CurrFlow;
                    if (glass.CurrLevel > 0.999999) glass.Isfull = true;  //if (Math.Abs(glass.CurrLevel - 1.0) < 0.000001)
                }
                //reset flow exept for top-glass
                if (i > 0) glass.CurrFlow = 0.0;
            }
            // new estimate + new flow

            for (int i = 0; i < glasses.Count; i++)
            {
                Glass glass = glasses[i];
                if (glass.Isfull && !(i >= inxLastRow))
                {
                    int inxGlassL = GetListIndex(glass.Row + 1, glass.Col);
                    Glass glassL = glasses[inxGlassL];
                    Glass glassR = glasses[inxGlassL + 1];
                    glassL.CurrFlow += 0.5 * glass.CurrFlow; // half of glas above
                    glassR.CurrFlow += 0.5 * glass.CurrFlow; // half of glas above
                }
                else
                {
                    if (glass.CurrFlow > 0) glass.EstimateSecFull = (1.0 - glass.CurrLevel) / glass.CurrFlow;
                }
            }
        }
    }
}

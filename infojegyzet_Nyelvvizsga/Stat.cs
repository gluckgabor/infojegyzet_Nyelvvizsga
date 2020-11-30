using System;
using System.Collections.Generic;
using System.Text;

namespace infojegyzet_Nyelvvizsga
{
    class Stat
    {
        public string nyelv { get; set; }
        public Dictionary<int,int> evSikeres { get; set; }
        public Dictionary<int, int> evSikertelen { get; set; }
        public int osszesNyelvvizsga { get; set; }
        public Dictionary<int, double> evsikertelenPerOsszesAranya { get; set; }
        public Dictionary<int, double> evsikeresPerOsszesAranya { get; set; }      

        public Stat(string sikertelenLine, string sikeresLine)
        {
            evSikeres = new Dictionary<int, int>();
            evSikertelen = new Dictionary<int, int>();
            
            evsikertelenPerOsszesAranya = new Dictionary<int, double>();
            evsikeresPerOsszesAranya = new Dictionary<int, double>();

            string[] sikertelenLineSplitted = sikertelenLine.Split(";");
            string[] sikeresLineSplitted = sikeresLine.Split(";");

            nyelv = sikertelenLineSplitted[0];
            
            int yearCounter = 2009;
            for (int i = 1; i < sikertelenLineSplitted.Length; i++)
            {
                evSikertelen.Add(yearCounter, Convert.ToInt32(sikertelenLineSplitted[i]));
                yearCounter++;
            }

            yearCounter = 2009;
            for (int i = 1; i < sikeresLineSplitted.Length; i++)
            {
                evSikeres.Add(yearCounter, Convert.ToInt32(sikeresLineSplitted[i]));
                yearCounter++;
            }
            
            for (int i = 1; i < sikeresLineSplitted.Length; i++)
            {
                osszesNyelvvizsga += Convert.ToInt32(sikeresLineSplitted[i]) + Convert.ToInt32(sikertelenLineSplitted[i]);
            }

            yearCounter = 2009;
            for (int i = 1; i < sikeresLineSplitted.Length; i++)
            {
                int divisor = Convert.ToInt32(sikeresLineSplitted[i]) + Convert.ToInt32(sikertelenLineSplitted[i]);

                if (divisor == 0)
                {
                    evsikertelenPerOsszesAranya.Add(yearCounter, Convert.ToDouble(sikertelenLineSplitted[i]) / 1);
                    yearCounter++;
                }
                else
                {
                    evsikertelenPerOsszesAranya.Add(yearCounter, Convert.ToDouble(sikertelenLineSplitted[i]) / divisor);
                    yearCounter++;
                }
            }

            yearCounter = 2009;
            for (int i = 1; i < sikeresLineSplitted.Length; i++)
            {
                int divisor = Convert.ToInt32(sikeresLineSplitted[i]) + Convert.ToInt32(sikertelenLineSplitted[i]);

                if (divisor == 0)
                {
                    evsikeresPerOsszesAranya.Add(yearCounter, Convert.ToDouble(sikeresLineSplitted[i]) / 1);
                    yearCounter++;
                }
                else
                {
                    evsikeresPerOsszesAranya.Add(yearCounter, Convert.ToDouble(sikeresLineSplitted[i]) / divisor);
                    yearCounter++;
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace infojegyzet_Nyelvvizsga
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] sikeresLines = File.ReadAllLines("C:/Users/Glück Gábor/source/repos/infojegyzet_Nyelvvizsga/infojegyzet_Nyelvvizsga/sikeres.csv");
            string[] sikertelenLines = File.ReadAllLines("C:/Users/Glück Gábor/source/repos/infojegyzet_Nyelvvizsga/infojegyzet_Nyelvvizsga/sikertelen.csv");

            List<Stat> statList = new List<Stat>();

            for (int i = 1; i < sikertelenLines.Length; i++)
            {
                statList.Add(new Stat(sikertelenLines[i], sikeresLines[i]));
            }

            masodikFeladat(statList);

            int queriedYear = harmadikFeladat(statList);

            negyedikFeladat(queriedYear, statList);

            otodikFeladat(queriedYear, statList);

            hatodikFeladat(statList);

            Console.ReadKey();
        }

        private static void hatodikFeladat(List<Stat> statList)
        {
            //nyelv;9 év alatti összes vizsga sz., sikeres vizsgák aránya két tizedesre kerekítve
            
            using (StreamWriter sw = new StreamWriter("C:/Users/Glück Gábor/source/repos/infojegyzet_Nyelvvizsga/infojegyzet_Nyelvvizsga/osszesites.csv", false)){ 

                foreach (var stat in statList)
                {
                    sw.WriteLine(stat.nyelv + ";" + stat.osszesNyelvvizsga + ";" + String.Format("{0:0.00}", Convert.ToDouble(stat.evsikeresPerOsszesAranya.Values.Sum()/stat.evsikeresPerOsszesAranya.Count*100)));
                }
                sw.Close();
            }
        }

        private static void otodikFeladat(int queriedYear, List<Stat> statList)
        {
            List<string> vizsgazoNelkuliNyelvList = new List<string>();

            foreach (var stat in statList)
            {
                for (int i = 0; i < stat.evSikeres.Count; i++)
                {
                    if (stat.evSikeres[queriedYear]+ stat.evSikeres[queriedYear] == 0 && !(vizsgazoNelkuliNyelvList.Contains(stat.nyelv)))
                    {
                        vizsgazoNelkuliNyelvList.Add(stat.nyelv);
                    }
                }
            }

            if (vizsgazoNelkuliNyelvList.Count == 0)
            {
                Console.WriteLine("Minden nyelvből volt vizsgázó");
            }
            else
            {
                foreach (var vizsgazoNelkuliNyelv in vizsgazoNelkuliNyelvList)
                {
                    Console.WriteLine(Convert.ToString(vizsgazoNelkuliNyelv));
                }
            }
        }

        private static void negyedikFeladat(int queriedYear, List<Stat> statList)
        {
            List<int> sikertelenAranyList = new List<int>();
            string nyelv = "";
            double legnagyobbSikertelenVizsgaArany = 0;

            foreach (var stat in statList)
            {
                for (int i = 0; i < stat.evsikertelenPerOsszesAranya.Count; i++)
                {
                    if (stat.evsikertelenPerOsszesAranya[queriedYear] >= legnagyobbSikertelenVizsgaArany)
                    {
                        
                        legnagyobbSikertelenVizsgaArany = stat.evsikertelenPerOsszesAranya[queriedYear];
                    }
                }
                nyelv = stat.nyelv;
            }

        Console.WriteLine(queriedYear + "-ben " + nyelv + "nyelvből a sikertelen vizsgák aránya " + String.Format("{0:0.00}",
            legnagyobbSikertelenVizsgaArany*100) + "%");
        }

        private static int harmadikFeladat(List<Stat> statList)
        {
            Console.WriteLine("3. feladat: ");
            int queriedYear = 0;

            do
            {
                Console.Write("Vizsgálandó év: ");
                queriedYear = Convert.ToInt32(Console.ReadLine());

            } while (!(2009 <= queriedYear && queriedYear <= 2018));

            return queriedYear;
        }

        private static void masodikFeladat(List<Stat> statList)
        {
            Dictionary<string, int> totalOf10YearsList = new Dictionary<string, int>();

            foreach (var stat in statList)
            {
                totalOf10YearsList.Add(stat.nyelv, stat.osszesNyelvvizsga); 
            }

            var top3 = totalOf10YearsList.OrderByDescending(pair => pair.Value).Take(3);

            Console.WriteLine("2. feladat: A legnépszerűbb nyelvek: ");
            foreach (var top in top3)
            {
                Console.WriteLine(top.Key + " " + top.Value);          
            }
        }
    }
}

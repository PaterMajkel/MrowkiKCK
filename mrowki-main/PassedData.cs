using System;
using System.Collections.Generic;
using System.Text;

namespace mrowki
{
    public class PassedData
    {
        public int mutChance { get; set; }
        public int lifeLength { get; set; }
        public int popSize { get; set; }

        public PassedData()
        {
            mutChance = 5;
            lifeLength = 500;
            popSize = 100;
        }
    }
}

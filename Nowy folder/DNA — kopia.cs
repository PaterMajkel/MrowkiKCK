using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace mrowki.genetic
{
    public class DNA
    {
        // The genetic sequence
        public int[] genes;
        Point slocation;
        public Point currentPos;
        int[] prob = new int[8];
        /*
         * 0 1 2
         * 3   4
         * 5 6 7
         * 
         */
        public bool dead = false;
        Random rand = new Random();

        // Constructor (makes a random DNA)
        public DNA(Point location,int time)
        {
            genes = new int[time];
            this.slocation = location;
            this.currentPos = location;

            for(int i=0; i<time; i++)
            {
                genes[i]=rand.Next(0, 8);
                prob[genes[i]]++;
            }

        }

        // Crossover
        public DNA Crossover(DNA partner)
        {
            // A new child
            DNA child = new DNA(slocation, partner.genes.Length);

            int midpoint = rand.Next(genes.Count()); // Pick a midpoint

            // Half from one, half from the other
            for (int i = 0; i < genes.Count(); i++)
            {
                if (i > midpoint) child.genes[i]=genes[i];
                else child.genes[i]=partner.genes[i];
            }
            return child;
        }

        // Based on a mutation probability, picks a new random character
        public void Mutate(double mutationRate)
        {
            for (int i = 0; i < genes.Count(); i++)
            {
                if (rand.NextDouble() < mutationRate)
                {
                    genes[i] = rand.Next(0, 8);
                }
            }
        }
    }
}

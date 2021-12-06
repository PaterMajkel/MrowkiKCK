using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Globalization;
using System.Windows.Controls.Primitives;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace mrowki.genetic
{
    public class Population
    {
        double mutationRate;           // Mutation rate
        public Ant[] population;             // Array to hold the current population
        List<Ant> matingPool;    // ArrayList which we will use for our "mating pool"
        public int generations;              // Number of generations
        public bool finished;             // Are we finished evolving?
        int perfectScore;
        public int time;
        public List<Rectangle> rectangles;
        Canvas Mrowisko;
        public Population(float m, int num, int time, ref List<Rectangle> rectangles, ref Canvas Mrowisko)
        {
            this.Mrowisko = Mrowisko;
            this.rectangles = rectangles;
            mutationRate = m;
            population = new Ant[num];
            this.time = time;
            matingPool = new List<Ant>();
            for (int i = 0; i < population.Count(); i++)
            {
                population[i] = new Ant();
                matingPool.Add(population[i]);
            }
            CalcFitness();
            //matingPool = new List<DNA>();
            finished = false;
            generations = 0;

            perfectScore = 1;
        }

        // Fill our fitness array with a value for every member of the population
        public void CalcFitness()
        {
            for (int i = 0; i < population.Count(); i++)
            {
                population[i].Fitness();
            }
        }




        public Ant MonteCarlo(double maxFitness)
        {
            Random rand = new Random();
            int escape = 0;
            while (true)
            {
                int pop = rand.Next(0, population.Length);
                int max = (int)Math.Floor(maxFitness * 1000) + 1;
                int r = rand.Next(0, max);
                Ant partner = this.population[pop];
                if (r < partner.Fitness() * 1000)
                {
                    return partner;
                }
                escape++;
                if (escape > 10000)
                {
                    return null;
                }

            }

        }

        // Create a new generation
        public void Generate()
        {

            double maxFitness = 0;
            for (int i = 0; i < population.Count(); i++)
            {
                if (population[i].fitness > maxFitness)
                {
                    maxFitness = population[i].fitness;
                }
            }

            Ant[] newPopulation = new Ant[population.Length];
            // Refill the population with children from the mating pool
            for (int i = 0; i < population.Count(); i++)
            {
                Ant partnerA = MonteCarlo(maxFitness);
                Ant partnerB = MonteCarlo(maxFitness);
                Ant child = new Ant();
                child.gene = partnerA.gene.Crossover(partnerB.gene);
                child.gene.Mutate(mutationRate);
                newPopulation[i] = child;
            }
            population = newPopulation;
            generations++;
        }

        /*public void NaturalSelection()
        {
            matingPool.Clear();
            CalcFitness();
            foreach (DNA c in population)
            {
                for (int i = 0; i < (c.fitness * 100); i++)
                {
                    matingPool.Add(c);
                }
            }
            DNA best = population[GetBest()];
            for (int i = 0; i < best.fitness * 100; i++)
            {
                matingPool.Add(best);
            }
        }
        public void Generate()
        {
            Random random = new Random();
            for (int i = 0; i < population.Count(); i++)
            {
                int a = random.Next() % matingPool.Count;
                int b = random.Next() % matingPool.Count;
                DNA parent1 = matingPool[a];
                DNA parent2 = matingPool[b];
                DNA child = parent1.Crossover(parent2);
                child.Mutate(mutationRate);
                population[i] = child;

            }
            generations++;
        }*/


        // Compute the current "most fit" member of the population
        public int GetBest()
        {
            double worldrecord = 0.0;
            int index = 0;
            for (int i = 0; i < population.Length; i++)
            {
                if (population[i].fitness > worldrecord)
                {
                    index = i;
                    worldrecord = population[i].fitness;
                    if (population[i].finished)
                        finished = true;
                }
            }

            if (worldrecord == perfectScore) finished = true;
            return index;
        }

        public bool Finished()
        {
            return finished;
        }

        public int GetGenerations()
        {
            return generations;
        }

        // Compute average fitness for the population
        public double GetAverageFitness()
        {
            double total = 0;
            for (int i = 0; i < population.Count(); i++)
            {
                total += population[i].fitness;
            }
            return total / (population.Count());
        }

        public Point[] GetLocations()
        {
            Point[] points = new Point[population.Length];
            for (int i = 0; i < population.Length; i++)
            {
                points[i] = population[i].currentPos;
            }
            return points;

        }
        public Point[] GetEndLocations()
        {
            Point[] points = new Point[population.Length];
            for (int i = 0; i < population.Length; i++)
            {
                points[i] = population[i].endPosition;
            }
            return points;

        }

        public void Update(int index)
        {
            for (int i = 0; i < population.Length; i++)
            {
                population[i].Update(index, ref rectangles, ref Mrowisko);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace mrowki.genetic
{
    public class Ant
    {
        bool outofbounds=false;
        bool hitawall=false;
        bool isDead = false;
        public bool finished = false;
        public double fitness;
        public DNA gene;
        public Point currentPos = AntData.start;
        public Point endPosition = AntData.start;
        int index;

        public Ant()
        {
            gene = new DNA(AntData.start, AntData.lifetime);
            GetEndPoint();
        }

        public double Fitness()
        {
            //napisac funkcje fitness nadajaca punktacje w zaleznosci od czasu, odleglosci od celu i trafien w przeszkody

            double length = Math.Sqrt(Math.Pow(AntData.target.X - currentPos.X, 2) + Math.Pow(AntData.target.Y - currentPos.Y, 2));
            
            if (length == 0)
            {
                length = 1;
            }
            fitness = (1 / length) + 1 / (1 + index) / 10;

            if (hitawall)
                fitness *= 0.2;
            if (outofbounds)
                fitness *= 0.2;
            if (finished)
                fitness *= 10000;

            return fitness;


        }

        public void changeLocation(int a, bool end= false)
        { 
            Point position = new Point
            {
                X = 0,
                Y = 0,

            };
        
            switch (a)
            {
                case 0:
                    {
                        position.X -= 3;
                        position.Y += 3;
                        break;
                    }
                case 1:
                    {
                        position.Y += 3;
                        break;
                    }
                case 2:
                    {
                        position.X += 3;
                        position.Y += 3;
                        break;
                    }
                case 3:
                    {
                        position.X -= 3;
                        break;
                    }
                case 4:
                    {
                        position.X += 3;
                        break;
                    }
                case 5:
                    {
                        position.X -= 3;
                        position.Y -= 3;
                        break;
                    }
                case 6:
                    {
                        position.Y -= 3;
                        break;
                    }
                case 7:
                    {
                        position.X += 3;
                        position.Y -= 3;
                        break;
                    }


            }
            if(end==true)
            {
                endPosition.X += position.X;
                endPosition.Y += position.Y;
            }
            else
            {
                currentPos.X += position.X;
                currentPos.Y += position.Y;
            }    
        }

        public void GetEndPoint()
        {
            foreach (var x in gene.genes)
                changeLocation(x, true);
        }

        public void Update(int index, ref List<Rectangle> rects, ref Canvas Mrowisko)
        {
            if (isDead || finished)
                return;
            this.index = index;
            changeLocation(gene.genes[index]);
            if(Math.Abs(currentPos.X-AntData.target.X)<10 && Math.Abs(currentPos.Y-AntData.target.Y)<10)
            {
                finished = true;
            }
            if(currentPos.X<=0 || currentPos.X>= 624 || currentPos.Y <= 0 || currentPos.Y >= 390)
            {
                outofbounds = true;
                isDead = true;
            }
            foreach(var rect in rects)
            {
                if(currentPos.X>=Canvas.GetLeft(rect) && currentPos.Y>=Canvas.GetTop(rect) && currentPos.X <= Canvas.GetLeft(rect)+rect.Width && currentPos.Y <= Canvas.GetTop(rect)+rect.Height)
                {
                    hitawall = true;
                    isDead = true;
                }
            }
        }

    }
}

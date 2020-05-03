using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_sharp
{
    class TerrainGenerator
    {
        int size;
        int seed;
        float roughness;
        float[,] terra;
        static int i_flag = int.MinValue / 4;
        public TerrainGenerator(int detail)
        {
            Random rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            this.roughness = 1.5f;
            this.seed = rnd.Next(int.MinValue/2, int.MaxValue/2);
            this.size = (int)Math.Pow(2, detail) + 1;
        }
        public void SetSize(int size)
        {
            this.size = (int)Math.Pow(2, Math.Abs(size)) + 1;
        }
        public int Size
        {
            get { return this.size; }
        }
        public void SetSeed(int seed)
        {
            this.seed = seed;
        }
        public int Seed
        {
            get { return this.seed; }
        }
        public void SetRoughness(float roughness)
        {
            this.roughness = Math.Abs(roughness);
        }
        public float Roughness
        {
            get { return this.roughness; }
        }
        private float GenerateRandomValue()
        {
            Random rnd = new Random(this.seed + i_flag);
            i_flag++;
            return rnd.Next(0, 255);
        }
        private float GetCellHeight(int x, int y)
        {
            try
            {
                return this.terra[x, y];
            }
            catch (Exception e)
            {
                return float.MinValue;
            }
        }
        private void Diamond(int x, int y, int size)
        {
            float a, b, c, d;
            if(this.GetCellHeight(x, y - size) != float.MinValue)
            {
                a = this.GetCellHeight(x, y - size);
            }
            else
            {
                a = this.GenerateRandomValue();
            }
            if (this.GetCellHeight(x, y + size) != float.MinValue)
            {
                b = this.GetCellHeight(x, y + size);
            }
            else
            {
                b = this.GenerateRandomValue();
            }
            if (this.GetCellHeight(x - size, y) != float.MinValue)
            {
                c = this.GetCellHeight(x - size, y);
            }
            else
            {
                c = this.GenerateRandomValue();
            }
            if (this.GetCellHeight(x + size, y) != float.MinValue)
            {
                d = this.GetCellHeight(x + size, y);
            }
            else
            {
                d = this.GenerateRandomValue();
            }

            float average = (a + b + c + d) / 4;

            this.terra[x, y] = average + this.GenerateRandomValue();
        }
        private void Square(int x, int y, int size)
        {
            float a, b, c, d;
            if (this.GetCellHeight(x, y - size) != float.MinValue)
            {
                a = this.GetCellHeight(x, y - size);
            }
            else
            {
                a = this.GenerateRandomValue();
            }
            if (this.GetCellHeight(x, y + size) != float.MinValue)
            {
                b = this.GetCellHeight(x, y + size);
            }
            else
            {
                b = this.GenerateRandomValue();
            }
            if (this.GetCellHeight(x - size, y) != float.MinValue)
            {
                c = this.GetCellHeight(x - size, y);
            }
            else
            {
                c = this.GenerateRandomValue();
            }
            if (this.GetCellHeight(x + size, y) != float.MinValue)
            {
                d = this.GetCellHeight(x + size, y);
            }
            else
            {
                d = this.GenerateRandomValue();
            }

            float average = (a + b + c + d) / 4;
            this.terra[x, y] = average + this.GenerateRandomValue();
            this.Diamond(x, y - size, size);
            this.Diamond(x - size, y, size);
            this.Diamond(x, y + size, size);
            this.Diamond(x + size, y, size);
        }
        private void Divide(int stepSize)
        {
            int half = (int)Math.Floor((double)stepSize / 2);
            if(half < 1)
            {
                return;
            }
            else
            {
                for (int x = half; x < this.size; x += stepSize)
                    for (int y = half; y < this.size; y += stepSize)
                        this.Square(x, y, half);
                this.Divide(half);
            }
        }
        public float[,] Generate()
        {
            int last = this.size - 1;
            this.terra = new float[size, size];
            this.terra[0, 0] =this.GenerateRandomValue();
            this.terra[0, last] =this.GenerateRandomValue();
            this.terra[last, 0] =this.GenerateRandomValue();
            this.terra[last, last] =this.GenerateRandomValue();


            this.Divide(this.size);
            return this.terra;
        }
    }
}

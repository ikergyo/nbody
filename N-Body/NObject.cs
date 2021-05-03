using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using Microsoft.Xna.Framework.Graphics;

namespace N_Body
{
    public class NObject
    {
        //1 pixel 1e+6 km --> 1e+9 meter
        const double converter = 1e+9;

        private string name;
        public double posx, posy;
        public double render_posx, render_posy;
        public double temp_posx, temp_posy;
        public double velx, vely;
        private float size;
        private double mass;
        private Texture2D _texture;
        private Microsoft.Xna.Framework.Color color;

        //Position should be added in pixelunit
        public NObject(string name, double posx, double posy, double velx, double vely, float size, double mass, Microsoft.Xna.Framework.Color color)
        {
            this.name = name;
            this.posx = posx * converter;
            this.posy = posy * converter;
            this.temp_posx = this.posx;
            this.temp_posy = this.posy;
            this.velx = velx;
            this.vely = vely;
            this.size = size;
            this.mass = mass;
            this.Color = color;
        }

        public void LoadTexture(Texture2D inputTex)
        {
            Texture = inputTex;
            Microsoft.Xna.Framework.Color[] data = new Microsoft.Xna.Framework.Color[Texture.Width * Texture.Height];
            Texture.GetData(data);
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i].R != 0 && data[i].G != 0 && data[i].B != 0)
                    data[i] = Color;
            }
            Texture.SetData(data);
        }
        public Vector2 getDrawVector()
        {
            float px = (float)(posx / converter);
            float py = (float)(posy / converter);
            return new Vector2(px, py);
        }
        public static NObject GenerateRandomObject(int h, int w)
        {
            var rand = new Random();

            int xCoord = rand.Next(0, w / 2);
            int yCoord = rand.Next(0, h / 2);

            int xmultiplier = 1;
            int ymultiplier = 1;

            if (rand.Next(0, 100) % 2 == 0)
                xmultiplier = -1;

            if (rand.Next(0, 100) % 2 == 0)
                ymultiplier = -1;

            xCoord *= xmultiplier;
            yCoord *= ymultiplier;

            double min = 0.010;
            double max = 0.019;
            double size = rand.NextDouble() * (max - min) + min;

            int r = rand.Next(0, 255);
            int g = rand.Next(0, 255);
            int b = rand.Next(0, 255);


            double mmin = 1.059;
            double mmax = 3.050;
            int massPower = rand.Next(19, 29);
            double massBase = rand.NextDouble() * (mmax - mmin) + mmin;

            //velocitiy x -343889.07078210439
            //velocity y 193042.90413259223
            
            return new NObject("RandObject", xCoord, yCoord, 0.0, 0.0, (float)size, massBase * Math.Pow(10, massPower), new Microsoft.Xna.Framework.Color(r, g, b));
        }
        public string Name { get => name; set => name = value; }
        public float Size { get => size; set => size = value; }
        public double Mass { get => mass; set => mass = value; }
        public Texture2D Texture { get => _texture; set => _texture = value; }
        public Microsoft.Xna.Framework.Color Color { get => color; set => color = value; }
    }
}

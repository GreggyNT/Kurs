using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurs
{
    internal class HeartCont
    {
        public Sprite sprite;
        public int count;

        public HeartCont(int c)
        {
            count = c;
            sprite = new Sprite(new Texture($"images/{count}hp.png"));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurs
{
    internal class PlatBox
    {
        public float _x, _y, _w, _h;
        public PlatBox(float X, float Y, float H, float W)
        {
            _x = X;
            _w = W;
            _h = H;
            _y = Y;
        }
    }
}

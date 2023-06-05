using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Kurs
{
    internal class Player
    {
        public bool onGround, onPlat, isShooting, loose;
        public float x, y, w, h, dx, dy, speed, a;
        public HeartCont hp = new HeartCont(3);
        public string File;
        public Sprite sprite;

        public Player(string F, float X, float Y, float W, float H)
        {
            dx = 0; dy = 0; speed = 0; a = 0; onGround = false; onPlat = false; isShooting = false; loose = false;
            File = F;
            w = W; h = H;

            sprite = new Sprite(new Texture($"images/{File}"));
            x = X; y = Y;
            sprite.Position = new SFML.System.Vector2f(0, 0);
            sprite.TextureRect = new IntRect(0,0,(int)w,(int)h);
        }

        public void proc(Bullet pl)
        {
            if ((x <= pl._x + 16 && pl._x + 16 <= x + 60) && (y <= pl._y + 16 && pl._y + 16 <= y + h) && !pl.destr)
            {
                hp.count--;
                hp.sprite.Texture = new Texture($"images/{hp.count}hp.png");
                pl.destr = true;
            }
        }

        public void update(PlatBox pl1, PlatBox pl2, PlatBox pl3, PlatBox pl4)
        {
            x += speed * 1;
            y =y+ (float)(a * 0.8);
            if ((int)y == 700)
            {
                a = 0;
                onGround = true;
            }
            else if (((x >= pl1._x && x <= pl1._x + pl1._w) && (y >= pl1._y - h && y <= pl1._y - h + pl1._h)) || ((x >= pl2._x && x <= pl2._x + pl2._w) && (y >= pl2._y - h && y <= pl2._y - h + pl2._h)) || ((x <= pl3._x && x >= pl3._x - pl3._w) && (y >= pl3._y - h && y <= pl3._y - h + pl3._h)) || ((x <= pl4._x && x >= pl4._x - pl4._w) && (y >= pl4._y - h && y <= pl4._y - h + pl4._h)))
            {
                onPlat = true;
                onGround = true;
                a = 0;
            }
            else
            {
                a =(float)(a+ 0.0025);
                onGround = false;
            }
            if (x < 60)
                x = 60;
            if (x > 1676)
                x = 1676;
            sprite.Position = new SFML.System.Vector2f(x, y);
        }
    }
}

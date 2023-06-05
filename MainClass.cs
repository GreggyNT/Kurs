using SFML;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using static System.Formats.Asn1.AsnWriter;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.InteropServices.JavaScript;
using System.Timers;
using System.Net.Sockets;
using System.Net;

namespace Kurs
{
    public class MainClass
    {
        public static bool fl = false;
        public static bool shot_spee1 = false;
        public static bool shot_spee2 = false;
        public static bool[] bools_tosen;
        public static bool[] receive;
        public static int score1 = 0;
        public static int score2 = 0;
        static List<Bullet> bullets;
        public static void Menu(RenderWindow window)
        {
            Sprite menu2 = new Sprite(new Texture("images/MenuPlay.png"));
            Sprite menu3 = new Sprite(new Texture("images/MenuQuit.png"));
            Sprite menuBg = new Sprite(new Texture("images/MenuMain.png"));
            Sprite menuBT = new Sprite(new Texture("images/button.png"));
            bool isMenu = true;
            int menuNum = 0;
            menuBT.Position = new Vector2f(270 + 61, 300);
            menu2.Position = new Vector2f(270, 400);
            menu3.Position = new Vector2f(270, 500);
            menuBg.Position = new Vector2f(0, 0);
            window.Closed += (sender, args) => window.Close();
            while (isMenu)
            {
                menu2.Color = Color.White;
                menu3.Color = Color.White;
                menuBT.Color = Color.White;
                menuNum = -1;
                if (new IntRect(331, 300, 98, 98).Contains(Mouse.GetPosition(window).X, Mouse.GetPosition(window).Y)) { menuBT.Color = Color.Blue; menuNum = 0; }
                if (new IntRect(270, 500, 220, 80).Contains(Mouse.GetPosition(window).X, Mouse.GetPosition(window).Y)) { menu3.Color = Color.Blue; menuNum = 2; }
                if (new IntRect(270, 400, 220, 80).Contains(Mouse.GetPosition(window).X, Mouse.GetPosition(window).Y)) { menu2.Color = Color.Blue; menuNum = 1; }
                if (Mouse.IsButtonPressed(Mouse.Button.Left))
                {
                    if (menuNum == 0) isMenu = false;
                    if (menuNum == 1)
                    {
                        RenderWindow window1 = new RenderWindow(new VideoMode(800, 600), "Guide");
                        window1.Clear(new Color(129, 181, 221));
                        Sprite spr = new Sprite(new Texture("images/Guide.png"));
                        spr.Position = new Vector2f(0, 0);
                        window1.Draw(spr);
                        window1.Display();
                        window1.Closed += (sender, args) => window1.Close();
                        while (window1.IsOpen)
                            window1.DispatchEvents();
                    }
                    if (menuNum == 2) { window.Close(); isMenu = false; fl = true; }
                }
                window.Draw(menuBg);
                RectangleShape rectangle = new RectangleShape(new Vector2f(220, 80));
                rectangle.FillColor = Color.White;
                rectangle.Position = new Vector2f(270, 400);
                window.Draw(rectangle);
                rectangle.Position = new Vector2f(270, 500);
                window.Draw(rectangle);
                window.Draw(menuBT);
                window.Draw(menu2);
                window.Draw(menu3);
                window.Display();
                window.DispatchEvents();
                if ((Keyboard.IsKeyPressed(Keyboard.Key.Escape)))
                    window.Close();
            }
        }
        static void Main()
        { 
            RenderWindow window1 = new RenderWindow(new VideoMode(800, 600), "Menu");
            Menu(window1);
            window1.Close();
            TcpClient client;
            if (Dns.GetHostEntry(Dns.GetHostName())
                .AddressList
                .First(address => address.AddressFamily == AddressFamily.InterNetwork)
.               ToString()== "192.168.189.1")
            {
                TcpListener tcpListener = new TcpListener(IPAddress.Any, 7000);
                Console.WriteLine("Starts");
                tcpListener.Start();
                client = tcpListener.AcceptTcpClient();
            }
            else
            {
                client = new TcpClient("192.168.50.11", 7000);
            }
            NetworkStream stream = client.GetStream();
            RenderWindow window = new RenderWindow(new VideoMode(1700, 894), "Moi megaman");
            window.Closed += (sender, args) => window.Close();
            if (fl == true)
                window.Close();
            Music music = new Music("music.ogg");
            music.Play();
            System.Timers.Timer timer2 = new System.Timers.Timer(200);
            timer2.Start();
            timer2.Elapsed += OnTimedEvent2;
            System.Timers.Timer timer1 = new System.Timers.Timer(200);
            timer1.Start();
            timer1.Elapsed += OnTimedEvent1;
        beg:
            bools_tosen = new bool[5];
            receive = new bool[5];
            bullets = new List<Bullet>();
            SoundBuffer shootBuffer = new SoundBuffer("shoot.ogg");
            Sound shoot = new Sound(shootBuffer);
            Font font = new Font("Roboto-Regular.ttf");
            SFML.Graphics.Text text = new SFML.Graphics.Text("", font, 40);
            String str;
            text.Style = SFML.Graphics.Text.Styles.Bold;
            text.Position = new Vector2f(850, 800);

            Player p = new Player("spritifulhd.png", 10, 700, 53, 78);
            Player p1 = new Player("spritifulhd1.png", 1620, 700, 53, 78);
            p1.sprite.Position = new Vector2f(1620, 700);
            p1.hp.sprite.Position = new Vector2f(1650, 800);
            p.sprite.Position = new Vector2f(10, 700);
            p.hp.sprite.Position = new Vector2f(50, 800);
            Bullet ff = new Bullet();
            Sprite SpriteBull = new Sprite(new Texture("images/spritifulhd.png"));
            SpriteBull.TextureRect = new IntRect(55 * 19 + 30, 31, 32, 32);
            Bullet ff1 = new Bullet();
            ff1.destr = true;
            ff.destr = true;
            Sprite SpriteBull1;
            SpriteBull1 = new Sprite(new Texture(("images/spritifulhd.png")));
            SpriteBull1.TextureRect = new IntRect(55 * 19 + 30, 31, 32, 32);
            Sprite PlatSprite;
            PlatSprite = new Sprite(new Texture("images/platform.png"));
            PlatSprite.TextureRect = new IntRect(132, 0, 277, 37);
            PlatSprite.Position = new Vector2f(100, 650);
            Clock clock = new Clock();
            int backI = 1;
            Sprite Backgr;
            Backgr = new Sprite(new Texture($"images/Background-{backI}.png"));
            PlatBox plat1 = new(100, 650, 37, 277);
            PlatBox plat2 = new(500, 450, 37, 277);
            PlatBox plat3 = new(1636, 650, 37, 277);
            PlatBox plat4 = new(1236, 450, 37, 277);
            float CurrentFrame = 0;
            float CurrentFrame1 = 0;
            float heroyawntimer1 = 0;
            float heroyawntimer2 = 0;
            while (((int)CurrentFrame <= 10))
            {
                float time = clock.ElapsedTime.AsMicroseconds();
                clock.Restart();
                time = time / 500;
                CurrentFrame += (float)(0.006 * time);
                p.sprite.TextureRect = new IntRect(55 * (int)(CurrentFrame), 0, (int)p.w, (int)p.h);
                p1.sprite.TextureRect = new IntRect(55 * (int)(CurrentFrame), 0, (int)p.w, (int)p.h);
                p1.sprite.Scale = new Vector2f(-1, 1);
                p1.hp.sprite.Scale = new Vector2f(-1, 1);
                window.Clear();
                window.Draw(Backgr);
                PlatSprite.Position = new Vector2f(100, 650);
                window.Draw(PlatSprite);
                PlatSprite.Position = new Vector2f(500, 450);
                window.Draw(PlatSprite);
                PlatSprite.Position = new Vector2f(1636, 650);
                PlatSprite.Scale = new Vector2f(-1, 1);
                window.Draw(PlatSprite);
                PlatSprite.Position = new Vector2f(1236, 450);
                window.Draw(PlatSprite);
                PlatSprite.Scale = new Vector2f(1, 1);
                window.Draw(p.sprite);
                window.Draw(p1.sprite);
                window.Draw(p.hp.sprite);
                window.Draw(p1.hp.sprite);
                window.Display();
            }
            float backtimer = 0;
            CurrentFrame = 0;
            stream.WriteByte(ConvertBoolArrayToByte(bools_tosen));
            while (window.IsOpen)
            {
                receive = ConvertByteToBoolArray((byte)stream.ReadByte());
                bools_tosen = new bool[5]; 
                if (Keyboard.IsKeyPressed(Keyboard.Key.R))
                    if (Dns.GetHostEntry(Dns.GetHostName())
                .AddressList
                .First(address => address.AddressFamily == AddressFamily.InterNetwork)
                .ToString() == "192.168.189.1")
                        goto beg;
                if ((Keyboard.IsKeyPressed(Keyboard.Key.Escape)))
                    window.Close();
                float time = clock.ElapsedTime.AsMicroseconds();
                clock.Restart();
                time = time / 500;
                heroyawntimer1 += time;
                heroyawntimer2 += time;
                backtimer += time;
                window.DispatchEvents();
                if ((Keyboard.IsKeyPressed(Keyboard.Key.Enter)))
                {
                    bools_tosen[0] = true;
                    p.isShooting = true;
                    if (shot_spee1)
                    {
                        shot_spee1 = false;
                        ff = new Bullet();
                        
                        shoot.Play();
                        ff.destr = false;
                        if (p.sprite.Scale.X == 1)
                        {
                            ff._speed = 5F;
                            ff._x = p.x + p.w;
                            ff._y = p.y + p.h / 2;

                        }

                        else
                        {
                            ff._speed = -5F;
                            ff._x = p.x - p.w - 30;
                            ff._y = p.y + p.h / 2;
                        }
                        bullets.Add(ff);
                        heroyawntimer1 = 0;
                    }
                }
                if ((Keyboard.IsKeyPressed(Keyboard.Key.Up)))
                {
                    bools_tosen[1] = true;
                    if (p.onGround)
                    {
                        p.a = -2F;
                        p.onGround = false;
                    }
                    heroyawntimer1 = 0;
                }
                if ((Keyboard.IsKeyPressed(Keyboard.Key.Left)))
                {
                    bools_tosen[2] = true;
                    CurrentFrame +=(float) 0.005 * time;
                    p.speed = -1F;
                    if (CurrentFrame > 3) CurrentFrame -= 3;
                    p.sprite.TextureRect = new IntRect(72 * (int)CurrentFrame, 78, 72,(int) p.h);
                    p.sprite.Scale = new Vector2f(-1, 1);
                    heroyawntimer1 = 0;
                }
                else
                    if ((Keyboard.IsKeyPressed(Keyboard.Key.Right)))
                {
                    bools_tosen[3] = true;
                    p.speed = 1F;
                    CurrentFrame +=(float) 0.005 * time;
                    if (CurrentFrame > 3) CurrentFrame -= 3;
                    p.sprite.TextureRect = new IntRect(72 * (int)(CurrentFrame), 78, 72,(int) p.h);
                    p.sprite.Scale = new Vector2f(1, 1);

                }
                else
                {
                    p.speed = 0;
                    if (!p.onGround)
                        p.sprite.TextureRect = new IntRect(55 * 16, 0, 55, 78);
                    else
                        p.sprite.TextureRect = new IntRect(55 * 10, 0, 51, 78);
                    if (p.isShooting)
                    {
                        p.sprite.TextureRect = new IntRect(56 * 18, 0, 70, 78);
                        p.isShooting = false;
                    }
                    if (heroyawntimer1 > 10000)
                        if (CurrentFrame > 5)
                        {
                            CurrentFrame -= 5;
                            heroyawntimer1 = 0;
                            p.sprite.TextureRect = new IntRect(55 * 11, 0,(int) p.w,(int) p.h);
                        }
                        else
                        {
                            CurrentFrame +=(float)0.002 * time;
                            p.sprite.TextureRect= new IntRect(54 * (int)(CurrentFrame) + 55 * 11, 0, 51, 78);
                        }
                }
                p.update(plat1, plat2, plat3, plat4);
                if (receive[0])
                {
                   p1.isShooting = true;
                    if (shot_spee2)
                    {
                        ff1 = new Bullet();
                        shot_spee2 = false;

                        shoot.Play();
                        ff1.destr = false;
                        if (p1.sprite.Scale.X == 1)
                        {
                            ff1._speed = 5F;
                            ff1._x = p1.x + p1.w;
                            ff1._y = p1.y + p1.h / 2;
                        }
                        else
                        {
                            ff1._speed = -5F;
                            ff1._x = p1.x - p1.w - 30;
                            ff1._y = p1.y + p1.h / 2;
                        }
                        heroyawntimer2 = 0;
                        bullets.Add(ff1);
                    }
                  
                }
                if (receive[1])
                {
                    if (p1.onGround)
                    {
                        p1.a = -2F;
                        p1.onGround = false;
                    }
                    heroyawntimer2 = 0;
                }
                if (receive[3])
                {
                    CurrentFrame1 +=(float) 0.005 * time;
                    p1.speed = -1F;
                    if (CurrentFrame1 > 3) CurrentFrame1 -= 3;
                    p1.sprite.TextureRect = new IntRect(72 * (int)(CurrentFrame1), 78, 72, (int)p1.h);
                    p1.sprite.Scale = new Vector2f(-1, 1);
                    heroyawntimer2 = 0;
                }
                else
                    if (receive[2])
                {
                    p1.speed = 1F;
                    CurrentFrame1 += (float)0.005 * time;
                    if (CurrentFrame1 > 3) CurrentFrame1 -= 3;
                    p1.sprite.TextureRect = new IntRect(72 * (int)(CurrentFrame1), 78, 72,(int) p1.h);
                    p1.sprite.Scale = new Vector2f(1, 1);

                }
                else
                {
                    p1.speed = 0;
                    if (!p1.onGround)
                        p1.sprite.TextureRect = new IntRect(55 * 16, 0, 55, 78);
                    else
                        p1.sprite.TextureRect = new IntRect(55 * 10, 0, 51, 78);
                    if (p1.isShooting)
                    {

                        p1.sprite.TextureRect = new IntRect(56 * 18, 0, 70, 78);
                        p1.isShooting = false;
                    }
                    if (heroyawntimer2 > 10000)
                        if (CurrentFrame1 > 5)
                        {
                            CurrentFrame1 -= 5;
                            heroyawntimer2 = 0;
                            p1.sprite.TextureRect = new IntRect(55 * 11, 0,(int) p1.w,(int) p1.h);
                        }
                        else
                        {
                            CurrentFrame1 += (float) 0.002 * time;
                            p1.sprite.TextureRect = new IntRect(54 * (int)(CurrentFrame1) + 55 * 11, 0, 51, 78);
                        }
                }
                p1.update(plat1, plat2, plat3, plat4);
                if (backtimer > 300)
                {
                    backI =++backI%8;
                    Backgr = new Sprite (new Texture($"images/Background-{backI}.png"));
                    backtimer = 0;

                }
                window.Clear();
                window.Draw(Backgr);
                PlatSprite.Position = new Vector2f(100, 650);
                window.Draw(PlatSprite);
                PlatSprite.Position = new Vector2f(500, 450);
                window.Draw(PlatSprite);
                PlatSprite.Scale = new Vector2f(-1, 1);
                PlatSprite.Position = new Vector2f(1636, 650);
                window.Draw(PlatSprite);
                PlatSprite.Position = new Vector2f(1236, 450);
                window.Draw(PlatSprite);
                PlatSprite.Scale = new Vector2f(1, 1);
                if (p.hp.count > 0)
                {
                }
                else
                {
                    score2++;
                    p1.sprite.TextureRect = new IntRect(21, 434, 62, 62);
                    p.sprite.TextureRect = new IntRect(324, 586, 75, 75);
                    p1.sprite.Position = new Vector2f(p1.x, p1.y + 30);
                    p.sprite.Position = new Vector2f(p.x, p.y + 30);
                    window.Draw(p.sprite);
                    window.Draw(p1.sprite);
                    Sprite image = new Sprite(new Texture("images/2-win.png"));
                    image.Position=  new Vector2f(650, 200);
                    window.Draw(image);
                    window.Display();
                    while (!((Keyboard.IsKeyPressed(Keyboard.Key.R)) || (Keyboard.IsKeyPressed(Keyboard.Key.Escape)) || Keyboard.IsKeyPressed(Keyboard.Key.Space) || receive[4]))
                    {
                        stream.WriteByte(ConvertBoolArrayToByte(bools_tosen));
                        receive = ConvertByteToBoolArray((byte)stream.ReadByte());
                    }
                    if (Keyboard.IsKeyPressed(Keyboard.Key.R) || Keyboard.IsKeyPressed(Keyboard.Key.Space) || receive[4])
                    {

                        ff._speed = 0;
                        ff1._speed = 0;
                        bools_tosen[4] = true;
                        stream.WriteByte(ConvertBoolArrayToByte(bools_tosen));
                        goto beg;
                    }
                    else
                        window.Close();
                }
                if (p1.hp.count > 0)
                    window.Draw(p1.sprite);
                else
                {
                    score1++;
                    p.sprite.TextureRect = new IntRect(21, 434, 62, 62);
                    p1.sprite.TextureRect = new IntRect(324, 586, 75, 75);
                    p.sprite.Position = new Vector2f(p.x, p.y + 30);
                    p1.sprite.Position = new Vector2f(p1.x, p1.y + 30);
                    window.Draw(p.sprite);
                    window.Draw(p1.sprite);
                    Sprite image = new Sprite(new Texture("images/1-win.png"));
                    image.Position = new Vector2f(650, 200);
                    window.Draw(image);
                    window.Display();
                    while (!(Keyboard.IsKeyPressed(Keyboard.Key.R) || (Keyboard.IsKeyPressed(Keyboard.Key.Escape)) || Keyboard.IsKeyPressed(Keyboard.Key.Space)|| receive[4]))
                    {
                        stream.WriteByte(ConvertBoolArrayToByte(bools_tosen));
                        receive = ConvertByteToBoolArray((byte)stream.ReadByte());
                    }
                    if (Keyboard.IsKeyPressed(Keyboard.Key.R) || Keyboard.IsKeyPressed(Keyboard.Key.Space) || Keyboard.IsKeyPressed(Keyboard.Key.Space) || receive[4])
                    {
                        bools_tosen[4] = true; 
                        p.isShooting = true;
                        p1.isShooting = true;
                        goto beg;
                    }
                    else
                        window.Close();

                }
                window.Draw(p.sprite);
                foreach (var f in bullets)
                {
                    f._x += f._speed;
                    SpriteBull.Position = new Vector2f(f._x, f._y);
                    p1.proc(f);
                    p.proc(f);
                    if (!f.destr)
                        window.Draw(SpriteBull);
                }
              /*  ff._x += ff._speed;
                SpriteBull.Position = new Vector2f(ff._x, ff._y);
                ff = p1.proc(ff);
                if (!ff.destr)
                    window.Draw(SpriteBull);
                ff1._x += ff1._speed;
                SpriteBull1.Position = new Vector2f(ff1._x, ff1._y);
                if (!ff1.destr)
                    window.Draw(SpriteBull1);
                ff1 = p.proc(ff1);*/
                str = $"{score1}:{score2}";
                text.DisplayedString = str;
                window.Draw(text);
                window.Draw(p.hp.sprite);
                window.Draw(p1.hp.sprite);
                window.Display();
                stream.WriteByte(ConvertBoolArrayToByte(bools_tosen));
                bools_tosen = new bool[5];
            }
        }

        private static void OnTimedEvent1(Object source, ElapsedEventArgs e)
        {
            shot_spee1 = true;
        }
        private static void OnTimedEvent2(Object source, ElapsedEventArgs e)
        {
            shot_spee2 = true;
        }
        private static byte ConvertBoolArrayToByte(bool[] source)
        {
            byte result = 0;
            // This assumes the array never contains more than 8 elements!
            int index = 8 - source.Length;

            // Loop through the array
            foreach (bool b in source)
            {
                // if the element is 'true' set the bit at that position
                if (b)
                    result |= (byte)(1 << (7 - index));

                index++;
            }

            return result;
        }

        private static bool[] ConvertByteToBoolArray(byte b)
        {
            // prepare the return result
            bool[] result = new bool[5];

            // check each bit in the byte. if 1 set to true, if 0 set to false
            for (int i = 0; i < 5; i++)
                result[i] = (b & (1 << i)) != 0;

            // reverse the array
            Array.Reverse(result);

            return result;
        }

    }
}
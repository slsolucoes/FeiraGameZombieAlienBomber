using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;
using System.Timers;


namespace FeiraGameZombieAlienBomber {
    class GraphicEngine {
        /*-MEMBERS-*/

        private Graphics drawHandle;
        private Thread renderThread;
        public static Player gari = new Player();
        public static genericElement bg1 = new genericElement();
        

        private static System.Timers.Timer aTimer;
        private static int Cont;
        private static int vCont;
        private static int vObject;
        private static int vx;
        private static int vy;



        /*FUNCTIONS*/
        public GraphicEngine(Graphics g) {
            drawHandle = g;
        }
        public void init() {
            //LOAD ASSETS
            loadAssets();
            loadCharacters();
            loadElements();


            
            aTimer = new System.Timers.Timer(200);
            aTimer.Elapsed += new ElapsedEventHandler(animLoop);
            aTimer.Interval = 250;
            aTimer.Enabled = true;



            renderThread = new Thread(new ThreadStart(render));
            renderThread.Start();

        }
        private void animLoop(object source, ElapsedEventArgs e) {
            if (gari.animState <= 1 && gari.lastState <= 1) {
                gari.animState += 1;
            }
            if(bg1.posX > -1280) {
                bg1.posX -= 5;
            }
            else {
                bg1.posX = 0;
            }
        }
        private void loadAssets() {
            gari.sprite = FeiraGameZombieAlienBomber.Properties.Resources.varredor;
            bg1.sprite = FeiraGameZombieAlienBomber.Properties.Resources.background1;
        }
        private void loadCharacters() {
            gari.Xcrop = new int[3];
            gari.Xcrop[0] = 0;
            gari.Xcrop[1] = 46;
            gari.Xcrop[2] = 93;
            gari.width = 46;
            gari.height = 64;
            gari.posX = 10;
            gari.posY = 615;
        }
        private void loadElements() {
            bg1.posX = 0;
            bg1.posY = 0;
            bg1.height = 720;
            bg1.width = 2560;
        }
        public void Stop() {
            renderThread.Abort();
        }
        public void Inpots(char e) {

        }


        private void render() {
            int framesRendered = 0;
            long startTime = Environment.TickCount;
            Bitmap frame = new Bitmap(Game.CANVAS_WIDTH, Game.CANVAS_HEIGHT);
            Graphics frameGraphics = Graphics.FromImage(frame);
            GraphicsUnit units = GraphicsUnit.Pixel;

            while (true) {

                if (gari.posY > 615)
                {
                    gari.posY = 615;
                }


                frameGraphics.DrawImage(bg1.sprite, bg1.posX, bg1.posY);
                frameGraphics.DrawImage(bg1.sprite, bg1.posX + bg1.width, bg1.posY);
                frameGraphics.FillRectangle(new SolidBrush(Color.Black), 0, Game.CANVAS_HEIGHT - 30, Game.CANVAS_WIDTH, 30);
                frameGraphics.DrawImage(gari.sprite, gari.posX, gari.posY, new Rectangle(gari.Xcrop[gari.animState], 0, gari.width, gari.height), units);

                vObject += 10;
                vx = Game.CANVAS_WIDTH - vObject;
                vy = Game.CANVAS_HEIGHT - 60;
                frameGraphics.FillRectangle(new SolidBrush(Color.Blue), vx, vy, 30, 30);

                if(vObject > Game.CANVAS_WIDTH)
                {
                    vObject = 0;
                }

                if(gari.posY + 45 == vy && gari.posX == vx )
                {
                    Console.WriteLine("Opa");
                    Console.WriteLine("Opa");

                }

                if (GameWindow.jump == 1)
                {
                    if (Cont < 10 && vCont == 0)
                    {
                        Cont++;
                        gari.posY -= 10;
                        if (Cont == 9)
                        {
                            vCont = 1;
                        }            
                    }
                    if(Cont >= 0 && vCont == 1)
                    {
                        if(Cont == 0 && vCont == 1)
                        {
                            vCont = 0;
                            GameWindow.jump = 0;
                        }
                        Cont--;
                        if(gari.posY > 615)
                        {
                            gari.posY = 615;
                        }
                        else
                        {
                            gari.posY += 10;
                        }                        
                                               
                    }

                }
                
                drawHandle.DrawImage(frame,0,0);
                //Benchmark
                framesRendered++;
                if (Environment.TickCount >= startTime + 1000){
                    Console.WriteLine("GraphicEngine: " + framesRendered + " fps");
                    framesRendered = 0;
                    startTime = Environment.TickCount;
                }
            }
        }
    }
}

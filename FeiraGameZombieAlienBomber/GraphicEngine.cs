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
        public static genericElement[] elemento = new genericElement[1];

        private static System.Timers.Timer aTimer;

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
        private void gravidade(Player target) {
            if (target.isJumping == true) {
                target.posY -= 25;
            }
            if(target.isJumping == true && target.posY < 500) {
                target.isJumping = false;
            }

            if (target.isStepingOnObject() == true) {
                target.isJumping = false;

            }
            else if (target.posY < 615) {
                target.posY += 10;
            }


            

        }
        private void gravidade(genericElement target) {
            if (target.posY < 605) {
                target.posY += 10;
            }

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
            /*for(int i = 0; i < elemento.Length; i++) {
                if (elemento[0].posX > -32) {
                    elemento[0].posX -= 5;
                }
                else {
                    elemento[0].posX = 1280;
                }
            }*/
        }
        private void loadAssets() {
            gari.sprite = FeiraGameZombieAlienBomber.Properties.Resources.zomb;
            bg1.sprite = FeiraGameZombieAlienBomber.Properties.Resources.background1;
        }
        private void loadCharacters() {
            gari.Xcrop = new int[3];
            gari.Xcrop[0] = 0;
            gari.Xcrop[1] = 0;
            gari.Xcrop[2] = 0;
            gari.width = 12;
            gari.height = 58;
            gari.posX = 0;
            gari.posY = 0;
        }
        private void loadElements() {
            elemento[0] = new genericElement();
            elemento[0].sprite = FeiraGameZombieAlienBomber.Properties.Resources.cactus;
            elemento[0].posX = 100;
            elemento[0].posY = 597;
            elemento[0].width = 64;
            elemento[0].height = 64;

            bg1.posX = 0;
            bg1.posY = 0;
            bg1.height = 720;
            bg1.width = 2560;
        }
        public void Stop() {
            renderThread.Abort();
        }

        private void render() {
            int framesRendered = 0;
            long startTime = Environment.TickCount;
            Bitmap frame = new Bitmap(Game.CANVAS_WIDTH, Game.CANVAS_HEIGHT);
            Graphics frameGraphics = Graphics.FromImage(frame);
            GraphicsUnit units = GraphicsUnit.Pixel;

            while (true) {
                gravidade(gari);
                gravidade(elemento[0]);
                frameGraphics.DrawImage(bg1.sprite, bg1.posX, bg1.posY);
                frameGraphics.DrawImage(bg1.sprite, bg1.posX + bg1.width, bg1.posY);
                frameGraphics.FillRectangle(new SolidBrush(Color.Black), 0, Game.CANVAS_HEIGHT - 30, Game.CANVAS_WIDTH, 30);
                frameGraphics.DrawImage(gari.sprite, gari.posX, gari.posY, new Rectangle(gari.Xcrop[gari.animState], 0, gari.width, gari.height), units);
                frameGraphics.DrawImageUnscaled(elemento[0].sprite, elemento[0].posX, elemento[0].posY);


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

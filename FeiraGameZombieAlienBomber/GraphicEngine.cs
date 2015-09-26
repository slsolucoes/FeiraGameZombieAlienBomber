﻿using System;
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

        private Bitmap tex_mczombie;
        private static System.Timers.Timer aTimer;

        /*FUNCTIONS*/
        public GraphicEngine(Graphics g) {
            drawHandle = g;
        }
        public void init() {
            //LOAD ASSETS
            loadAssets();
            loadCharacters();


            aTimer = new System.Timers.Timer(500);
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
        }
        private void loadAssets() {
            tex_mczombie = FeiraGameZombieAlienBomber.Properties.Resources.mczombie;
            gari.sprite = FeiraGameZombieAlienBomber.Properties.Resources.varredor;
        }
        private void loadCharacters() {
            gari.Xcrop = new int[3];
            gari.Xcrop[0] = 0;
            gari.Xcrop[1] = 46;
            gari.Xcrop[2] = 93;
            gari.width = 46;
            gari.height = 64;
            gari.posX = 10;
            gari.posX = 10;
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
                frameGraphics.FillRectangle(new SolidBrush(Color.Aqua), 0, 0, Game.CANVAS_WIDTH, Game.CANVAS_HEIGHT);
                frameGraphics.DrawImage(gari.sprite, gari.posX, gari.posY, new Rectangle(gari.Xcrop[gari.animState], 0, gari.width, gari.height), units);

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

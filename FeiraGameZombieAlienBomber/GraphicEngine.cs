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

        private Bitmap tex_mczombie;
        private Bitmap tex_varredor;
        private static System.Timers.Timer aTimer;

        /*FUNCTIONS*/
        public GraphicEngine(Graphics g) {
            drawHandle = g;
        }
        public void init() {
            //LOAD ASSETS
            loadAssets();
            aTimer = new System.Timers.Timer(500);
            aTimer.Elapsed += new ElapsedEventHandler(animLoop);
            aTimer.Interval = 500;
            aTimer.Enabled = true;

            renderThread = new Thread(new ThreadStart(render));
            renderThread.Start();

        }
        private static void animLoop(object source, ElapsedEventArgs e) {

        }
        private void loadAssets() {
            tex_mczombie = FeiraGameZombieAlienBomber.Properties.Resources.mczombie;
            tex_varredor = FeiraGameZombieAlienBomber.Properties.Resources.varredor;
        }

        public void Stop() {
            renderThread.Abort();
        }

        private void render() {
            int framesRendered = 0;
            long startTime = Environment.TickCount;
            Bitmap frame = new Bitmap(Game.CANVAS_WIDTH, Game.CANVAS_HEIGHT);
            Graphics frameGraphics = Graphics.FromImage(frame);

            while (true) {
                frameGraphics.FillRectangle(new SolidBrush(Color.Aqua), 0, 0, Game.CANVAS_WIDTH, Game.CANVAS_HEIGHT);
                frameGraphics.DrawImageUnscaledAndClipped(tex_varredor, new Rectangle(0, 0, 46, 64));

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

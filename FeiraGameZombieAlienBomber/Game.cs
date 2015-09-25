using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FeiraGameZombieAlienBomber {
    class Game {
        //CONSTANTES
        public const int CANVAS_HEIGHT = 720;
        public const int CANVAS_WIDTH = 1280;

        //MEMBERS
        private GraphicEngine gEngine;


        //FUNCTIONS
        public void startGraphics(Graphics g) {
            gEngine = new GraphicEngine(g);
            gEngine.init();
        }
        
        public void stopGame() {
            gEngine.Stop();
        }
    }
}

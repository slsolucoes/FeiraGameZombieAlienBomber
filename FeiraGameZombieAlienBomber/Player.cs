using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FeiraGameZombieAlienBomber {
    class Player {
        public int[] Xcrop;
        public int[] Ycrop;
        public int posX;
        public int posY;
        public int width;
        public int height;
        public Bitmap sprite;
        public int animState = 0;
        public int lastState = 0;
        
    }
}

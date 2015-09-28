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
        public Boolean isJumping = false;
        public int animState = 0;
        public int lastState = 0;
        
        public void jump() {
            this.isJumping = true;
        }

        public Boolean isStepingOnObject() {
            for(int i = 0; i < GraphicEngine.elemento.Length; i++) {
                if (this.posX > GraphicEngine.elemento[i].posX && this.posX < (GraphicEngine.elemento[i].posX + GraphicEngine.elemento[i].width)) {
                    if ((this.posY + this.height) < (GraphicEngine.elemento[i].posY + 220) && (this.posY +this.height) > GraphicEngine.elemento[i].posY) {
                        return true;
                    }
                    return false;
                }
            }
            return false;
        }




    }
}

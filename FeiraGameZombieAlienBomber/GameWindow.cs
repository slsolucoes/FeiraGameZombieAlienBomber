using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace FeiraGameZombieAlienBomber {
    public partial class GameWindow : Form {
        private Game game = new Game();
        public static int jump;
        
        public GameWindow() {
            InitializeComponent();
        }

        private void canvas_Paint(object sender, PaintEventArgs e) {
            Graphics g = canvas.CreateGraphics();
            game.startGraphics(g);
        }

        private void GameWindow_FormClosing(object sender, FormClosingEventArgs e) {
            game.stopGame();
        }

        private void GameWindow_Load(object sender, EventArgs e) {
            AllocConsole();
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }


        //---------
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAsAttribute(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        private void GameWindow_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == 'd' && GraphicEngine.gari.posX <= 1220) {
                GraphicEngine.gari.posX += 5;
            }
            else if (e.KeyChar == 'a' && GraphicEngine.gari.posX >= 0) {
                GraphicEngine.gari.posX -= 5;
            }
            else if(e.KeyChar == 'w' && GraphicEngine.gari.posY > 615 || e.KeyChar == 'w' && GraphicEngine.gari.isStepingOnObject()) {
                GraphicEngine.gari.jump();
            }
            else {
                Console.WriteLine("KEY PRESSED: " + e.KeyChar);
            }
        }
    }
}

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
        }


        //---------
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAsAttribute(UnmanagedType.Bool)]
        static extern bool AllocConsole();
    }
}

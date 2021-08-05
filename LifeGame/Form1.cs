using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace LifeGame
{
    public partial class Form1 : Form
    {
        private int boxW = 20;
        private int boxH = 20;
        private int w, h;
        
        Label [,]  lab; 
        Life life;

        private Color None = Color.White;
        private Color Live = Color.Black;
        private Color Born = Color.Yellow;
        private Color Died = Color.Gray;
        
        public Form1()
        {
            InitializeComponent();
            InitLabels();
        }

        void InitLabels()
        {
            w = (panel.Width - 1)/ boxW;
            h = (panel.Height - 1) / boxH;

            life = new Life(w, h);
            lab = new Label[w, h];

            for (int x = 0; x < w; x++)
            for (int y = 0; y < h; y++)
                AddLabel(x, y);
        }

        private void AddLabel(int x, int y)
        {
            lab[x, y] = new Label();
            lab[x, y].BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            lab[x, y].Location = new System.Drawing.Point(x * boxW, y * boxH);
            lab[x, y].Size = new System.Drawing.Size(boxW + 1, boxH + 1);
            lab[x, y].Parent = panel;
            lab[x, y].MouseClick += new System.Windows.Forms.MouseEventHandler(this.label1_MouseClick);

        }

        private void label1_MouseClick(object sender, MouseEventArgs e)
        {
            int x = ((Label) sender).Location.X / boxW;
            int y = ((Label) sender).Location.Y / boxH;
            int color = life.Turn(x, y);
            lab[x, y].BackColor = color == 1 ? Live : None;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            life.StepOne();
            Ref();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            life.StepTwo();
            Ref();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            life.StepOne();
            life.StepTwo();
            Ref();
        }
        
        private void Ref()
        {
            for (int x = 0; x < w; x++)
            for (int y = 0; y < h; y++)
                switch (life.GetMap(x,y))
                {
                    case 0:
                        lab[x, y].BackColor = None;
                        break;
                    case 1:
                        lab[x, y].BackColor = Live;
                        break;
                    case 2:
                        lab[x, y].BackColor = Died;
                        break;
                    case -1:
                        lab[x, y].BackColor = Born;
                        break;
                }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            timer.Enabled = !timer.Enabled;
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            life.StepOne();
            life.StepTwo();
            Ref();
        }
    }
}
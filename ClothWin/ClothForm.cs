using System;
using System.Diagnostics.Contracts;
using System.Drawing.Drawing2D;
using System.Timers;
using System.Windows.Forms;

namespace ClothWin
{
    public sealed partial class ClothForm : Form
    {
        private Cloth _cloth;
        private System.Timers.Timer _physicsTimer;
        private System.Timers.Timer _paintTimer;

        public ClothForm()
        {
            InitializeComponent();
            EnableDoubleBuffering();
        }

        public void EnableDoubleBuffering()
        {
            // Set the value of the double-buffering style bits to true. 
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
        }

        private void ClothForm_Load(object sender, EventArgs e)
        {
            
            _cloth = new Cloth(this.Width);

            _physicsTimer = new System.Timers.Timer();
            _physicsTimer.Elapsed += PhysicsTimer_Tick;
            _physicsTimer.Interval = 10; //ms
            _physicsTimer.Start();


            _paintTimer = new System.Timers.Timer();
            _paintTimer.Elapsed += PaintTimer_Tick;
            _paintTimer.Interval = 5; //ms
            _paintTimer.AutoReset = false;
            _paintTimer.Start();

        }


        private void PhysicsTimer_Tick(object sender, EventArgs eventArgs)
        {
            if (this.ClientSize.IsEmpty)
                return;

            var boundsx = this.ClientSize.Width - 1;
            var boundsy = this.ClientSize.Height - 20;

            _cloth.Update(_mouse, boundsx, boundsy);
        }

        private void PaintTimer_Tick(object sender, EventArgs eventArgs)
        {
            while (true)
            {
                if (this.ClientSize.IsEmpty)
                    return;

                this.Invalidate();
            }
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            Contract.Requires(e != null);
            
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            
            ClothGraphics.DrawCloth(e.Graphics, _cloth);
            base.OnPaint(e);
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            //Don't allow the background to paint
        }


        private Mouse _mouse = new();

        private void ClothForm_MouseDown(object sender, MouseEventArgs e)
        {       
            _mouse.Button = e.Button;
            _mouse.px = _mouse.x;
            _mouse.py = _mouse.y;
            _mouse.x = e.X;
            _mouse.y = e.Y;
            _mouse.Down = true;
        }

        private void ClothForm_MouseUp(object sender, MouseEventArgs e)
        {
            _mouse.Down = false;
        }

        private void ClothForm_MouseMove(object sender, MouseEventArgs e)
        {
            _mouse.px = _mouse.x;
            _mouse.py = _mouse.y;
            _mouse.x = e.X;
            _mouse.y = e.Y;
        }
    }
}
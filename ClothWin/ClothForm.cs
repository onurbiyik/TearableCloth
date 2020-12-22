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
        private System.Timers.Timer physicsTimer;
        private System.Timers.Timer paintTimer;

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

            physicsTimer = new System.Timers.Timer();
            physicsTimer.Elapsed += PhysicsTimer_Tick;
            physicsTimer.Interval = 10; //ms
            physicsTimer.Start();


            paintTimer = new System.Timers.Timer();
            paintTimer.Elapsed += PaintTimer_Tick;
            paintTimer.Interval = 15; //ms
            paintTimer.Start();

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
            if (this.ClientSize.IsEmpty)
                return;

            this.Invalidate();

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


        private Mouse _mouse = new Mouse();

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
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ClothWin
{
    internal class Point
    {

        public float x { get; set; }
        public float y { get; set; }

        internal float prevX { get; set; }
        internal float prevY { get; set; }
        private float? pin_x { get; set; }
        private float? pin_y { get; set; }

        public List<Constraint> Constraints { get; set; }


        public Point(float x, float y)
        {
            this.x = x;
            this.y = y;
            this.prevX = x;
            this.prevY = y;
            this.Constraints = new List<Constraint>();
        }


        public void Update(Mouse mouse)
        {

            if (mouse.Down)
            {
                var diffX = this.x - mouse.x;
                var diffY = this.y - mouse.y;
                var dist = Math.Sqrt(diffX * diffX + diffY * diffY);

                if (mouse.Button == MouseButtons.Left)
                {
                    const int MouseInfluenceRadius = 20;
        
                    if (dist < MouseInfluenceRadius)
                    {
                        prevX = x - (mouse.x - mouse.px) * 1.6f;
                        prevY = y - (mouse.y - mouse.py) * 1.6f;
                    }
                }
                else if (mouse.Button == MouseButtons.Right)
                {
                    const int MouseCutRadius = 6;

                    if (dist < MouseCutRadius)
                    {
                        this.Constraints.Clear();
                    }
                }
            }

            const float gravity = 0.1124f;
            const float friction = 0.9999f;

            var newX = x + ((x - prevX) * friction) + 0;
            var newY = y + ((y - prevY) * friction) + gravity;


            prevX = x;
            prevY = y;

            x = newX;
            y = newY;

        }


        public void ResolveConstraints(float boundsx, float boundsy)
        {
            if (this.pin_x != null && this.pin_y != null)
            {
                this.x = (float)this.pin_x;
                this.y = (float)this.pin_y;
                return;
            }


            var i = this.Constraints.Count;
            while (i-- > 0) // do not change this to foreach!
            {
                this.Constraints[i].Resolve();
            }



            if (this.x > boundsx)
            {
                this.x = 2 * boundsx - this.x;
            }
            else if (1 > this.x)
            {
                this.x = 2 - this.x;
            }
            
            if (this.y < 1)
            {
                this.y = 2 - this.y;
            }
            else if (this.y > boundsy)
            {
                this.y = 2 * boundsy - this.y;
            }
            
        }

        public void Attach(Point otherPoint)
        {
            this.Constraints.Add(new Constraint(this, otherPoint));
        }

        public void RemoveConstraint(Constraint lnk)
        {
            this.Constraints.Remove(lnk);
        }

        public void Pin(float pinx, float piny)
        {
            this.pin_x = pinx;
            this.pin_y = piny;
        }

        
    }
}
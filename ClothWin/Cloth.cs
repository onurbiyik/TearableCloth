using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClothWin
{
    internal class Cloth
    {
        public List<Point> Points = new List<Point>();


        public Cloth(float canvasWidth)
        {
            DrawCurtain(canvasWidth);

        }

        private void DrawPendulum(float canvasWidth)
        {
            Point prevoiusPoint = null;

            for (int i = 0; i < 30; i++)
            {
                int spacing = 6;
                
                var x = canvasWidth / 2;
                var y = 20 + i * spacing;

                var p = new Point(x, y);

                if (prevoiusPoint == null)
                    p.Pin(x, y);
                else
                    p.Attach(prevoiusPoint);

                this.Points.Add(p);

                prevoiusPoint = p;
            }

            
        }


        private void DrawCurtain(float canvasWidth)
        {
            const int Spacing = 7; // 7;
            const int ClothWidth = 50; // 50;
            const int ClothHeight = 30; // 30;
        
            const int StartY = 20;
            var startX = canvasWidth/2 - ClothWidth*Spacing/2;

            for (var y = 0; y <= ClothHeight; y++)
            {
                for (var x = 0; x <= ClothWidth; x++)
                {
                    var posX = startX + x*Spacing;
                    var posY = StartY + y*Spacing;
                    
                    var p = new Point(posX, posY);

                    if (y == 0) p.Pin(posX, posY);
                    if (y != 0) p.Attach(this.Points[x + (y - 1)*(ClothWidth + 1)]);
                    if (x != 0) p.Attach(this.Points[this.Points.Count - 1]);

                    this.Points.Add(p);
                }
            }
        }

        public void Update(Mouse mouse, float boundsx, float boundsy)
        {
            const int physicsAccuracy = 5;

            foreach (var point in Points)
            {
                point.Update(mouse);
            }
            // Parallel.ForEach(Points, point => point.Update(mouse));

            for (var i = 0; i < physicsAccuracy; i++)
            {
                foreach (var point in Points)
                {
                    point.ResolveConstraints(boundsx, boundsy);
                }

                //Parallel.ForEach(Points, point => point.ResolveConstraints(boundsx, boundsy));
                
            }
            
        }

        
    }
}

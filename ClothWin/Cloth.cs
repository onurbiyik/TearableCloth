using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClothWin
{
    internal class Cloth
    {
        internal readonly IEnumerable<Point> Points;


        public Cloth(float canvasWidth)
        {
            this.Points = DrawCurtain(canvasWidth);

        }

        private IEnumerable<Point> DrawPendulum(float canvasWidth)
        {
            var result = new List<Point>();

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

                result.Add(p);

                prevoiusPoint = p;
            }

            return result;
        }


        private IEnumerable<Point> DrawCurtain(float canvasWidth)
        {
            var result = new List<Point>();

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
                    if (y != 0) p.Attach(result[x + (y - 1)*(ClothWidth + 1)]);
                    if (x != 0) p.Attach(result[result.Count - 1]);

                    result.Add(p);
                }
            }

            return result;
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

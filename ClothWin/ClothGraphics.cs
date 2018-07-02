using System;
using System.Drawing;

namespace ClothWin
{
    internal class ClothGraphics
    {
        public static void DrawCloth(Graphics graphics, Cloth cloth)
        {
            graphics.Clear(Color.FromArgb(51,51,51));

            foreach (var point in cloth.Points)
            {
                DrawPoint(graphics, point);
            }

        }

        private static void DrawPoint(Graphics graphics, Point point)
        {
            var rad = 2;
            // var halfRad = rad / 2;
            // graphics.FillEllipse(Brushes.LawnGreen, (float)point.x - halfRad, (float)point.y - halfRad, rad, rad);

            graphics.FillRectangle(Brushes.LawnGreen, point.x - 1, point.y - 1, rad, rad);

            var i = point.Constraints.Count;
            while (i-- > 0)
            {
                if (point.Constraints[i] == null)
                    continue;

                DrawConstraint(graphics, point.Constraints[i]);
            }
        }

        private static void DrawConstraint(Graphics graphics, Constraint constraint)
        {
            var p1 = constraint.P1;
            var p2 = constraint.P2;

            var pen = GetPen(constraint.CompressionRatio);
            
            graphics.DrawLine(pen, p1.x, p1.y, p2.x, p2.y);
        }

        private static Pen GetPen(double compressionRatio)
        {
            if (compressionRatio > 1.35)
                return Pens.Red;
            if (compressionRatio > 1.15)
                return Pens.LightPink;
            if (compressionRatio < 0.80)
                return Pens.Blue;
            if (compressionRatio < 0.95)
                return Pens.CornflowerBlue;

            return Pens.Silver;
        }

        // Given H,S,L,A in range of 0-1
        // Returns a Color (RGB struct) in range of 0-255
        private static Color ColorFromHSLA(double H, double S, double L, double A)
        {
            if (A > 1.0)
                A = 1.0;

            var r = L;
            var g = L;
            var b = L;
            var v = (L <= 0.5) ? (L * (1.0 + S)) : (L + S - L * S);
            if (v > 0)
            {
                var m = L + L - v;
                var sv = (v - m) / v;
                H *= 6.0;
                var sextant = (int)H;
                var fract = H - sextant;
                var vsf = v * sv * fract;
                var mid1 = m + vsf;
                var mid2 = v - vsf;
                switch (sextant)
                {
                    case 0:
                        r = v;
                        g = mid1;
                        b = m;
                        break;
                    case 1:
                        r = mid2;
                        g = v;
                        b = m;
                        break;
                    case 2:
                        r = m;
                        g = v;
                        b = mid1;
                        break;
                    case 3:
                        r = m;
                        g = mid2;
                        b = v;
                        break;
                    case 4:
                        r = mid1;
                        g = m;
                        b = v;
                        break;
                    case 5:
                        r = v;
                        g = m;
                        b = mid2;
                        break;
                }
            }
            var R = Convert.ToByte(r * 255.0f);
            var G = Convert.ToByte(g * 255.0f);
            var B = Convert.ToByte(b * 255.0f);
            var alpha = Convert.ToByte(A * 255.0f);
            return Color.FromArgb(alpha, R, G, B);
        }
    }
}

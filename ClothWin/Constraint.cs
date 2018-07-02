using System;

namespace ClothWin
{
    internal class Constraint
    {
        public Point P1 { get; set; }
        public Point P2 { get; set; }


        public const float TearWhenLengthIsThisTimesTheOriginal = 7.5f;
        private readonly float _initialLength;
        private readonly float _tearLength;
        private float _length;

        public double CompressionRatio
        {
            get
            {
                if (_initialLength == 0)
                    return 1.0;

                if (_length == 0)
                    return 1.0;
                
                return _length / _initialLength;
            }
        }



        public Constraint(Point pOwner, Point pOther)
        {
            P1 = pOwner;
            P2 = pOther;

            var diffX = this.P1.x - this.P2.x;
            var diffY = this.P1.y - this.P2.y;
            this._initialLength = (float)Math.Sqrt(diffX * diffX + diffY * diffY);

            this._tearLength = _initialLength * TearWhenLengthIsThisTimesTheOriginal;
        }

        

        

        public void Resolve()
        {

            var diffX = P1.x - P2.x;
            var diffY = P1.y - P2.y;
            this._length = (float)Math.Sqrt(diffX * diffX + diffY * diffY);

            if (_length > _tearLength)
            {
                this.P1.RemoveConstraint(this);
            }

            var diff = (_initialLength - _length) / _length;



            var px = diffX * diff * 0.5f;
            var py = diffY * diff * 0.5f;

            P1.x += px;
            P1.y += py;

            P2.x -= px;
            P2.y -= py;
        }

    }
}

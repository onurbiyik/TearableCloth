using System.Windows.Forms;

namespace ClothWin
{
    public struct Mouse
    {
        public bool Down { get; set; }

        public MouseButtons Button { get; set; }

        public float x { get; set; }
        public float y { get; set; }

        public float px { get; set; }
        public float py { get; set; }

    }
}
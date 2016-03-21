using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Cells
{
    class StandardCell : AbstractCell
    {
        public StandardCell(int row, int col) : base(row, col) { }
        public char Display { get; set; }
        public override Image Content
        {
            get
            {
                var image = new Bitmap(30, 30);
                using (Graphics graphics = Graphics.FromImage(image))
                {
                    var font = new Font(SystemFonts.DefaultFont.FontFamily, 15, FontStyle.Bold);
                    graphics.DrawString("" + Display, font, Brushes.Firebrick, 0, 5);
                }
                return image;
            }
        }
    }
}

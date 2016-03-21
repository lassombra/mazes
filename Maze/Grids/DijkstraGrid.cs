using Maze.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Maze.Grids
{
    class DijkstraGrid : AbstractGrid
    {
        public DijkstraGrid(int rows, int columns) : base(rows, columns) { }
        protected override ICell newCell(int row, int column)
        {
            return new CountedCell(row, column);
        }
        private bool isCounted = false;
        private void count()
        {
            this[0, 0].RunCount();
        }
        public override Image Image
        {
            get
            {
                if (!isCounted)
                {
                    this.count();
                }
                return base.Image;
            }
        }
    }
}

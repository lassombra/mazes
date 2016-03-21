using Maze.Cells;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Grids
{
    class StartFinishGrid : AbstractGrid
    {
        public StartFinishGrid(int rows, int columns) : base(rows, columns) { }
        protected override ICell newCell(int row, int col)
        {
            return new StandardCell(row, col);
        }
        StandardCell last = null;
        StandardCell first = null;
        public override Image Image
        {
            get
            {
                if (last == null)
                {
                    this[0, 0].RunCount();
                    last = (from c in Cells
                            orderby c.Count descending
                            select c as StandardCell).First();
                    last.RunCount();
                    first = (from c in Cells
                             orderby c.Count descending
                             select c as StandardCell).First();
                    last.Display = 'F';
                    first.Display = 'S';
                }
                return base.Image;
            }
        }
    }
}

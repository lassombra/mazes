using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze
{
    interface IGrid
    {
        IEnumerable<ICell> Cells { get; }
        ICell this[int row, int column]
        {
            get;
        }
        int Rows { get; }
        int Columns { get; }
        ICell RandomCell { get; }
        IEnumerable<IEnumerable<ICell>> AllRows { get; }
        Image Image { get; }
        int CellSize { get; set; }
    }
}

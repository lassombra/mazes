using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze
{
    interface ICell
    {
        int Column { get; }
        int Row { get; }
        ICell this[Direction direction]{ get; set; }
        IEnumerable<ICell> Neighbors { get; }
        Image Content { get; }
        IEnumerable<ICell> Links { get; }
        int Count { get; set; }
        void RunCount();

        bool isLinked(ICell cell);
        bool isNeighbor(ICell cell);
        void link(ICell cell, bool bidirectional = true);
        void unlink(ICell cell, bool bidirectional = true);
    }
    enum Direction
    {
        West,
        North,
        South,
        East
    }
}

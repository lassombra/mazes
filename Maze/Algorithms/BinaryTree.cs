using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Algorithms
{
    class BinaryTree : IAlgorithm
    {
        Random rand = new Random();

        public void run(IGrid grid)
        {
            IList<ICell> options = new List<ICell>();
            foreach (var cell in grid.Cells)
            {
                options.Clear();
                if (null != cell[Direction.North]) options.Add(cell[Direction.North]);
                if (null != cell[Direction.East]) options.Add(cell[Direction.East]);
                if (options.Count > 0)
                {
                    var toLink = options[rand.Next(options.Count)];
                    cell.link(toLink);
                }
            }
        }

        public void solve(IGrid grid)
        {
            throw new NotImplementedException();
        }
    }
}

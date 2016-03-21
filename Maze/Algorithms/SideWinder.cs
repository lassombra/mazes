using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Algorithms
{
    class SideWinder : IAlgorithm
    {
        Random rand = new Random();
        public void run(IGrid grid)
        {
            IList<ICell> run = new List<ICell>();
            foreach(var row in grid.AllRows)
            {
                run.Clear();
                foreach (var cell in row)
                {
                    if (null != cell[Direction.East] || null != cell[Direction.North])
                    {
                        int direction = -1;
                        if (null == cell[Direction.North]) direction = 0;
                        if (null == cell[Direction.East]) direction = 1;
                        if (direction < 0) direction = rand.Next(2);
                        if (direction == 0)
                        {
                            // going east
                            cell.link(cell[Direction.East]);
                            run.Add(cell);
                        }
                        if (direction == 1)
                        {
                            // going north, sort of
                            run.Add(cell);
                            var target = run[rand.Next(run.Count)];
                            target.link(target[Direction.North]);
                            run.Clear();
                        }
                    }
                }
            }
        }

        public void solve(IGrid grid)
        {
            throw new NotImplementedException();
        }
    }
}

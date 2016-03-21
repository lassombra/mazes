using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Algorithms
{
    class RecursiveBacktrack : IAlgorithm
    {
        Random rand = new Random();
        public void run(IGrid grid)
        {
            var stack = new Stack<ICell>();
            var visited = new HashSet<ICell>();
            stack.Push(grid[0,0]);
            while (stack.Count > 0)
            {
                var current = stack.Peek();
                visited.Add(current);
                var neighbors = (from n in current.Neighbors
                                 where !visited.Contains(n)
                                 select n).ToList();
                if (neighbors.Count > 0)
                {
                    var next = neighbors[rand.Next(neighbors.Count)];
                    current.link(next);
                    stack.Push(next);
                }
                else
                {
                    stack.Pop();
                }
            }
        }

        public void solve(IGrid grid)
        {
            throw new NotImplementedException();
        }
    }
}

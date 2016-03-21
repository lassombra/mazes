using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze
{
    interface IAlgorithm
    {
        void run(IGrid grid);
        void solve(IGrid grid);
    }
}

using Maze.Algorithms;
using Maze.Grids;
using Maze.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Maze
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            IGrid grid = new StartFinishGrid(20, 20);
            IAlgorithm algorithm = new RecursiveBacktrack();
            algorithm.run(grid);
            grid.CellSize = 55;
            var image = grid.Image;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var form = new MainUI();
            form.Maze = image;
            Application.Run(form);
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.DefaultExt = ".png";
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                image.Save(dialog.FileName);
            }
        }
    }
}

using Maze.Cells;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Grids
{
    abstract class AbstractGrid : IGrid
    {
        ICell topLeft;
        Random rand = new Random();
        public virtual IEnumerable<IEnumerable<ICell>> AllRows
        {
            get
            {
                if (null != topLeft)
                {
                    var current = topLeft;
                    while (null != current)
                    {
                        yield return this[current];
                        current = current[Direction.South];
                    }
                }
            }
        }
        IEnumerable<ICell> this[ICell row]
        {
            get
            {
                var current = row;
                while (null != current)
                {
                    yield return current;
                    current = current[Direction.East];
                }
            }
        }
        public AbstractGrid(int rows, int columns)
        {
            topLeft = prepare(rows, columns);
        }
        public virtual IEnumerable<ICell> Cells
        {
            get
            {
                foreach (var row in AllRows)
                {
                    foreach(var cell in row)
                    {
                        yield return cell;
                    }
                }
            }
        }
        public virtual ICell this[int row, int column]
        {
            get
            {
                if (null == topLeft)
                {
                    return null;
                }
                var current = topLeft;
                for (int i = 0; i < row; i++)
                {
                    current = current[Direction.South];
                    if (null == current)
                    {
                        return current;
                    }
                }
                for (int i = 0; i < column; i++)
                {
                    current = current[Direction.East];
                    if (null == current)
                    {
                        return current;
                    }
                }
                return current;
            }
        }
        public virtual int Rows
        {
            get
            {
                var count = 0;
                var current = topLeft;
                while (null != current)
                {
                    current = current[Direction.South];
                    count++;
                }
                return count;
            }
        }
        public virtual int Columns
        {
            get
            {
                var count = 0;
                var current = topLeft;
                while (null != current)
                {
                    current = current[Direction.East];
                    count++;
                }
                return count;
            }
        }
        public virtual ICell RandomCell
        {
            get
            {
                var row = rand.Next(Rows);
                var col = rand.Next(Columns);
                return this[row, col];
            }
        }

        protected virtual ICell prepare(int rows, int columns)
        {
            var root = newCell(0, 0);
            var currentRow = root;
            // init the first column
            for (int i = 1; i < rows; i++)
            {
                currentRow[Direction.South] = newCell(i, 0);
                currentRow[Direction.South][Direction.North] = currentRow;
                currentRow = currentRow[Direction.South];
            }
            currentRow = root;
            for (int i = 0; i < rows; i++)
            {
                var currentColumn = currentRow;
                var aboveColumn = currentRow[Direction.North];
                for (int ii = 1; ii < columns; ii++)
                {
                    currentColumn[Direction.East] = newCell(i, ii);
                    currentColumn[Direction.East][Direction.West] = currentColumn;
                    currentColumn = currentColumn[Direction.East];
                    if (null != aboveColumn)
                    {
                        aboveColumn = aboveColumn[Direction.East];
                        currentColumn[Direction.North] = aboveColumn;
                        aboveColumn[Direction.South] = currentColumn;
                    }
                }
                currentRow = currentRow[Direction.South];
            }
            return root;
        }
        protected virtual ICell newCell(int row, int column)
        {
            return new StandardCell(row, column);
        }
        public virtual int CellSize { get; set; }
        public virtual Image Image
        {
            get
            {
                if (CellSize == 0)
                {
                    CellSize = 10;
                }
                var width = (CellSize * Columns) + 1;
                var height = (CellSize * Rows) + 1;
                Bitmap image = new Bitmap(width, height);
                using (Graphics graphics = Graphics.FromImage(image))
                {
                    graphics.Clear(Color.AliceBlue);
                    foreach (var row in AllRows)
                    {
                        foreach(var cell in row)
                        {
                            var left = cell.Column * CellSize;
                            var right = (cell.Column + 1) * CellSize;
                            var top = cell.Row * CellSize;
                            var bottom = (cell.Row + 1) * CellSize;
                            graphics.DrawImage(cell.Content, left + 1, top + 1, CellSize - 2, CellSize - 2);
                            // North
                            if (null == cell[Direction.North])
                                graphics.DrawLine(Pens.ForestGreen, left, top, right, top);
                            // West
                            if (null == cell[Direction.West])
                                graphics.DrawLine(Pens.ForestGreen, left, top, left, bottom);
                            // East
                            if (null == cell[Direction.East])
                                graphics.DrawLine(Pens.ForestGreen, right, top, right, bottom);
                            else if (!cell.isLinked(cell[Direction.East]))
                                graphics.DrawLine(Pens.Black, right, top, right, bottom);
                            // South
                            if (null == cell[Direction.South])
                                graphics.DrawLine(Pens.ForestGreen, left, bottom, right, bottom);
                            else if (!cell.isLinked(cell[Direction.South]))
                                graphics.DrawLine(Pens.Black, left, bottom, right, bottom);
                        }
                    }
                }
                return image;
            }
        }
        //public override string ToString()
        //{
        //    StringBuilder builder = new StringBuilder();
        //    builder.Append('+');
        //    for (int i = 0; i < cells.GetLength(1); i++)
        //    {
        //        builder.Append("---+");
        //    }
        //    builder.AppendLine();
        //    for (int i = 0; i < cells.GetLength(0); i++)
        //    {
        //        var rowTop = new StringBuilder();
        //        var rowBottom = new StringBuilder();
        //        for (int ii = 0; ii < cells.GetLength(1); ii++)
        //        {
        //            var cell = this[i, ii];
        //            // if we need to do the left wall
        //            if (null == cell[Direction.West])
        //            {
        //                rowTop.Append("|");
        //                rowBottom.Append("+");
        //            }
        //            // center
        //            rowTop.Append("   ");
        //            if (null == cell[Direction.South] || !cell.isLinked(cell[Direction.South]))
        //            {
        //                rowBottom.Append("---");
        //            }
        //            else
        //            {
        //                rowBottom.Append("   ");
        //            }
        //            // right wall
        //            if (null == cell[Direction.East] || !cell.isLinked(cell[Direction.East]))
        //            {
        //                rowTop.Append("|");
        //            }
        //            else
        //            {
        //                rowTop.Append(" ");
        //            }
        //            rowBottom.Append("+");
        //        }
        //        builder.Append(rowTop.ToString());
        //        builder.AppendLine();
        //        builder.Append(rowBottom.ToString());
        //        builder.AppendLine();
        //    }
        //    return builder.ToString();
        //}
    }
}

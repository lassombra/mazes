using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Cells
{
    abstract class AbstractCell : ICell
    {
        public AbstractCell(int row, int column)
        {
            this.Row = row;
            this.Column = column;
        }
        protected ISet<ICell> links = new HashSet<ICell>();
        protected IDictionary<Direction, ICell> neighbors = new Dictionary<Direction, ICell>();
        public virtual ICell this[Direction direction]
        {
            get
            {
                if (neighbors.ContainsKey(direction))
                {
                    return neighbors[direction];
                }
                return null;
            }
            set
            {
                if (value != null)
                {
                    neighbors[direction] = value;
                }
            }
        }
        public virtual IEnumerable<ICell> Links
        {
            get
            {
                return new HashSet<ICell>(links);
            }
        }
        public virtual int Count { get; set; }
        
        public virtual int Column { get; private set; }
        public virtual int Row { get; private set; }
        public virtual IEnumerable<ICell> Neighbors {
            get
            {
                foreach(var neighbor in neighbors)
                {
                    yield return neighbor.Value;
                }
            }
        }
        static Image content;
        public virtual Image Content
        {
            get
            {
                if (content != null)
                {
                    return content;
                }
                var image = new Bitmap(30, 30);
                using (Graphics graphics = Graphics.FromImage(image))
                {
                    graphics.Clear(Color.Transparent);
                }
                content = image;
                return content;
            }
        }
        
        public virtual void RunCount()
        {
            var touched = new HashSet<ICell>();
            var counted = new HashSet<ICell>();
            var currentLeaves = new HashSet<ICell>();
            var nextLeaves = new HashSet<ICell>();
            touched.Add(this);
            counted.Add(this);
            this.Count = 0;
            foreach (var cell in Links)
            {
                cell.Count = 1;
                currentLeaves.Add(cell);
            }
            while (currentLeaves.Count > 0)
            {
                foreach (var cell in currentLeaves)
                {
                    touched.Add(cell);
                    foreach (var neighbor in cell.Links)
                    {
                        if (!counted.Contains(neighbor))
                        {
                            neighbor.Count = cell.Count + 1;
                            counted.Add(neighbor);
                        }
                        else
                        {
                            if (neighbor.Count > cell.Count + 1)
                            {
                                neighbor.Count = cell.Count + 1;
                            }
                        }
                        if (!touched.Contains(neighbor))
                        {
                            nextLeaves.Add(neighbor);
                        }
                    }
                }
                currentLeaves.Clear();
                // when there are no more leaves that haven't been touched, this should initialize empty.
                currentLeaves = new HashSet<ICell>(nextLeaves);
                nextLeaves.Clear();
            }
        }
        
        public virtual bool isLinked(ICell cell)
        {
            return links.Contains(cell);
        }
        public virtual bool isNeighbor(ICell cell)
        {
            foreach (var neighbor in neighbors)
            {
                if (neighbor.Value == cell)
                {
                    return true;
                }
            }
            return false;
        }
        public virtual void link(ICell cell, bool bidirectional = true)
        {
            if (!links.Contains(cell))
            {
                links.Add(cell);
                if (bidirectional)
                {
                    cell.link(this, false);
                }
            }
        }

        public virtual void unlink(ICell cell, bool bidirectional = true)
        {
            if (links.Contains(cell))
            {
                links.Remove(cell);
                if (bidirectional)
                {
                    cell.unlink(this, false);
                }
            }
        }
    }
}

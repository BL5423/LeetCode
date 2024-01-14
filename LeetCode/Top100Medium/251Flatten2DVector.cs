using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Top100Medium
{
    public class _251Flatten2DVector
    {
        private int[][] data;

        private int curRow, curCol;

        public _251Flatten2DVector(int[][] vec)
        {
            this.data = vec;
            this.curRow = 0;
            this.curCol = -1;
            this.MoveNext();
        }

        public int Next()
        {
            int val = this.data[this.curRow][this.curCol];
            this.MoveNext();
            return val;
        }

        public bool HasNext()
        {
            if (this.curRow >= this.data.Length)
                return false;

            return true;
        }

        private void MoveNext()
        {
            if (this.HasNext() && ++this.curCol >= this.data[this.curRow].Length)
            {
                while (++this.curRow < this.data.Length &&
                      (this.data[this.curRow] == null ||
                       this.data[this.curRow].Length == 0))
                    ;
                this.curCol = 0;
            }
        }
    }
}

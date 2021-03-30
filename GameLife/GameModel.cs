using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace GameLife
{
    class GameModel
    {
        private int cols, rows;

        public int RowCount
        {
            get => rows;
            set
            {
                rows = Math.Min(100, Math.Max(10, value));
                SetFieldSize();
            }
        }
        public int ColCount
        {
            get => cols;
            set
            {
                cols = Math.Min(100, Math.Max(10, value));
                SetFieldSize();
            }
        }
        private bool[,] field;

        public bool this[int row, int col]
        {
            get
            {
                row = row % RowCount;
                if (row < 0 ) row += RowCount;
                col= col % ColCount;
                if (col < 0) col += ColCount;
                return field[row, col];
            }
            set
            {
                row = row % RowCount;
                if (row < 0) row += RowCount;
                col = col % ColCount;
                if (col < 0) col += ColCount;
                field[row, col] = value;
            }
        }

        public GameModel(int rows = 30, int cols = 30)
        {
            RowCount = rows;
            ColCount = cols;
        }

        private void SetFieldSize()
        {
            field = new bool[rows, cols];
        }

        public void CreateNextGeneration()
        {
            bool[,] newGen = new bool[rows, cols];
            for (int i = 0; i<rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    newGen[i, j] = GetNewStatus(i, j);
                }
            }

            field = newGen;
        }

        private bool GetNewStatus(int row, int col)
        {
            var aliveNeighbours = GetAliveNeighbours(row, col);
            return
                this[row, col] && aliveNeighbours == 2
                               || aliveNeighbours == 3;
        }

        private int GetAliveNeighbours(int row, int col)
        {
            int cnt = 0;
            for (int i = row - 1; i <= row + 1; i++)
            {
                for (int j = col - 1; j <= col + 1; j++)
                {
                    if (i == row && j == col) continue;
                    cnt += (this[i, j]) ? 1 : 0;
                }
            }

            return cnt;
        }
    }
}

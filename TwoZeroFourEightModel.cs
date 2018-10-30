using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace twozerofoureight
{
    class TwoZeroFourEightModel : Model
    {
        protected int boardSize; // default is 4
        protected int[,] board;
        protected Random rand;
        bool canDown = true;
        bool canUp = true;
        bool canLeft = true;
        bool canRight = true;
        protected bool gameOver = false;

        public TwoZeroFourEightModel() : this(4)
        {
            // default board size is 4 
        }

        public int[,] GetBoard()
        {
            return board;
        }

        public bool GetGameOver()
        {
            return gameOver;
        }

        public TwoZeroFourEightModel(int size)
        {
            boardSize = size;
            board = new int[boardSize, boardSize];
            var range = Enumerable.Range(0, boardSize);
            foreach(int i in range) {
                foreach(int j in range) {
                    board[i,j] = 0;
                }
            }
            rand = new Random();
            board = Random(board);
            NotifyAll();
        }

        private int[,] Random(int[,] input)
        {
            var Numbers = new List<int> { 0, 1, 2, 3 };
            int count = 0;
            while (true)
            {
                int x = rand.Next(boardSize);
                int y = rand.Next(boardSize);
                foreach (int i in Numbers)
                {
                    foreach (int j in Numbers)
                    {
                        if (board[i, j] == 0)
                        {
                            count++;
                        }
                    }
                }
                if (count == 0)
                {
                    break;
                }
                if (board[x, y] == 0)
                {
                    board[x, y] = 2;
                    break;
                }
            }
            return input;
        }




        public void PerformDown()
        {
            int count = 0; // add by me
            int[] buffer;
            int pos;
            int[] rangeX = Enumerable.Range(0, boardSize).ToArray();
            int[] rangeY = Enumerable.Range(0, boardSize).ToArray();
            Array.Reverse(rangeY);
            foreach (int i in rangeX)
            {
                pos = 0;
                buffer = new int[4];
                foreach (int k in rangeX)
                {
                    buffer[k] = 0;
                }
                //shift left
                foreach (int j in rangeY)
                {
                    if (board[j, i] != 0)
                    {
                        buffer[pos] = board[j, i];
                        pos++;
                    }
                }
                // check duplicate
                foreach (int j in rangeX)
                {
                    if (j > 0 && buffer[j] != 0 && buffer[j] == buffer[j - 1])
                    {
                        buffer[j - 1] *= 2;
                        buffer[j] = 0;
                        count++;
                    }
                }
                if (count > 0)
                {
                    canDown = true;
                    canUp = true;
                    canLeft = true;
                    canRight = true;
                }
                else
                {
                    canDown = false;
                }
                // shift left again
                pos = 3;
                foreach (int j in rangeX)
                {
                    if (buffer[j] != 0)
                    {
                        board[pos, i] = buffer[j];
                        pos--;
                    }
                }
                // copy back
                for (int k = pos; k != -1; k--)
                {
                    board[k, i] = 0;
                }
            }
            if (canDown == false && canUp == false && canLeft == false && canRight == false)
            {
                gameOver = true;
            }
            board = Random(board);
            NotifyAll();
        }

        public void PerformUp()
        {
            int count = 0; // add by me
            int[] buffer;
            int pos;

            int[] range = Enumerable.Range(0, boardSize).ToArray();
            foreach (int i in range)
            {
                pos = 0;
                buffer = new int[4];
                foreach (int k in range)
                {
                    buffer[k] = 0;
                }
                //shift left
                foreach (int j in range)
                {
                    if (board[j, i] != 0)
                    {
                        buffer[pos] = board[j, i];
                        pos++;
                    }
                }
                // check duplicate
                foreach (int j in range)
                {
                    if (j > 0 && buffer[j] != 0 && buffer[j] == buffer[j - 1])
                    {
                        buffer[j - 1] *= 2;
                        buffer[j] = 0;
                        count++;
                    }
                }
                if (count > 0)
                {
                    canDown = true;
                    canUp = true;
                    canLeft = true;
                    canRight = true;
                }
                else
                {
                    canUp = false;
                }
                // shift left again
                pos = 0;
                foreach (int j in range)
                {
                    if (buffer[j] != 0)
                    {
                        board[pos, i] = buffer[j];
                        pos++;
                    }
                }
                // copy back
                for (int k = pos; k != boardSize; k++)
                {
                    board[k, i] = 0;
                }
            }
            if (canDown == false && canUp == false && canLeft == false && canRight == false)
            {
                gameOver = true;
            }
            board = Random(board);
            NotifyAll();
        }

        public void PerformRight()
        {
            int count = 0; // add by me
            int[] buffer;
            int pos;

            int[] rangeX = Enumerable.Range(0, boardSize).ToArray();
            int[] rangeY = Enumerable.Range(0, boardSize).ToArray();
            Array.Reverse(rangeX);
            foreach (int i in rangeY)
            {
                pos = 0;
                buffer = new int[4];
                foreach (int k in rangeY)
                {
                    buffer[k] = 0;
                }
                //shift left
                foreach (int j in rangeX)
                {
                    if (board[i, j] != 0)
                    {
                        buffer[pos] = board[i, j];
                        pos++;
                    }
                }
                // check duplicate
                foreach (int j in rangeY)
                {
                    if (j > 0 && buffer[j] != 0 && buffer[j] == buffer[j - 1])
                    {
                        buffer[j - 1] *= 2;
                        buffer[j] = 0;
                        count++;
                    }
                }
                if (count > 0)
                {
                    canDown = true;
                    canUp = true;
                    canLeft = true;
                    canRight = true;
                }
                else
                {
                    canRight = false;
                }

                // shift left again
                pos = 3;
                foreach (int j in rangeY)
                {
                    if (buffer[j] != 0)
                    {
                        board[i, pos] = buffer[j];
                        pos--;
                    }
                }
                // copy back
                for (int k = pos; k != -1; k--)
                {
                    board[i, k] = 0;
                }
            }
            if (canDown == false && canUp == false && canLeft == false && canRight == false)
            {
                gameOver = true;
            }
            board = Random(board);
            NotifyAll();
        }

        public void PerformLeft()
        {
            int count = 0; // add by me
            int[] buffer;
            int pos;
            int[] range = Enumerable.Range(0, boardSize).ToArray();
            foreach (int i in range)
            {
                pos = 0;
                buffer = new int[boardSize];
                foreach (int k in range)
                {
                    buffer[k] = 0;
                }
                //shift left
                foreach (int j in range)
                {
                    if (board[i, j] != 0)
                    {
                        buffer[pos] = board[i, j];
                        pos++;
                    }
                }
                // check duplicate
                foreach (int j in range)
                {
                    if (j > 0 && buffer[j] != 0 && buffer[j] == buffer[j - 1])
                    {
                        buffer[j - 1] *= 2;
                        buffer[j] = 0;
                        count++;
                    }
                }
                if (count > 0)
                {
                    canDown = true;
                    canUp = true;
                    canLeft = true;
                    canRight = true;
                }
                else
                {
                    canLeft = false;
                }

                // shift left again
                pos = 0;
                foreach (int j in range)
                {
                    if (buffer[j] != 0)
                    {
                        board[i, pos] = buffer[j];
                        pos++;
                    }
                }
                for (int k = pos; k != boardSize; k++)
                {
                    board[i, k] = 0;
                }
            }
            if (canDown == false && canUp == false && canLeft == false && canRight == false)
            {
                gameOver = true;
            }
            board = Random(board);
            NotifyAll();
        }

        

    }
}

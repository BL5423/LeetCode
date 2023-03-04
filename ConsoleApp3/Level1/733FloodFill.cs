using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level1
{
    public class _733FloodFill
    {
        public int[][] FloodFill(int[][] image, int sr, int sc, int color)
        {
            DFSIterative(image, sr, sc, image[sr][sc], color);
            return image;
        }

        private static int[,] dirs = new [,] { { 0, 1 }, { 0, -1 }, { 1, 0 }, { -1, 0 } };

        private void DFS(int[][] image, int sr, int sc, int startColor, int color)
        {
            if (startColor != color)
            {
                image[sr][sc] = color;
                for(int i = 0; i < dirs.GetLength(0); ++i)
                {
                    int nextR = sr + dirs[i, 0];
                    int nextC = sc + dirs[i, 1];

                    if (nextR < 0 || nextR >= image.Length || nextC < 0 || nextC >= image[0].Length || 
                        image[nextR][nextC] != startColor || 
                        image[nextR][nextC] == color)
                        continue;

                    DFS(image, nextR, nextC, startColor, color);
                }
            }
        }

        private void DFSIterative(int[][] image, int sr, int sc, int startColor, int color)
        {
            if (startColor != color)
            {
                Stack<int> stack = new Stack<int>();
                stack.Push(sc);
                stack.Push(sr);
                while (stack.Count > 0)
                {
                    int r = stack.Pop();
                    int c = stack.Pop();
                    image[r][c] = color;
                    for (int i = 0; i < dirs.GetLength(0); ++i)
                    {
                        int nextR = r + dirs[i, 0];
                        int nextC = c + dirs[i, 1];
                        if (nextR < 0 || nextR >= image.Length || nextC < 0 || nextC >= image[0].Length ||
                            image[nextR][nextC] == color ||
                            image[nextR][nextC] != startColor)
                            continue;

                        stack.Push(nextC);
                        stack.Push(nextR);
                    }
                }
            }
        }

        private void BFS(int[][] image, int sr, int sc, int startColor, int color)
        {
            if (startColor != color)
            {
                Queue<int> queue = new Queue<int>();
                image[sr][sc] = color;
                queue.Enqueue(sr);
                queue.Enqueue(sc);
                while(queue.Count > 0)
                {
                    int r = queue.Dequeue();
                    int c = queue.Dequeue();
                    for(int i = 0; i < dirs.GetLength(0); ++i)
                    {
                        int nextR = r + dirs[i, 0];
                        int nextC = c + dirs[i, 1];

                        if (nextR < 0 || nextR >= image.Length || nextC < 0 || nextC >= image[0].Length ||
                            image[nextR][nextC] == color ||
                            image[nextR][nextC] != startColor)
                            continue;

                        image[nextR][nextC] = color;
                        queue.Enqueue(nextR);
                        queue.Enqueue(nextC);
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level2
{
    public class Stone
    {
        public Stone[] neighbors;

        public int row, col, lastNeighbor, index;

        public Stone(int row, int col)
        {
            this.row = row;
            this.col = col;
            this.lastNeighbor = 0;
            this.neighbors = new Stone[4];
        }
    }

    //https://leetcode.com/problems/most-stones-removed-with-same-row-or-column/discuss/197668/Count-the-Number-of-Islands-O(N)

    //https://en.wikipedia.org/wiki/Disjoint-set_data_structure
    //The combination of path compression, splitting, or halving, with union by size or by rank, reduces the running time for m operations of any type
    //to O(m*a(n)), This makes the amortized running time of each operation O(a(n)).
    //Here, the function a(n) is the inverse Ackermann function.
    //The inverse Ackermann function grows extraordinarily slowly, so this factor is 4 or less for any n that can actually be written in the physical universe.This makes disjoint-set operations practically amortized constant time.
    public class _947MostStonesRemovedwithSameRoworColumnV3
    {
        private int islands;

        private Dictionary<int, int> parents;

        private int GetParent(int index)
        {
            if (!this.parents.TryGetValue(index, out int parent))
            {
                ++this.islands;
                this.parents[index] = parent = index;
            }

            if (parent == index)
            {
                return parent;
            }

            // path compression
            return parents[index] = this.GetParent(parent);
        }

        private void Union(int index1, int index2)
        {
            int parent1 = this.GetParent(index1);
            int parent2 = this.GetParent(index2);

            if (parent1 != parent2)
            {
                this.parents[parent1] = parent2;
                --this.islands;
            }
        }

        public int RemoveStones(int[][] stones)
        {
            this.islands = 0;
            this.parents = new Dictionary<int, int>(stones.Length);
            foreach(var stone in stones)
            {
                // use '~' to differentiate row(0 - 10000) and col(0 - 10000), we can also add an offset(say 10001) to either of them
                this.Union(stone[0], ~stone[1]);
            }

            return stones.Length - this.islands;
        }
    }

    public class _947MostStonesRemovedwithSameRoworColumnV2
    {
        private const int Boundary = 10001;

        private int GetParent(IDictionary<int, int> parents, int coordinate)
        {
            var parent = parents[coordinate];
            if (parent == coordinate)
            {
                return parent;
            }    

            // set coordinate'parent to its parent's parent for compression
            return parents[coordinate] = GetParent(parents, parent);
        }

        private int Union(IDictionary<int, int> parents, IDictionary<int, int> familySize, int coordinate1, int coordinate2)
        {
            var parent1 = this.GetParent(parents, coordinate1);
            var parent2 = this.GetParent(parents, coordinate2);

            if (parent1 == parent2)
            {
                return 0;
            }

            var familySize1 = familySize[parent1];
            var familySize2 = familySize[parent2];
            if (familySize1 > familySize2)
            {
                // merge family2 into family1
                familySize[parent1] += familySize2;
                parents[parent2] = parent1;
            }
            else
            {
                // merge family1 into family2
                familySize[parent2] += familySize1;
                parents[parent1] = parent2;
            }

            // indicates they are merged
            return 1;
        }

        public int RemoveStones(int[][] stones)
        {
            Dictionary<int, int> parents = new Dictionary<int, int>(stones.Length);
            Dictionary<int, int> familySize = new Dictionary<int, int>(stones.Length);

            int totalDifferentCoordinates = 0;
            foreach (var stone in stones)
            {
                int coordinate1 = stone[0];
                parents[coordinate1] = coordinate1;
                if (!familySize.ContainsKey(coordinate1))
                {
                    ++totalDifferentCoordinates;
                    familySize[coordinate1] = 1;
                }

                int coordinate2 = stone[1] + Boundary;
                parents[coordinate2] = coordinate2;
                if (!familySize.ContainsKey(coordinate2))
                {
                    ++totalDifferentCoordinates;
                    familySize[coordinate2] = 1;
                }
            }

            int differentCoordinatesLeft = totalDifferentCoordinates;
            foreach(var stone in stones)
            {
                // use the coordinate(row and col) of each stone to merge
                // it means if a stone on row X and col Y, then X and Y should be unioned
                // note for each stone, X != Y because of the Boundary we added
                differentCoordinatesLeft -= this.Union(parents, familySize, stone[0], stone[1] + Boundary);
            }

            // if there are N coordinates left, then it means that there are N stones left
            // this is because each stone only counts as 1 unique coordinate 'Z', the other one has been merged with 'Z' or other stones' coordiantes
            return stones.Length - differentCoordinatesLeft;
        }
    }

    public class _947MostStonesRemovedwithSameRoworColumn
    {
        private int[] parents;

        private int[] familySize;

        private IList<Stone> stonesGraph;

        private int GetParent(Stone stone)
        {
            int parent = this.parents[stone.index];
            if (parent == stone.index)
            {
                return stone.index;
            }

            // set parent to parent's parent for compression
            return this.parents[stone.index] = GetParent(stonesGraph[parent]);
        }

        private int Union(Stone stone1, Stone stone2)
        {
            int parent1 = GetParent(stone1);
            int parent2 = GetParent(stone2);
            if (parent1 == parent2)
                return 0;

            if (this.familySize[parent1] > this.familySize[parent2])
            {
                this.parents[parent2] = parent1;
                this.familySize[parent1] += this.familySize[parent2];
            }
            else
            {
                this.parents[parent1] = parent2;
                this.familySize[parent2] += this.familySize[parent1];
            }

            return 1;
        }

        public int RemoveStones(int[][] stones)
        {
            this.parents = new int[stones.Length];
            for (int index = 0; index < this.parents.Length; ++index)
            {
                this.parents[index] = index;
            }

            this.familySize = new int[stones.Length];
            for(int index = 0; index < this.familySize.Length; ++index)
            {
                this.familySize[index] = 1;
            }

            this.stonesGraph = CreateStones(stones);

            int stonesLeft = stones.Length;
            for(int index1 = 0; index1 < this.stonesGraph.Count; ++index1)
            {
                for(int index2 = index1 + 1; index2 < this.stonesGraph.Count; ++index2)
                {
                    var stone1 = this.stonesGraph[index1];
                    var stone2 = this.stonesGraph[index2];
                    if (this.ShouldUnion(stone1, stone2))
                    {
                        stonesLeft -= this.Union(stone1, stone2);
                    }
                }
            }

            return stones.Length - stonesLeft;
        }

        private bool ShouldUnion(Stone stone1, Stone stone2)
        {
            return (stone1.col == stone2.col || stone1.row == stone2.row);                
        }

        public int RemoveStones_Graph(int[][] stones)
        {
            IList<Stone> stonesToRemove = this.CreateStones(stones);
            HashSet<Stone> visited = new HashSet<Stone>(stones.Length);
            int removed = 0;
            Stack<Stone> stack = new Stack<Stone>(stones.Length);
            Queue<Stone> queue = new Queue<Stone>(stones.Length);
            foreach (var stoneToRemove in stonesToRemove)
            {
                if (visited.Add(stoneToRemove))
                {
                    removed += BFS(stoneToRemove, queue, visited);
                }
            }

            return removed;
        }

        private int BFS(Stone stoneToStart, Queue<Stone> queue, HashSet<Stone> visited)
        {
            int removed = 0;
            queue.Enqueue(stoneToStart);
            while (queue.Count > 0)
            {
                var top = queue.Dequeue();
                foreach (var neighbor in top.neighbors)
                {
                    if (neighbor == null || !visited.Add(neighbor))
                        continue;

                    ++removed;
                    queue.Enqueue(neighbor);
                }
            }

            return removed;
        }

        private int DFS(Stone stoneToStart, Stack<Stone> stack, HashSet<Stone> visited)
        {
            int removed = 0;
            stack.Push(stoneToStart);
            while (stack.Count > 0)
            {
                var top = stack.Pop();
                foreach (var neighbor in top.neighbors)
                {
                    if (neighbor == null || !visited.Add(neighbor))
                        continue;

                    ++removed;
                    stack.Push(neighbor);
                }
            }

            return removed;
        }

        private IList<Stone> CreateStones(int[][] stones)
        {
            var res = new List<Stone>(stones.Length);
            Dictionary<int, LinkedList<Stone>> rows = new Dictionary<int, LinkedList<Stone>>();
            Dictionary<int, LinkedList<Stone>> cols = new Dictionary<int, LinkedList<Stone>>();
            int index = 0;
            foreach(var stone in stones)
            {
                int row = stone[0];
                int col = stone[1];
                var stoneObj = new Stone(row, col) { index = index++ };
                if (!rows.TryGetValue(row, out LinkedList<Stone> stonesOntheRow))
                {
                    stonesOntheRow = new LinkedList<Stone>();
                    rows[row] = stonesOntheRow;
                }

                if (stonesOntheRow.Count > 0)
                {
                    var last = stonesOntheRow.Last();
                    last.neighbors[last.lastNeighbor++] = stoneObj;
                    stoneObj.neighbors[stoneObj.lastNeighbor++] = last;
                }
                stonesOntheRow.AddLast(stoneObj);

                if (!cols.TryGetValue(col, out LinkedList<Stone> stonesOntheCol))
                {
                    stonesOntheCol = new LinkedList<Stone>();
                    cols[col] = stonesOntheCol;
                }
                if (stonesOntheCol.Count > 0)
                {
                    var last = stonesOntheCol.Last();
                    last.neighbors[last.lastNeighbor++] = stoneObj;
                    stoneObj.neighbors[stoneObj.lastNeighbor++] = last;
                }
                stonesOntheCol.AddLast(stoneObj);

                res.Add(stoneObj);
            }

            return res;
        }
    }
}

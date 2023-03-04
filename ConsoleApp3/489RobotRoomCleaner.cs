using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC
{
    public class _489RobotRoomCleaner
    {
        public void CleanRoom(Robot robot)
        {
            // initial relative pos
            int row = 0, col = 0;
            Direction direction = Direction.Up;
            Stack<Position> stack = new Stack<Position>();
            HashSet<(int, int)> inStack = new HashSet<(int, int)>();
            stack.Push(new Position(row, col, direction));
            inStack.Add((row, col));
            while (stack.Count > 0)
            {
                var pos = stack.Peek();
                if (pos.directionsUsed == 4)
                {
                    // explored all 4 directions
                    stack.Pop();
                    //inStack.Remove((pos.row, pos.col));

                    // move back by turning
                    if (stack.Count != 0)
                    {
                        var prevPos = stack.Peek();
                        if (prevPos.row + 1 == pos.row && prevPos.col == pos.col)
                        {
                            // the robot came from above and right now it faces down
                            robot.TurnLeft();
                            robot.TurnLeft();
                            robot.Move();
                            robot.TurnLeft();
                            robot.TurnLeft();
                        }
                        else if (prevPos.row == pos.row && prevPos.col + 1 == pos.col)
                        {
                            // the robot came from left and right now it faces left
                            robot.TurnLeft();
                            robot.TurnLeft();
                            robot.Move();
                            robot.TurnLeft();
                            robot.TurnLeft();
                        }
                        else if (prevPos.row - 1 == pos.row && prevPos.col == pos.col)
                        {
                            // the robot came from below and right now it faces up
                            robot.TurnLeft();
                            robot.TurnLeft();
                            robot.Move();
                            robot.TurnLeft();
                            robot.TurnLeft();
                        }
                        else
                        {
                            // the robot came from right and right now it faces left
                            robot.TurnLeft();
                            robot.TurnLeft();
                            robot.Move();
                            robot.TurnLeft();
                            robot.TurnLeft();
                        }
                    }
                }
                else
                {
                    robot.Clean();
                    var nextPos = new Position(pos.row + moves[(int)pos.direction, 0], pos.col + moves[(int)pos.direction, 1], pos.direction);
                    if (!inStack.Contains((nextPos.row, nextPos.col)) && robot.Move())
                    {
                        // can move forward
                        stack.Push(nextPos);
                        inStack.Add((nextPos.row, nextPos.col));
                    }
                    else
                    {
                        // if not, turn right and try again
                        robot.TurnRight();
                        ++pos.directionsUsed;
                        pos.direction = (Direction)(((int)pos.direction + 1) % 4);
                    }
                }
            }
        }

        private static int[,] moves = new int[,] { { -1, 0 }, { 0, 1 }, { 1, 0 }, { 0, -1 } };
    }

    public enum Direction
    {
        Up = 0,

        Right,

        Down,

        Left
    }

    public class Position
    {
        public int row, col, directionsUsed;

        public Direction direction;

        public Position(int row, int col, Direction direction)
        {
            this.row = row;
            this.col = col;
            this.directionsUsed = 0;
            this.direction = direction;
        }
    }

    // This is the robot's control interface.
    // You should not implement it, or speculate about its implementation
    public interface Robot
    {
        // Returns true if the cell in front is open and robot moves into the cell.
        // Returns false if the cell in front is blocked and robot stays in the current cell.
        public bool Move();

        // Robot will stay in the same cell after calling turnLeft/turnRight.
        // Each turn will be 90 degrees.
        public void TurnLeft();
        public void TurnRight();

        // Clean the current cell.
        public void Clean();
    }
}

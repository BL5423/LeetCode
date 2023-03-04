using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level2
{
    public class _735AsteroidCollision
    {
        public int[] AsteroidCollision(int[] asteroids)
        {
            LinkedList<int> res = new LinkedList<int>();
            if (asteroids != null && asteroids.Length > 0)
            {
                for (int index = 0; index < asteroids.Length; ++index)
                {
                    int next = asteroids[index];
                    if (res.Count > 0)
                    {
                        int cur = res.Last();
                        if (!(cur > 0 && next < 0))
                        {
                            // no collison
                            res.AddLast(next);
                        }
                        else
                        {
                            while (res.Count > 0 && (res.Last() > 0 && next < 0))
                            {
                                cur = res.Last();
                                int absCur = Math.Abs(cur);
                                int absNext = Math.Abs(next);
                                if (absCur <= absNext)
                                {
                                    res.RemoveLast();
                                    if (absCur == absNext)
                                    {
                                        next = 0;
                                        break;
                                    }
                                }
                                else
                                {
                                    next = 0;
                                    break;
                                }
                            }

                            if (next != 0)
                            {
                                res.AddLast(next);
                            }
                        }
                    }
                    else
                    {
                        res.AddLast(next);
                    }
                }
            }

            return res.ToArray();
        }
    }
}

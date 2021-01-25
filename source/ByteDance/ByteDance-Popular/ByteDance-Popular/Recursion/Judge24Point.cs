//679. 24 点游戏
//你有 4 张写有 1 到 9 数字的牌。你需要判断是否能通过 *，/，+，-，(，) 的运算得到 24。

//示例 1:
//输入:[4, 1, 8, 7]
//输出: True
//解释: (8 - 4) * (7 - 1) = 24

//示例 2:
//输入:[1, 2, 1, 2]
//输出: False
//注意:

//除法运算符 / 表示实数除法，而不是整数除法。例如 4 / (1 - 2 / 3) = 12 。
//每个运算符对两个数进行运算。特别是我们不能用 - 作为一元运算符。例如，[1, 1, 1, 1] 作为输入时，表达式 - 1 - 1 - 1 - 1 是不允许的。
//你不能将数字连接在一起。例如，输入为[1, 2, 1, 2] 时，不能写成 12 + 12 。

//来源：力扣（LeetCode）
//链接：https://leetcode-cn.com/problems/24-game
//著作权归领扣网络所有。商业转载请联系官方授权，非商业转载请注明出处。

//思路
//暴力, 维护一个数字池，

using System;
using System.Collections.Generic;

namespace ByteDancePopular
{
    public partial class Solution
    {
        private static int Point24Add = 0, Point24Minus = 2, Point24Multiply = 1, Point24Divided = 3;
        private static double MiniGap = 0.0000001f;
        public bool JudgePoint24(int[] nums)
        {
            List<double> list = new List<double>();
            for (int i = 0; i < nums.Length; ++i)
            {
                list.Add(nums[i]);
            }

            double a = 9,  b = 1;
            //Console.WriteLine(a - b);
            return Solve(list);
        }

        public bool Solve(List<Double> list)
        {
            if (list.Count == 0)
            {
                return false;
            }
            if (list.Count == 1)
            {
                return Math.Abs(list[0] - 24) < MiniGap;
            }
            int size = list.Count;
            if (size == 3)
            {
                //Console.WriteLine(string.Format("{0},{1},{2}", list[0], list[1], list[2]));
            }
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i != j)
                    {
                        List<Double> list2 = new List<Double>();
                        for (int k = 0; k < size; k++)
                        {
                            if (k != i && k != j)
                            {
                                list2.Add(list[k]);
                            }
                        }
                        for (int k = 0; k < 4; k++)
                        {
                            if (  k   < 2 && i > j)
                            {
                                continue;
                            }
                            if (k == Point24Add)
                            {
                                list2.Add(list[i] + list[j]);
                            }
                            else if (k == Point24Multiply)
                            {
                                list2.Add(list[i] * list[j]);
                            }
                            else if (k == Point24Minus)
                            {
                                list2.Add(list[i] - list[j]);
                            }
                            else if (k == Point24Divided)
                            {
                                if (Math.Abs(list[j]) < MiniGap)
                                {
                                    continue;
                                }
                                else
                                {
                                    list2.Add(list[i] / list[j]);
                                }
                            }
                            if (Solve(list2))
                            {
                                return true;
                            }
                            list2.RemoveAt(list2.Count - 1);
                        }
                    }
                }
            }
            return false;
        }



        private bool JudgePoint24Permuation(int d1, int d2, int d3, int d4)
        {
            List<double> listd1 = CalculateFormular(d1, d2);
            List<double> listd2 = CalculateFormular(d3, d4);

            for (int i = 0; i < listd1.Count; ++i)
            {
                for (int j = 0; j < listd2.Count; ++j)
                {
                    if (JudgePoint24ByTwoDigit(listd1[i], listd2[j]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private List<double> CalculateFormular(double a, double b)
        {
            List<double> ret = new List<double>();
            ret.Add(a + b);
            ret.Add(a - b);
            ret.Add(b - a);
            if (Math.Abs(a) > MiniGap && Math.Abs(b) > MiniGap)
            {
                ret.Add(a * b);
                ret.Add(a / b);
                ret.Add(b / a);
            }
            return ret;
        }

        private bool JudgePoint24ByTwoDigit(double a, double b)
        {
            if (Math.Abs(a + b  - 24) < MiniGap)
            {
                return true;
            }
            else if (Math.Abs(a * b -24) < MiniGap)
            {
                return true;
            }
            else if (Math.Abs(a - b - 24) < MiniGap || Math.Abs(b-a-24)< MiniGap)
            {
                return true;
            }
            else if (Math.Abs (a) > MiniGap && Math.Abs(b) > MiniGap && ((Math.Abs(a / b  -24) < MiniGap) || Math.Abs(b/a - 24) < MiniGap))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}
using System;
using System.Collections.Generic;
using System.Text;

namespace ByteDancePopular
{
    public partial class Solution
    {
        bool IsUpper(char c) { return c >= 'A' && c <= 'Z'; }
        bool IsLower(char c) { return c >= 'a' && c <= 'z'; }
        bool IsDigit(char c) { return c >= '0' && c <= '9'; }

        public string CountOfAtoms(string formula)
        {
            Dictionary<string,int> ret = Parse(formula, 0, formula.Length - 1);
            List<string> sortKey = new List<string>();
            foreach(string key in ret.Keys)
            {
                sortKey.Add(key);
            }
            sortKey.Sort();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < sortKey.Count; ++i)
            {
                string key = sortKey[i];
                sb.Append(key);
                if (ret[key] > 1)
                {
                    sb.Append(ret[key]);
                }             
            }
            return sb.ToString();
        }

        public int index;

        public Dictionary<string, int> Parse(string formula, int l, int r)
        {
            index = r;
            Dictionary<string, int> ret = new Dictionary<string, int>();
            int val = 1;
            string atom = "";
            while (index >= l)
            {
                //第一次数字
                if (IsDigit(formula[index]))
                {

                    Stack<int> dig = new Stack<int>();
                    dig.Push(formula[index] - '0');
                    index--;
                    //向前查找连续数字
                    while (index>=l && IsDigit(formula[index]))
                    {       
                        dig.Push(formula[index] - '0');
                        index--;
                    }
                    //
                    val = 0;
                    while (dig.Count != 0)
                    {
                        val = dig.Pop() + val *10;
                    }
                }
                else if (IsLower(formula[index]))
                {
                    index--;
                    StringBuilder sb = new StringBuilder();
                    sb.Append(formula[index]);
                    sb.Append(formula[index + 1]);
                    atom = sb.ToString();
                    if (!ret.ContainsKey(atom))
                    {
                        ret[atom] = 0;
                    }
                    ret[atom] += val;
                    index--;
                    //重置回默认系数
                    val = 1;
                }
                else if (IsUpper(formula[index]))
                {
                    atom = formula[index].ToString();
                    if (!ret.ContainsKey(atom))
                    {
                        ret[atom] = 0;
                    }
                    ret[atom] += val;
                    index--;
                    //重置回默认系数
                    val = 1;
                }

                //如果是括号，递归
                else if (formula[index] == ')')
                {
                    index--;
                    Merge(ret,Parse(formula, l, index), val);
                    val = 1;
                }
                //结束的括号，退出一层递归
                else if (formula[index] == '(')
                {
                    index--;
                    val = 1;
                    return ret;
                }
            }
            return ret;
        }

        public void Merge(Dictionary<string, int> main, Dictionary<string ,int> sub, int multiply)
        {
            foreach(string key in sub.Keys)
            {
                if (!main.ContainsKey(key))
                {
                    main[key] = 0;
                }
                main[key] += multiply * sub[key];
            }

        }
    }
}

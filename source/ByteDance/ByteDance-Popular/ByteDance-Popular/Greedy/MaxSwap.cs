using System;
using System.Collections.Generic;

//670.最大交换

//给定一个非负整数，你至多可以交换一次数字中的任意两位。返回你能得到的最大值。

//示例 1 :

//输入: 2736
//输出: 7236
//解释: 交换数字2和数字7。
//示例 2 :

//输入: 9973
//输出: 9973
//解释: 不需要交换。
//注意:

//给定数字的范围是[0, 108]
namespace ByteDancePopular
{
    public partial class Solution
    {
        public int MaximumSwap(int num)
        {
            int[] last = new int[9];
            string strNum = num.ToString();
            for (int i = 0; i < strNum.Length; ++i)
            {
                int index = strNum[i] - '1';
                if (index >= 0)
                {
                    last[index] = i;
                }
                
            }

            for (int i = 0; i < strNum.Length; ++i)
            {
                for (int j = last.Length - 1; j >= 0; --j)
                {
                    if (strNum[i] < (j+ '1') && i < last[j])
                    {
                        //交换
                        char[] charNum = strNum.ToCharArray();
                        charNum[last[j]] = charNum[i];
                        charNum[i] = (char)( j + '1');
                        string strRet = new string(charNum);
                        return int.Parse(strRet);
                    }
                }
            }
            return num;
        }
    }
}
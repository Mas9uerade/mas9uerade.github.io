using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
{
    public class MaxNumofSubstring
    {

        public class Line
        {
            public int Start;
            public int End;
            public Line(int _start, int _end)
            {
                Start = _start;
                End = _end;
            }
        }
        public IList<string> MaxNumOfSubstrings(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return new List<string>();
            }

            List<string> ret = new List<string>();
            List<Line> tmp = new List<Line>();
            //获取线段
            Line[] lines = new Line[26];
            for (int i = 0; i < lines.Length; ++i)
            {
                lines[i] = new Line(-1, -1);
            }
            for (int i = 0; i < s.Length; ++i)
            {
                int charIndex = s[i] - 'a';
                if (lines[charIndex].Start == -1)
                {
                    lines[charIndex].Start = i;
                }
                lines[charIndex].End = i;
            }

            //合并
            for (int i = 0; i < lines.Length; ++i)
            {
                int charIndex =i;
                int start = lines[charIndex].Start;
                if (start == -1)
                {
                    continue;
                }
                int end = lines[charIndex].End;

                for (int j = start+1; j <end; ++j)
                {
                    int charIndex2 = s[j] - 'a';
                    if (charIndex2 == charIndex)
                    {
                        continue;
                    }
                    int subStart = lines[charIndex2].Start;
                    if (subStart == -1)
                    {
                        continue;
                    }
                    int subEnd = lines[s[j]-'a'].End;
                    if (subEnd > end || subStart < start)
                    {
                        lines[charIndex2].End = Math.Max(subEnd, end);
                        lines[charIndex].End =  Math.Max(subEnd, end);
                        lines[charIndex2].Start = Math.Min(start, subStart);
                        lines[charIndex].Start = Math.Min(start, subStart);
                        end = Math.Max(subEnd, end);
                        start = Math.Min(start, subStart);
                        j = start + 1;
                    }
                }
            }

            //贪心
            Line first = lines[s[0] - 'a'];
            int frontIndex = first.Start, backIndex = first.End;
            tmp.Add(lines[s[0] - 'a']);
            for (int i = 1; i < s.Length;++i)
            {
                int charIndex = s[i] - 'a';
                int start = lines[charIndex].Start;
                int end = lines[charIndex].End;
                if (start == -1)
                {
                    continue;
                }

                for (int j = 0; j <tmp.Count; ++j)
                {
                    if (start > tmp[j].Start && end < tmp[j].End)
                    {
 
                        if (backIndex == tmp[j].End)
                        {
                            backIndex = end;
                        }
                        tmp[j].Start = start;
                        tmp[j].End = end;
                        break;
                    }
                    if (start > backIndex )
                    {
                        tmp.Add(new Line(start, end));
                        backIndex = end;
                        break;
                    }
                }

                //frontIndex = Math.Min(start, frontIndex);
                //backIndex = Math.Max(end, backIndex);
            }
            for (int i = 0; i <tmp.Count; ++i)
            {
                ret.Add(s.Substring(tmp[i].Start, tmp[i].End - tmp[i].Start + 1));
            }
            return ret;
        }
    }
}

//864.获取所有钥匙的最短路径
//给定一个二维网格 grid。 "." 代表一个空房间， "#" 代表一堵墙， "@" 是起点，（"a", "b", ...）代表钥匙，（"A", "B", ...）代表锁。
//我们从起点开始出发，一次移动是指向四个基本方向之一行走一个单位空间。我们不能在网格外面行走，也无法穿过一堵墙。如果途经一个钥匙，我们就把它捡起来。除非我们手里有对应的钥匙，否则无法通过锁。
//假设 K 为钥匙/锁的个数，且满足 1 <= K <= 6，字母表中的前 K 个字母在网格中都有自己对应的一个小写和一个大写字母。换言之，每个锁有唯一对应的钥匙，每个钥匙也有唯一对应的锁。另外，代表钥匙和锁的字母互为大小写并按字母顺序排列。
//返回获取所有钥匙所需要的移动的最少次数。如果无法获取所有钥匙，返回 -1 。

//示例 1：
//输入：["@.a.#","###.#","b.A.B"]
//输出：8

//示例 2：
//输入：["@..aA","..B#.","....b"]
//输出：6

//提示：
//1 <= grid.length <= 30
//1 <= grid[0].length <= 30
//grid[i][j] 只含有 '.', '#', '@', 'a' - 'f' 以及 'A' - 'F'
//钥匙的数目范围是[1, 6]，每个钥匙都对应一个不同的字母，正好打开一个对应的锁。

//来源：力扣（LeetCode）
//链接：https://leetcode-cn.com/problems/shortest-path-to-get-all-keys
//著作权归领扣网络所有。商业转载请联系官方授权，非商业转载请注明出处。


// 思路
// BFS 不同钥匙获取的状态下，visited 状态不同
using System;
using System.Collections.Generic;

namespace ByteDancePopular
{
    public partial class Solution
    {
        private class PathNode
        {
            public int x;
            public int y;
            public int key;
            public int len;

            public PathNode(int _key, int _x, int _y, int _len)
            {
                key = _key;
                x = _x;
                y = _y;
                len = _len;
            }
        }

        public int ShortestPathAllKeys(string[] grid)
        {
            int row = grid.Length;
            int col = grid[0].Length;

            //起点位置
            int startX = 0, startY =0;

            int[] dirRow = new int[] { -1, 0, 1, 0 };
            int[] dirCol = new int[] { 0, 1, 0, -1 };
            //获取钥匙集合
            HashSet<char> keys = new HashSet<char>(); 
            for (int i = 0; i < grid.Length; ++i)
            {
                for (int j = 0; j < grid[i].Length; ++j)
                {
                    if (grid[i][j] >= 'a' && grid[i][j] <= 'f')
                    {
                        keys.Add(grid[i][j]);
                    }
                    if (grid[i][j] == '@')
                    {
                        startX = j;
                        startY = i;
                    }
                }
            }

            int keycount = keys.Count;
            if (keycount == 0) return 0;
            int allKeyFlag = 0;
            for (int i = 0; i <keycount;++i)
            {
                allKeyFlag += (int)Math.Pow(2, i);
            }
            //初始化状态图
            bool[][][] vistied = new bool[(int)Math.Pow(2, keycount)][][];
            for (int i = 0; i < vistied.Length; ++i)
            {
                vistied[i] = new bool[row][];
                for (int j = 0; j < vistied[i].Length; ++j)
                {
                    vistied[i][j] = new bool[col];
                }
            }
            //
            Queue<PathNode> choice = new Queue<PathNode>();
            choice.Enqueue(new PathNode(0, startX, startY, 0));
            vistied[0][startY][startX] = false;
            PathNode node = null;
            Console.WriteLine("===============================");
            while (choice.Count > 0)
            {
                node = choice.Dequeue();
                int key = node.key;
                if (vistied[key][node.y][node.x])
                {
                    continue;
                }
                vistied[key][node.y][node.x] = true;

                Console.WriteLine(string.Format("Key:{0}, Try {1},{2} , len:{3} ", key, node.x, node.y, node.len));
                if (key == allKeyFlag)
                {
                    return node.len;
                }

                for (int i = 0; i < dirRow.Length; ++i)
                {                 
                    int x = node.x + dirRow[i];
                    int y = node.y + dirCol[i];

                    x = Math.Max(0, x);
                    x = Math.Min(x, col - 1);

                    y = Math.Max(0, y);
                    y = Math.Min(y, row - 1);

                    char c = grid[y][x];

                    if (vistied[key][y][x])
                    {
                        continue;
                    }
                    else if (c == '#')
                    {
                        continue;
                    }
                    else if (c >='A' && c <= 'F')
                    {
                        if ((key >> c-'A' & 1) ==1)
                        {
                            //vistied[key][x][y] = true;
                            choice.Enqueue(new PathNode(key, x, y, node.len + 1));
                        }
                    }
                    else if (c >= 'a' && c <= 'f')
                    {
                        //vistied[key][x][y] = true;
                        int tmpkey = (1 << (c - 'a')) | key;
                        choice.Enqueue(new PathNode(tmpkey, x, y, node.len + 1));
                    }
                    else if (c == '.' || c == '@')
                    {
                        //vistied[key][x][y] = true;
                        choice.Enqueue(new PathNode(key, x, y, node.len + 1));
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            return 0;
        }
    }
}
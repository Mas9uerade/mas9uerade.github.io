//460.LFU 缓存
//请你为 最不经常使用（LFU）缓存算法设计并实现数据结构。

//实现 LFUCache 类：

//LFUCache(int capacity) -用数据结构的容量 capacity 初始化对象
//int get(int key) -如果键存在于缓存中，则获取键的值，否则返回 - 1。
//void put(int key, int value) -如果键已存在，则变更其值；如果键不存在，请插入键值对。当缓存达到其容量时，则应该在插入新项之前，使最不经常使用的项无效。在此问题中，当存在平局（即两个或更多个键具有相同使用频率）时，应该去除 最久未使用 的键。
//注意「项的使用次数」就是自插入该项以来对其调用 get 和 put 函数的次数之和。使用次数会在对应项被移除后置为 0 。

//进阶：

//你是否可以在 O(1) 时间复杂度内执行两项操作？

//来源：力扣（LeetCode）
//链接：https://leetcode-cn.com/problems/lfu-cache
//著作权归领扣网络所有。商业转载请联系官方授权，非商业转载请注明出处。


//设计思路：
// 1. 一个是记载数据的双向链表
// 2. 一个是记载频次的双向链表

using System.Collections.Generic;

public class LFUCache
{
    public class DLinkedNode
    {
        public int key;
        public int val;
        public int freq;
        public DLinkedNode() { }
        public DLinkedNode(int _key, int _val)
        {
            key = _key;
            val = _val;
            freq = 1;
        }
    }

    private Dictionary<int, DLinkedNode> cache;
    private Dictionary<int, LinkedList<DLinkedNode>> freqDict;
    public int Capacity;

    private int minFreq = 1;

    public LFUCache(int capacity)
    {
        cache = new Dictionary<int, DLinkedNode>(capacity);
        freqDict = new Dictionary<int, LinkedList<DLinkedNode>>();

        Capacity = capacity;

    }

    public int Get(int key)
    {
        //判断容量为0的情况
        if (Capacity == 0) return -1;
        if (cache.ContainsKey(key))
        {
            //移除在原freq列表里的元素，添加到新列表里
            freqDict[cache[key].freq].Remove(cache[key]);
            //移除之后判断列表是否为空更新minifreq
            if (freqDict[minFreq].Count == 0)
            {
                if (minFreq == cache[key].freq)
                {
                    minFreq++;
                }
            }
            cache[key].freq++;
            if (!freqDict.ContainsKey(cache[key].freq))
            {
                freqDict[cache[key].freq] = new LinkedList<DLinkedNode>();
            }
            freqDict[cache[key].freq].AddLast(cache[key]);
            return cache[key].val;
        }
        else
        {
            return -1;
        }
    }

    public void Put(int key, int value)
    {
        //判断容量为0的情况
        if (Capacity == 0) return;
        if (cache.ContainsKey(key))
        {
            cache[key].val = value;
            freqDict[cache[key].freq].Remove(cache[key]);
            //当链表为空，则minifreq为此频率时，minifreq++；
            if (freqDict[minFreq].Count == 0)
            {
                if (minFreq == cache[key].freq)
                {
                    minFreq++;
                }
            }
            cache[key].freq++;
        }
        else
        {
            //若超出容量则要移除
            if (cache.Count == Capacity)
            {
                int rm_key = freqDict[minFreq].First.Value.key;
                freqDict[minFreq].RemoveFirst();
                cache.Remove(rm_key);
            }
            cache[key] = new DLinkedNode(key, value);
            minFreq = 1;
           
        }
        if (!freqDict.ContainsKey(cache[key].freq))
        {
            freqDict[cache[key].freq] = new LinkedList<DLinkedNode>();
        }
        freqDict[cache[key].freq].AddLast(cache[key]);
    }

}
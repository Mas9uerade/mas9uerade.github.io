using System;
using System.Collections.Generic;
using System.Text;


public class Solution
{
    public class Meeting : IComparable<Meeting>
    {
        public int startDay;
        public int endDay;

        public Meeting(int _startDay, int _endDay)
        {
            startDay = _startDay;
            endDay = _endDay;
        }
        public int CompareTo(Meeting other)
        {
            if (endDay > other.endDay)
            {
                return 1;
            }
            else if (endDay < other.endDay)
            {
                return -1;
            }
            else
            {
                if (startDay <other.startDay)
                {
                    return -1;
                }
                else if (startDay > other.startDay)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }
    }

    public class MeetingInPool: IComparable<MeetingInPool>
    {
        public int startDay;
        public int endDay;

        public MeetingInPool(int _startDay, int _endDay)
        {
            startDay = _startDay;
            endDay = _endDay;
        }
        public int CompareTo(MeetingInPool other)
        {
            if (startDay > other.startDay)
            {
                return 1;
            }
            else if (startDay < other.startDay)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        public Meeting ToMeeting()
        {
            return new Meeting(startDay, endDay);
        }
    }

    public static int MaxEvents(int[][] events)
    {

        if (events == null || events.Length == 0)
        {
            return 0;
        }
        PriorityQueue<Meeting> pq = new PriorityQueue<Meeting>();
        PriorityQueue<MeetingInPool> pool = new PriorityQueue<MeetingInPool>();
        List<int> viewMeeting = new List<int>();

        int today = 0;
        int meetingCount = 0;

        for (int i = 0; i < events.Length; ++i)
        {
            pool.Enqueue(new MeetingInPool(events[i][0], events[i][1]));
        }
        //还有会议没开始
        while (pool.Count>0 ||pq.Count>0)
        {
            today++;
            //载入可选队列
            while (pool.Count >0 && pool.Peek().startDay <= today)
            {
                pq.Enqueue(pool.Dequue().ToMeeting());
            }

            //清掉过期的会议
            while (pq.Count>0 && pq.Peek().endDay < today)
            {
                pq.Dequue();
            }

            //开会
            if (pq.Count >0)
            {
                pq.Dequue();
                meetingCount++;
            }
        }
        return meetingCount;
    }

    //public class PriorityIntQueue
    //{
    //    private List<int> data;
    //    public PriorityIntQueue()
    //    {
    //        data = new List<int>();
    //    }

    //    public int Count
    //    {
    //        get
    //        {
    //            return data.Count;
    //        }
    //    }
    //    public void Enqueue(int item)
    //    {
    //        data.Add(item);
    //        int childIndex = data.Count - 1;
    //        while (childIndex > 0)
    //        {
    //            int parentIndex = (childIndex - 1) / 2;
    //            if (data[childIndex] >= data[parentIndex])
    //            {
    //                break;
    //            }
    //            int tmp = data[childIndex];
    //            data[childIndex] = data[parentIndex];
    //            data[parentIndex] = tmp;
    //        }
    //    }

    //    public int Dequeue()
    //    {
    //        int lastIndex = data.Count - 1;
    //        int front = data[0];
    //        data[0] = data[lastIndex];
    //        data.RemoveAt(lastIndex);

    //        --lastIndex;
    //        int parentIndex = 0;
    //        while(true)
    //        {
    //            int childIndex = parentIndex * 2 + 1;
    //            if (childIndex > lastIndex)
    //            {
    //                break;
    //            }
    //            int rightChild = childIndex + 1;
    //            if (rightChild <= lastIndex && data[rightChild] < data[childIndex])
    //            {
    //                childIndex = rightChild;
    //            }
    //            if (data[parentIndex] < data[childIndex])
    //            {
    //                break;
    //            }
    //            int tmp = data[parentIndex];
    //            data[parentIndex] = data[childIndex];
    //            data[childIndex] = tmp;
    //            parentIndex = childIndex;
    //        }
    //        return front;
    //    }
    //}



    //优先队列的实现方式是小根堆/大根堆
    public class PriorityQueue<T> where T : IComparable<T>
    {
        private List<T> data;

        public PriorityQueue()
        {
            this.data = new List<T>();
        }

        public int Count
        {
            get
            {
                return data.Count;
            }
        }

        public void Enqueue(T item)
        {
            data.Add(item);
            int childIndex = data.Count - 1;
            while (childIndex > 0)
            {
                int parentIndex = (childIndex - 1) / 2;
                //此时已经完成了小节点的上浮
                if (data[childIndex].CompareTo(data[parentIndex]) >= 0)
                {
                    break;
                }
                //否则交换节点
                T tmp = data[childIndex];
                data[childIndex] = data[parentIndex];
                data[parentIndex] = tmp;
                childIndex = parentIndex;
            }
        }

        public T Peek()
        {
            return data[0];
        }

        public T Dequue()
        {
            //交换节点数据去尾，减少时间复杂度
            int lastIndex = data.Count -1;
            T frontItem = data[0];   // fetch the front
            data[0] = data[lastIndex];
            data.RemoveAt(lastIndex);
            --lastIndex;
            int parentIndex = 0;

            //小根堆维护
            while (true)
            {
                int childIndex = parentIndex * 2 + 1; // left child index of parent
                if (childIndex > lastIndex) break;  // no children so done
                int rightChild = childIndex + 1;     // right child
                // if there is a rc (ci + 1), and it is smaller than left child, use the rc instead
                if (rightChild <= lastIndex && data[rightChild].CompareTo(data[childIndex]) < 0)
                {
                    childIndex = rightChild;
                }
                // parent is smaller than (or equal to) smallest child so done
                if (data[parentIndex].CompareTo(data[childIndex]) <= 0)
                {
                    break;
                }
                T tmp = data[parentIndex];
                data[parentIndex] = data[childIndex];
                data[childIndex] = tmp; // swap parent and child
                parentIndex = childIndex;
            }
            return frontItem;
        }
    }
}

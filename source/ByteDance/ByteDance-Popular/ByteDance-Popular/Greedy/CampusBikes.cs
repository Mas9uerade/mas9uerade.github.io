using System;
using System.Collections.Generic;

namespace ByteDancePopular
{
//1、将所有自行车加入队列；
//2、计算每个自行车距离最近的工人，把自行车分配给该工人；
//3、如果自行车分配的工人已经分配了自行车，且该自行车距离该工人的距离小于之前已经分配的距离，则更新，把已经分配的自行车重新放回队列；
//4、如果自行车分配的工人已经分配了自行车，且该自行车距离该工人的距离等于之前等于分配的距离，计算该工人对应的自行车ID是不是小于已经分配自行车ID，满足则更新，把已经分配的自行车重新放回队列；
//5、如果自行车分配的工人已经分配了自行车，且该自行车距离该工人的距离等于之前已经分配的距离，计算该自行车ID分配的工人ID是否小于已经分配的工人ID，满足则更新，把已经分配的自行车重新放回队列；
    public partial class Solution
    {
        public class BikeWokrerPair
        {
            public int WokrerIndex;
            public int BikeIndex;
            public int Distance;
        }

        //public int[] AssignBikes(int[][] workers, int[][] bikes)
        //{
        //    Queue<BikeWokrerPair> queuePair = new Queue<BikeWokrerPair>();
        //    //key 是自行车， value 是工人
        //    Dictionary<int, int> dictPair = new Dictionary<int, int>();

        //    //长度
        //    int workerLen = workers.Length;
        //    int bikeLen = bikes.Length;

        //    List<int> AvaiableWorker = new List<int>(workerLen);
        //    for (int i = 0; i <workerLen; ++i)
        //    {
        //        AvaiableWorker.Add(i);
        //    }

        //    List<int> AvaiableBike = new List<int>(bikeLen);
        //    for(int i = 0; i < bikeLen; ++i)
        //    {
        //        AvaiableBike.Add(i);
        //    }

        //    SortedList<>

        //    int[][] distance = new int[workerLen][];
        //    for (int i =0; i < workerLen; ++i)
        //    {
        //        distance[i] = new int[bikeLen];

        //        for (int j =0; j < bikeLen; ++j)
        //        {
        //            distance[i][j] = Math.Abs(bikes[i][0] - workers[i][0]) + Math.Abs(bikes[i][1] - workers[i][1]);
        //        }
        //    }


        //    for (int i = 0; i < bikes.Length; ++i)
        //    {
        //        queuePair.Enqueue(new BikeWokrerPair()
        //        {
        //            BikeIndex = i,
        //            Distance = -1,
        //        });
        //    }

        //    while(queuePair.Count > 0)
        //    {
        //        BikeWokrerPair pair =queuePair.Dequeue();
        //        for (int i = 0; i < workers.Length; ++i)
        //        {
        //            int distance = Math.Abs(workers[i][0] - bikes[pair.BikeIndex][0]) + Math.Abs(workers[i][1] - bikes[pair.BikeIndex][1]);
        //            if (pair.Distance  == -1)
        //            {
        //                pair.Distance = distance;
        //                pair.WokrerIndex = i;
        //                dictPair[pair.BikeIndex] = pair.WokrerIndex;
        //            }
        //            else
        //            {
        //                if (pair.Distance > distance)
        //                {
                            
        //                }
        //                else
        //                {
                            
        //                }
        //            }
        //        }
        //    }

        //}
    }
}
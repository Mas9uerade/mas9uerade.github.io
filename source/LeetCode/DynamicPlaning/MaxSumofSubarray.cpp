int maxsumofSubarray(vector<int>& arr)
{
	if (arr.size() == 0)
	{
		return 0;
	}
	if (arr.size() == 1)
	{
		return arr[0];
	}
	int max = arr[0];
	int end_index = 0;
	int relink_cost = 0;
	for (int i = 1; i < arr.size(); ++i)
	{
		//判断是否可以续链
		if (end_index == i - 1)
		{
			//判断取 arr[i] 与 max + arr[i] 与 max 的最大值，若取max 则不续链
			if (max > arr[i] && max > max + arr[i])
			{
				max = max;

				//此时已经断链了，需要增加relink_cost
				relink_cost += arr[i];
			}
			else
			{
				end_index = i;
				//如果单取一个元素大于之前的累加,则单取元素
				if (arr[i] >= max + arr[i])
				{
					max = arr[i];
				}
				else
				{
					max = arr[i] + max;
				}
			}
		}
		//若不可续链，则判断arr[i] 与 max 的最大值
		else
		{
			if (arr[i] >= max)
			{
				max = arr[i];
				end_index = i;
				relink_cost = 0;
			}
			else 
			{
				relink_cost += arr[i];
				//接链收益大，则接链
				if (relink_cost  + max >= max)
				{
					max = relink_cost + max;
					end_index = i;
					relink_cost = 0;
				}
			}
		}
	}
	return max;
}
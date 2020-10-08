#include <iostream>
#include <vector>
using namespace std;

int binary_search(int begin_index, int end_index, int val, std::vector<int>& data, int total_len)
{
	if (data[end_index] < val)
	{
		return total_len + 1;
	}
	int mid;
	int marker_index = 0;
	while (data[begin_index] <= data[end_index])
	{
		mid = (begin_index + end_index) / 2;
		if (data[mid] < val)
		{
			begin_index = mid + 1;
			//marker_index = mid;
		}
		if (data[mid] >= val)
		{
			end_index = mid - 1;
			marker_index = mid;
		}
	}
	return marker_index + 1;
}

int main()
{
	std::cout << "Hello World!\n";
	int a[100] = {3, 3, 4, 4, 4, 5, 6, 6, 6, 7, 8, 8, 12, 13, 15, 16, 21, 21, 22, 24, 24, 27, 28, 32, 34, 35, 35, 36, 36, 39, 40, 41, 41, 42, 44, 44, 45, 45, 47, 47, 47, 47, 48, 48, 50, 51, 51, 53, 53, 53, 54, 54, 54, 56, 56, 57, 59, 60, 60, 60, 60, 61, 62, 63, 65, 65, 65, 65, 67, 67, 68, 70, 71, 71, 74, 75, 75, 79, 81, 84, 84, 86, 86, 87, 90, 90, 90, 90, 91, 92, 93, 94, 94, 94, 95, 97, 97, 98, 98, 99};

	std::vector<int> vect;
	vect.assign(a, a + 100);
	

	int index = binary_search(0, 99, 97, vect, 100);
	cout << index;
	
	std::cout << "Hello World!\n";
}
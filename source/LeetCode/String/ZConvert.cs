public class Solution {
    public string Convert(string s, int numRows) 
    {
        if (numRows == 1)
        {
            return s;
        }
        int cycle = 2 * numRows -2;
        
        List<List<char>> result = new List<List<char>>();
        for (int i = 0; i < numRows; ++i)
        {
            result.Add(new List<char>());
        }
        for (int i = 0; i < s.Length; ++i)
        {
            int round = i / cycle;
            int index = i % cycle;

            int col = round * (numRows -1)  +  (index > numRows-1?  index-(numRows-1): 0);
            int row = index > numRows-1 ?  (numRows-1) - (index- (numRows-1)) : index;

            result[row].add(s[i]);
        }

        stringbuilder sb = new stringBuilder();
        for (int i = 0; i < result.Length; ++i)
        {
            for (int j  = 0; j < result[i].Length; ++j)
            {
                sb.append(result[i][j]);
            }
        }
        return sb.ToString();
    }
}
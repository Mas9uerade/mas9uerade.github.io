using System;
using System.Collections.Generic;

public class CMinTransfer
{

    public int MinTransfers(int[][] transactions)
    {
        int res = int.MaxValue;
        Dictionary<int, int> loans = new Dictionary<int, int>();
        int[] accounts;

        if (transactions == null || transactions.Length == 0)
        {
            return 0;
        }
        for (int i = 0; i < transactions.Length; ++i)
        {
            int[] lend = transactions[i];
            int lender = lend[0];
            int borrower = lend[1];

            if (!loans.ContainsKey(lender))
            {
                loans[lender] = 0;
            }

            if (!loans.ContainsKey(borrower))
            {
                loans[borrower] = 0;
            }
            loans[lender] += lend[2];
            loans[borrower] -= lend[2];
        }
        accounts = new int[loans.Values.Count];
        loans.Values.CopyTo(accounts, 0);
        DFS(0, 0, ref res, ref accounts);
        return res;
    }

    public void DFS(int index, int count, ref int res, ref int[] accounts)
    {
        if (count >= res) return;
        while (index < accounts.Length &&accounts[index] == 0)
        {
            index++;
        }
        if (index == accounts.Length)
        {
            res = Math.Min(res, count);
            return;
        }

        if (index == accounts.Length)
        {
            res = Math.Min(res, count);
            return;
        }
        for (int j = index + 1; j < accounts.Length; ++j)
        {
            if (accounts[index] * accounts[j] < 0)
            {
                accounts[j] += accounts[index];
                DFS(index + 1, count + 1, ref res, ref accounts);
                accounts[j] -= accounts[index];
            }
        }
    }
}

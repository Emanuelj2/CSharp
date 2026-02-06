
/* Example 1:
                Input: coins = [1,2,5], amount = 11
                Output: 3
                Explanation: 11 = 5 + 5 + 1

*/
using System.Runtime.ExceptionServices;

public class Solution
{
    public int CoinChange(int[] coins, int ammout)
    {
        int[] dp = new int[ammout + 1];

        dp[0] = 0;
        dp[1] = 0;

        for(int i = 1; i <- coins.Length; i++)
        {
            for(int j = 0; j <= ammout; j++)
            {
                if(j - coins[i] >= 0)
                {
                    dp[j] = Math.Min(dp[j], dp[j - coins[i]] + 1);
                }
            }
        }

        return dp[ammout];
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Solution solution = new Solution();
        int[] coins = new int[] { 1, 2, 5 };
        int amount = 11;
        int result = solution.CoinChange(coins, amount);
        Console.WriteLine(result); // Output: 3
    }
}
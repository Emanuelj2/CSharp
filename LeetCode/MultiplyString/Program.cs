// See https://aka.ms/new-console-template for more information
using System;
using System.Text;

class Solution {
    public string Multiply(string num1, string num2) {
        if (num1 == "0" || num2 == "0") return "0";
        
       //use assci table
        int m = num1.Length, n = num2.Length;
        int[] pos = new int[m + n];
        
        for (int i = m - 1; i >= 0; i--) {
            for (int j = n - 1; j >= 0; j--) {
                int mul = (num1[i] - '0') * (num2[j] - '0');
                int sum = mul + pos[i + j + 1];
                
                pos[i + j + 1] = sum % 10;
                pos[i + j] += sum / 10;
            }
        }
        
        StringBuilder sb = new StringBuilder();
        foreach (int p in pos) {
            if (!(sb.Length == 0 && p == 0)) {
                sb.Append(p);
            }
        }
        
        return sb.Length == 0 ? "0" : sb.ToString();
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Solution solution = new Solution();
        string num1 = "123";
        string num2 = "456";
        string result = solution.Multiply(num1, num2);
        Console.WriteLine($"Multiplication of {num1} and {num2} is: {result}");
    }
}
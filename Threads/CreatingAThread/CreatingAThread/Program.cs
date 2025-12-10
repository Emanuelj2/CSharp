using System;
using System.Threading;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<Thread> threads = new List<Thread>();

        // create multiple threads
        for (int i = 1; i <= 5; i++)
        {
            int localCopy = i; // capture the value properly
            Thread t = new Thread(() =>
            {
                Console.WriteLine($"worker thread {localCopy}...");
                Thread.Sleep(2000);
            });
            threads.Add(t);
            t.Start();
        }

        Console.WriteLine("main thread");

        // wait for all threads to finish
        foreach (Thread t in threads)
        {
            t.Join();
        }

        Console.WriteLine("threads finished");
    }
}
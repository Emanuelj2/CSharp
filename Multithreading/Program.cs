using System;
using System.Collections.Concurrent;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
/*
public class ServerClass
{
    public void InstanceMethod()
    {
        Console.WriteLine("Server.Class: Instance method called.");

        //pause for a momenr
        Thread.Sleep(5000);
        Console.WriteLine("Server.Class: Instance method completed.");
    }

    public static void StaticMethod()
    {
        Console.WriteLine("Server.Class: Static method called.");
        //pause for a moment
        Thread.Sleep(5000);
        Console.WriteLine("Server.Class: Static method completed.");
    }
}

public class Simple
{
    public static void Main()
    {
        ServerClass serverObj = new ServerClass();

        //create a thread to call the instance method
        Thread InstanceCaller = new Thread(new ThreadStart(serverObj.InstanceMethod));
        InstanceCaller.Start(); //start the thread

        Console.WriteLine("The Main() thread calls this after "
            + "starting the new InstanceCaller thread.");

        Thread StaticCaller = new Thread(new ThreadStart(ServerClass.StaticMethod));
        StaticCaller.Start(); //start the thread

        Console.WriteLine("The Main() thread calls this after "
            + "starting the new StaticCaller thread.");

    }
}*/


/*public class ThreadWithState
{
    private String boilerplate;
    private Int32 numberValue;

    public ThreadWithState(String boilerplate, Int32 numberValue)
    {
        this.boilerplate = boilerplate;
        this.numberValue = numberValue;
    }

    public void ThreadProc()
    {
        Console.WriteLine(boilerplate + " " + numberValue);
    }
}

public class Example
{
    public static void Main()
    {
        ThreadWithState tws = new ThreadWithState("Hello", 42);
        Thread t = new Thread(new ThreadStart(tws.ThreadProc));
        t.Start();
        Console.WriteLine("Main thread does some work, then waits.");
        t.Join();
        Console.WriteLine("Independent thread has completed; main thread ends.");
    }
}*/

/*public class ThreadWithState
{
    private String boilerplate;
    private Int32 numberValue;

    private ExampleCallback callback;
    public ThreadWithState(String boilerplate, Int32 numberValue, ExampleCallback callback)
    {
        this.boilerplate = boilerplate;
        this.numberValue = numberValue;
        this.callback = callback;
    }
    public void ThreadProc()
    {
        Console.WriteLine(boilerplate + " " + numberValue);
        callback?.Invoke(boilerplate, numberValue);
    }
}

public delegate void ExampleCallback(String s, Int32 i);

public class Example
{
    public static void Main()
    {
        ExampleCallback callback = new ExampleCallback(ExampleMethod);
        ThreadWithState tws = new ThreadWithState("Hello", 42, callback);
        Thread t = new Thread(new ThreadStart(tws.ThreadProc));
        t.Start();
        Console.WriteLine("Main thread does some work, then waits.");
        t.Join();
        Console.WriteLine("Independent thread has completed; main thread ends.");
    }
    public static void ExampleMethod(String s, Int32 i)
    {
        Console.WriteLine("Callback method called with parameters: " + s + " " + i);
    }
}*/

class LabOrder
{
    public Int32 OrderId { get; }
    public String PatientName { get; }
    public String TestType { get; }

    public LabOrder(Int32 OrderId, String PatientName, String TestType)
    {
        this.OrderId = OrderId;
        this.PatientName = PatientName;
        this.TestType = TestType;
    }
}


class LabSystem
{
    private readonly ConcurrentQueue<LabOrder> orderQueue = new();

    private Int32 completedOrders = 0;
    
    private readonly List<String> completedResults = new();

    public void AddOrder(LabOrder order)
    {
        orderQueue.Enqueue(order);
    }

    public async Task ProcessOrdersAsync()
    {
        Int32 workerCount = 4;
        Task[] workers = new Task[workerCount];

        for(Int32 i = 0; i < workerCount; i++)
        {
            workers[i] = Task.Run(ProccessOrderWorker);
        }
    }

    private void ProccessOrderWorker()
    {
        while (orderQueue.TryDequeue(out LabOrder? order))
        {
            Console.WriteLine(
                $"Thread {Environment.CurrentManagedThreadId} " +
                $"processing Order {order.OrderId} " +
                $"for {order.PatientName}");

            Thread.Sleep(Random.Shared.Next(1000, 3000));

            string result =
                $"Order {order.OrderId}: {order.TestType} completed.";

            //multiple threads could access this at the same time
            lock (completedResults)
            {
                completedResults.Add(result);
            }

            //atomic Increment
            Interlocked.Increment(ref completedOrders);

            Console.WriteLine(
                $"Thread {Environment.CurrentManagedThreadId} " +
                $"finished Order {order.OrderId}");
        }
    }


    public void PrintResults()
    {
        Console.WriteLine("\nCompleted Results:");

        lock (completedResults)
        {
            foreach (string result in completedResults)
            {
                Console.WriteLine(result);
            }
        }

        Console.WriteLine($"\nTotal completed: {completedOrders}");
    }

}


class Program
{
    static async Task Main()
    {
        LabSystem system = new LabSystem();

        system.AddOrder(new LabOrder(1001, "John", "Blood Test"));
        system.AddOrder(new LabOrder(1002, "Maria", "X-Ray"));
        system.AddOrder(new LabOrder(1003, "David", "MRI"));
        system.AddOrder(new LabOrder(1004, "Sarah", "Blood Test"));
        system.AddOrder(new LabOrder(1005, "James", "CT Scan"));
        system.AddOrder(new LabOrder(1006, "Emily", "Urine Test"));
        system.AddOrder(new LabOrder(1007, "Robert", "MRI"));
        system.AddOrder(new LabOrder(1008, "Linda", "X-Ray"));

        await system.ProcessOrdersAsync();

        system.PrintResults();
    }
}
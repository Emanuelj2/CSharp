public class Caller
{
    private readonly RateLimiter_FixedWIndow _rateLimiter;

    public Caller(RateLimiter_FixedWIndow rateLimiter)
    {
        _rateLimiter = rateLimiter;
    }

    public async Task SingleCall()
    {
        await SimulateRequest(1);
    }

    public async Task MultipleCalls(int calls)
    {
        for (int i = 0; i < calls; i++)
        {
            await SimulateRequest(i + 1);
        }
    }

    public async Task ConcurrentCalls(int numberOfCalls)
    {
        Task[] requests = new Task[numberOfCalls];

        for (int i = 0; i < numberOfCalls; i++)
        {
            int requestId = i + 1;
            requests[i] = SimulateRequest(requestId);
        }

        await Task.WhenAll(requests);
    }

    private async Task SimulateRequest(int requestId)
    {
        if (_rateLimiter.TryAcquire())
        {
            Console.WriteLine($"Request {requestId}: ACCEPTED");

            await Task.Delay(1000);
        }
        else
        {
            Console.WriteLine($"Request {requestId}: REJECTED");
        }
    }
}


public class  RateLimiter_FixedWIndow
{
    private readonly int _maxRequest;
    private readonly TimeSpan _windowDurration;

    private readonly object _lock = new object();

    private DateTime _windowStart;
    private int _requestInWindow;

    private int _totalRejectedRequests;
    private int _totalAcceptedRequests;


    public RateLimiter_FixedWIndow(int maxRequest, TimeSpan windowDurration)
    {

        if(maxRequest <= 0)
        {
            throw new ArgumentException("maxRequest must be greater than 0");
        }
        if(windowDurration <= TimeSpan.Zero)
        {
            throw new ArgumentException("windowDurration must be greater than 0");
        }

        _maxRequest = maxRequest;
        _windowDurration = windowDurration;
        _windowStart = DateTime.UtcNow;
        _requestInWindow = 0;
    }


    public bool TryAcquire()
    {
        lock (_lock)
        {
            var now = DateTime.UtcNow;
            if (now - _windowStart >= _windowDurration)
            {
                _windowStart = now;
                _requestInWindow = 0;
            }
            if (_requestInWindow < _maxRequest)
            {
                _requestInWindow++;
                _totalAcceptedRequests++;
                return true;
            }
            else
            {
                _totalRejectedRequests++;
                return false;
            }
        }
    }

    public int TotalAllowed
    {
        get
        {
            lock (_lock)
            {
                return _totalAcceptedRequests;
            }
        }
    }

    public int TotalRejected
    {
        get
        {
            lock (_lock)
            {
                return _totalRejectedRequests;
            }
        }
    }
}


//main program
public class Program
{
    public static async Task Main(string[] args)
    {
        // Configure the rate limiter.
        var limiter = new RateLimiter_FixedWIndow(
            maxRequest: 5,
            windowDurration: TimeSpan.FromSeconds(60)
        );

        // Give Caller access to the rate limiter.
        var caller = new Caller(limiter);

        // Send 20 concurrent requests.
        Console.WriteLine("Starting 20 concurrent requests...\n");

        await caller.ConcurrentCalls(20);

        // Display the results.
        Console.WriteLine("\n--- Results ---");
        Console.WriteLine($"Accepted: {limiter.TotalAllowed}");
        Console.WriteLine($"Rejected: {limiter.TotalRejected}");
    }
}

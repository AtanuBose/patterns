using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace RetryAsyncMsgProcessing.Models;

public class RetryPolicy
{
    [JsonPropertyName("retryCount")]
    public int RetryCount { get; private set; }

    [JsonPropertyName("retryWindowInMinutes")]
    public Collection<int> RetryWindowInMinutes { get; }

    public RetryPolicy(int retryCount, Collection<int> retryWindowInMinutes)
    {
        RetryCount = retryCount;

        RetryWindowInMinutes = retryWindowInMinutes ?? [];
    }

    public void IncrementRetryCount()
    {
        if (!RetryLimitReached())
        {
            RetryCount++;
        }
    }

    public bool RetryLimitReached()
    {
        return RetryCount >= RetryWindowInMinutes.Count;
    }
}

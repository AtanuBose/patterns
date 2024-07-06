using System.Text.Json.Serialization;

namespace RetryAsyncMsgProcessing.Models;

public class DataEnvelope<T>
{
    [JsonPropertyName("data")]
    public T Data { get; }

    [JsonPropertyName("retry")]
    public RetryPolicy RetryPolicy { get; }

    public DataEnvelope(T data, RetryPolicy retryPolicy)
    {
        Data = data ?? throw new ArgumentNullException(nameof(data));
        RetryPolicy = retryPolicy ?? throw new ArgumentNullException(nameof(retryPolicy));
    }
}

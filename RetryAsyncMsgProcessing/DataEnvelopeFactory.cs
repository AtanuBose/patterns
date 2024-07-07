using RetryAsyncMsgProcessing.Models;

namespace RetryAsyncMsgProcessing;

public class DataEnvelopeFactory<T> : IDataEnvelopeFactory<T>
{
    private readonly RetryPolicy _retryPolicy;

    public DataEnvelopeFactory(RetryPolicy retryPolicy)
    {
        _retryPolicy = retryPolicy ?? throw new ArgumentNullException(nameof(retryPolicy));
    }

    public DataEnvelope<T> Create(T data)
    {
        return new DataEnvelope<T>(data, _retryPolicy);
    }
}

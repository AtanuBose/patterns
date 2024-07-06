namespace RetryAsyncMsgProcessing.Models;

public interface IDataEnvelopeFactory<T>
{
    public DataEnvelope<T> Create(T data);
}

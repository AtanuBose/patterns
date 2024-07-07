using RetryAsyncMsgProcessing.Models;

namespace RetryAsyncMsgProcessing;

public class Program(
    IDataEnvelopeFactory<SampleModel> factory,
    SampleModel defaultOutputModel)
{
    private readonly Service _service = new Service(factory, defaultOutputModel) ?? throw new ArgumentNullException(nameof(factory));

    public DataEnvelope<SampleModel> Run()
    {
        var message = ReadMessageFromBroker();

        var output = _service.Process(message.Data);

        if (output.Data == defaultOutputModel)
        {
            RetryProcessor(message);
        }

        return output;
    }

    private static DataEnvelope<SampleModel> ReadMessageFromBroker()
    {
        //Read the message from the message-broker
        //Deserialize message to DataEnvelope<SampleModel> object
        throw new NotImplementedException();
    }

    private static void RetryProcessor(DataEnvelope<SampleModel> message)
    {
        if (message.RetryPolicy.RetryLimitReached())
        {
            //Deadletter the message or move to poison queue
        }

        RequeueMessage(message);
    }

    private static void RequeueMessage(DataEnvelope<SampleModel> message)
    {
        var currentRetryCount = message.RetryPolicy.RetryCount;

        message.RetryPolicy.IncrementRetryCount();

        var nextScheduledMessageDeliveryTime = DateTimeOffset.Now.AddMinutes(message.RetryPolicy.RetryWindowInMinutes[currentRetryCount]);

        //Push the message in the message-broker with the nextScheduledMessageDeliveryTime
    }
}

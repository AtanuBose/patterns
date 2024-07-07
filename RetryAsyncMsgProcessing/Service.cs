using RetryAsyncMsgProcessing.Models;

namespace RetryAsyncMsgProcessing;

public class Service(
    IDataEnvelopeFactory<SampleModel> factory,
    SampleModel defaultOutputModel)
{
    private readonly IDataEnvelopeFactory<SampleModel> _factory = factory ?? throw new ArgumentNullException(nameof(factory));

    public DataEnvelope<SampleModel> Process(SampleModel input)
    {
        var output = defaultOutputModel;

        if (IsValid(input))
        {
            try
            {
                output = new SampleModel()
                {
                    Id = input.Id,
                    FirstName = input.FirstName,
                    Birthdate = input.Birthdate,
                    Age = input.Age == 0 ? CalculateAge(input.Birthdate) : input.Age
                };
            }
            catch (Exception)
            {
                //Log exception
            }
            
        }

        return _factory.Create(output);
    }

    private static bool IsValid(SampleModel data)
    {
        if (data is null)
        {
            return false;
        }

        return true;
    }

    private static uint CalculateAge(DateTimeOffset birthdate)
    {
        //This method simulates as API call that can fail and might need to be retied if the failure type is transient
        return (uint)(DateTimeOffset.UtcNow.Year - birthdate.UtcDateTime.Year);
    }
}

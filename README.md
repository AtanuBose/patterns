# Patterns
## 1. Retry Pattern in Asynchronous Message Processing
This pattern is used in asynchronous message processing where there is a compute service picking up messages from a message-broker, like a queue. If a message fails to be processed by the service, then it is retried based on a retry-policy. If all retries fail, then the message is either dead-lettered or pushed to a poison queue from offline processing.

The message or *DataEnvelope* that is pushed to the message-broker looks something like this:
<pre>
{
  "data" {
    //message
  },
  "retryPolicy": {
    "retryCount": 0,
    "retryWindowInMinutes": [] //integer array
  }
}
</pre>

The initial retryCount = 0, which gets incremented as the message is retried. The model and the service code can be found in this repo.

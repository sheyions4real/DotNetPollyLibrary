using Polly;
using Polly.Retry;

namespace RequestService.Policies;

// creating the Polly policy
public class ClientPolicy
{
   public AsyncRetryPolicy<HttpResponseMessage> ImmediateHttpRetry { get;}
   public AsyncRetryPolicy<HttpResponseMessage> linearHttpRetry { get;}
   public AsyncRetryPolicy<HttpResponseMessage> exponentialHttpRetry { get;}

   public AsyncPolicy<HttpResponseMessage> noHttpPolicy {get;}

 



    public ClientPolicy()
    {
        // Policy to retry 5 times
        ImmediateHttpRetry = Policy.HandleResult<HttpResponseMessage>(res => !res.IsSuccessStatusCode)
        .RetryAsync(5);


        //Another policy to retry 5 times but with after 3 seconds - delay
        linearHttpRetry = Policy.HandleResult<HttpResponseMessage>(
            res => !res.IsSuccessStatusCode)
            .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(3));

// another policy which is mostly likely used in real world to retry at exponential time
        exponentialHttpRetry = Policy.HandleResult<HttpResponseMessage>(
            res => !res.IsSuccessStatusCode)
             .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        

        noHttpPolicy= Policy.HandleResult<HttpResponseMessage>(res => !res.IsSuccessStatusCode).RetryAsync(0);
      
    }


    
}
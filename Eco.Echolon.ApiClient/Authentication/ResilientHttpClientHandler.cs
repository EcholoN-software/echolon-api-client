using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Eco.Echolon.ApiClient.Log;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;

namespace Eco.Echolon.ApiClient.Authentication
{
    public class ResilientHttpClientHandler : HttpClientHandler
    {
        private static readonly ILog Logger = LogService.Instance.GetLogger(typeof(ResilientHttpClientHandler));

        private readonly Lazy<IAsyncPolicy<HttpResponseMessage>> _policy;

        public static ResilientHttpClientHandler Create()
        {
            return new ResilientHttpClientHandler();
        }

        public ResilientHttpClientHandler()
        {
            _policy = new Lazy<IAsyncPolicy<HttpResponseMessage>>(CreateRetryPolicy);
        }

        private IAsyncPolicy<HttpResponseMessage> Policy => _policy.Value;

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            request.Headers.ExpectContinue = false;

            return Policy.ExecuteAsync(() => base.SendAsync(request, cancellationToken));
        }

        protected virtual AsyncRetryPolicy<HttpResponseMessage> CreateRetryPolicy()
        {
            var retryAttempts = new[]
            {
                TimeSpan.Zero,
                TimeSpan.FromMilliseconds(100),
                TimeSpan.FromMilliseconds(200),
                TimeSpan.FromMilliseconds(400),
                TimeSpan.FromMilliseconds(800),
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(2),
                TimeSpan.FromSeconds(3),
                TimeSpan.FromSeconds(4),
                TimeSpan.FromSeconds(5)
            };

            static async Task LogAttempt(DelegateResult<HttpResponseMessage> outcome,
                TimeSpan delayTime, int retryAttempt, Context context)
            {
                var authority = outcome.Result?.RequestMessage?.RequestUri?.Authority ?? "remote party";
                var message = $"Connection could not be established with {authority} ...";

                if (!string.IsNullOrWhiteSpace(outcome.Result?.ReasonPhrase))
                    message += $" {outcome.Result.ReasonPhrase}.";

                message += $" Retry attempt #{retryAttempt} after {delayTime}.";

                if (outcome.Result?.Content != null)
                {
                    var content = await outcome.Result.Content.ReadAsStringAsync();
                    if (!string.IsNullOrWhiteSpace(content))
                    {
                        message += Environment.NewLine;
                        message += Environment.NewLine;
                        message += content;
                    }
                }

                Logger.Info(message, outcome.Exception);
            }

            return HttpPolicyExtensions.HandleTransientHttpError()
                .WaitAndRetryAsync(retryAttempts, LogAttempt);
        }
    }
}
using Prism.Events;
using Store.Client.Events;
using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tiny.RestClient;

namespace Store.Client.Communication
{
    public class ClientListener : IListener
    {
        public bool MeasureTime => true;

        private readonly IEventAggregator eventAggregator;

        public ClientListener(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
        }

        public Task OnFailedToReceiveResponseAsync(Uri uri, HttpMethod httpMethod, Exception exception, TimeSpan? elapsedTime, CancellationToken cancellationToken)
        {
            var builder = new StringBuilder();

            builder.AppendLine($"URI:{uri}");
            builder.AppendLine($"HttpMethod:{httpMethod.Method}");
            builder.AppendLine($"Exception:{exception}");
            builder.AppendLine($"ElapsedTime:{elapsedTime}");

            eventAggregator.GetEvent<PrintLogEvent>().Publish(builder.ToString());
            builder = null;

            return Task.CompletedTask;
        }

        public async Task OnReceivedResponseAsync(Uri uri, HttpMethod httpMethod, HttpResponseMessage response, TimeSpan? elapsedTime, CancellationToken cancellationToken)
        {
            var builder = new StringBuilder();
            var content = await response.Content.ReadAsStringAsync();

            builder.AppendLine($"URI:{uri}");
            builder.AppendLine($"HttpMethod:{httpMethod.Method}");
            builder.AppendLine($"HttpResponseMessage:{response}");
            builder.AppendLine($"HttpResponseContent:{content}");
            builder.AppendLine($"ElapsedTime:{elapsedTime}");

            eventAggregator.GetEvent<PrintLogEvent>().Publish(builder.ToString());
            builder = null;
        }

        public Task OnSendingRequestAsync(Uri uri, HttpMethod httpMethod, HttpRequestMessage httpRequestMessage, CancellationToken cancellationToken)
        {
            var builder = new StringBuilder();

            builder.AppendLine($"URI:{uri}");
            builder.AppendLine($"HttpMethod:{httpMethod.Method}");
            builder.AppendLine($"HttpRequestMessage:{httpRequestMessage}");

            eventAggregator.GetEvent<PrintLogEvent>().Publish(builder.ToString());
            builder = null;

            return Task.CompletedTask;
        }
    }
}

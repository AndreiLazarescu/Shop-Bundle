using Prism.Events;
using System;

namespace Store.Client.Events
{
    public class ErrorMessageEvent : PubSubEvent<Exception>
    {
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Autofac;
using AggregatorPackage.Shared;

namespace AggregatorPackage
{
    public interface IAggregateMessage
    {
    }

    public class AggregateMessage : IAggregateMessage
    {
        private static IEventAggregator _aggregator;
        internal AggregateMessage(IEventAggregator aggregator)
        {
            _aggregator = aggregator;
        }
        /// <summary>
        /// Sends the Event over to the aggregator and returns an expected type
        /// </summary>
        /// <typeparam name="TReturn">Expected return</typeparam>
        /// <typeparam name="TObject">Event Model class</typeparam>
        /// <param name="sender">the event to be sent over</param>
        /// <returns>Returns the Expected Type</returns>
        public static TReturn SendToAggregator<TReturn, TObject>(TObject sender) where TObject : class
        {
            return (TReturn)_aggregator.PublishWithReturn(sender).Result;
        }
    }
}

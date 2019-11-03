using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AggregatorPackage.Shared
{

    /// <summary>
    /// Interface for Reverse Dependency Injection
    /// </summary>
    internal interface IEventAggregator
    {
        void Subscribe(Type caller, Func<object, Task<object>> onEvent);
        //void Subscribe(Type caller, Func<object, Task> onEvent);
        Task Publish<TCaller>(TCaller publishEvent) where TCaller: class;
        Task<object> PublishWithReturn<TCaller>(TCaller publishEvent) where TCaller : class;
    }
    internal class EventAggregator : IEventAggregator
    {
        private readonly List<object[]> _subscribers = new List<object[]>();
        
        /// <summary>
        /// Singleton so that no other Aggregator is created
        /// </summary>
        public static EventAggregator Instance { get; } = new EventAggregator();


        /// <summary>
        /// Subscription for a Task to be run with a return
        /// </summary>
        /// <param name="caller">Class Model for Call</param>
        /// <param name="onEvent">Task that runs and returns</param>
        public void Subscribe(Type caller, Func<object, Task<object>> onEvent)
        {
            var item = new object[]
            {
                caller, onEvent
            };
            _subscribers.Add(item);
        }

        /// <summary>
        /// Subscription for a no return to run just a task
        /// </summary>
        /// <param name="caller">Class Model for call</param>
        /// <param name="onEvent">Event task to be run</param>
        //public void Subscribe(Type caller, Func<object, Task> onEvent)
        //{
        //    var item = new object[]
        //    {
        //        caller, onEvent
        //    };
        //    _subscribers.Add(item);
        //}
        

        /// <summary>
        /// Call the subscribed method passing the publish event no return
        /// </summary>
        /// <typeparam name="TCaller"></typeparam>
        /// <param name="publishEvent">Class Model for call</param>
        public async Task Publish<TCaller>(TCaller publishEvent) where TCaller: class
        {
            var eventRun = _subscribers.FindAll(c=> c[0].Equals(publishEvent.GetType()));
            
            foreach(object[] obj in eventRun)
            {
                if(obj[1] != null)
                {  
                    var myTask = (Func<object, Task>) obj[1];

                    await myTask.Invoke(publishEvent);
                    
                }
            }
            
        }
        /// <summary>
        /// Call the subscribed method passing the publish event with a return object
        /// </summary>
        /// <typeparam name="TCaller"></typeparam>
        /// <param name="publishEvent">Class Model for call</param>
        /// <returns></returns>
        public async Task<object> PublishWithReturn<TCaller>(TCaller publishEvent) where TCaller : class
        {
            var eventRun = _subscribers.FindAll(c=> c[0].Equals(publishEvent.GetType()));
            
            object returnObject = null;

            foreach(var obj in eventRun)
            {
                if(obj[1] != null)
                {  
                    var runnableTask = (Func<object, Task<object>>) obj[1];

                    returnObject = await runnableTask.Invoke(publishEvent).ConfigureAwait(true);
                }
            }
            return returnObject;
        }

        
    }
}

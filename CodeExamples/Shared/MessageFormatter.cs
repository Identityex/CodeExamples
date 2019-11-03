using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace AggregatorPackage.Shared
{
    /// <summary>
    /// Message Formatter is used to format Json serialized classes back into thier respective deserialized counterparts 
    /// </summary>
    internal class MessageFormatter 
    {
        private static IEventAggregator _aggregator;
        public MessageFormatter(IEventAggregator aggregator)
        {
            _aggregator = aggregator;
        }

        /// <summary>
        /// Takes a Json serialized event and grabs the corresponding event using className
        /// </summary>
        /// <param name="jsonMessage">Serialized "Event"</param>
        /// <returns>The actual class Model</returns>
        public static dynamic GetClassModel(string jsonMessage)
        {


            JObject obj = JObject.Parse(jsonMessage);

            var myType = Type.GetType(obj.Value<string>("className"));
            Type classType = Type.GetType("AggregatorPackage.Events." +  obj.Value<string>("className") + ", AggregatorEvents");
            if(classType != null)
            {
                dynamic classModel = Activator.CreateInstance(classType);

                return JsonConvert.DeserializeObject(jsonMessage, classType);
            }
            else
            {
                return null;
            }
        }
    }
}

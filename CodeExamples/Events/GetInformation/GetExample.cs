using System;
using System.Collections.Generic;
using System.Text;

namespace AggregatorPackage.Events.GetInformation
{
    /// <summary>
    /// A Class Model for getting companies
    /// </summary>
    public class GetExample
    {
        /// <summary>
        /// Default class name used to translate into a class 
        /// </summary>
        public string className {get; } = "GetExample";

        /// <summary>
        /// Example Text
        /// </summary>
        public string exampleText {get; set;}
        
    }
}

using System.Linq;
using System.Threading.Tasks;
using AggregatorPackage.Shared;
using AggregatorPackage.Events.GetInformation;

namespace AggregatorPackage.Functions
{
    internal interface ISearchFunctions
    {
        void Initialize();
    }
    /// <summary>
    /// Class of all functions used to Search for Data based on inputs
    /// </summary>
    internal class SearchFunctions : ISearchFunctions
    {
        private IEventAggregator _aggregator;

        public SearchFunctions(IEventAggregator aggregator)
        {
            _aggregator = aggregator;
        }

        /// <summary>
        /// Initialize this class to subscribe all the Events to Functions
        /// </summary>
        public void Initialize()
        {
            _aggregator.Subscribe(typeof(GetExample), async obj => await GetExample((GetExample)obj));
        }

        /// <summary>
        /// Get an example which is sent in the object
        /// </summary>
        /// <param name="sender">The event class</param>
        /// <returns>An Queryable list of Companies</returns>
        #region GetCompanies
        public async Task<object>GetExample(GetExample sender)
        {
            return sender.exampleText;
        }
        #endregion

    }
}

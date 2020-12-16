using System.Configuration;
using DB.DataAccess;
using DB.Models.SearchModels;

namespace DB.Samples
{
    public class DataManager
    {
        public void GetData(int count, int page, string targPath)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["Northwind"];
            var ordersRepository = new OrdersRepository(connectionString.ConnectionString);

            var searchCriteria = new SearchCriteria
            {
                Count = count,
                Page = page
            };
            var result = ordersRepository.GetOrders(searchCriteria);
            var xmlDoc = new XmlGenerator(targPath);
            xmlDoc.XmlGenerate(result);
        }
    }
}

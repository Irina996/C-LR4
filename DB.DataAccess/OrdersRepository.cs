using System;
using DB.Models;
using DB.Models.SearchModels;
using System.Data.SqlClient;
using System.Data;
using DB.DataAccess.SqlCommandExtensions;
using System.Collections.Generic;

namespace DB.DataAccess
{
    public class OrdersRepository
    {
        private readonly string _connection;
        private SqlConnection _sqlConnection;

        public OrdersRepository(string connection)
        {
            _connection = connection ?? throw new ArgumentOutOfRangeException($"{nameof(connection)} can not be null");
            _sqlConnection = new SqlConnection(_connection);
        }

        public IEnumerable<Order> GetOrders(SearchCriteria searchCriteria)
        {
            try
            {
                _sqlConnection.Open();

                var command = new SqlCommand();

                command.CommandText = "Orders_ReadMany";
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = _sqlConnection;

                command.Parameters.AddWithValue("@Page", searchCriteria.Page);
                command.Parameters.AddWithValue("@Count", searchCriteria.Count);

                var entities = command.ReadMany<Order>();

                return entities;
            }
            finally
            {
                _sqlConnection.Close();
            }
        }
    }
}

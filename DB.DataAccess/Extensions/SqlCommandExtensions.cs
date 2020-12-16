using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DB.DataAccess.SqlCommandExtensions
{
    public static class SqlCommandExtensions
    {
        public static IEnumerable<TEntity> ReadMany<TEntity>(this SqlCommand command) where TEntity : new()
        {
            var result = ReadManyInternal<TEntity>(command);
            return result;
        }

        private static IEnumerable<TEntity> ReadManyInternal<TEntity>(this SqlCommand command) where TEntity : new()
        {
            var reader = command.ExecuteReader();

            if (!reader.HasRows)
            {
                return Enumerable.Empty<TEntity>();
            }

            var entities = reader.ParseFromReaderInternal<TEntity>();

            reader.Close();

            return entities;
        }

        private static IEnumerable<TEntity> ParseFromReaderInternal<TEntity>(this SqlDataReader reader) where TEntity : new()
        {
            var entityType = typeof(TEntity);
            var entityProps = entityType.GetProperties();

            var entities = new List<TEntity>();

            while (reader.Read())
            {
                var entity = new TEntity();

                foreach (var entityPropInfo in entityProps)
                {
                    var valueFromReader = reader[entityPropInfo.Name];

                    if (valueFromReader is DBNull)
                    {
                        valueFromReader = null;
                    }
                    entityPropInfo.SetValue(entity, valueFromReader);
                }

                entities.Add(entity);

            }

            return entities;

        }
    }
}

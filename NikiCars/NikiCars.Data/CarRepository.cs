using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using NikiCars.Command.Interfaces;
using NikiCars.Data.Models;
using NikiCars.Data.Interfaces;
using NikiCars.Data.Models.Includes;
using NikiCars.Data.Search;
using NikiCars.Search;
using NikiCars.Search.Interfaces;
using System.Linq;
using NikiCars.Data.DataConstants;

namespace NikiCars.Data
{
    public class CarRepository : BaseRepository<Car>, ICarRepository
    {
        private const string PRIMARY_KEY = CarColumns.CAR_ID;

        public CarRepository(IConfig config)
            : base(config)
        {
        }

        protected override Dictionary<string, SqlParameter> GetCreateParameters(Car item)
        {
            return new Dictionary<string, SqlParameter>
            {
                {CarColumns.CARMODEL_ID, new SqlParameter() { Value = item.CarModelID.ToString() } },
                {CarColumns.CARCOUPE_ID, new SqlParameter() { Value = item.CarCoupeID.ToString() } },
                {CarColumns.NUMBEROFDOORS_ID, new SqlParameter() { Value = item.NumberOfDoorsID.ToString() } },
                {CarColumns.FUELTYPE_ID, new SqlParameter() { Value = (object)item.FuelTypeID ?? DBNull.Value} },
                {CarColumns.COLOUR_ID, new SqlParameter() { Value = (object)item.ColourID ?? DBNull.Value } },
                {CarColumns.GEARBOXTYPE_ID, new SqlParameter() { Value = (object)item.GearboxTypeID ?? DBNull.Value } },
                {CarColumns.HORSEPOWER, new SqlParameter() { Value = item.HorsePower.ToString() } },
                {CarColumns.FIRST_REGISTRATION_DATE, new SqlParameter() { Value = item.FirstRegistrationDate.ToString() } },
                {CarColumns.ENGINE_CAPACITY, new SqlParameter() { Value = item.EngineCapacity.ToString() } },
                {CarColumns.KILOMETERS, new SqlParameter() { Value = item.Kilometers.ToString() } },
                {CarColumns.PRICE, new SqlParameter() { Value = item.Price.ToString() } },
                {CarColumns.DISCRIPTION, new SqlParameter() { Value = item.Discription } },
                {CarColumns.IS_LEFTSTEERING, new SqlParameter() { Value = item.IsLeftSteering.ToString() } },
                {CarColumns.IS_USED, new SqlParameter() { Value = item.IsUsed.ToString() } },
                {CarColumns.IS_FORPARTS, new SqlParameter() { Value = item.IsForParts.ToString() } },
                {CarColumns.IS_DAMAGED, new SqlParameter() { Value = item.IsDamaged.ToString() } },
                {CarColumns.TITLE, new SqlParameter() { Value = (object)item.Title ?? DBNull.Value } },
                {CarColumns.DATE_CREATED, new SqlParameter() { Value = item.DateCreated.ToString() } },
                {CarColumns.USER_ID, new SqlParameter() { Value = item.UserID.ToString() } },
                {CarColumns.CARMAKE_ID, new SqlParameter() { Value = item.CarMakeID.ToString() } }
            };
        }

        protected override string GetPrimaryKeyValue(Car item)
        {
            return item.ID.ToString();
        }

        protected override string GetPrimaryKeyName()
        {
            return PRIMARY_KEY;
        }

        protected override string GetTableName()
        {
            return DatabaseTableNames.CARS;
        }

        protected override void MapPrimaryKey(Car item, SqlDataReader reader)
        {
            item.ID = Convert.ToInt32(reader[PRIMARY_KEY]);
        }

        protected override Car MapProperties(SqlDataReader reader)
        {
            Car car = new Car();
            car.ID = Convert.ToInt32(reader[CarColumns.CAR_ID]);
            car.CarModelID = Convert.ToInt32(reader[CarColumns.CARMODEL_ID]);
            car.CarCoupeID = Convert.ToInt32(reader[CarColumns.CARCOUPE_ID]);
            car.NumberOfDoorsID = Convert.ToInt32(reader[CarColumns.NUMBEROFDOORS_ID]);
            car.FuelTypeID = Convert.IsDBNull(reader[CarColumns.FUELTYPE_ID]) ? default(int?) : Convert.ToInt32(reader[CarColumns.FUELTYPE_ID]);
            car.ColourID = Convert.IsDBNull(reader[CarColumns.COLOUR_ID]) ? default(int?) : Convert.ToInt32(reader[CarColumns.COLOUR_ID]);
            car.GearboxTypeID = Convert.IsDBNull(reader[CarColumns.GEARBOXTYPE_ID]) ? default(int?) : Convert.ToInt32(reader[CarColumns.GEARBOXTYPE_ID]);
            car.HorsePower = Convert.ToInt32(reader[CarColumns.HORSEPOWER]);
            car.FirstRegistrationDate = Convert.ToDateTime(reader[CarColumns.FIRST_REGISTRATION_DATE]);
            car.EngineCapacity = Convert.ToInt32(reader[CarColumns.ENGINE_CAPACITY]);
            car.Kilometers = Convert.ToInt32(reader[CarColumns.KILOMETERS]);
            car.Price = Convert.ToDecimal(reader[CarColumns.PRICE]);
            car.Discription = Convert.ToString(reader[CarColumns.DISCRIPTION]);
            car.IsLeftSteering = Convert.ToBoolean(reader[CarColumns.IS_LEFTSTEERING]);
            car.IsUsed = Convert.ToBoolean(reader[CarColumns.IS_USED]);
            car.IsForParts = Convert.ToBoolean(reader[CarColumns.IS_FORPARTS]);
            car.IsDamaged = Convert.ToBoolean(reader[CarColumns.IS_DAMAGED]);
            car.Title = Convert.IsDBNull(reader[CarColumns.TITLE]) ? null : Convert.ToString(reader[CarColumns.TITLE]);
            car.DateCreated = Convert.ToDateTime(reader[CarColumns.DATE_CREATED]);
            car.UserID = Convert.ToInt32(reader[CarColumns.USER_ID]);
            car.CarMakeID = Convert.ToInt32(reader[CarColumns.CARMAKE_ID]);

            return car;
        }

        private Car Map(SqlDataReader reader)
        {
            var car = MapProperties(reader);
            car.GearboxType = new GearBoxType() { Type = Convert.ToString(reader[$"{DatabaseTableNames.GEARBOX_TYPES}.{GearboxTypeColumns.TYPE}"]) };
            car.FuelType = new FuelType() { Name = Convert.ToString(reader[$"{DatabaseTableNames.FUEL_TYPES}.{FuelTypesColumns.NAME}"]) };
            car.Colour = new Colour() { Name = Convert.ToString(reader[$"{DatabaseTableNames.COLOURS}.{ColoursColumns.NAME}"]) };
            car.CarModel = new CarModel() { Name = Convert.ToString(reader[$"{DatabaseTableNames.CAR_MODELS}.{CarModelsColumns.NAME}"]) };
            car.CarMake = new CarMake() { Name = Convert.ToString(reader[$"{DatabaseTableNames.CAR_MAKES}.{CarMakesColumns.NAME}"]) };

            return car;
        }

        public List<Car> GetAll(List<IEntitySearch<Car>> search, List<IEntityOrderBy<Car>> order, Pagination pagination, List<CarIncludes> includes)
        {
            SqlCommand command = new SqlCommand()
            {
                CommandType = CommandType.Text,
                Connection = Connection
            };

            StringBuilder builder = new StringBuilder();
            builder.Append($@"SELECT {DatabaseTableNames.CARS}.{CarColumns.HORSEPOWER}, {DatabaseTableNames.CARS}.{CarColumns.FIRST_REGISTRATION_DATE}, {DatabaseTableNames.CARS}.{CarColumns.KILOMETERS},
                 {DatabaseTableNames.CARS}.{CarColumns.PRICE}, {DatabaseTableNames.CARS}.{CarColumns.DISCRIPTION}, {DatabaseTableNames.CARS}.{CarColumns.TITLE}, {DatabaseTableNames.CARS}.{CarColumns.DATE_CREATED} ,
                 {DatabaseTableNames.CARS}.{CarColumns.CAR_ID},{DatabaseTableNames.CARS}.{CarColumns.CARMODEL_ID}, {DatabaseTableNames.CARS}.{CarColumns.CARMAKE_ID}, {DatabaseTableNames.CARS}.{CarColumns.CARCOUPE_ID},
                 {DatabaseTableNames.CARS}.{CarColumns.NUMBEROFDOORS_ID},{DatabaseTableNames.CARS}.{CarColumns.FUELTYPE_ID}, {DatabaseTableNames.CARS}.{CarColumns.COLOUR_ID}, {DatabaseTableNames.CARS}.{CarColumns.GEARBOXTYPE_ID}, 
                 {DatabaseTableNames.CARS}.{CarColumns.ENGINE_CAPACITY}, {DatabaseTableNames.CARS}.{CarColumns.USER_ID},{DatabaseTableNames.CARS}.{CarColumns.IS_LEFTSTEERING},
                 {DatabaseTableNames.CARS}.{CarColumns.IS_FORPARTS}, {DatabaseTableNames.CARS}.{CarColumns.IS_DAMAGED}, {DatabaseTableNames.CARS}.{CarColumns.IS_USED}");

            var whereClauses = new List<Tuple<string, List<SqlParameter>>>();
            var innerJoins = new HashSet<string>();
            var orderByClauses = new List<IOrderByClause>();

            CreateClauses(whereClauses, innerJoins, orderByClauses, search, order);
            ProcessIncludes(includes, builder, innerJoins);

            builder.Append($" FROM {DatabaseTableNames.CARS}");

            ApplyInnerJoin(innerJoins, builder);
            ApplyWhere(whereClauses, builder, command);
            ApplyOrderBy(builder, orderByClauses);
            ApplyPaging(builder, command, pagination);

            command.CommandText = builder.ToString();

            var resultList = new List<Car>();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    resultList.Add(Map(reader));
                }
            }

            ProcessExtras(resultList, includes);

            return resultList;
        }

        private void ProcessExtras(List<Car> resultList, List<CarIncludes> includes)
        {
            if (includes.Contains(CarIncludes.Extras))
            {
                foreach (var item in resultList)
                {
                    item.Extras = new List<Extra>();
                    SqlCommand command = new SqlCommand()
                    {
                        CommandType = CommandType.Text,
                        Connection = Connection
                    };

                    command.CommandText = $@"SELECT DISTINCT {DatabaseTableNames.CAR_EXTRAS}.{CarExtrasColumns.NAME} 
                                            FROM {DatabaseTableNames.CARS} 
                                            INNER JOIN {DatabaseTableNames.CARS_EXTRAS} 
                                            ON {DatabaseTableNames.CARS}.{CarColumns.CAR_ID} = {DatabaseTableNames.CARS_EXTRAS}.{Cars_CarExtrasColumns.CAR_ID} 
                                            INNER JOIN {DatabaseTableNames.CAR_EXTRAS}  
                                            ON {DatabaseTableNames.CARS_EXTRAS}.{Cars_CarExtrasColumns.CAR_EXTRA_ID} = {DatabaseTableNames.CAR_EXTRAS}.{CarExtrasColumns.CAR_EXTRA_ID} 
                                            WHERE {DatabaseTableNames.CARS}.{CarColumns.CAR_ID} = @param";

                    command.Parameters.AddWithValue("@param", item.ID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            item.Extras.Add(new Extra() { Name = Convert.ToString(reader[$"{CarExtrasColumns.NAME}"]) });
                        }
                    }
                }
            }
        }

        private void ProcessIncludes(List<CarIncludes> includes, StringBuilder builder, HashSet<string> innerJoins)
        {
            if (includes.Count > 0)
            {
                foreach (var item in includes)
                {
                    switch (item)
                    {
                        case CarIncludes.FuelType:
                            builder.Append(", ");
                            builder.Append($"{DatabaseTableNames.FUEL_TYPES}.{FuelTypesColumns.NAME} AS [{DatabaseTableNames.FUEL_TYPES}.{FuelTypesColumns.NAME}]");
                            innerJoins.Add($" INNER JOIN {DatabaseTableNames.FUEL_TYPES} ON {DatabaseTableNames.CARS}.{CarColumns.FUELTYPE_ID} = {DatabaseTableNames.FUEL_TYPES}.{FuelTypesColumns.FUEL_TYPE_ID} ");
                            break;
                        case CarIncludes.GearboxType:
                            builder.Append(", ");
                            builder.Append($"{DatabaseTableNames.GEARBOX_TYPES}.{GearboxTypeColumns.TYPE} AS [{DatabaseTableNames.GEARBOX_TYPES}.{GearboxTypeColumns.TYPE}]");
                            innerJoins.Add($" INNER JOIN {DatabaseTableNames.GEARBOX_TYPES} ON {DatabaseTableNames.CARS}.{CarColumns.GEARBOXTYPE_ID} = {DatabaseTableNames.GEARBOX_TYPES}.{GearboxTypeColumns.TYPE} ");
                            break;
                        case CarIncludes.Colour:
                            builder.Append(", ");
                            builder.Append($"{DatabaseTableNames.COLOURS}.{ColoursColumns.NAME} AS [{DatabaseTableNames.COLOURS}.{ColoursColumns.NAME}]");
                            innerJoins.Add($" INNER JOIN {DatabaseTableNames.COLOURS} ON {DatabaseTableNames.CARS}.{CarColumns.COLOUR_ID} = {DatabaseTableNames.COLOURS}.{ColoursColumns.COLOUR_ID} ");
                            break;
                        case CarIncludes.CarMake:
                            builder.Append(", ");
                            builder.Append($"{DatabaseTableNames.CAR_MAKES}.{CarMakesColumns.NAME} AS [{DatabaseTableNames.CAR_MAKES}.{CarMakesColumns.NAME}]");
                            innerJoins.Add($" INNER JOIN {DatabaseTableNames.CAR_MAKES} ON {DatabaseTableNames.CARS}.{CarColumns.CARMAKE_ID} = {DatabaseTableNames.CAR_MAKES}.{CarMakesColumns.CAR_MAKE_ID} ");
                            break;
                        case CarIncludes.CarModel:
                            builder.Append(", ");
                            builder.Append($"{DatabaseTableNames.CAR_MODELS}.{CarModelsColumns.NAME} AS [{DatabaseTableNames.CAR_MODELS}.{CarModelsColumns.NAME}]");
                            innerJoins.Add($" INNER JOIN {DatabaseTableNames.CAR_MODELS} ON {DatabaseTableNames.CARS}.{CarColumns.CARMODEL_ID} = {DatabaseTableNames.CAR_MODELS}.{CarModelsColumns.CAR_MODEL_ID} ");
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void ApplyInnerJoin(HashSet<string> innerJoins, StringBuilder builder)
        {
            if (innerJoins.Count > 0)
            {
                foreach (var item in innerJoins)
                {
                    builder.Append(item);
                }
            }
        }

        private void ApplyWhere(List<Tuple<string, List<SqlParameter>>> whereClauses, StringBuilder builder, SqlCommand command)
        {
            if (whereClauses.Count > 0)
            {
                builder.Append(" WHERE ");
                for (int i = 0; i < whereClauses.Count; i++)
                {
                    builder.Append(whereClauses[i].Item1);
                    command.Parameters.AddRange(whereClauses[i].Item2.ToArray());

                    if (i < whereClauses.Count - 1)
                    {
                        builder.Append(" AND ");
                    }
                }
            }
        }

        private void ApplyOrderBy(StringBuilder builder, List<IOrderByClause> orderByClauses)
        {
            if (orderByClauses.Count > 0)
            {
                builder.Append(" ORDER BY ");
                for (int i = 0; i < orderByClauses.Count; i++)
                {
                    builder.Append(orderByClauses[i].GenerateOrderByClause());

                    if (i < orderByClauses.Count - 1)
                    {
                        builder.Append(" , ");
                    }
                }
            }
        }

        private void CreateClauses(List<Tuple<string, List<SqlParameter>>> whereClauses, HashSet<string> innerJoins, List<IOrderByClause> orderByClauses, List<IEntitySearch<Car>> search, List<IEntityOrderBy<Car>> order)
        {
            if (search != null)
            {
                foreach (var item in search)
                {
                    var res = WhereFactory.CreateWhereClause(item);
                    var join = res.GenerateJoin();
                    innerJoins.Add(join);
                    whereClauses.Add(res.GenerateWhereClause());
                }
            }

            if (order != null)
            {
                foreach (var item in order)
                {
                    var res = OrderByFactory.CreateOrderByClause(item);
                    var join = res.GenerateInnerJoin();
                    orderByClauses.Add(res);
                    innerJoins.Add(join);
                }
            }
        }

        private void ApplyPaging(StringBuilder builder, SqlCommand command, Pagination pagination)
        {
            if (pagination != null)
            {
                builder.Append(" OFFSET @number ROWS FETCH NEXT @size ROWS ONLY");
                command.Parameters.AddWithValue("@number", pagination.PageNumber * pagination.PageSize);
                command.Parameters.AddWithValue("@size", pagination.PageSize);
            }
        }

        public Car GetById(int id, List<CarIncludes> includes = null)
        {
            var search = new CarIDSearch(id, SearchTypeEnum.Equals);
            var list = new List<IEntitySearch<Car>>() { search };
            return this.GetAll(list, null, null, includes).FirstOrDefault();
        }
    }
}

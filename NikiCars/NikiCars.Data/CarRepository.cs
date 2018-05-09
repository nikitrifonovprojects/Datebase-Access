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
        private const string PRIMARY_KEY = CarColumns.CARID;

        public CarRepository(IConfig config) 
            : base(config)
        {
        }

        protected override Dictionary<string, SqlParameter> GetCreateParameters(Car item)
        {
            return new Dictionary<string, SqlParameter>
            {
                {CarColumns.CARMODELID, new SqlParameter() { Value = item.CarModelID.ToString() } },
                {CarColumns.CARCOUPEID, new SqlParameter() { Value = item.CarCoupeID.ToString() } },
                {CarColumns.NUMBEROFDOORSID, new SqlParameter() { Value = item.NumberOfDoorsID.ToString() } },
                {CarColumns.FUELTYPEID, new SqlParameter() { Value = (object)item.FuelTypeID ?? DBNull.Value} },
                {CarColumns.COLOURID, new SqlParameter() { Value = (object)item.ColourID ?? DBNull.Value } },
                {CarColumns.GEARBOXTYPEID, new SqlParameter() { Value = (object)item.GearboxTypeID ?? DBNull.Value } },
                {CarColumns.HORSEPOWER, new SqlParameter() { Value = item.HorsePower.ToString() } },
                {CarColumns.FIRSTREGISTRATIONDATE, new SqlParameter() { Value = item.FirstRegistrationDate.ToString() } },
                {CarColumns.ENGINECAPACITY, new SqlParameter() { Value = item.EngineCapacity.ToString() } },
                {CarColumns.KILOMETERS, new SqlParameter() { Value = item.Kilometers.ToString() } },
                {CarColumns.PRICE, new SqlParameter() { Value = item.Price.ToString() } },
                {CarColumns.DISCRIPTION, new SqlParameter() { Value = item.Discription } },
                {CarColumns.ISLEFTSTEERING, new SqlParameter() { Value = item.IsLeftSteering.ToString() } },
                {CarColumns.ISUSED, new SqlParameter() { Value = item.IsUsed.ToString() } },
                {CarColumns.ISFORPARTS, new SqlParameter() { Value = item.IsForParts.ToString() } },
                {CarColumns.ISDAMAGED, new SqlParameter() { Value = item.IsDamaged.ToString() } },
                {CarColumns.TITLE, new SqlParameter() { Value = (object)item.Title ?? DBNull.Value } },
                {CarColumns.DATECREATED, new SqlParameter() { Value = item.DateCreated.ToString() } },
                {CarColumns.USERID, new SqlParameter() { Value = item.UserID.ToString() } },
                {CarColumns.CARMAKEID, new SqlParameter() { Value = item.CarMakeID.ToString() } }
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
            car.ID = Convert.ToInt32(reader[CarColumns.CARID]);
            car.CarModelID = Convert.ToInt32(reader[CarColumns.CARMODELID]);
            car.CarCoupeID = Convert.ToInt32(reader[CarColumns.CARCOUPEID]);
            car.NumberOfDoorsID = Convert.ToInt32(reader[CarColumns.NUMBEROFDOORSID]);
            car.FuelTypeID = Convert.IsDBNull(reader[CarColumns.FUELTYPEID]) ? default(int?) : Convert.ToInt32(reader[CarColumns.FUELTYPEID]);
            car.ColourID = Convert.IsDBNull(reader[CarColumns.COLOURID]) ? default(int?) : Convert.ToInt32(reader[CarColumns.COLOURID]);
            car.GearboxTypeID = Convert.IsDBNull(reader[CarColumns.GEARBOXTYPEID]) ? default(int?) : Convert.ToInt32(reader[CarColumns.GEARBOXTYPEID]);
            car.HorsePower = Convert.ToInt32(reader[CarColumns.HORSEPOWER]);
            car.FirstRegistrationDate = Convert.ToDateTime(reader[CarColumns.FIRSTREGISTRATIONDATE]);
            car.EngineCapacity = Convert.ToInt32(reader[CarColumns.ENGINECAPACITY]);
            car.Kilometers = Convert.ToInt32(reader[CarColumns.KILOMETERS]);
            car.Price = Convert.ToDecimal(reader[CarColumns.PRICE]);
            car.Discription = Convert.ToString(reader[CarColumns.DISCRIPTION]);
            car.IsLeftSteering = Convert.ToBoolean(reader[CarColumns.ISLEFTSTEERING]);
            car.IsUsed = Convert.ToBoolean(reader[CarColumns.ISUSED]);
            car.IsForParts = Convert.ToBoolean(reader[CarColumns.ISFORPARTS]);
            car.IsDamaged = Convert.ToBoolean(reader[CarColumns.ISDAMAGED]);
            car.Title = Convert.IsDBNull(reader[CarColumns.TITLE]) ? null : Convert.ToString(reader[CarColumns.TITLE]);
            car.DateCreated = Convert.ToDateTime(reader[CarColumns.DATECREATED]);
            car.UserID = Convert.ToInt32(reader[CarColumns.USERID]);
            car.CarMakeID = Convert.ToInt32(reader[CarColumns.CARMAKEID]);

            return car;
        }

        private Car Map(SqlDataReader reader)
        {
            var car = MapProperties(reader);
            car.GearboxType = new GearBoxType() { Type = Convert.ToString(reader["GearboxTypes.Type"]) };
            car.FuelType = new FuelType() {  Name = Convert.ToString(reader["FuelTypes.Name"]) };
            car.Colour = new Colour() { Name = Convert.ToString(reader["Colours.Name"]) };
            car.CarModel = new CarModel() { Name = Convert.ToString(reader["CarModels.Name"]) };
            car.CarMake = new CarMake() { Name = Convert.ToString(reader["CarMakes.Name"]) };

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
            builder.Append(@"SELECT Cars.HorsePower, Cars.FirstRegistrationDate, Cars.Kilometers, Cars.Price, Cars.Discription, Cars.Title, Cars.DateCreated , Cars.CarID,
                 Cars.CarModelID, Cars.CarMakeID, Cars.CarCoupeID, Cars.NumberOfDoorsID, Cars.FuelTypeID, Cars.ColourID, Cars.GearboxTypeID, Cars.EngineCapacity, Cars.UserID,
                 Cars.IsLeftSteering, Cars.IsForParts, Cars.IsDamaged, Cars.IsUsed");

            var whereClauses = new List<Tuple<string, List<SqlParameter>>>();
            var innerJoins = new HashSet<string>();
            var orderByClauses = new List<IOrderByClause>();

            CreateClauses(whereClauses, innerJoins, orderByClauses, search, order);
            ProcessIncludes(includes, builder, innerJoins);

            builder.Append(" FROM Cars");

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

                    command.CommandText = "SELECT DISTINCT CarExtras.Name FROM Cars INNER JOIN Cars_CarExtras ON Cars.CarID = Cars_CarExtras.CarID INNER JOIN CarExtras ON Cars_CarExtras.CarExtraID = Cars_CarExtras.CarExtraID WHERE Cars.CarID = @param";

                    command.Parameters.AddWithValue("@param", item.ID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            item.Extras.Add(new Extra() { Name = Convert.ToString(reader["Name"]) });
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
                            builder.Append("FuelTypes.Name AS [FuelTypes.Name]");
                            innerJoins.Add(" INNER JOIN FuelTypes ON Cars.FuelTypeID = FuelTypes.FuelTypeID ");
                            break;
                        case CarIncludes.GearboxType:
                            builder.Append(", ");
                            builder.Append("GearboxTypes.Type AS [GearboxTypes.Type]");
                            innerJoins.Add(" INNER JOIN GearboxTypes ON Cars.GearboxTypeID = GearboxTypes.GearboxTypeID ");
                            break;
                        case CarIncludes.Colour:
                            builder.Append(", ");
                            builder.Append("Colours.Name AS [Colours.Name]");
                            innerJoins.Add(" INNER JOIN Colours ON Cars.ColourID = Colours.ColourID ");
                            break;
                        case CarIncludes.CarMake:
                            builder.Append(", ");
                            builder.Append("CarMakes.Name AS [CarMakes.Name]");
                            innerJoins.Add(" INNER JOIN CarMakes ON Cars.CarMakeID = CarMakes.CarMakeID ");
                            break;
                        case CarIncludes.CarModel:
                            builder.Append(", ");
                            builder.Append("CarModels.Name AS [CarModels.Name]");
                            innerJoins.Add(" INNER JOIN CarModels ON Cars.CarModelID = CarModels.CarModelID ");
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

        public Car GetById(int id , List<CarIncludes> includes = null)
        {
            var search = new CarIDSearch(id, SearchTypeEnum.Equals);
            var list = new List<IEntitySearch<Car>>() { search };
            return this.GetAll(list, null, null, includes).FirstOrDefault();
        }
    }
}

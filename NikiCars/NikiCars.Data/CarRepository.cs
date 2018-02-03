using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using NikiCars.Data.Models;

namespace NikiCars.Data
{
    public class CarRepository : BaseRepository<Car>
    {
        private const string PRIMARY_KEY = "CarID";

        protected override Dictionary<string, SqlParameter> GetCreateParameters(Car item)
        {
            return new Dictionary<string, SqlParameter>
            {
                {"CarModelID", new SqlParameter() { Value = item.CarModelID.ToString() } },
                {"CarCoupeID", new SqlParameter() { Value = item.CarCoupeID.ToString() } },
                {"NumberOfDoorsID", new SqlParameter() { Value = item.NumberOfDoorsID.ToString() } },
                {"FuelTypeID", new SqlParameter() { Value = item.FuelTypeID.ToString() } },
                {"ColourID", new SqlParameter() { Value = item.ColourID.ToString() } },
                {"GearboxTypeID", new SqlParameter() { Value = item.GearboxTypeID.ToString() } },
                {"HorsePower", new SqlParameter() { Value = item.HorsePower.ToString() } },
                {"FirstRegistrationDate", new SqlParameter() { Value = item.FirstRegistrationDate.ToString() } },
                {"EngineCapacity", new SqlParameter() { Value = item.EngineCapacity.ToString() } },
                {"Kilometers", new SqlParameter() { Value = item.Kilometers.ToString() } },
                {"Price", new SqlParameter() { Value = item.Price.ToString() } },
                {"Discription", new SqlParameter() { Value = item.Discription } },
                {"IsLeftSteering", new SqlParameter() { Value = item.IsLeftSteering.ToString() } },
                {"IsUsed", new SqlParameter() { Value = item.IsUsed.ToString() } },
                {"IsForParts", new SqlParameter() { Value = item.IsForParts.ToString() } },
                {"IsDamaged", new SqlParameter() { Value = item.IsDamaged.ToString() } },
                {"Title", new SqlParameter() { Value = (object)item.Title ?? DBNull.Value } },
                {"DateCreated", new SqlParameter() { Value = item.DateCreated.ToString() } },
                {"UserID", new SqlParameter() { Value = item.UserID.ToString() } }
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
            return "Cars";
        }

        protected override void MapPrimaryKey(Car item, SqlDataReader reader)
        {
            item.ID = Convert.ToInt32(reader[PRIMARY_KEY]);
        }

        protected override Car MapProperties(SqlDataReader reader)
        {
            Car car = new Car();
            car.ID = Convert.ToInt32(reader["CarID"]);
            car.CarModelID = Convert.ToInt32(reader["CarModelID"]);
            car.CarCoupeID = Convert.ToInt32(reader["CarCoupeID"]);
            car.NumberOfDoorsID = Convert.ToInt32(reader["NumberOfDoorsID"]);
            car.FuelTypeID = Convert.ToInt32(reader["FuelTypeID"]);
            car.ColourID = Convert.ToInt32(reader["ColourID"]);
            car.GearboxTypeID = Convert.ToInt32(reader["GearboxTypeID"]);
            car.HorsePower = Convert.ToInt32(reader["HorsePower"]);
            car.FirstRegistrationDate = Convert.ToDateTime(reader["FirstRegistrationDate"]);
            car.EngineCapacity = Convert.ToInt32(reader["EngineCapacity"]);
            car.Kilometers = Convert.ToInt32(reader["Kilometers"]);
            car.Price = Convert.ToDecimal(reader["Price"]);
            car.Discription = Convert.ToString(reader["Discription"]);
            car.IsLeftSteering = Convert.ToBoolean(reader["IsLeftSteering"]);
            car.IsUsed = Convert.ToBoolean(reader["IsUsed"]);
            car.IsForParts = Convert.ToBoolean(reader["IsForParts"]);
            car.IsDamaged = Convert.ToBoolean(reader["IsDamaged"]);
            car.Title = Convert.ToString(reader["Title"]);
            car.DateCreated = Convert.ToDateTime(reader["DateCreated"]);
            car.UserID = Convert.ToInt32(reader["UserID"]);

            return car;
        }
    }
}

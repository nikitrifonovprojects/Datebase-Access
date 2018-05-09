using System;
using NikiCars.Data.Models;
using NikiCars.Search;
using NikiCars.Search.Interfaces;

namespace NikiCars.Data.Search
{
    internal static class WhereFactory
    {
        public static IWhere CreateWhereClause(ISearch search)
        {
            switch (search)
            {
                case IEntitySearch<Car> c:
                    return CreateCarWhereClause(c);
                case IEntitySearch<CarMake> cm:
                    return CreateCarMakeWhereClause(cm);
                default:
                    throw new NotSupportedException();
            }
        }

        private static IWhere CreateCarMakeWhereClause(IEntitySearch<CarMake> search)
        {
            switch (search)
            {
                case NameSearch r:
                    var nameWhere = new NameWhere(r);
                    return nameWhere;
                case CarMakeIDSearch s:
                    var carMake = new CarMakeIDWhere(s);
                    return carMake;
                default:
                    throw new NotSupportedException();
            }
        }

        private static IWhere CreateCarWhereClause(IEntitySearch<Car> search)
        {
            switch (search)
            {
                case CarMakeIDSearch s:
                    var carMake = new CarMakeIDWhere(s);
                    return carMake;
                case CarEngineSearch s:
                    var carEngine = new CarEngineWhere(s);
                    return carEngine;
                case CarGearboxIDSearch s:
                    var carGearbox = new CarGearboxIDWhere(s);
                    return carGearbox;
                case CarModelIDSearch s:
                    var carModel = new CarModelIDWhere(s);
                    return carModel;
                case CarPriceSearch s:
                    var carPrice = new CarPriceWhere(s);
                    return carPrice;
                case CarYearSearch s:
                    var carYear = new CarYearWhere(s);
                    return carYear;
                case CarExtrasSearch s:
                    var carExtras = new CarExtrasWhere(s);
                    return carExtras;
                case CarConditionSearch s:
                    var carCondition = new CarContitionWhere(s);
                    return carCondition;
                case CarHorsePowerSearch s:
                    var carHorsePower = new CarHorsePowerWhere(s);
                    return carHorsePower;
                case CarKilometersSearch s:
                    var carKilometers = new CarKilometersWhere(s);
                    return carKilometers;
                case CarColourSearch s:
                    var carColour = new CarColourWhere(s);
                    return carColour;
                case CarDateCreatedSearch s:
                    var carLastTwoDays = new CarDateCreatedWhere(s);
                    return carLastTwoDays;
                case CarIDSearch s:
                    var carID = new CarIDWhere(s);
                    return carID;
                default:
                    throw new NotSupportedException();
            }
        }
    }
}

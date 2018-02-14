using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace NikiCars.Data.Revisions
{
    public class Issue_13
    {
        public void Remove()
        {
            using (var conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
            {
                string commandText = "ALTER TABLE Cars DROP CONSTRAINT FK__Cars__FuelTypeID__6383C8BA " +
                                     "ALTER TABLE Cars DROP CONSTRAINT FK__Cars__ColourID__6477ECF3 " +
                                     "ALTER TABLE Cars DROP CONSTRAINT FK__Cars__GearboxTyp__656C112C " +
                                     "ALTER TABLE Cars ALTER COLUMN FuelTypeID int NULL " +
                                     "ALTER TABLE Cars ALTER COLUMN GearboxTypeID int NULL " +
                                     "ALTER TABLE Cars ALTER COLUMN ColourID int NULL";

                SqlCommand command = new SqlCommand(commandText)
                {
                    CommandType = CommandType.Text,
                    Connection = conn
                };

                command.ExecuteNonQuery();
            }
        }

        public void ReverseRemove()
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (var conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                    {
                        string commandText = "DECLARE @fuelType int " +
                                             "DECLARE @gearboxType int " +
                                             "DECLARE @colour int " +
                                             "SELECT TOP 1 @fuelType = f.FuelTypeID FROM FuelTypes f " +
                                             "SELECT TOP 1 @gearboxType = g.GearboxTypeID FROM GearboxTypes g " +
                                             "SELECT TOP 1 @colour = c.ColourID FROM Colours c " +
                                             "UPDATE Cars " +
                                             "SET FuelTypeID = ISNULL(FuelTypeID, @fuelType), " +
                                             "GearboxTypeID = ISNULL(GearboxTypeID, @gearboxType), " +
                                             "ColourID = ISNULL(ColourID, @colour) " +
                                             "ALTER TABLE Cars WITH CHECK ADD CONSTRAINT FK__Cars__FuelTypeID__6383C8BA FOREIGN KEY(FuelTypeID) REFERENCES FuelTypes(FuelTypeID) " +
                                             "ALTER TABLE Cars WITH CHECK ADD CONSTRAINT FK__Cars__ColourID__6477ECF3 FOREIGN KEY(GearboxTypeID) REFERENCES GearboxTypes(GearboxTypeID) " +
                                             "ALTER TABLE Cars WITH CHECK ADD CONSTRAINT FK__Cars__GearboxTyp__656C112C FOREIGN KEY(ColourID) REFERENCES Colours(ColourID) " +
                                             "ALTER TABLE Cars ALTER COLUMN FuelTypeID int NOT NULL " +
                                             "ALTER TABLE Cars ALTER COLUMN GearboxTypeID int NOT NULL " +
                                             "ALTER TABLE Cars ALTER COLUMN ColourID int NOT NULL";

                        SqlCommand command = new SqlCommand(commandText)
                        {
                            CommandType = CommandType.Text,
                            Connection = conn
                        };

                        command.ExecuteNonQuery();
                    }

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Markup;
using Capstone.Exceptions;
using Capstone.Models;
using Capstone.Security;
using Capstone.Security.Models;

namespace Capstone.DAO
{
    public class F1DriverSqlDao : IF1DriverDao
    {
        private readonly string connectionString;

        public F1DriverSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public IList<F1Driver> GetF1Drivers()
        {
            IList<F1Driver> f1Drivers = new List<F1Driver>();

            string sql = "SELECT driver_popularity, driver_id, driver_name, driver_DOB, driver_team, driver_car_no, driver_nationality, driver_worldchampionships, driver_no_of_GP_starts, driver_no_of_GP_podiums, driver_no_of_GP_wins, driver_no_of_DNFs FROM f1_drivers ORDER BY driver_popularity desc;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        F1Driver f1Driver = MapRowToDriver(reader);
                        f1Drivers.Add(f1Driver);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return f1Drivers;
        }

        public IList<F1Driver> GetF1DriverByLikeName(string name)
        {
            List<F1Driver> f1Drivers = new List<F1Driver>();
            string sql = "SELECT driver_popularity, driver_id, driver_name, driver_DOB, driver_team, driver_car_no, driver_nationality, driver_worldchampionships, driver_no_of_GP_starts, driver_no_of_GP_podiums, driver_no_of_GP_wins, driver_no_of_DNFs FROM f1_drivers WHERE driver_name LIKE '%'+@driver_name+'%'";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@driver_name", name);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        F1Driver f1Driver = new F1Driver();
                        f1Driver = MapRowToDriver(reader);
                        f1Drivers.Add(f1Driver);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occured", ex);
            }
            return f1Drivers;
        }

        public F1Driver GetF1DriverById(int driverId)
        {
            F1Driver f1Driver = null;
            string sql = "SELECT driver_popularity, driver_id, driver_name, driver_DOB, driver_team, driver_car_no, driver_nationality, driver_worldchampionships, driver_no_of_GP_starts, driver_no_of_GP_podiums, driver_no_of_GP_wins, driver_no_of_DNFs FROM f1_drivers WHERE driver_id = @driver_id";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@driver_id", driverId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        f1Driver = MapRowToDriver(reader);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occured", ex);
            }
            return f1Driver;
        }

        public F1Driver AddF1Driver(F1Driver f1Driver)
        {
            string sql = "INSERT INTO f1_drivers (driver_popularity, driver_name, driver_DOB, driver_team, driver_car_no, driver_nationality, driver_worldchampionships, driver_no_of_GP_starts, driver_no_of_GP_podiums, driver_no_of_GP_wins, driver_no_of_DNFs) " +
                "OUTPUT INSERTED.driver_id " +
                "VALUES (@driver_popularity, @driver_name, @driver_DOB, @driver_team, @driver_car_no, @driver_nationality, @driver_worldchampionships, @driver_no_of_GP_starts, @driver_no_of_GP_podiums, @driver_no_of_GP_wins, @driver_no_of_DNFs);";
            F1Driver newF1Driver = null;
            int newF1DriverId = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@driver_popularity", f1Driver.DriverPopularity);
                    cmd.Parameters.AddWithValue("@driver_name", f1Driver.Name);
                    cmd.Parameters.AddWithValue("@driver_DOB", f1Driver.DOB);
                    cmd.Parameters.AddWithValue("@driver_team", f1Driver.Team);
                    cmd.Parameters.AddWithValue("@driver_car_no", f1Driver.CarNum);
                    cmd.Parameters.AddWithValue("@driver_nationality", f1Driver.Nationality);
                    cmd.Parameters.AddWithValue("@driver_worldchampionships", f1Driver.WorldChampionships);
                    cmd.Parameters.AddWithValue("@driver_no_of_GP_starts", f1Driver.NumberOfGPStarts);
                    cmd.Parameters.AddWithValue("@driver_no_of_GP_podiums", f1Driver.NumberOfGPPodiums);
                    cmd.Parameters.AddWithValue("@driver_no_of_GP_wins", f1Driver.NumberOfWins);
                    cmd.Parameters.AddWithValue("@driver_no_of_DNFs", f1Driver.NumberOfDNFs);
                    newF1DriverId = Convert.ToInt32(cmd.ExecuteScalar());
                }
                newF1Driver = GetF1DriverById(newF1DriverId);
            }
            catch (SqlException ex)
            {
                throw new Exception("SQL exception occurred", ex);
            }
            return newF1Driver;
        }

        public F1Driver AddOneToDriverPopularity(int driverId)
        {
            string sql = "UPDATE f1_drivers SET driver_popularity = driver_popularity + 1 WHERE driver_id = @driver_id;";
            
            F1Driver f1Driver = new F1Driver();
            f1Driver = GetF1DriverById(driverId);
            f1Driver.DriverPopularity = f1Driver.DriverPopularity + 1;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@driver_id", driverId);

                    int count = cmd.ExecuteNonQuery();
                    if (count == 1)
                    {
                        return f1Driver;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public F1Driver UndoAddOneToDriverPopularity(int driverId)
        {
            string sql = "UPDATE f1_drivers SET driver_popularity = driver_popularity - 1 WHERE driver_id = @driver_id;";

            F1Driver f1Driver = new F1Driver();
            f1Driver = GetF1DriverById(driverId);
            f1Driver.DriverPopularity = f1Driver.DriverPopularity - 1;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@driver_id", driverId);

                    int count = cmd.ExecuteNonQuery();
                    if (count == 1)
                    {
                        return f1Driver;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        private F1Driver MapRowToDriver(SqlDataReader reader)
        {
            F1Driver f1Driver = new F1Driver();

            f1Driver.DriverPopularity = Convert.ToInt32(reader["driver_popularity"]);
            f1Driver.DriverId = Convert.ToInt32(reader["driver_id"]);
            f1Driver.Name = Convert.ToString(reader["driver_name"]);
            f1Driver.DOB = Convert.ToString(reader["driver_DOB"]);
            f1Driver.Team = Convert.ToString(reader["driver_team"]);
            f1Driver.CarNum = Convert.ToInt32(reader["driver_car_no"]);
            f1Driver.Nationality = Convert.ToString(reader["driver_nationality"]);
            f1Driver.WorldChampionships = Convert.ToInt32(reader["driver_worldchampionships"]);
            f1Driver.NumberOfGPStarts = Convert.ToInt32(reader["driver_no_of_GP_starts"]);
            f1Driver.NumberOfGPPodiums = Convert.ToInt32(reader["driver_no_of_GP_podiums"]);
            f1Driver.NumberOfWins = Convert.ToInt32(reader["driver_no_of_GP_wins"]);
            f1Driver.NumberOfDNFs = Convert.ToInt32(reader["driver_no_of_DNFs"]);

            return f1Driver;
        }
    }
}

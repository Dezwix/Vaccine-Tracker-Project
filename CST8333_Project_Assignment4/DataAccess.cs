using Caliburn.Micro;
using CST8333_Project_Assignment4.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CST8333_Project_Assignment4
{
    // Aleksandr Anseev
    /// <summary>
    /// Static DataAccess class, that contains methods for
    /// data procession and data manipulation.
    /// </summary>
    public static class DataAccess
    {
        // connection string
        static string cs = @"server=localhost;userid=root;database=vaccines";

        // prepared statements for record insertion
        static string insertStatement =
            "INSERT INTO vaccines(" +
            "pruid, prename, prfrname, week_end, product_name, numtotal_atleast1dose, numtotal_partially, numtotal_fully, prop_atleast1dose, " +
            "prop_partially, prop_fully, numweekdelta_atleast1dose, numweekdelta_fully, propweekdelta_partially, propweekdelta_fully) " +
            "VALUES(" +
            "@pruid, @prename, @prfrname, @week_end, @product_name, @numtotal_atleast1dose, @numtotal_partially, @numtotal_fully, @prop_atleast1dose, " +
            "@prop_partially, @prop_fully, @numweekdelta_atleast1dose, @numweekdelta_fully, @propweekdelta_partially, @propweekdelta_fully)";

        // Aleksandr Anseev
        /// <summary>
        /// Resets the entire table in DB.
        /// Uses original dataset with over
        /// 3000 rows in it.
        /// </summary>
        public static void Reset()
        {
            using (var con = new MySqlConnection(cs))
            {
                con.Open();

                // this part DROPs and CREATEs a table
                // from scratch, with no preliminary records
                using (var createCommand = new MySqlCommand())
                {
                    createCommand.Connection = con;
                    createCommand.CommandText = "DROP TABLE IF EXISTS vaccines";
                    createCommand.ExecuteNonQuery();

                    createCommand.CommandText =
                        @"CREATE TABLE vaccines(
                        pruid                       TEXT,
                        prename                     TEXT,
                        prfrname                    TEXT,
                        week_end                    TEXT,
                        product_name                TEXT,
                        numtotal_atleast1dose       DOUBLE(10, 2),
                        numtotal_partially          DOUBLE(10, 2),
                        numtotal_fully              DOUBLE(10, 2),
                        prop_atleast1dose           DOUBLE(10, 2),
                        prop_partially              DOUBLE(10, 2),
                        prop_fully                  DOUBLE(10, 2),
                        numweekdelta_atleast1dose   TEXT,
                        numweekdelta_fully          TEXT,
                        propweekdelta_partially     TEXT,
                        propweekdelta_fully         TEXT
                    )";
                    createCommand.ExecuteNonQuery();

                    // this part uses prepared insertStatement
                    // and adds original records to an empty table
                    try
                    {
                        // Creates an instance of StreamReader object to read from a file.
                        // The using statement also closes the StreamReader automatically.
                        using (StreamReader sr = new StreamReader("../../Files/vaccination-coverage-byVaccineType.csv"))
                        {
                            // reads (skips) first line that contains headers
                            string line = sr.ReadLine();

                            // reads file until it's null
                            while ((line = sr.ReadLine()) != null)
                            {
                                // split raw data into cells using
                                // comma as a delimiter
                                string[] row = line.Split(',');

                                using (var insertCommand = new MySqlCommand(insertStatement, con))
                                {
                                    // adds value to prepared statements
                                    insertCommand.Parameters.AddWithValue("@pruid", row[0]);
                                    insertCommand.Parameters.AddWithValue("@prename", row[1]);
                                    insertCommand.Parameters.AddWithValue("@prfrname", row[2]);
                                    insertCommand.Parameters.AddWithValue("@week_end", row[3]);
                                    insertCommand.Parameters.AddWithValue("@product_name", row[4]);
                                    insertCommand.Parameters.AddWithValue("@numtotal_atleast1dose", row[5]);
                                    insertCommand.Parameters.AddWithValue("@numtotal_partially", row[6]);
                                    insertCommand.Parameters.AddWithValue("@numtotal_fully", row[7]);
                                    insertCommand.Parameters.AddWithValue("@prop_atleast1dose", row[8]);
                                    insertCommand.Parameters.AddWithValue("@prop_partially", row[9]);
                                    insertCommand.Parameters.AddWithValue("@prop_fully", row[10]);
                                    insertCommand.Parameters.AddWithValue("@numweekdelta_atleast1dose", row[12]);
                                    insertCommand.Parameters.AddWithValue("@numweekdelta_fully", row[13]);
                                    insertCommand.Parameters.AddWithValue("@propweekdelta_partially", row[14]);
                                    insertCommand.Parameters.AddWithValue("@propweekdelta_fully", row[15]);

                                    // prepares a complete statement with values
                                    insertCommand.Prepare();

                                    // executes completed statement
                                    insertCommand.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                    // Aleksandr Anseev
                    catch (Exception e)
                    {
                        Console.WriteLine("Something went horribly wrong: " + e.Message);
                    }
                }
            }
        }

        /// <summary>
        /// Saves currently displayed table to the DB table.
        /// </summary>
        /// <param name="vaccines">Passed on data from displayed table</param>
        public static void SaveToDatabase(BindableCollection<VaccineModel> vaccines)
        {
            using (var con = new MySqlConnection(cs))
            {
                con.Open();
                using (var saveCommand = new MySqlCommand())
                {
                    saveCommand.Connection = con;
                    saveCommand.CommandText = "DELETE FROM vaccines";
                    saveCommand.ExecuteNonQuery();

                    foreach (VaccineModel vaccineRow in vaccines)
                    {
                        using (var insertCommand = new MySqlCommand(insertStatement, con))
                        {
                            // adds value to prepared statements
                            insertCommand.Parameters.AddWithValue("@pruid", vaccineRow.Pruid);
                            insertCommand.Parameters.AddWithValue("@prename", vaccineRow.Prename);
                            insertCommand.Parameters.AddWithValue("@prfrname", vaccineRow.Prfrname);
                            insertCommand.Parameters.AddWithValue("@week_end", vaccineRow.Week_end);
                            insertCommand.Parameters.AddWithValue("@product_name", vaccineRow.Product_name);
                            insertCommand.Parameters.AddWithValue("@numtotal_atleast1dose", vaccineRow.Numtotal_atleast1dose);
                            insertCommand.Parameters.AddWithValue("@numtotal_partially", vaccineRow.Numtotal_partially);
                            insertCommand.Parameters.AddWithValue("@numtotal_fully", vaccineRow.Numtotal_fully);
                            insertCommand.Parameters.AddWithValue("@prop_atleast1dose", vaccineRow.Prop_atleast1dose);
                            insertCommand.Parameters.AddWithValue("@prop_partially", vaccineRow.Prop_partially);
                            insertCommand.Parameters.AddWithValue("@prop_fully", vaccineRow.Prop_fully);
                            insertCommand.Parameters.AddWithValue("@numweekdelta_atleast1dose", vaccineRow.Numweekdelta_atleast1dose);
                            insertCommand.Parameters.AddWithValue("@numweekdelta_fully", vaccineRow.Numweekdelta_fully);
                            insertCommand.Parameters.AddWithValue("@propweekdelta_partially", vaccineRow.Propweekdelta_partially);
                            insertCommand.Parameters.AddWithValue("@propweekdelta_fully", vaccineRow.Propweekdelta_fully);

                            // prepares a complete statement with values
                            insertCommand.Prepare();

                            // executes completed statement
                            insertCommand.ExecuteNonQuery();
                        }
                    }
                }
            }
            // Aleksandr Anseev
        }

        /// <summary>
        /// Method that gets all rows from the database.
        /// If currently displayed table is not saved
        /// anywhere, then it gets overwritten by 
        /// data pulled from DB table.
        /// </summary>
        /// <returns>Returns a collection of VaccineModel objects that's used
        /// to fill visual table (DataGrid) with pulled data from the DB table.</returns>
        public static BindableCollection<VaccineModel> GetVaccines()
        {
            // Object that will be filled with data
            // and returned at the end.
            BindableCollection<VaccineModel> vaccines = new BindableCollection<VaccineModel>();

            using (var con = new MySqlConnection(cs))
            {
                con.Open();
                string retrieveStatement = "SELECT * FROM vaccines";
                using (var retrieveCommand = new MySqlCommand(retrieveStatement, con))
                {
                    try
                    {
                        using (MySqlDataReader reader = retrieveCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                VaccineModel vaccineRow = new VaccineModel();

                                // checks if passed data is null to avoid NullPointerException
                                // being thrown
                                if (!reader.IsDBNull(0)) { vaccineRow.Pruid = reader.GetString(0); }
                                if (!reader.IsDBNull(1)) { vaccineRow.Prename = reader.GetString(1); }
                                if (!reader.IsDBNull(2)) { vaccineRow.Prfrname = reader.GetString(2); }
                                if (!reader.IsDBNull(3)) { vaccineRow.Week_end = reader.GetString(3); }
                                if (!reader.IsDBNull(4)) { vaccineRow.Product_name = reader.GetString(4); }
                                if (!reader.IsDBNull(5)) { vaccineRow.Numtotal_atleast1dose = reader.GetString(5); }
                                if (!reader.IsDBNull(6)) { vaccineRow.Numtotal_partially = reader.GetString(6); }
                                if (!reader.IsDBNull(7)) { vaccineRow.Numtotal_fully = reader.GetString(7); }
                                if (!reader.IsDBNull(8)) { vaccineRow.Prop_atleast1dose = reader.GetString(8); }
                                if (!reader.IsDBNull(9)) { vaccineRow.Prop_partially = reader.GetString(9); }
                                if (!reader.IsDBNull(10)) { vaccineRow.Prop_fully = reader.GetString(10); }
                                if (!reader.IsDBNull(11)) { vaccineRow.Numweekdelta_atleast1dose = reader.GetString(11); }
                                if (!reader.IsDBNull(12)) { vaccineRow.Numweekdelta_fully = reader.GetString(12); }
                                if (!reader.IsDBNull(13)) { vaccineRow.Propweekdelta_partially = reader.GetString(13); }
                                if (!reader.IsDBNull(14)) { vaccineRow.Propweekdelta_fully = reader.GetString(14); }
                                vaccines.Add(vaccineRow);
                            }
                        }
                    }
                    catch (MySqlException e)
                    {
                        Console.WriteLine(e);
                        Console.WriteLine("Resetting database to default");
                        Reset();
                    }
                }
            } // Aleksandr Anseev
            // returns a collection of VaccineModel objects
            return vaccines;
        }

        // Calculates percentage of the given province
        public static List<string> ProvinceNames()
        {
            // calculate percentage for each given province
            // use ProvinceModel province's Title property to go through DB
            List<string> provinceNames = new List<string>();
            using (var con = new MySqlConnection(cs))
            {
                con.Open();
                string retrieveStatement = "SELECT DISTINCT prename FROM vaccines ORDER BY prename ASC";
                using (var retrieveCommand = new MySqlCommand(retrieveStatement, con))
                {
                    try
                    {
                        using (MySqlDataReader reader = retrieveCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                provinceNames.Add(reader.GetString(0));
                            }
                        }
                    }
                    catch (MySqlException e)
                    {
                        Console.WriteLine(e);
                        Console.WriteLine("Resetting database to default");
                        Reset();
                    }
                }
            }
            return provinceNames;
        }

        public static double CountProvince(string province, string doses)
        {
            double maxCount = 0;
            List<double> queryResult = new List<double>();
            using (var con = new MySqlConnection(cs))
            {
                con.Open();
                string retrieveStatement = "SELECT " + doses + " FROM vaccines WHERE prename LIKE \"" + province + "\"";
                using (var retrieveCommand = new MySqlCommand(retrieveStatement, con))
                {
                    try
                    {
                        using (MySqlDataReader reader = retrieveCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (!reader.GetString(0).Equals("") && !reader.GetString(0).Equals("0") && !reader.GetString(0).Equals("na")) 
                                { 
                                    queryResult.Add(reader.GetDouble(0));
                                }
                            }
                        }
                    }
                    catch (MySqlException e)
                    {
                        Console.WriteLine(e);
                        Console.WriteLine("Resetting database to default");
                        Reset();
                    }
                }
            }

            foreach (double count in queryResult)
            {
                if (count > maxCount)
                {
                    maxCount = count;
                }
            }
            return maxCount;
        }
    }
}

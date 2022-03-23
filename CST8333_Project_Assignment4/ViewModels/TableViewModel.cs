using Caliburn.Micro;
using CST8333_Project_Assignment4.Models;
using CST8333_Project_Assignment4.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CST8333_Project_Assignment4.ViewModels
{
    // Aleksandr Anseev
    /// <summary>
    /// This is a Shell ViewModel class.
    /// It has all logic for the Shell View.
    /// </summary>
    public class TableViewModel : Conductor<object>
    {
        /// <summary>
        /// This variable is binded to a DataGrid element.
        /// </summary>
        private DataGrid _vaccineTable;

        /// <summary>
        /// This variable is a collection, that is holding
        /// all rows sorted to columns arrocdingly and is used
        /// for filling up DataGrid and to save .csv files
        /// or to load from them
        /// </summary>
        private BindableCollection<VaccineModel> _vaccines = new BindableCollection<VaccineModel>();

        /// <summary>
        /// Constructor, that runs whenever Shell View is initialized.
        /// By default, it gives Vaccines 100 records from original .csv file
        /// and since Vaccine is binded to DataGrid via x:Name="Vaccines", 
        /// DataGrid is being filled with data from Vaccines.
        /// </summary>
        public TableViewModel()
        {
            /// <summary> Getting all records from the DB table</summary>

            // In case database table doesn't exist, GetVaccines will throw MySqlException.
            // It will be caught and database table will be reset to the default state
            // In that case, nothing will be returned and GetVaccines will need to be called
            // again. To avoid empty table at the start, GetVaccines is called in case
            // table doesn't exist (which is most likely on a different PC)
            // IMPORTANT: it does not solve absence of schema, it only solves absence of table
            // within schema. Schema still needs to be created manually beforehand 
            // (More about environment set up in the attached word document)
            DataAccess.GetVaccines();
            Vaccines = new BindableCollection<VaccineModel>(DataAccess.GetVaccines());
        }

        /// <summary>
        /// Getters and setters for _vaccines variable.
        /// Notifies VaccineTable and Vaccines (itself)
        /// of changes whenever something is changed in 
        /// the dataset.
        /// </summary>
        public BindableCollection<VaccineModel> Vaccines
        {
            get { return _vaccines; }
            set
            {
                _vaccines = value;
                NotifyOfPropertyChange(() => VaccineTable);
                NotifyOfPropertyChange(() => Vaccines);
            }
        }

        /// <summary>
        /// Getters and setters for _vaccineTable variable.
        /// 
        /// </summary>
        public DataGrid VaccineTable
        {
            get { return _vaccineTable; }
            set
            {
                _vaccineTable = value;
            }
        }

        /// <summary>
        /// Method that loads all records from
        /// the original .csv file and puts it in the
        /// Vaccines variable to display it in the table.
        /// </summary>
        public void LoadAll()
        {
            Vaccines = new BindableCollection<VaccineModel>(DataAccess.GetVaccines());
            MessageBoxResult result = MessageBox.Show("Loaded from the database successfully!");
        }


        public void SaveTableToDatabase()
        {
            DataAccess.SaveToDatabase(Vaccines);
            MessageBoxResult result = MessageBox.Show("Saved to the database successfully!");
        }


        public void ResetToDefault()
        {
            // Define what buttons MessageBox will have.
            MessageBoxButton buttons = MessageBoxButton.YesNo;

            // Show a message box with confirmation option
            // and store the result.
            MessageBoxResult result1 = MessageBox.Show(
                "You are about to reset everything to default. " +
                "Are you sure you wish to continue?", "Reset to default", buttons);

            // if clicked "Yes" in the MessageBox, then execute
            // reset method
            if (result1 == MessageBoxResult.Yes)
            {
                // reset
                DataAccess.Reset();

                // let the user know that table was reset
                MessageBoxResult result2 = MessageBox.Show("Reset to default successfully!");

                // updates ui table with default records
                LoadAll();
            }


        }
    }
}

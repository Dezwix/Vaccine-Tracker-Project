using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CST8333_Project_Assignment4.ViewModels
{
    // Aleksandr Anseev
    // Main View Model that wraps all views together
    class MainViewModel : Conductor<object>
    {
        IWindowManager manager = new WindowManager();

        // Constructor for the main view model
        public MainViewModel()
        {
            // Loads TableView at the very start,
            // Otherwise reserved space for it
            // stays empty
            LoadPageTable();
        }

        // Activates TableViewModel, thus shows TableView
        public void LoadPageTable()
        {
            ActivateItemAsync(new TableViewModel());
        }

        // Activates PieViewMode, thus shows PieView
        public void LoadPagePie()
        {
            manager.ShowWindowAsync(new PieViewModel());
        }
    }
}

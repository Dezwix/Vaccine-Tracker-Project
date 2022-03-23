using Caliburn.Micro;
using CST8333_Project_Assignment4.Models;
using CST8333_Project_Assignment4.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CST8333_Project_Assignment4.ViewModels
{
    // Aleksandr Anseev

    // Pie View Model that contains pie chart
    // and hard-coded categoris (provinces)
    // with four buttons
    class PieViewModel : Conductor<object>
    {
        private double _x1;
        private double _x2;
        private double _y1;
        private double _y2;
        private Canvas _mainCanvas;

        // Creates an ObservacleCollection with lines in it
        // Whenever this collection is changed, referenced
        // element in the view is being notified about the change
        public PieViewModel()
        {
            Lines = new ObservableCollection<LineModel>();
        }

        public ObservableCollection<LineModel> Lines { get; set; }

        // Binds X1 variable to the view
        public double X1
        {
            get { return _x1; }
            set 
            { 
                _x1 = value;
                NotifyOfPropertyChange(() => X1);
            }
        }

        // Binds X2 variable to the view
        public double X2
        {
            get { return _x2; }
            set 
            { 
                _x2 = value;
                NotifyOfPropertyChange(() => X2);
            }
        }

        // Binds Y1 variable to the view
        public double Y1
        {
            get { return _y1; }
            set 
            { 
                _y1 = value;
                NotifyOfPropertyChange(() => Y1);
            }
        }

        // Binds Y2 variable to the view
        public double Y2
        {
            get { return _y2; }
            set 
            { 
                _y2 = value;
                NotifyOfPropertyChange(() => Y2);
            }
        }

        // // Binds MainCanvas variable to the view
        public Canvas MainCanvas
        {
            get { return _mainCanvas; }
            set 
            { 
                _mainCanvas = value;
                NotifyOfPropertyChange(() => MainCanvas);
            }
        }

        // Is binded to the "One dose" button
        // Loads provinces that have records
        // for numtotal_atleast1dose column
        // and renders pie
        public void OneDose()
        {
            List<string> provinceNames = DataAccess.ProvinceNames();
            List<double> provincesCount = new List<double>();
            List<double> provincesPercentage = new List<double>();

            string doses = "numtotal_atleast1dose";
            double totalCount = 0, provinceCount = 0;
            foreach (string provinceName in provinceNames)
            {
                provinceCount = DataAccess.CountProvince(provinceName, doses);
                if (!provinceName.Equals("Canada"))
                {
                    totalCount += provinceCount;
                    provincesCount.Add(provinceCount);
                }
            }
            foreach(string provinceName in provinceNames)
            {
                if (!provinceName.Equals("Canada"))
                {
                    provincesPercentage.Add(Math.Round(DataAccess.CountProvince(provinceName, doses) / totalCount * 100, 2));
                }
            }

            provinceNames.Remove("Canada");
            RenderPie(provinceNames, provincesPercentage);
        }

        // Is binded to the "One dose" button
        // Loads provinces that have records
        // for numtotal_partial column
        // and renders pie
        public void PartialDose()
        {
            List<string> provinceNames = DataAccess.ProvinceNames();
            List<double> provincesCount = new List<double>();
            List<double> provincesPercentage = new List<double>();

            string doses = "numtotal_partially";
            double totalCount = 0, provinceCount = 0;
            foreach (string provinceName in provinceNames)
            {
                provinceCount = DataAccess.CountProvince(provinceName, doses);
                if (!provinceName.Equals("Canada"))
                {
                    totalCount += provinceCount;
                    provincesCount.Add(provinceCount);
                }
            }
            foreach (string provinceName in provinceNames)
            {
                if (!provinceName.Equals("Canada"))
                {
                    provincesPercentage.Add(Math.Round(DataAccess.CountProvince(provinceName, doses) / totalCount * 100, 2));
                }
            }
            RenderPie(provinceNames, provincesPercentage);
        }

        // Is binded to the "Full Dose" button
        // Loads provinces that have records
        // for numtotal_fully column
        // and renders pie
        public void FullDose()
        {
            List<string> provinceNames = DataAccess.ProvinceNames();
            List<double> provincesCount = new List<double>();
            List<double> provincesPercentage = new List<double>();

            string doses = "numtotal_fully";
            double totalCount = 0, provinceCount = 0;
            foreach (string provinceName in provinceNames)
            {
                provinceCount = DataAccess.CountProvince(provinceName, doses);
                if (!provinceName.Equals("Canada"))
                {
                    totalCount += provinceCount;
                    provincesCount.Add(provinceCount);
                }
            }
            foreach (string provinceName in provinceNames)
            {
                if (!provinceName.Equals("Canada"))
                {
                    provincesPercentage.Add(Math.Round(DataAccess.CountProvince(provinceName, doses) / totalCount * 100, 2));
                }
            }
            RenderPie(provinceNames, provincesPercentage);
        }

        public void RenderPie(List<string> names, List<double> percentages)
        {
            double radius = 120;
            double centerX = 125;
            double centerY = 125;
            double radians = 0;
            double X2;
            double Y2;

            string[] Colors = new string[] { "Orange", "LightBlue", "Red", "Green", "Yellow", "Purple", 
                "DarkBlue", "DarkGreen", "Brown", "LightGreen", "Pink", "Blue", "LightGray" };
            List<double> angleMilestones = new List<double>();
            double prevMilestone = 0;

            foreach(string name in names)
            {
                Console.WriteLine(name);
            }
            
            foreach(double percentage in percentages)
            {
                angleMilestones.Add(percentage * 3.6 + prevMilestone);
                prevMilestone = percentage * 3.6 + prevMilestone;
            }

            int milestoneIndex = 0;
            double angleDelta = 0.5;
            for(double angle = 0; angle <= 360; angle += angleDelta)
            {
                radians = angle * Math.PI / 180;
                X2 = 125 + (Math.Cos(radians) * radius);
                Y2 = 125 + (Math.Sin(radians) * radius);
                LineModel newLine = new LineModel { X1 = centerX, Y1 = centerY, X2 = X2, Y2 = Y2 };


                if(angle < angleMilestones[milestoneIndex])
                {
                    newLine.Color = Colors[milestoneIndex];
                    Lines.Add(newLine);
                }
                else
                {
                    milestoneIndex++;
                }
            }
        }

        // Closes a pie
        public void ClosePie()
        {
            (GetView() as Window).Close();
        }
    }
}

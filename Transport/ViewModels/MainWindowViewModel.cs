using CsvHelper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Transport.Enums;
using Transport.Infrastructure;
using Transport.Interface;
using Transport.Models;
using Transport.ViewModels.Base;
using Transport.Views.Windows;

namespace Transport.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        #region ScreenWidth

        private double _screenWidth = SystemParameters.PrimaryScreenWidth;

        public double ScreenWidth
        {
            get => _screenWidth;
        }

        #endregion

        #region ScreenHeight

        private double _screenHeight = SystemParameters.PrimaryScreenHeight - 20;

        public double ScreenHeight
        {
            get => _screenHeight;
        }

        #endregion

        #region CanvasFilling
        private ObservableCollection<RectangleProps> _canvasFilling = RoadsCarsControlService.CanvasFiling;

        public ObservableCollection<RectangleProps> CanvasFilling
        {
            get => _canvasFilling;
        }
        #endregion

        #region SliderValue

        private int _sliderValue = 0;

        public int SliderValue
        {
            get => _sliderValue;
            set
            {
                _sliderValue = value;

                RoadsCarsControlService.FillCanvas(value);
            }
        }

        #endregion

        #region Commands

        #region
        public ICommand OpenWindowCommand { get; }

        private bool CanOpenWindowCommandExecute(object p) => true;

        private void OnOpenWindowCommandExecuted(object p)
        {
            if (p is not Windows windows) return;

            switch(windows)
            {
                case Windows.RoadsControl:
                    new RoadsControlWindow().Show();

                break;
            }
           
        }

        #endregion

        #region
        public ICommand GetCsvCommand { get; }

        private bool CanGetCsvCommandExecute(object p) => true;

        private void OnGetCsvCommandExecuted(object p)
        {
            var list = new List<Csv>();
            int i = 1;
            foreach (var magazine in RoadsCarsControlService.TransportMagazine)
            {
                var fuelCount = "";
                foreach (var fuel in magazine.Value.RemainingFuel)
                {
                    fuelCount += fuel.ToString();
                    fuelCount += ", ";
                }

                var isWorking = "";
                foreach (var a in magazine.Value.IsWorking)
                {
                    isWorking += a.ToString();
                    isWorking += ", ";
                }

                list.Add(new Csv() { 
                    Time = i,
                    FuelCount = fuelCount,
                    IsWorking = isWorking
                });

                i++;
            }

            using (var writer = new StreamWriter("csv"))

            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(list);
            }
        }

        #endregion

        #region
        public ICommand StartCommand { get; }

        private bool CanStartCommandExecute(object p) 
        {
            if (RoadsCarsControlService.IsStart)
                return false;

            return true;
        }

        private void OnStartCommandExecuted(object p)
        {
            RoadsCarsControlService.Start();
        }

        #endregion

        #endregion

        public MainWindowViewModel()
        {
            RoadsCarsControlService.ScreenWidth = ScreenWidth;
            RoadsCarsControlService.ScreenHeight = ScreenHeight;

            OpenWindowCommand = new LambdaCommand(OnOpenWindowCommandExecuted, CanOpenWindowCommandExecute);
            StartCommand = new LambdaCommand(OnStartCommandExecuted, CanStartCommandExecute);
            GetCsvCommand = new LambdaCommand(OnGetCsvCommandExecuted, CanGetCsvCommandExecute);
        }
    }
}

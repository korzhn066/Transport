using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Transport.Enums;
using Transport.Models;
using Transport.ViewModels.Base;
using Transport.Views.Windows;
using Transport.Infrastructure;
using Transport.Helpers;
using Transport.Models.Roads;
using Transport.Models.Transport;

namespace Transport.ViewModels
{
    internal class RoadsControlViewModel : ViewModelBase
    {
        #region ComboBoxFiling

        private List<string> _comboBoxFiling = EnumHelper.GetEnumTypes(typeof(RoadEnum));

        public List<string> ComboBoxFiling
        {
            get => _comboBoxFiling;
        }

        #endregion

        #region TransportTypes

        private List<string> _transportTypes = EnumHelper.GetEnumTypes(typeof(TransportEnum));

        public List<string> TransportTypes
        {
            get => _transportTypes;
        }

        #endregion

        #region RoadsList

        private ObservableCollection<string> _roadsList = RoadsCarsControlService.RoadsList;

        public ObservableCollection<string> RoadsList
        {
            get => _roadsList;
        }

        #endregion

        #region RoadsListSelectedValue

        private string _roadsListSelectedValue = RoadEnum.Highway.ToString();

        public string RoadsListSelectedValue
        {
            get => _roadsListSelectedValue;
            set => _roadsListSelectedValue = value;
        }

        #endregion

        #region SelectedTransportType

        private string _selectedTransportType = TransportEnum.Bus.ToString();

        public string SelectedTransportType
        {
            get => _selectedTransportType;
            set => _selectedTransportType = value;
        }

        #endregion

        #region EnteredAcceleration

        private string _enteredacceleration = "0";

        public string EnteredAcceleration
        {
            get => _enteredacceleration;
            set => _enteredacceleration = value;
        }

        #endregion

        #region EnteredStartSpeed

        private string _enteredStartSpeed = "0";

        public string EnteredStartSpeed
        {
            get => _enteredStartSpeed;
            set => _enteredStartSpeed = value;
        }

        #endregion

        #region EnteredMaxSpeed

        private string _enteredMaxSpeed = "0";

        public string EnteredMaxSpeed
        {
            get => _enteredMaxSpeed;
            set => _enteredMaxSpeed = value;
        }

        #endregion

        #region EnteredFuelConsumption

        private string _enteredFuelConsumption = "0";

        public string EnteredFuelConsumption
        {
            get => _enteredFuelConsumption;
            set => _enteredFuelConsumption = value;
        }

        #endregion

        #region EnteredFuelCount

        private string _enteredFuelCount = "0";

        public string EnteredFuelCount
        {
            get => _enteredFuelCount;
            set => _enteredFuelCount = value;
        }

        #endregion

        #region EnteredRoadIndex

        private string _enteredRoadIndex = "0";

        public string EnteredRoadIndex
        {
            get => _enteredRoadIndex;
            set => _enteredRoadIndex = value;
        }

        #endregion

        #region Commands

        #region AddRoadCommand
        public ICommand AddRoadCommand { get; }

        private bool CanAddRoadCommandExecute(object p) => true;

        private void OnAddRoadCommandExecuted(object p)
        {
            if (RoadsListSelectedValue == RoadEnum.Highway.ToString())
                RoadsCarsControlService.AddRoad(new Highway());
            else if (RoadsListSelectedValue == RoadEnum.Sidewalk.ToString())
                RoadsCarsControlService.AddRoad(new Sidewalk());
            else if (RoadsListSelectedValue == RoadEnum.Tramrails.ToString())
                RoadsCarsControlService.AddRoad(new Tramrails());
        }

        #endregion

        #region AddTransportCommand
        public ICommand AddTransportCommand { get; }

        private bool CanAddTransportCommandExecute(object p) => true;

        private void OnAddTransportCommandExecuted(object p)
        {
            bool result = int.TryParse(EnteredRoadIndex, out int roadIndex);
            if (!result)
            {
                MessageBox.Show("Invalid format of road index");
                return;
            }

            result = double.TryParse(EnteredAcceleration, out double acceleration);
            if (!result)
            {
                MessageBox.Show("Invalid format of acceleration");
                return;
            }

            result = double.TryParse(EnteredStartSpeed, out double startSpeed);
            if (!result)
            {
                MessageBox.Show("Invalid format of start speed");
                return;
            }

            result = double.TryParse(EnteredMaxSpeed, out double maxSpeed);
            if (!result)
            {
                MessageBox.Show("Invalid format of max speed");
                return;
            }

            result = double.TryParse(EnteredFuelCount, out double fuelCount);
            if (!result)
            {
                MessageBox.Show("Invalid format of fuel count");
                return;
            }

            result = double.TryParse(EnteredFuelConsumption, out double fuelConsumption);
            if (!result)
            {
                MessageBox.Show("Invalid format of fuel consumption");
                return;
            }

            Response response = new Response() {
                Result = false,
                Message = "Unexpected error"
            };

            if (SelectedTransportType == TransportEnum.Bus.ToString())
            {
                var bus = new Bus();
                bus.SetParameters(startSpeed, acceleration, maxSpeed, fuelCount, fuelConsumption);

                response = RoadsCarsControlService.AddTransport(bus, roadIndex);
            }
            else if (SelectedTransportType == TransportEnum.Tram.ToString())
            {
                var tram = new Tram();
                tram.SetParameters(startSpeed, acceleration, maxSpeed, fuelCount, fuelConsumption);

                response = RoadsCarsControlService.AddTransport(tram, roadIndex);
            }
            else if (SelectedTransportType == TransportEnum.Truck.ToString())
            {
                var truck = new Truck();
                truck.SetParameters(startSpeed, acceleration, maxSpeed, fuelCount, fuelConsumption);

                response = RoadsCarsControlService.AddTransport(truck, roadIndex);
            }
            else if (SelectedTransportType == TransportEnum.Bike.ToString())
            {
                var bike = new Bike();
                bike.SetParameters(startSpeed, acceleration, maxSpeed);
                
                response = RoadsCarsControlService.AddTransport(bike,roadIndex);
            }

            if (!response.Result)
                MessageBox.Show(response.Message);

        }

        #endregion
        #endregion

        public RoadsControlViewModel()
        {
            AddRoadCommand = new LambdaCommand(OnAddRoadCommandExecuted, CanAddRoadCommandExecute);
            AddTransportCommand = new LambdaCommand(OnAddTransportCommandExecuted, CanAddTransportCommandExecute);
        }
    }
}

using System.Collections.Generic;
using System.Text;
using System;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private readonly string      r_LicenseNumber;
        private readonly List<Wheel> r_WheelsList;
        private string               m_ModelName;
        private float                m_CurrentEnergyPercent;
        private EnergySource         m_Engine;

        public Vehicle(string i_LicenseNumber, int i_NumberOfWheels)
        {
            r_LicenseNumber = i_LicenseNumber;
            r_WheelsList = new List<Wheel>(i_NumberOfWheels);
        }

        public string LicenseNumber
        {
            get { return r_LicenseNumber; }
        }

        public string Model
        {
            get { return m_ModelName; }
            set { m_ModelName = value; }
        }

        public float CurrentEnergyPercent
        {
            get { return m_CurrentEnergyPercent; }
        }

        public EnergySource Engine
        {
            get { return m_Engine; }
        }

        public List<Wheel> WheelsList
        {
            get { return r_WheelsList; }
        }

        public void InitializeWheels(float i_MaxAirPressure)
        {
            for (int i = 0; i < r_WheelsList.Capacity; i++)
            {
                r_WheelsList.Add(new Wheel(i_MaxAirPressure));
            }
        }

        public void FillWheelsInfo(string i_ManufacturerName, float i_CurrentAirPressure)
        {
            foreach(Wheel wheel in r_WheelsList)
            {
                wheel.InflatingWheel(i_CurrentAirPressure);
                wheel.Manufacturer = i_ManufacturerName;
            }
        }

        public void InflatingWheelsToMax()
        {
            foreach (Wheel wheel in r_WheelsList)
            {
                wheel.InflatingWheel(wheel.MissingAirPressureToMax());
            }
        }

        public string GetWheelsDetails()
        {
            StringBuilder details = new StringBuilder("Wheels info:");
            string        currentWheel;

            foreach (Wheel wheel in WheelsList)
            {
                details.Append(Environment.NewLine);
                currentWheel = string.Format(
@"Manufacturer: {0}, Current air pressure: {1}",
wheel.Manufacturer,
wheel.CurrentAirPressure.ToString());
                details.Append(currentWheel);
            }

            return details.ToString();
        }

        public string GetEngineDetails()
        {
            StringBuilder details = new StringBuilder("Engine info: ");
            string        specificDetails;
            Fuel          fuelEngine = m_Engine as Fuel;

            details.Append(Environment.NewLine);
            if (fuelEngine != null)
            {
                specificDetails = fuelEngine.GetFuelDetails();
                details.Append(specificDetails);
            }
            else if (m_Engine is Electric)
            {
                specificDetails = ((Electric)m_Engine).GetElectricDetails();
                details.Append(specificDetails);
            }

            return details.ToString();
        }

        public void UpdateCurrentEnergyPercent(float i_MaxFuelAmount, float i_MaxBatteryTime)
        {
            Fuel engineFuel = Engine as Fuel;

            if (engineFuel != null)
            {
                m_CurrentEnergyPercent = (engineFuel.CurrentFuelAmount / i_MaxFuelAmount) * 100;
            }
            else
            {
                m_CurrentEnergyPercent = (((Electric)Engine).CurrentBatteryTime / i_MaxBatteryTime) * 100;
            }
        }

        public void AddEnergy(float i_EnergyToAdd)
        {
            Engine.AddEnergy(i_EnergyToAdd);
        }

        protected void AllocateEngine(EnergySource.eEnergyType i_EnergySource)
        {
            if (i_EnergySource == EnergySource.eEnergyType.Electric)
            {
                m_Engine = new Electric();
            }
            else
            {
                m_Engine = new Fuel();
            }
        }
        
        public abstract void CreateEngineAndWheels(EnergySource.eEnergyType i_EnergySource);

        public abstract string GetSpecificDetails();

        public abstract void UpdateEnergyPercent();

        public abstract void FillDetails(SpecificDetailsForm i_DetailsForm);
    }
}
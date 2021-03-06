﻿namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        internal const float k_MaxFuelAmount = 115f;
        internal const float k_MaxAirPressure = 28f;
        internal const int   k_NumberOfWheels = 12;
        internal const float k_NoneBattery = 0;
        private bool         m_IsCooledBaggage;
        private float        m_BaggageCapacity;

        public Truck(string i_LicenseNumber) : base(i_LicenseNumber, k_NumberOfWheels)
        {
        }

        public override void CreateEngineAndWheels(EnergySource.eEnergyType i_EnergySource)
        {
            AllocateEngine(i_EnergySource);
            InitializeWheels(k_MaxAirPressure);
            Engine.UpdateMaxEnergyAmount(k_MaxFuelAmount);
            ((Fuel)Engine).FuelType = Fuel.eFuelType.Soler;
        }

        public override void FillDetails(SpecificDetailsForm i_DetailsForm)
        {
            m_IsCooledBaggage = i_DetailsForm.IsCooledBaggage;
            m_BaggageCapacity = i_DetailsForm.BaggageCapacity;
            UpdateEnergyPercent();
        }

        public override string GetSpecificDetails()
        {
            string isCoolBaggage = m_IsCooledBaggage == true ? string.Empty : "NOT ";
            string message = string.Format(
@"The Baggage capacity is: {0}
The baggage is {1}cooled",
m_BaggageCapacity.ToString(),
isCoolBaggage);

            return message;
        }

        public override void UpdateEnergyPercent()
        {
            UpdateCurrentEnergyPercent(k_MaxFuelAmount, k_NoneBattery);
        }
    }
}
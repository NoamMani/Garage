﻿using System;

namespace Ex03.GarageLogic
{
    public class OwnerDetails
    {
        public enum eVehicleStatus
        {
            InProcess = 1,
            Fixed,
            Paid,
        }

        private string         m_OwnerName;
        private string         m_OwnerPhoneNumber;
        private eVehicleStatus m_VehicleStatus = eVehicleStatus.InProcess;

        public static string[] GetStatusOptions()
        {
            return Enum.GetNames(typeof(eVehicleStatus));
        }

        public OwnerDetails(string i_OwnerName, string i_OwnerPhone)
        {
            m_OwnerName = i_OwnerName;
            m_OwnerPhoneNumber = i_OwnerPhone;
        }

        public string OwnerName
        {
            get { return m_OwnerName; }
        }

        public string OwnerPhoneNumber
        {
            get { return m_OwnerPhoneNumber; }
        }

        public eVehicleStatus VehicleStatus
        {
            get { return m_VehicleStatus; }
            set { m_VehicleStatus = value; }
        }
    }
}
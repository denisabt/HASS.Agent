﻿using System;
using System.Diagnostics;
using System.Globalization;
using HASS.Agent.Shared.Functions;
using HASS.Agent.Shared.Models.HomeAssistant;

namespace HASS.Agent.Shared.HomeAssistant.Sensors
{
    /// <summary>
    /// Sensor containing the current value of the provided performance counter
    /// </summary>
    public class PerformanceCounterSensor : AbstractSingleValueSensor
    {
        protected PerformanceCounter Counter = null;

        public string CategoryName;
        public string CounterName;
        public string InstanceName;

        public bool NeedRound { get; private set; }
        public int? Round { get; private set; }

        public PerformanceCounterSensor(string categoryName, string counterName, string instanceName, bool needRound = false, int? round = null, int? updateInterval = null, string name = "performancecountersensor", string id = default) : base(name ?? "performancecountersensor", updateInterval ?? 10, id)
        {
            CategoryName = categoryName;
            CounterName = counterName;
            InstanceName = instanceName;
            NeedRound = needRound;
            Round = round;

            Counter = PerformanceCounters.GetSingleInstanceCounter(categoryName, counterName);
            if (Counter == null) throw new Exception("PerformanceCounter not found");

            Counter.InstanceName = instanceName;

            Counter.NextValue();
        }

        public void Dispose() => Counter?.Dispose();

        public override DiscoveryConfigModel GetAutoDiscoveryConfig()
        {
            if (Variables.MqttManager == null) return null;

            var deviceConfig = Variables.MqttManager.GetDeviceConfigModel();
            if (deviceConfig == null) return null;

            return AutoDiscoveryConfigModel ?? SetAutoDiscoveryConfigModel(new SensorDiscoveryConfigModel
            {
                Name = Name,
                Unique_id = Id,
                Device = deviceConfig,
                State_topic = $"{Variables.MqttManager.MqttDiscoveryPrefix()}/{Domain}/{deviceConfig.Name}/{Name}/state",
                Availability_topic = $"{Variables.MqttManager.MqttDiscoveryPrefix()}/{Domain}/{deviceConfig.Name}/availability"
            });
        }
        
        public override string GetState() => Math.Round(Counter.NextValue()).ToString(CultureInfo.InvariantCulture);

        public override string GetAttributes() => string.Empty;
    }
}

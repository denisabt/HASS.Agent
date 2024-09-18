﻿using HASS.Agent.Shared.Enums;
using HASS.Agent.Shared.Managers;
using HASS.Agent.Shared.Managers.Audio;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace HASS.Agent.Shared.HomeAssistant.Commands.InternalCommands
{
    public class SetAudioOutputCommand : InternalCommand
    {
        private const string DefaultName = "setaudiooutput";

        private string OutputDevice { get => CommandConfig; }

        public SetAudioOutputCommand(string entityName = DefaultName, string name = DefaultName, string audioDevice = "", CommandEntityType entityType = CommandEntityType.Button, string id = default) : base(entityName ?? DefaultName, name ?? null, audioDevice, entityType, id)
        {
            State = "OFF";
        }

        public override void TurnOn()
        {
            if (string.IsNullOrWhiteSpace(OutputDevice))
            {
                Log.Error("[SETAUDIOOUT] Error, output device name cannot be null/blank");

                return;
            }

            TurnOnWithAction(OutputDevice);
        }

        public override void TurnOnWithAction(string action)
        {
            State = "ON";
 
            try
            {
                AudioManager.ActivateDevice(action);
            }
            catch (Exception ex)
            {
                Log.Error("[SETAUDIOOUT] Error while processing action '{action}': {err}", action, ex.Message);
            }
            finally
            {
                State = "OFF";
            }
        }
    }
}

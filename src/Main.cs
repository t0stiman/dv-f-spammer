using System;
using UnityEngine;
using UnityModManagerNet;
using WindowsInput;
using WindowsInput.Native;

namespace dv_f_spammer
{
    [EnableReloading]
    static class Main
    {
        public static bool enabled;

        private static InputSimulator inputSim;
        private static DateTime previousKeyPressTime = new DateTime(0);

        //================================================================

        private static bool Load(UnityModManager.ModEntry modEntry)
        {
            modEntry.OnToggle = OnToggle;
            modEntry.OnUpdate = OnUpdate;

            modEntry.Logger.Log("loaded");
            
            inputSim = new InputSimulator();

            return true;
        }

        private static bool OnToggle(UnityModManager.ModEntry modEntry, bool value) 
        {
            enabled = value;
            string msg = enabled ? "hello!" : "goodbye!";
            modEntry.Logger.Log(msg);

            return true;
        }

        private static void OnUpdate(UnityModManager.ModEntry modEntry, float idk)
        {
            bool getKey = Input.GetKey(KeyCode.F) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));
            if (!getKey)
            {
                return;
            }

            if ((DateTime.UtcNow - previousKeyPressTime).TotalMilliseconds < 50)
            {
                return;
            }

            inputSim.Keyboard.KeyPress(VirtualKeyCode.VK_F);
            previousKeyPressTime = DateTime.UtcNow;
        }
    }
}
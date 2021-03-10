/************************************************************************************
 【PXR SDK】
 Copyright 2015-2020 Pico Technology Co., Ltd. All Rights Reserved.

************************************************************************************/

using System;
using UnityEngine;
using UnityEngine.XR;

namespace Unity.XR.PXR
{
    public static class PXR_Input
    {
        public enum ControllerDevice
        {
            G2 = 3,
            Neo2,
            Neo3,
            NewController = 10
        }

        public enum Controller
        {
            LeftController,
            RightController,
        }

        /// <summary>
        /// Get the current master control controller
        /// </summary>
        /// <returns></returns>
        public static Controller GetDominantHand()
        {
            return (Controller)PXR_Plugin.Controller.UPxr_GetMainController();
        }

        /// <summary>
        /// Set the current master control controller
        /// </summary>
        public static void SetDominantHand(Controller controller)
        {
            PXR_Plugin.Controller.UPxr_SetMainController((int)controller);
        }

        /// <summary>
        /// Set the controller vibrate 
        /// </summary>
        public static void SetControllerVibration(float strength, int time, Controller controller)
        {
            PXR_Plugin.Controller.UPxr_VibrateController(strength, time, (int)controller);
        }

        /// <summary>
        /// Get the controller device
        /// </summary>
        /// <returns></returns>
        public static ControllerDevice GetActiveController()
        {
            return (ControllerDevice)PXR_Plugin.Controller.UPxr_GetControllerType();
        }

        /// <summary>
        /// Get the connection status of Controller
        /// </summary>
        public static bool IsControllerConnected(Controller controller)
        {
            var state = false;
            switch (controller)
            {
                case Controller.LeftController:
                    InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(PXR_Usages.controllerStatus, out state);
                    return state;
                case Controller.RightController:
                    InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(PXR_Usages.controllerStatus, out state);
                    return state;
            }
            return state;
        }
    }
}


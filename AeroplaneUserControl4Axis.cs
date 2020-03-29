using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Aeroplane
{
    [RequireComponent(typeof (AeroplaneController))]
    public class AeroplaneUserControl4Axis : MonoBehaviour
    {
        // reference to the aeroplane that we're controlling
        private AeroplaneController m_Aeroplane;
        private float m_Throttle;
        private bool m_AirBrakes;


        private void Awake()
        {
            // Set up the reference to the aeroplane controller.
            m_Aeroplane = GetComponent<AeroplaneController>();
        }


        private void FixedUpdate()
        {
            // Read input for the pitch, yaw, roll and throttle of the aeroplane.
            float roll = CrossPlatformInputManager.GetAxis("Horizontal") * Time.deltaTime;
            float pitch = CrossPlatformInputManager.GetAxis("Vertical") * Time.deltaTime;
            m_Throttle = CrossPlatformInputManager.GetAxis("Mouse Y") * Time.deltaTime;
            m_AirBrakes = CrossPlatformInputManager.GetButton("Fire1");

            // Pass the input to the aeroplane
            m_Aeroplane.Move(roll, pitch, m_Throttle, m_AirBrakes);
        }
    }
}

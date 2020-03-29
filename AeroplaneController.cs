using System;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Aeroplane
{
    [RequireComponent(typeof (Rigidbody))]
    public class AeroplaneController : MonoBehaviour
    {
        [SerializeField] private float m_MaxEnginePower = 40f;        // The maximum output of the engine.
        [SerializeField] private float m_RollEffect = 50f;             // The strength of effect for roll input.
        [SerializeField] private float m_PitchEffect = 50f;            // The strength of effect for pitch input.
        [SerializeField] private float m_ThrottleChangeSpeed = 0.3f;  // The speed with which the throttle changes.

        public float Throttle { get; private set; }                     // The amount of throttle being used.
        public bool AirBrakes { get; private set; }                     // Whether or not the air brakes are being applied.
        public float EnginePower { get; private set; }                  // How much power the engine is being given.
        public float RollInput { get; private set; }
        public float PitchInput { get; private set; }
        public float ThrottleInput { get; private set; }
        private Rigidbody m_Rigidbody;
        public float TerminalSpeed;
        public float Speed;

        // Used by other scripts
        public float RollAngle;
        public float PitchAngle;
        private void Start()
        {
            m_Rigidbody = GetComponent<Rigidbody>();

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
        }


        public void Move(float rollInput, float pitchInput, float throttleInput, bool airBrakes)
        {
            // transfer input parameters into properties.s
            RollInput = rollInput;
            PitchInput = pitchInput;
            ThrottleInput = throttleInput;
            AirBrakes = airBrakes;

            ClampInputs();
            ControlThrottle();
            CalculateRollAndPitchAngles();
            CalculateTorque();
            CalculateLinearForces();
        }


        private void ClampInputs()
        {
            // clamp the inputs to -1 to 1 range
            RollInput = Mathf.Clamp(RollInput, -1, 1);
            PitchInput = Mathf.Clamp(PitchInput, -1, 1);
            ThrottleInput = Mathf.Clamp(ThrottleInput, -1, 1);
        }

        private void CalculateRollAndPitchAngles()
        {
            // Calculate roll & pitch angles
            // Calculate the flat forward direction (with no y component).
            var flatForward = transform.forward;
            flatForward.y = 0;
            // If the flat forward vector is non-zero (which would only happen if the plane was pointing exactly straight upwards)
            if (flatForward.sqrMagnitude > 0)
            {
                flatForward.Normalize();
                // calculate current pitch angle
                var localFlatForward = transform.InverseTransformDirection(flatForward);
                PitchAngle = Mathf.Atan2(localFlatForward.y, localFlatForward.z);
                // calculate current roll angle
                var flatRight = Vector3.Cross(Vector3.up, flatForward);
                var localFlatRight = transform.InverseTransformDirection(flatRight);
                RollAngle = Mathf.Atan2(localFlatRight.y, localFlatRight.x);
            }
        }

        private void ControlThrottle()
        {
            // Adjust throttle based on throttle input (or immobilized state)
            Throttle = Mathf.Clamp01(Throttle + ThrottleInput*Time.deltaTime*m_ThrottleChangeSpeed);
            Speed = m_Rigidbody.velocity.magnitude;
            EnginePower = Speed < TerminalSpeed ? Throttle*m_MaxEnginePower : 0;
        }

        private void CalculateLinearForces()
        {
            // Now calculate forces acting on the aeroplane:
            // we accumulate forces into this variable:
            var forces = Vector3.zero;
            // Add the engine power in the forward direction
            forces += EnginePower*transform.forward;
            // var heading = transform.forward;
            // heading.Normalize();
            // transform.forward += heading;


            // Apply the calculated forces to the the Rigidbody
            m_Rigidbody.AddForce(forces);
        }

        private void CalculateTorque()
        {
            // We accumulate torque forces into this variable:
            var torque = Vector3.zero;

            // Add torque for the pitch based on the pitch input.
            torque += PitchInput * m_PitchEffect * transform.right;

            // Add torque for the roll based on the roll input.
            torque += -RollInput * m_RollEffect * transform.forward;

            // The total torque is multiplied by the forward speed, so the controls have more effect at high speed,
            // and little effect at low speed, or when not moving in the direction of the nose of the plane
            // (i.e. falling while stalled)
            m_Rigidbody.AddTorque(torque);
        }
    }
}

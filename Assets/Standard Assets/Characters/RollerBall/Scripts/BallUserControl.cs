using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Ball
{
    public class BallUserControl : MonoBehaviour
    {
        private Ball ball; // Reference to the ball controller.

        private Vector3 move;
        // the world-relative desired move direction, calculated from the camForward and user input.
        public bool player1;
        private Transform cam; // A reference to the main camera in the scenes transform
        private Vector3 camForward; // The current forward direction of the camera
        private bool jump; // whether the jump button is currently pressed
        public Camera extCam;

        private void Awake()
        {
            // Set up the reference.
            ball = GetComponent<Ball>();


            // get the transform of the main camera
            if (Camera.main != null)
            {
                cam = extCam.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Ball needs a Camera tagged \"MainCamera\", for camera-relative controls.");
                // we use world-relative controls in this case, which may not be what the user wants, but hey, we warned them!
            }
        }


        private void Update()
        {
            float h = 0;
            float v = 0;
            // Get the axis and jump input.

            if (player1)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    v = 1;
                }
                else if(Input.GetKey(KeyCode.S))
                {
                    v = -1;
                }
                
                if (Input.GetKey(KeyCode.D))
                {
                    h = 1;
                    
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    h = -1;
                }
                if (Input.GetKey(KeyCode.Q))
                {
                    jump = true;
                }

            }
            else
            {
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    v = 1;
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    v = -1;
                }

                if (Input.GetKey(KeyCode.RightArrow))
                {
                    h = 1;

                }
                else if (Input.GetKey(KeyCode.LeftArrow))
                {
                    h = -1;
                }
                if (Input.GetKey(KeyCode.Space))
                {
                    jump = true;
                }
            }

           
            
            // calculate move direction
            if (cam != null)
            {
                // calculate camera relative direction to move:
                camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
                move = (v*camForward + h*cam.right).normalized;
            }
            else
            {
                // we use world-relative directions in the case of no main camera
                move = (v*Vector3.forward + h*Vector3.right).normalized;
            }
        }


        private void FixedUpdate()
        {
            // Call the Move function of the ball controller
            ball.Move(move, jump);
            jump = false;
        }
    }
}

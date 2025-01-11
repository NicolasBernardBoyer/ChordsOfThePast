    using System;
    using UnityEngine;
    using UnityEngine.InputSystem;

    public class GearsGameManager : MonoBehaviour
    {
        public InputSystem_Actions inputControls;
        public GameObject Gear;
        private float F1Xpos = -3f;
        private float F2Xpos = -1f;
        private float F3Xpos = 1f;
        private float F4Xpos = 3f;
        public float intensity = 5f;

        private bool moveToF1 = false;
        private bool F2pressed = false;
        private bool F3pressed = false;
        private bool moveToF4 = false;

        private Vector3 cursor = new Vector3(-3.0f, -3.0f, 0);

        //create a new input action for each different key
        private InputAction playKey;
        private InputAction stopKey;
        private InputAction slider;
        private InputAction leftKey;
        private InputAction rightKey;

        private void Start()
        {
            Gear = Instantiate(Gear, new Vector3(-3.0f, -3.0f, 0), Quaternion.identity);
        }
        private void Update()
        {
            Gear.transform.position = Vector3.MoveTowards(Gear.transform.position, cursor, 1.0f);
        }

        private void Awake()
        {
            //initialize the input system
            inputControls = new InputSystem_Actions();
        }

        private void OnEnable()
        {
            playKey = inputControls.GearsPuzzle.Play;
            playKey.Enable();
            //register to the event
            playKey.performed += PlayKey;

            stopKey = inputControls.GearsPuzzle.Stop;
            stopKey.Enable();
            //register to the event
            stopKey.performed += StopKey;

            //enable the command (input system.map.command)
            slider = inputControls.GearsPuzzle.Column1;
            slider.Enable();
            //register to the event
            slider.performed += Slider1;
        }

        private void OnDisable()
        {
            playKey.Disable();
            stopKey.Disable();
            slider.Disable();

        }

        //the event I used to register it 
        public void PlayKey(InputAction.CallbackContext context)
        {

            Debug.Log("Pressed the play key");
        }
        public void StopKey(InputAction.CallbackContext context)
        {
            Debug.Log("Pressed the stop key");
        }

        public void Slider(InputAction.CallbackContext context)
        {
            Debug.Log(context.ReadValue<float>());
            if (Gear.transform.position.x == F1Xpos)
            {
                Gear.transform.position = new Vector3(Gear.transform.position.x, -3 + context.ReadValue<float>() * intensity, Gear.transform.position.z);
            }
            //Debug.Log("Slider1 being used");
        }
    }
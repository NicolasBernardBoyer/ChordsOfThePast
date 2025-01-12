using UnityEngine;
using UnityEngine.InputSystem;

public class GearsGameManager : MonoBehaviour
{
    public InputSystem_Actions inputControls;

    //create a new input action for each different key
    private InputAction playKey;
    private InputAction stopKey;
    private InputAction slider1;
    private InputAction slider2;
    private InputAction slider3;
    private InputAction slider4;
    private InputAction leftKey;
    private InputAction rightKey;


    private void Awake()
    {
<<<<<<< Updated upstream
        //initialize the input system
        inputControls = new InputSystem_Actions();
=======
<<<<<<< HEAD
        public InputSystem_Actions inputControls;
        public GameObject Gear;
        public GameObject SmallGear;
        public GameObject MediumGear;
        public GameObject LargeGear;
        public float intensity = 5f;
        public float sliderIntensity = 7f;
        public float keyboardIntensity = 0.0001f;
        public float cursorMax = 3.0f;
        public float cursorMin = -3.0f;

        private Vector3 cursor = new Vector3(-3.0f, -3.0f, 0);

        //create a new input action for each different key
        private InputAction playKey;
        private InputAction stopKey;
        private InputAction slider;
        private InputAction leftKey;
        private InputAction rightKey;

        private void Start()
        {
            Gear = Instantiate(SmallGear, cursor, Quaternion.identity);
        }
        private void Update()
        {
            Gear.transform.position = Vector3.MoveTowards(Gear.transform.position, cursor, 1.0f);

        if (Input.GetKey(KeyCode.W) && cursor.y < 4.0f)
            {
                cursor = new Vector3(cursor.x, cursor.y + 0.05f, cursor.z);
            }
        if (Input.GetKey(KeyCode.S) && cursor.y > -4.0f)
            {
                cursor = new Vector3(cursor.x, cursor.y - 0.05f, cursor.z);
            }
        }
   
        
        public void PlaceGear()
        {
            Gear = Instantiate(MediumGear, cursor, Quaternion.identity);
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
            slider = inputControls.GearsPuzzle.Slider;
            slider.Enable();
            //register to the event
            slider.performed += Slider;

            rightKey = inputControls.GearsPuzzle.Right;
            rightKey.Enable();
            //register to the event
            rightKey.performed += RightKey;

            leftKey = inputControls.GearsPuzzle.Left;
            leftKey.Enable();
            //register to the event
            leftKey.performed += LeftKey;
=======
        //initialize the input system
        inputControls = new InputSystem_Actions();
>>>>>>> 3ebebbfa50573bb1b42b01f5b8bc0869d8f18728
>>>>>>> Stashed changes
    }

    private void OnEnable()
    {
        //playKey = inputControls.GearsPuzzle.playKey;

        //enable the command (input system.map.command)
        slider1 = inputControls.GearsPuzzle.Column1;
        slider1.Enable();
        //register to the event
        slider1.performed += Slider1;

<<<<<<< Updated upstream
        slider2 = inputControls.GearsPuzzle.Column2;
        slider2.Enable();
        slider2.performed += Slider2;
=======
<<<<<<< HEAD
            Debug.Log("Pressed the play key");
        }
        public void StopKey(InputAction.CallbackContext context)
        {
            Debug.Log("Pressed the stop key");
            PlaceGear();
            
        }
=======
        slider2 = inputControls.GearsPuzzle.Column2;
        slider2.Enable();
        slider2.performed += Slider2;
>>>>>>> 3ebebbfa50573bb1b42b01f5b8bc0869d8f18728
>>>>>>> Stashed changes

        slider3 = inputControls.GearsPuzzle.Column3;
        slider3.Enable();
        slider3.performed += Slider3;

        slider4 = inputControls.GearsPuzzle.Column4;
        slider4.Enable();
        slider4.performed += Slider4;
    }

    private void OnDisable()
    {
        slider1.Disable();
    }

    //the event I used to register it 
    public void PlayKey(InputAction.CallbackContext context)
    {
        Debug.Log("Pressed the key");
    }

    public void Slider1(InputAction.CallbackContext context)
    {
        Debug.Log("Slider1 being used");
    }

    public void Slider2(InputAction.CallbackContext context)
    {
        Debug.Log("Slider2 being used");
    }

    public void Slider3(InputAction.CallbackContext context)
    {
        Debug.Log("Slider3 being used");
    }

    public void Slider4(InputAction.CallbackContext context)
    {
        Debug.Log("Slider4 being used");
    }


<<<<<<< Updated upstream

}
=======
<<<<<<< HEAD
    public void Slider(InputAction.CallbackContext context)
        {   
            Debug.Log(context.ReadValue<float>());
            cursor = new Vector3(cursor.x, -3 + context.ReadValue<float>() * intensity, cursor.z);
            //Debug.Log("Slider1 being used");
        }
    }
=======

}
>>>>>>> 3ebebbfa50573bb1b42b01f5b8bc0869d8f18728
>>>>>>> Stashed changes

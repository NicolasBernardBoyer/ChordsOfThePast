using UnityEngine;
using UnityEngine.InputSystem;

public class GearsGameManager : MonoBehaviour
{
    public InputSystem_Actions inputControls;
    public GameObject Gear;
    public float intensity = 5f;
    public float sliderIntensity = 5f;
    public float keyboardIntensity = 2.5f;
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
        Gear = Instantiate(Gear, cursor, Quaternion.identity);
    }
    private void Update()
    {
        Gear.transform.position = Vector3.MoveTowards(Gear.transform.position, cursor, 1.0f);
    }

    public void PlaceGear()
    {
        Gear = Instantiate(Gear, cursor, Quaternion.identity);
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
        Debug.Log("Slider1 being used");
    }

    private void OnDisable()
    {
        playKey.Disable();
        stopKey.Disable();
        slider.Disable();
        leftKey.Disable();
        rightKey.Disable();
    }

    //the event I used to register it 
    public void PlayKey(InputAction.CallbackContext context)
    {

        Debug.Log("Pressed the play key");
    }
    public void StopKey(InputAction.CallbackContext context)
    {
        Debug.Log("Pressed the stop key");
        PlaceGear();
    }

    public void RightKey(InputAction.CallbackContext context)
    {
        Debug.Log("Pressed the right key");
        if (cursor.x >= cursorMax) cursor.x = cursorMin;
        else cursor.x += 2;
    }

    public void LeftKey(InputAction.CallbackContext context)
    {
        Debug.Log("Pressed the left key");
        if (cursor.x <= cursorMin) cursor.x = cursorMax;
        else cursor.x -= 2;
    }


    public void Slider(InputAction.CallbackContext context)
    {
        Debug.Log(context.ReadValue<float>());
        if (context.control.device is Keyboard)
        {
            intensity = keyboardIntensity;
        }
        else
        {
            intensity = sliderIntensity;
        }
        cursor = new Vector3(cursor.x, -3 + context.ReadValue<float>() * intensity, cursor.z);
        //Debug.Log("Slider1 being used");
    }   
}
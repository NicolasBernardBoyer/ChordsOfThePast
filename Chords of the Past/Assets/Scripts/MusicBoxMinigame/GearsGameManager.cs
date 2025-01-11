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

    //create a new input action for each different key
    private InputAction playKey;
    private InputAction stopKey;
    private InputAction slider1;
    private InputAction slider2;
    private InputAction slider3;
    private InputAction slider4;
    private InputAction F1;
    private InputAction F2;
    private InputAction F3;
    private InputAction F4;

    private void Start()
    {
        Gear = Instantiate(Gear, new Vector3(-3.0f, -3.0f, 0), Quaternion.identity);
    }
    private void Update()
    {
        /*if (moveToF4)
        {
            Debug.Log("Moving");
            Vector3.MoveTowards(Gear.transform.position, new Vector3(F4Xpos, Gear.transform.position.y, Gear.transform.position.z), 1.0f);
        }*/
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
        slider1 = inputControls.GearsPuzzle.Column1;
        slider1.Enable();
        //register to the event
        slider1.performed += Slider1;

        slider2 = inputControls.GearsPuzzle.Column2;
        slider2.Enable();
        slider2.performed += Slider2;

        slider3 = inputControls.GearsPuzzle.Column3;
        slider3.Enable();
        slider3.performed += Slider3;

        slider4 = inputControls.GearsPuzzle.Column4;
        slider4.Enable();
        slider4.performed += Slider4;

        F1 = inputControls.GearsPuzzle.F1;
        F1.Enable();
        F1.performed += F1Pressed;

        F2 = inputControls.GearsPuzzle.F2;
        F2.Enable();
        F2.performed += F2Pressed;

        F3 = inputControls.GearsPuzzle.F3;
        F3.Enable();
        F3.performed += F3Pressed;

        F4 = inputControls.GearsPuzzle.F4;
        F4.Enable();
        F4.performed += F4Pressed;
    }

    private void OnDisable()
    {
        playKey.Disable();
        stopKey.Disable();
        slider1.Disable();
        slider2.Disable();
        slider3.Disable();
        slider4.Disable();
        F1.Disable();
        F2.Disable();
        F3.Disable();
        F4.Disable();
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

    public void Slider1(InputAction.CallbackContext context)
    {
        Debug.Log(context.ReadValue<float>());
        if (Gear.transform.position.x == F1Xpos)
        {
            Gear.transform.position = new Vector3(Gear.transform.position.x, -3 + context.ReadValue<float>() * intensity, Gear.transform.position.z);
        }
        //Debug.Log("Slider1 being used");
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
    public void F1Pressed(InputAction.CallbackContext context)
    {
        Gear.transform.position = new Vector3(F1Xpos, Gear.transform.position.y, Gear.transform.position.z);
        Debug.Log("Pressed the F1 key");
    }
    public void F2Pressed(InputAction.CallbackContext context)
    {
        Gear.transform.position = new Vector3(F2Xpos, Gear.transform.position.y, Gear.transform.position.z);
        Debug.Log("Pressed the F2 key");
    }
    public void F3Pressed(InputAction.CallbackContext context)
    {
        Gear.transform.position = new Vector3(F3Xpos, Gear.transform.position.y, Gear.transform.position.z);
        Debug.Log("Pressed the F3 key");
    }
    public void F4Pressed(InputAction.CallbackContext context)
    {
        Gear.transform.position = new Vector3(F4Xpos, Gear.transform.position.y, Gear.transform.position.z);
        Debug.Log("Pressed the F4 key");
    }
}
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
        //initialize the input system
        inputControls = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        //enable the command (input system.map.command)
        slider1 = inputControls.GearsPuzzle.Column1;
        slider1.Enable();
        //register to the event
        slider1.performed += Slider1;
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



}
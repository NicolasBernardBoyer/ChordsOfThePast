using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public InputSystem_Actions inputControls;

    //create a new input action for each different key
    private InputAction playKey;

    private void Awake()
    {
        //initialize the input system
        inputControls = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        //enable the command (input system.map.command)
        playKey = inputControls.Player.Try;
        playKey.Enable();
        //register to the event
        playKey.performed += PlayKey; 
    }

    private void OnDisable()
    {
        playKey.Disable();
    }

    //the event I used to register it 
    public void PlayKey(InputAction.CallbackContext context)
    {
        Debug.Log("Pressed the key");
    }
}

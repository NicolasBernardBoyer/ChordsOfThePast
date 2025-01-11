using UnityEngine;
using UnityEngine.InputSystem; //allows us to use MIDI keyboard :3

public class ChooseHarpLevelManager : MonoBehaviour
{
    public InputSystem_Actions inputControls;

    //creates a different input action for each of the keys LMAO

    private InputAction firstHarpAction;
    private InputAction secondHarpAction;
    private InputAction thirdHarpAction;
    private InputAction fourthHarpAction;

    private void Awake() //happens before Start()
    { 
        //"wakes up" the input system
        inputControls = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        //enables the commaned (input system map)
        firstHarpAction = inputControls.ChooseHarp.ChooseFirstHarp;
        secondHarpAction = inputControls.ChooseHarp.ChooseSecondHarp;
        thirdHarpAction = inputControls.ChooseHarp.ChooseThirdHarp;
        fourthHarpAction = inputControls.ChooseHarp.ChooseFourthHarp;
        //enable all input maps to make them work
        firstHarpAction.Enable();
        secondHarpAction.Enable();
        thirdHarpAction.Enable();
        fourthHarpAction.Enable();
    }

    private void OnDisable()
    {
        firstHarpAction.Disable();
        secondHarpAction.Disable();
        thirdHarpAction.Disable();
        fourthHarpAction.Disable();
    }




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

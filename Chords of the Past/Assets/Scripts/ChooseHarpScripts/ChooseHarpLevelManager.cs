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
    private InputAction playSongAction;

    private void Awake() //happens before Start()
    { 
        //"wakes up" the input system
        inputControls = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        //enables the command (input system map)
        firstHarpAction = inputControls.ChooseHarp.ChooseFirstHarp;
        secondHarpAction = inputControls.ChooseHarp.ChooseSecondHarp;
        thirdHarpAction = inputControls.ChooseHarp.ChooseThirdHarp;
        fourthHarpAction = inputControls.ChooseHarp.ChooseFourthHarp;
        playSongAction = inputControls.ChooseHarp.PlaySong;

        //enable all input maps to make them work
        firstHarpAction.Enable();
        secondHarpAction.Enable();
        thirdHarpAction.Enable();
        fourthHarpAction.Enable();
        playSongAction.Enable();

        firstHarpAction.performed += SelectFirstHarp;
        secondHarpAction.performed += SelectSecondHarp;
        thirdHarpAction.performed += SelectThirdHarp;
        fourthHarpAction.performed += SelectFourthHarp;
        playSongAction.performed += PlaySong;
    }

    private void OnDisable()
    {
        firstHarpAction.Disable();
        secondHarpAction.Disable();
        thirdHarpAction.Disable();
        fourthHarpAction.Disable();
    }

    [SerializeField] GameObject firstHarp;
    [SerializeField] GameObject secondHarp;
    [SerializeField] GameObject thirdHarp;
    [SerializeField] GameObject fourthHarp; //VS is god-tier

    public void FadeOut(GameObject chosenHarp)
    {
        chosenHarp.SetActive(false);
    }


    public void SelectFirstHarp(InputAction.CallbackContext context)
    {
        Debug.Log("Harp 1");
        firstHarp.transform.position = new Vector3(0, 0, 0);
        FadeOut(secondHarp);
        FadeOut(thirdHarp);
        FadeOut(fourthHarp);

    }

    public void SelectSecondHarp(InputAction.CallbackContext context)
    {
        Debug.Log("Harp 2");
        secondHarp.transform.position = new Vector3(0, 0, 0);
        FadeOut(firstHarp);
        FadeOut(thirdHarp);
        FadeOut(fourthHarp);

    }

    public void SelectThirdHarp(InputAction.CallbackContext context)
    {
        Debug.Log("Harp 3");
        thirdHarp.transform.position = new Vector3(0, 0, 0);
        FadeOut(firstHarp);
        FadeOut(secondHarp);
        FadeOut(fourthHarp);

    }

    public void SelectFourthHarp(InputAction.CallbackContext context)
    {
        Debug.Log("Harp 4");
        fourthHarp.transform.position = new Vector3(0, 0, 0);
        FadeOut(firstHarp);
        FadeOut(secondHarp);
        FadeOut(thirdHarp);
        

    }

    public void PlaySong(InputAction.CallbackContext context)
    {
        Debug.Log("Playing song...");
        
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

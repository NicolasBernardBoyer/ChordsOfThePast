using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class HarpController : MonoBehaviour
{
    public InputSystem_Actions inputControls;

    private InputAction firstCordAction;
    private InputAction secondCordAction;
    private InputAction thirdCordAction;
    private InputAction fourthCordAction;
    private InputAction playSongAction;
    private InputAction rotationAction;

    [SerializeField] private GameObject firstCord;
    [SerializeField] private GameObject secondCord;
    [SerializeField] private GameObject thirdCord;
    [SerializeField] private GameObject fourthCord;

    public AudioSource audioSound;

    private GameObject selectedCord;

    private void Awake()
    {
        inputControls = new InputSystem_Actions();
        audioSound = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        // Get input actions
        firstCordAction = inputControls.HarpTuning.ChooseFirstCord;
        secondCordAction = inputControls.HarpTuning.ChooseSecondCord;
        thirdCordAction = inputControls.HarpTuning.ChooseThirdCord;
        fourthCordAction = inputControls.HarpTuning.ChooseFourthCord;
        playSongAction = inputControls.HarpTuning.PlaySong;
        rotationAction = inputControls.HarpTuning.Rotate;

        // Enable actions
        firstCordAction.Enable();
        secondCordAction.Enable();
        thirdCordAction.Enable();
        fourthCordAction.Enable();
        playSongAction.Enable();
        rotationAction.Enable();

        // Bind input actions to methods
        firstCordAction.performed += context => SelectCord(firstCord);
        secondCordAction.performed += context => SelectCord(secondCord);
        thirdCordAction.performed += context => SelectCord(thirdCord);
        fourthCordAction.performed += context => SelectCord(fourthCord);
        playSongAction.performed += PlaySong;
        rotationAction.performed += RotateCord;
    }

    private void OnDisable()
    {
        firstCordAction.Disable();
        secondCordAction.Disable();
        thirdCordAction.Disable();
        fourthCordAction.Disable();
        rotationAction.Disable();
    }

    private void SelectCord(GameObject cord)
    {
        Debug.Log($"Selected: {cord.name}");
        ResetCordBorders();
        selectedCord = cord;

        // Add border to selected cord
        var border = selectedCord.transform.Find("Border");
        if (border != null) border.gameObject.SetActive(true);
    }

    private void ResetCordBorders()
    {
        // Reset all cord borders
        ResetBorder(firstCord);
        ResetBorder(secondCord);
        ResetBorder(thirdCord);
        ResetBorder(fourthCord);
    }

    private void ResetBorder(GameObject cord)
    {
        var border = cord.transform.Find("Border");
        if (border != null) border.gameObject.SetActive(false);
    }

    private void RotateCord(InputAction.CallbackContext context)
    {
        if (selectedCord == null) return;

        float rotationValue = context.ReadValue<float>();
        Debug.Log($"Rotation value: {rotationValue}");

        float rotationSpeed = 400f; // Speed of rotation
        selectedCord.transform.Rotate(0, 0, rotationValue * rotationSpeed * Time.deltaTime);

        // Adjust sound volumes based on rotation
        AdjustPitch(0.5f);
    }


    // Modify pitch based on a value
    void AdjustPitch(float value)
    {
        Debug.Log($"audioVolume pitch before: {audioSound.pitch}");
        audioSound.pitch = Mathf.Clamp(value, -3f, 3f);
        Debug.Log($"audioVolume pitch after: {audioSound.pitch}");
        audioSound = GetComponent<AudioSource>();
    }


    private void AdjustSoundVolumes(float rotationValue)
    {
        // Map rotation value to desired volume ratios (50%, 100%, 50%)
        float normalizedValue = Mathf.Clamp01((rotationValue + 1f) / 2f); // Map rotation (-1 to 1) to (0 to 1)

        audioSound.volume = Mathf.Lerp(0.5f, 1f, normalizedValue); // Adjust volume of sound1

        Debug.Log($"Volumes - Sound1: {audioSound.volume}");
    }

    private void PlaySong(InputAction.CallbackContext context)
    {
        audioSound.Play();
        Debug.Log("Playing song...");
    }
}

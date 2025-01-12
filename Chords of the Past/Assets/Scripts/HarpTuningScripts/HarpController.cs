using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    [SerializeField] private AudioSource[] cordSounds = new AudioSource[4];

    [SerializeField] private Slider pitchSlider; // Slider to control pitch
    [SerializeField] private Button evaluateButton;

    private GameObject selectedCord;

    private void Awake()
    {
        inputControls = new InputSystem_Actions();

        // Initialize random pitches for each cord
        foreach (var sound in cordSounds)
        {
            if (sound != null)
            {
                sound.pitch = Random.Range(0.5f, 2f);
            }
        }

        // Link button to EvaluateAndProceed
        if (evaluateButton != null)
        {
            evaluateButton.onClick.AddListener(EvaluateAndProceed);
        }
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

        // Bind slider value change event
        pitchSlider.onValueChanged.AddListener(OnPitchSliderChanged);
    }

    private void OnDisable()
    {
        firstCordAction.Disable();
        secondCordAction.Disable();
        thirdCordAction.Disable();
        fourthCordAction.Disable();
        playSongAction.Disable();
        rotationAction.Disable();

        // Remove slider listener
        pitchSlider.onValueChanged.RemoveListener(OnPitchSliderChanged);
    }

    private void SelectCord(GameObject cord)
    {
        Debug.Log($"Selected: {cord.name}");
        ResetCordBorders();
        selectedCord = cord;

        // Add border to selected cord
        var border = selectedCord.transform.Find("Border");
        if (border != null) border.gameObject.SetActive(true);

        // Update slider to match the pitch of the selected cord
        int cordIndex = GetCordIndex(selectedCord);
        if (cordIndex != -1 && cordSounds[cordIndex] != null)
        {
            pitchSlider.value = cordSounds[cordIndex].pitch; // Set slider value
        }
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

        float rotationSpeed = 400f;
        selectedCord.transform.Rotate(0, 0, rotationValue * rotationSpeed * Time.deltaTime);

        // Get current cord index and adjust its pitch
        int cordIndex = GetCordIndex(selectedCord);
        if (cordIndex != -1)
        {
            float currentRotation = selectedCord.transform.eulerAngles.z;
            float normalizedPitch = Mathf.Lerp(0.5f, 2f, (currentRotation % 360) / 360f);
            AdjustPitch(normalizedPitch, cordIndex);

            // Update slider to reflect new pitch
            pitchSlider.value = cordSounds[cordIndex].pitch;
        }
    }

    private int GetCordIndex(GameObject cord)
    {
        if (cord == firstCord) return 0;
        if (cord == secondCord) return 1;
        if (cord == thirdCord) return 2;
        if (cord == fourthCord) return 3;
        return -1;
    }

    void AdjustPitch(float value, int cordIndex)
    {
        if (cordIndex < 0 || cordIndex >= cordSounds.Length) return;
        if (cordSounds[cordIndex] == null)
        {
            Debug.LogError($"AudioSource for cord {cordIndex} is not assigned!");
            return;
        }

        Debug.Log($"Cord {cordIndex} pitch before: {cordSounds[cordIndex].pitch}");
        cordSounds[cordIndex].pitch = Mathf.Clamp(value, 0.5f, 2f);
        Debug.Log($"Cord {cordIndex} pitch after: {cordSounds[cordIndex].pitch}");
    }

    private void PlaySong(InputAction.CallbackContext context)
    {
        if (selectedCord == null)
        {
            Debug.LogWarning("No cord selected!");
            return;
        }

        int cordIndex = GetCordIndex(selectedCord);
        if (cordIndex != -1 && cordSounds[cordIndex] != null)
        {
            cordSounds[cordIndex].Play();
            Debug.Log($"Playing cord {cordIndex}...");
        }
        else
        {
            Debug.LogError($"AudioSource for cord {cordIndex} is not assigned!");
        }
    }

    private void OnPitchSliderChanged(float newPitch)
    {
        if (selectedCord == null) return;

        int cordIndex = GetCordIndex(selectedCord);
        if (cordIndex != -1)
        {
            AdjustPitch(newPitch, cordIndex);
        }
    }

    private void EvaluateAndProceed()
    {
        float targetPitch = 1.25f;
        float totalScore = 0f;

        foreach (var sound in cordSounds)
        {
            if (sound != null)
            {
                float pitchDifference = Mathf.Abs(sound.pitch - targetPitch);
                float score = Mathf.Clamp(pitchDifference * 100, 0, 100); // Scaled difference
                totalScore += score;
            }
        }

        Debug.Log($"Total Score (Lower is Better): {totalScore}");

        // Proceed to the next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}


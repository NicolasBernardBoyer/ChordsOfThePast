using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem; //allows us to use MIDI keyboard :3
using UnityEngine.SceneManagement; //allows us to change scenes in Unity via code!

public class ChooseHarpLevelManager : MonoBehaviour
{
    public InputSystem_Actions inputControls;

    //creates a different input action for each of the keys LMAO

    private InputAction firstHarpAction;
    private InputAction secondHarpAction;
    private InputAction thirdHarpAction;
    private InputAction fourthHarpAction;
    private InputAction playSongAction;

    [SerializeField] GameObject firstHarp;
    [SerializeField] GameObject secondHarp;
    [SerializeField] GameObject thirdHarp;
    [SerializeField] GameObject fourthHarp; //VS is god-tier

    [SerializeField] Vector3 firstHarpPosition;
    [SerializeField] Vector3 secondHarpPosition;
    [SerializeField] Vector3 thirdHarpPosition;
    [SerializeField] Vector3 fourthHarpPosition;

    [SerializeField] TextMeshProUGUI endOfRoundText;

    bool firstHarpSelected = false;
    bool secondHarpSelected = false;
    bool thirdHarpSelected = false;
    bool fourthHarpSelected = false;

    public Sprite[] allHarpSprites;

    public AudioClip[] allHarpChords;

    AudioClip chosenChord;

    public GameObject harpPrefab;

    public GameObject[] harpArray = new GameObject[4];
    //public Transform[] harpTransformArray = new Transform[4];

    int round_counter = 1;

    private void Awake() //happens before Start()
    {
        //"wakes up" the input system
        inputControls = new InputSystem_Actions();

        int random = UnityEngine.Random.Range(0, allHarpSprites.Length);
        chosenChord = allHarpChords[random];

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
        playSongAction.Disable();
    }



    public void FadeOut(GameObject chosenHarp)
    {
        chosenHarp.SetActive(false);
    }

    public void BeVisible(GameObject chosenHarp)
    {
        chosenHarp.SetActive(true);
    }


    public void SelectFirstHarp(InputAction.CallbackContext context)
    {
        if (firstHarpSelected)
        {
            //round ends
            Debug.Log("Round Ends");
            FadeOut(firstHarp);
            endOfRoundText.CrossFadeAlpha(1, 0.01f, false);
        }

        else
        {
            firstHarpSelected = true;
            Debug.Log("Harp 1");
            firstHarp.transform.position = Vector3.MoveTowards(firstHarpPosition, new Vector3(0, 0.5f, 0), 5000);
            FadeOut(secondHarp);
            FadeOut(thirdHarp);
            FadeOut(fourthHarp);
        }


    }

    public void SelectSecondHarp(InputAction.CallbackContext context)
    {
        if (secondHarpSelected)
        {
            //round ends
            Debug.Log("Round Ends");
            FadeOut(secondHarp);
            endOfRoundText.CrossFadeAlpha(1, 0.01f, false);
        }

        else
        {
            secondHarpSelected = true;
            Debug.Log("Harp 2");
            secondHarp.transform.position = new Vector3(0, 0.5f, 0);
            FadeOut(firstHarp);
            FadeOut(thirdHarp);
            FadeOut(fourthHarp);
        }

    }

    public void SelectThirdHarp(InputAction.CallbackContext context)
    {
        if (thirdHarpSelected)
        {
            //round ends
            Debug.Log("Round Ends");
            FadeOut(thirdHarp);
            endOfRoundText.CrossFadeAlpha(1, 0.01f, false);
        }

        else
        {
            thirdHarpSelected = true;
            Debug.Log("Harp 3");
            thirdHarp.transform.position = new Vector3(0, 0.5f, 0);
            FadeOut(firstHarp);
            FadeOut(secondHarp);
            FadeOut(fourthHarp);
        }

    }

    public void SelectFourthHarp(InputAction.CallbackContext context)
    {

        if (fourthHarpSelected)
        {
            //round ends
            Debug.Log("Round Ends");
            FadeOut(fourthHarp);
            endOfRoundText.CrossFadeAlpha(1, 0.01f, false);

        }

        else
        {
            fourthHarpSelected = true;
            Debug.Log("Harp 4");
            fourthHarp.transform.position = new Vector3(0, 0.5f, 0);
            FadeOut(firstHarp);
            FadeOut(secondHarp);
            FadeOut(thirdHarp);
        }


    }

    public void PlaySong(InputAction.CallbackContext context)
    {
        Debug.Log("Playing song...");

    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


        endOfRoundText.CrossFadeAlpha(0, 0.01f, false);

        for (int i = 0; i < 4; i++)
        {
            harpArray[i] = Instantiate(harpPrefab);
            //harpArray[i].transform.position = harpTransformArray[i].position;
            do
            {
                int randomSprite = UnityEngine.Random.Range(0, allHarpSprites.Length);
                harpArray[i].GetComponent<SpriteRenderer>().sprite = allHarpSprites[randomSprite];
                allHarpSprites[randomSprite] = null;
            } while (harpArray[i].GetComponent<SpriteRenderer>().sprite != null);

            do
            {
                int randomChord = UnityEngine.Random.Range(0, allHarpChords.Length);
                harpArray[i].GetComponent<AudioSource>().resource = allHarpChords[randomChord];
                allHarpChords[randomChord] = null;
            } while (harpArray[i].GetComponent<AudioSource>().resource != null);


        } //end of for loop
        int chordToMessWith = UnityEngine.Random.Range(0, 4);
        harpArray[chordToMessWith].GetComponent<AudioSource>().resource = chosenChord;

        //harcode the starting position
        harpArray[0].transform.position = new Vector3(-5, 0.5f, 0);
        harpArray[1].transform.position = new Vector3(-2, 0.5f, 0);
        harpArray[2].transform.position = new Vector3(2, 0.5f, 0);
        harpArray[3].transform.position = new Vector3(5, 0.5f, 0);

    }

    // Update is called once per frame
    void Update()
    {
        if (round_counter >= 4)
        {
            SceneManager.LoadScene("MainMenu");
        }

    }
}

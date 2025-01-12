using System.Collections;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem; //allows us to use MIDI keyboard :3
using UnityEngine.SceneManagement; //allows us to change scenes in Unity via code!

public class ChooseHarpLevelManager2 : MonoBehaviour
{
    public InputSystem_Actions inputControls;

    //creates a different input action for each of the keys LMAO

    private InputAction firstHarpAction;
    private InputAction secondHarpAction;
    private InputAction thirdHarpAction;
    private InputAction fourthHarpAction;

    private InputAction firstHarpSelect;
    private InputAction secondHarpSelect;
    private InputAction thirdHarpSelect;
    private InputAction fourthHarpSelect;

    [SerializeField] Vector3 firstHarpPosition;
    [SerializeField] Vector3 secondHarpPosition;
    [SerializeField] Vector3 thirdHarpPosition;
    [SerializeField] Vector3 fourthHarpPosition;

    [SerializeField] TextMeshProUGUI endOfRoundText;

    public Sprite[] allHarpSprites;

    public AudioClip[] allHarpChords;

    AudioClip chosenChord;

    public GameObject harpPrefab;

    public GameObject[] harpArray = new GameObject[4];
    public GameObject currentHarpSelection;
    private GameObject chosenHarp;

    public ChooseHarpDialogue HarpDialogueBox;

    [SerializeField] public TextMeshProUGUI roundCompleteText;


    //public Transform[] harpTransformArray = new Transform[4];

    int round_counter = 1;

    bool isPlaying;

    public Timer timer;

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

        firstHarpSelect = inputControls.ChooseHarp.SelectCord1;
        secondHarpSelect = inputControls.ChooseHarp.SelectCord2;
        thirdHarpSelect = inputControls.ChooseHarp.SelectCord3;
        fourthHarpSelect = inputControls.ChooseHarp.SelectCord4;



        //enable all input maps to make them work
        firstHarpAction.Enable();
        secondHarpAction.Enable();
        thirdHarpAction.Enable();
        fourthHarpAction.Enable();

        firstHarpSelect.Enable();
        secondHarpSelect.Enable();
        thirdHarpSelect.Enable();
        fourthHarpSelect.Enable();

        firstHarpAction.performed += ChooseHarp;
        secondHarpAction.performed += ChooseHarp;
        thirdHarpAction.performed += ChooseHarp;
        fourthHarpAction.performed += ChooseHarp;

        firstHarpSelect.performed += SelectHarp;
        secondHarpSelect.performed += SelectHarp;
        thirdHarpSelect.performed += SelectHarp;
        fourthHarpSelect.performed += SelectHarp;

    }

    private void OnDisable()
    {
        firstHarpAction.Disable();
        secondHarpAction.Disable();
        thirdHarpAction.Disable();
        fourthHarpAction.Disable();
    }



    public void FadeOut(GameObject chosenHarp)
    {
        chosenHarp.SetActive(false);
    }

    public void BeVisible(GameObject chosenHarp)
    {
        chosenHarp.SetActive(true);
    }


    public void ChooseHarp(InputAction.CallbackContext context)
    {
        if (isPlaying)
        {
            return;
        }

        //find the chosen harp
        int harpIndex = -1;
        Debug.Log(context.action.name);
        if (context.action.name == "ChooseFirstHarp")
        {
            harpIndex = 0;
        }
        else if (context.action.name == "ChooseSecondHarp")
        {
            harpIndex = 1;
        }
        else if (context.action.name == "ChooseThirdHarp")
        {
            harpIndex = 2;
        }
        else if (context.action.name == "ChooseFourthHarp")
        {
            harpIndex = 3;
        }

        //select the new harp
        currentHarpSelection.GetComponent<Harp>().SetIsSelected(false);
        currentHarpSelection = harpArray[harpIndex];
        currentHarpSelection.GetComponent<Harp>().SetIsSelected(true);

        //play the sound
        StartCoroutine(PlayHarpSound());
    }
    public void SelectHarp(InputAction.CallbackContext context)
    {
        //find which harp was selected
        int harpIndex = -1;
        if (context.action.name == "SelectCord1")
        {
            harpIndex = 0;
        }
        else if (context.action.name == "SelectCord2")
        {
            harpIndex = 1;
        }
        else if (context.action.name == "SelectCord3")
        {
            harpIndex = 2;
        }
        else if (context.action.name == "SelectCord4")
        {
            harpIndex = 3;
        }

        //fade out all harps that are not the one selected
        for (int i = 0; i < 4; i++)
        {
            if (i != harpIndex)
            {
                harpArray[i].GetComponent<Harp>().StartFadeOut();

            }
        }

        //you would move the harp to the middle here, if I was you I would write the IEnumerator in the Harp script

        //you would have the check if they clicked the right harp here with
        if (harpArray[harpIndex].GetComponent<Harp>().isChosenHarp)
        {
            Score.addScore(100);
            Debug.Log(Score.getHealthScore());
            //add score

        }
        else
        {
            Score.addScore(1); //pity point
            Debug.Log("Failed the puzzle lol");

        }


        //delete all harp prefabs before going to the next round
        for (int i = 0; i < 4; i++)
        {
            Destroy(harpArray[i]);
        }

        StartCoroutine(TimeToNextRound());
        setUpRound();
        Debug.Log(round_counter);
    }

    private IEnumerator PlayHarpSound()
    {
        //make sure you cant input single taps until the music is done
        isPlaying = true;
        AudioSource audioSource = currentHarpSelection.GetComponent<AudioSource>();
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        isPlaying = false;
    }

    private IEnumerator TimeToNextRound()
    {

        OnDisable();
        yield return new WaitForSeconds(3f);
        OnEnable();
    }

    private IEnumerator PlayCutScene()
    {
        //make sure you cant input single taps until the music is done
        OnDisable();
        AudioSource audioSource = chosenHarp.GetComponent<AudioSource>();
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        OnEnable();
    }

    public void setUpRound()
    {
        if (round_counter == 1)
            roundCompleteText.text = "Round 1 Complete!";
        if (round_counter == 2)
            roundCompleteText.text = "Round 2 Complete!";
        if (round_counter == 3)
            roundCompleteText.text = "Round 3 Complete!";

        round_counter += 1;

        if (round_counter >= 4)
        {
            Debug.Log("Game Over");
            SceneManager.LoadScene("EndOfChooseHarp");
            return;
        }


        //endOfRoundText.CrossFadeAlpha(0, 0.01f, false);

        for (int i = 0; i < 4; i++)
        {
            harpArray[i] = Instantiate(harpPrefab);
            //harpArray[i].transform.position = harpTransformArray[i].position;
            //random sprite
            int randomSprite;
            do
            {
                randomSprite = UnityEngine.Random.Range(0, allHarpSprites.Length);
            } while (allHarpSprites[randomSprite] == null);

            harpArray[i].GetComponent<SpriteRenderer>().sprite = allHarpSprites[randomSprite];
            allHarpSprites[randomSprite] = null;

            //randomChord
            int randomChord;
            do
            {
                randomChord = UnityEngine.Random.Range(0, allHarpChords.Length);
            } while (allHarpChords[randomChord] == null);

            harpArray[i].GetComponent<AudioSource>().resource = allHarpChords[randomChord];
            allHarpChords[randomChord] = null;


        } //end of for loop


        //int chordToMessWith = UnityEngine.Random.Range(0, 4);
        //harpArray[chordToMessWith].GetComponent<AudioSource>().resource = chosenChord;

        //harcode the starting position
        harpArray[0].transform.position = new Vector3(-5, 0.5f, 0);
        harpArray[1].transform.position = new Vector3(-2, 0.5f, 0);
        harpArray[2].transform.position = new Vector3(2, 0.5f, 0);
        harpArray[3].transform.position = new Vector3(5, 0.5f, 0);

        //find the right harp
        int random = UnityEngine.Random.Range(0, 4);
        harpArray[random].GetComponent<Harp>().isChosenHarp = true;
        chosenHarp = harpArray[random];
        //to avoid edge cases
        currentHarpSelection = harpArray[0];

        StartCoroutine(PlayCutScene());
    } 




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        roundCompleteText.text = "";
        Debug.Log(round_counter);
        //endOfRoundText.CrossFadeAlpha(0, 0.01f, false);

        for (int i = 0; i < 4; i++)
        {
            harpArray[i] = Instantiate(harpPrefab);
            //harpArray[i].transform.position = harpTransformArray[i].position;
            //random sprite
            int randomSprite;
            do
            {
                randomSprite = UnityEngine.Random.Range(0, allHarpSprites.Length);
            } while (allHarpSprites[randomSprite] == null);

            harpArray[i].GetComponent<SpriteRenderer>().sprite = allHarpSprites[randomSprite];
            allHarpSprites[randomSprite] = null;

            //randomChord
            int randomChord;
            do
            {
                randomChord = UnityEngine.Random.Range(0, allHarpChords.Length);
            } while (allHarpChords[randomChord] == null);

            harpArray[i].GetComponent<AudioSource>().resource = allHarpChords[randomChord];
            allHarpChords[randomChord] = null;


        } //end of for loop

        
        //int chordToMessWith = UnityEngine.Random.Range(0, 4);
        //harpArray[chordToMessWith].GetComponent<AudioSource>().resource = chosenChord;

        //harcode the starting position
        harpArray[0].transform.position = new Vector3(-5, 0.5f, 0);
        harpArray[1].transform.position = new Vector3(-2, 0.5f, 0);
        harpArray[2].transform.position = new Vector3(2, 0.5f, 0);
        harpArray[3].transform.position = new Vector3(5, 0.5f, 0);

        //find the right harp
        int random = UnityEngine.Random.Range(0, 4);
        harpArray[random].GetComponent<Harp>().isChosenHarp = true;
        chosenHarp = harpArray[random];
        //to avoid edge cases
        currentHarpSelection = harpArray[0];

        StartCoroutine(PlayCutScene());
    }

    // Update is called once per frame
    void Update()
    {

    }
}

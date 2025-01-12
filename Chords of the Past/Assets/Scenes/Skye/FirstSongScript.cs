using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FirstSongScript : MonoBehaviour
{
    public InputSystem_Actions inputControls;

    //create a new input action for each different key
    private InputAction keyC;
    private InputAction keyCS;
    private InputAction keyD;
    private InputAction keyDS;
    private InputAction keyE;
    private InputAction keyF;
    private InputAction keyFS;
    private InputAction keyG;
    private InputAction keyGS;
    private InputAction keyA;
    private InputAction keyAS;
    private InputAction keyB;

    private InputAction[] keyActions;

    //have an array for each keynote
    public GameObject[] keys;

    private List<int> greenLeaves = new List<int> { 2, 7, 10, 9, 5, 7, 2, 5, 7, 7 };
    private List<int> rickRoll = new List<int> { 2, 2, 4, 2, 9, 9, 7, 2, 2, 4, 2, 7, 7, 5};
    private List<int> comptine = new List<int> {5, 4, 5, 9, 10, 9, 4, 5, 4, 5, 7, 5};

    private List<List<int>> allSongs = new List<List<int>>();

    public List<int> currentSong = new List<int>();

    private int currentLevel = 0; 


    private void Awake()
    {
        //initialize the input system
        inputControls = new InputSystem_Actions();

        // Create references to all key actions
        keyActions = new InputAction[]
        {
            inputControls.SimonGame.KeyC,//0
            inputControls.SimonGame.KeyCS, //1
            inputControls.SimonGame.KeyD, //2
            inputControls.SimonGame.KeyDS,//3
            inputControls.SimonGame.KeyE, //4
            inputControls.SimonGame.KeyF,//5
            inputControls.SimonGame.KeyFS,//6
            inputControls.SimonGame.KeyG,//7
            inputControls.SimonGame.KeyGS,//8
            inputControls.SimonGame.KeyA,//9
            inputControls.SimonGame.KeyAS,//10
            inputControls.SimonGame.KeyB,//11
            
        };
    }

    private void Start()
    {
        OnDisable();

        allSongs.Add(greenLeaves);
        allSongs.Add(rickRoll);
        allSongs.Add(comptine);

        //randomize the song
        currentSong = allSongs[Random.Range(0, allSongs.Count)];
    }

    private void OnEnable()
    {
        // Enable all keys and register the event dynamically
        foreach (InputAction keyAction in keyActions)
        {
            keyAction.Enable();
            keyAction.performed += PlayKey;
        }
    }

    private void OnDisable()
    {
        // Disable all keys to avoid memory leaks
        foreach (InputAction keyAction in keyActions)
        {
            keyAction.performed -= PlayKey;
            keyAction.Disable();
        }
    }

    private void nextLevelSimon()
    {
        if(currentLevel > currentSong.Count)
        {
            Debug.Log("won");
        }
        StartCoroutine(LoopWithDelay());
        
    }

    private IEnumerator LoopWithDelay()
    {
        OnDisable();
        for (int i = 0; i < Mathf.Min(currentLevel, currentSong.Count); i++)
        {
            //light up the sequence
            int index = currentSong[i];

            keys[index].GetComponent<SpriteRenderer>().color = Color.blue;
            //keys[index].GetComponent<PianoNote>().PlayAudioSource(); 
            yield return new WaitForSeconds(0.5f);
            if (index == 1 || index == 3 || index == 6 || index == 8 || index == 10)
            {
                keys[index].GetComponent<SpriteRenderer>().color = Color.black;
            }
            else
            {
                keys[index].GetComponent<SpriteRenderer>().color = Color.white;
            }
            yield return new WaitForSeconds(0.1f);
        }
        OnEnable(); 

        Debug.Log("Loop complete!");
    }

    //the event I used to register it 
    public void PlayKey(InputAction.CallbackContext context)
    {
        // Log which key was pressed
        Debug.Log($"Key pressed: {context.action.name}");
        int pianoNoteIndex = -1;

        //play the right key 
        if (context.action.name == "KeyC")
        {
            pianoNoteIndex = 0;
        }
        else if (context.action.name == "KeyCS")
        {
            pianoNoteIndex = 1;
        }
        else if (context.action.name == "KeyD")
        {
            pianoNoteIndex = 2;
        }
        else if (context.action.name == "KeyDS")
        {
            pianoNoteIndex = 3;
        }
        else if (context.action.name == "KeyE")
        {
            pianoNoteIndex = 4;
        }
        else if (context.action.name == "KeyF")
        {
            pianoNoteIndex = 5;
        }
        else if (context.action.name == "KeyFS")
        {
            pianoNoteIndex = 6;
        }
        else if (context.action.name == "KeyG")
        {
            pianoNoteIndex = 7;
        }
        else if (context.action.name == "KeyGS")
        {
            pianoNoteIndex = 8;
        }
        else if (context.action.name == "KeyA")
        {
            pianoNoteIndex = 9;
        }
        else if (context.action.name == "KeyAS")
        {
            pianoNoteIndex = 10;
        }
        else if (context.action.name == "KeyB")
        {
            pianoNoteIndex = 11;
        }
        else
        {
            currentLevel += 1;
            nextLevelSimon();
        }
        if(pianoNoteIndex != -1)
        {
            keys[pianoNoteIndex].GetComponent<PianoNote>().PlayAudioSource();

        }
    }
}

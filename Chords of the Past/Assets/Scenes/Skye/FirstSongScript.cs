using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
    public GameObject[] strings;

    //make song shorter???
    private List<int> greenLeaves = new List<int> { 2, 7, 10, 9, 5, 7, 2, 5, 7, 7 };
    private List<int> rickRoll = new List<int> { 2, 2, 4, 2, 9, 9, 7, 2, 2, 4, 2, 7, 7, 5};
    private List<int> comptine = new List<int> {5, 4, 5, 9, 10, 9, 4, 5, 4, 5, 7, 5};

    private List<List<int>> allSongs = new List<List<int>>();

    public List<int> currentSong = new List<int>();

    private int currentLevel = 0;
    private int currentNoteToBePlayed = 0; 


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
            inputControls.SimonGame.Next
            
        };
    }

    private void Start()
    {
        allSongs.Add(greenLeaves);
        allSongs.Add(rickRoll);
        allSongs.Add(comptine);

        //randomize the song
        currentSong = allSongs[Random.Range(0, allSongs.Count)];
        nextLevelSimon();
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
            SceneManager.LoadScene("Cutscene 4");
        }
        StartCoroutine(LoopWithDelay());
        
    }

    private IEnumerator LoopWithDelay()
    {
        OnDisable(); 
        yield return new WaitForSeconds(2f);
        currentLevel += 1;
        currentNoteToBePlayed = 0; 
        OnDisable();
        for (int i = 0; i < Mathf.Min(currentLevel, currentSong.Count); i++)
        {
            //light up the sequence
            int index = currentSong[i];

            keys[index].GetComponent<SpriteRenderer>().color = Color.blue;
            keys[index].GetComponent<PianoNote>().PlayAudioSource(); 
            yield return new WaitForSeconds(0.5f);
            if (index == 1 || index == 3 || index == 6 || index == 8 || index == 10)
            {
                keys[index].GetComponent<SpriteRenderer>().color = Color.black;
            }
            else
            {
                keys[index].GetComponent<SpriteRenderer>().color = Color.white;
            }
            yield return new WaitForSeconds(0.2f);
        }
        OnEnable(); 

        Debug.Log("Loop complete!");
    }

    //the event I used to register it 
    public void PlayKey(InputAction.CallbackContext context)
    {
            // Log which key was pressed
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
        if (pianoNoteIndex != -1)
        {
            strings[pianoNoteIndex].GetComponent<Animator>().SetTrigger("PlayOnce");

            Debug.Log($"Key to play: {currentSong[currentNoteToBePlayed]}");
            Debug.Log($"Key pressed: {pianoNoteIndex}");
            if (pianoNoteIndex != currentSong[currentNoteToBePlayed])
            {
                //minus 10 score
                Score.addScore(-10); 
                //play a bang
                foreach(GameObject key in keys)
                {
                    key.GetComponent<PianoNote>().PlayAudioSource(); 
                }

                //we can have an ienumator for the animation of failing
                //go to next level
                nextLevelSimon();
            }
            else
            {
                currentNoteToBePlayed++;
                Score.addScore(1); 
                if (currentNoteToBePlayed >= currentLevel)
                {
                    nextLevelSimon();
                }
            }

            
            keys[pianoNoteIndex].GetComponent<PianoNote>().PlayAudioSource();
        }
    }
    private IEnumerator PlaySound(PianoNote piano)
    {
        piano.PlayAudioSource();
        yield return new WaitForSeconds(0.2f);
    }
}

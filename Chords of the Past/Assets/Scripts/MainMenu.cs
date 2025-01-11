using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement; //allows us to change scenes in Unity via code!

public class MainMenu : MonoBehaviour
{
    public InputSystem_Actions inputControls;

    private InputAction playGame;
    private InputAction goToCredits;
    private InputAction quitGame;

    private void Awake() //happens before Start()
    {
        //"wakes up" the input system
        inputControls = new InputSystem_Actions();
    }


    public void StartGame()
    {
        SceneManager.LoadScene("ChooseHarp"); //changes the scene to the ChooseHarp minigame. Will be changed later on ofc
    }

    public void GoToCredits()
    {
        Debug.Log("LMAO");
    }

    public void QuitGame()
    {
        Application.Quit(); //quits the game!
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

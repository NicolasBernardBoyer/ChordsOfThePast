using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement; //allows us to change scenes in Unity via code!
public class EndOfGameManager : MonoBehaviour
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


  /* public void NextMinigame()
    {
        SceneManager.LoadScene("MainMenu"); //changes the scene to the ChooseHarp minigame. Will be changed later on ofc
    }
  */
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

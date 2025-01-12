using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReloader : MonoBehaviour
{
    public void ReloadScene()
    {
        // Reloads the currently active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadCutscene1()
    {
        SceneManager.LoadScene("Cutscene 1 Intro");
    }
    public void LoadGearboxPuzzle()
    {
        SceneManager.LoadScene("GearBoxPuzzle");
    }
}


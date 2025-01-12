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

        public void LoadCutscene2()
        {
            SceneManager.LoadScene("Cutscene 2 Finding Harp");
        }
        public void LoadCutscene3()
        {
            SceneManager.LoadScene("Cutscene 3");
        }

        public void LoadCutscene4()
        {
            SceneManager.LoadScene("Cutscene 4");
        }
        public void LoadCutscene5()
        {
            SceneManager.LoadScene("Cutscene 5");
        }
        public void LoadPartition1()
        {
            SceneManager.LoadScene("Partition 1");
        }
        public void LoadPartition2()
        {
            SceneManager.LoadScene("Partition 2");
        }

        public void LoadPartition3()
        {
            SceneManager.LoadScene("Partition 3");
        }

        public void LoadPartition4()
        {
            SceneManager.LoadScene("Partition 4");
        }

        public void LoadPartition5()
        {
            SceneManager.LoadScene("Partition 5");
        }

        public void LoadFirstSong()
        {
            SceneManager.LoadScene("FirstSong");
        }


    public void LoadChooseHarp()
    {
        SceneManager.LoadScene("PREFABChooseHarp");
    }

    public void LoadHarpTuning()
    {
        SceneManager.LoadScene("HarpTuning");
    }
}


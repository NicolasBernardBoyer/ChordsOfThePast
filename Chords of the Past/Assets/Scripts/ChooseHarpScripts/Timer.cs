using UnityEngine;
using TMPro;

public class ChooseHarpTimer : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI timerText; //so this variable is a TextMeshPro text :3
    //connect a Text in Unity to this SerializeField variable to make it work.

    [SerializeField] float totalSecondsLeft = 120;


    

    // Update is called once per frame
    void Update()
    {
        totalSecondsLeft += Time.deltaTime;
        //timerText.text = elapsedTime.ToString(); bro I just put in timerText and VS filled everything else in this IDE is so peak oml
        int minutes = Mathf.FloorToInt(totalSecondsLeft / 60);
        int seconds = Mathf.FloorToInt(totalSecondsLeft % 60);
        timerText.text = string.Format("Timer: {0:00}:{1:00}", minutes, seconds); //{} is where the values of the variables are put
        //{0} means to show the first variable in the format of ":00" and {1} is to show the second variable's parameters
    }
}

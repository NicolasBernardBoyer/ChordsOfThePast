using UnityEngine;

public class Notch : MonoBehaviour
{
    public bool taken = false;
    public int solution = 0;
    public bool correctSolution = false;

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Gear>() != null)
        {
            taken = false;
        }
    }
}

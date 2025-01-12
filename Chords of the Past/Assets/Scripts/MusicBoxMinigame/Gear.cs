using UnityEngine;

public class Gear : MonoBehaviour
{
    public int type = 0;
    public bool canPlace = false;
    public Notch hovering = null;
    public SpriteRenderer spriteRenderer;
    public Sprite smallSprite;
    public Sprite mediumSprite;
    public Sprite largeSprite;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (type)
        {
            case 0:
                spriteRenderer.sprite = smallSprite;
                break;
            case 1:
                spriteRenderer.sprite = mediumSprite;
                break;
            case 2:
                spriteRenderer.sprite = largeSprite;
                break;
        }
    }

    public bool attemptToPlace()
    {
        if (canPlace)
        {
            hovering.taken = true;
            if (type == hovering.solution)
            {
                hovering.correctSolution = true;
            }
            transform.position = hovering.transform.position;
            return true;
        }
        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Notch>() != null)
        {
            if (collision.GetComponent<Notch>().taken == false)
            {
                hovering = collision.GetComponent<Notch>();
                canPlace = true;
            } else
            {
                canPlace = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Notch>() != null)
        {
            if (collision.GetComponent<Notch>().taken == false)
            {
                hovering = null;
                canPlace = false;
            }
        }
    }
}

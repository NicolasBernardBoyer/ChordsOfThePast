using UnityEngine;

public class Gear : MonoBehaviour
{
    public int type = 0;
    public bool canPlace = false;
    public Notch hovering = null;
    public Gear gearHovering = null;
    public SpriteRenderer spriteRenderer;
    public Sprite smallSprite;
    public Sprite mediumSprite;
    public Sprite largeSprite;
    public GearsGameManager gameManager;
    
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
        transform.Rotate(0.0f, 0.0f, 90.0f * Time.deltaTime, Space.Self);
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
    public bool attemptToRemove()
    {
        if (gearHovering != null)
        {
            if (hovering != null)
            {
                hovering.taken = false;
                Destroy(gearHovering.gameObject);
                return true;
            }
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
        if (collision.GetComponent<Gear>() != null)
        {
            gearHovering = collision.GetComponent<Gear>();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Notch>() != null)
        {
            if (collision.GetComponent<Notch>().taken == false)
            {
                hovering = collision.GetComponent<Notch>();
                canPlace = true;
            }
            else
            {
                canPlace = false;
            }
        }
        if (collision.GetComponent<Gear>() != null)
        {
            gearHovering = collision.GetComponent<Gear>();
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
        if (collision.GetComponent<Gear>() != null)
        {
            gearHovering = null;
        }
    }
}

using UnityEngine;

//First Food Class
public class FastFood : MonoBehaviour
{
    //Adding Box Collider (gridArea)
    public BoxCollider2D gridArea;
    
    //Spriter Render
    public SpriteRenderer sr;
    
    //Method to respawn the food
    private void Start()
    {
        RandomizePosition();

    }

    //method to set the random position
    private void RandomizePosition()
    {
        Bounds bounds = this.gridArea.bounds;

        float x = Random.Range(bounds.min.y, bounds.max.y);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        
        float foodType = Random.Range(0, 2);
        sr = GetComponent<SpriteRenderer>();
        if (foodType==1)
        {
            this.transform.gameObject.tag = "fastFood";
            this.sr.color = Color.red;
        }
        else
        {
            this.transform.gameObject.tag = "lifeFood";
            this.sr.color = Color.blue;
        }
        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }

    //method to trigger the action when one of the snakes collide with the food
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            RandomizePosition();
        }

        else if (other.tag == "snakeBody")
        {
            RandomizePosition();
        }

        else if (other.tag == "snakeIABody")
        {
            RandomizePosition();
        }

        else if (other.tag == "Obstacle")
        {
            RandomizePosition();
        }
    }
}

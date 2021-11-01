using UnityEngine;

//First Food Class
public class FastFood : MonoBehaviour
{
    //Adding Box Collider (gridArea)
    public BoxCollider2D gridArea;
    
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

        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }

    //method to trigger the action when one of the snakes collide with the food
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            RandomizePosition();
        }

        if (other.tag == "Obstacle")
        {
            RandomizePosition();
        }
    }
}

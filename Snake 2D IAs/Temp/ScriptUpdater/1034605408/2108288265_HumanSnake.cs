using UnityEngine;
using System.Collections.Generic;

public class HumanSnake : MonoBehaviour
{
        private Vector2 _direction = Vector2.right;

        //Getting the update frequence (snake speed)
        private int updateCount = 0;
        private int updateFrequence = 10;
        public  int lives = 1;
        //Adding Body Features
        private List<Transform> _segments;
        public Transform segmentPrefab;
        
        private Vector3 velocity = new Vector3(10f, 10f, 0f);

        

        //Method to start position and body
        private void Start()
        {
                _segments = new List<Transform>();
                _segments.Add(this.transform);
                lives = 1;
                updateFrequence = 10;
                this.GetComponent<Rigidbody2D>().velocity = velocity;
        }

        private void Update()
        {
                if (Input.GetKeyDown(KeyCode.W))
                {
                        if (_direction != Vector2.down)
                        {_direction = Vector2.up;}
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                        if (_direction != Vector2.up)
                        {_direction = Vector2.down;}
                }

                else if (Input.GetKeyDown(KeyCode.A))
                {
                        if (_direction != Vector2.right)
                        {_direction = Vector2.left;}
                }

                else if (Input.GetKeyDown(KeyCode.D))
                {
                        if (_direction != Vector2.left)
                        {_direction = Vector2.right;}
                }
        }

        private void FixedUpdate()
        {
                if ((updateCount%20)%updateFrequence == 0)
                {
                        for (int i = _segments.Count - 1; i>0;i--)
                        { 
                                _segments[i].position = _segments[i-1].position;
                        }
                        
                        this.transform.position = new Vector3(
                            Mathf.Round(this.transform.position.x) + _direction.x,
                            Mathf.Round(this.transform.position.y) + _direction.y,
                            0.0f
                        );


                }
                this.updateCount+=1;
        }

        private void ResetState()
        {
                for (int i = 1; i< _segments.Count; i++)
                {
                        Destroy(_segments[i].gameObject);
                }
                _segments.Clear();
                _segments.Add(this.transform);
                lives = 1;
                updateFrequence = 10;

                this.transform.position = new Vector3(-10.0f, 0.0f,0.0f);

        }

        private void Grow()
        {
                Transform segment = Instantiate(this.segmentPrefab);
                segment.position =  new Vector3(
                            _segments[_segments.Count - 1].position.x - _direction.x,
                            _segments[_segments.Count - 1].position.y - _direction.y,
                            0.0f
                        );
                _segments.Add(segment);

        }

        private void OnTriggerEnter2D(Collider2D other)
        {
                if (other.tag == "Obstacle") {
                        
                        ResetState();    
 
                }

                else if (other.tag == "Obstacle") {
                        if (lives > 1)
                        {
                                lives-=1;
                        }
                        else
                        {
                                ResetState();    
                        }
                        
                }
                else if (other.tag == "fastFood")
                {
                        if (updateFrequence > 1)
                        {
                                updateFrequence-=1;
                        }
                        Grow();
                }
                else if (other.tag == "lifeFood")
                {
                        if (updateFrequence < 20)
                        {
                                updateFrequence+=1;
                        }
                        lives+=1;
                        Grow();
                } 

        }
}

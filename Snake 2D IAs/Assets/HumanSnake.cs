using UnityEngine;
using System.Collections.Generic;

public class HumanSnake : MonoBehaviour
{
        private Vector2 _direction = Vector2.right;

        //Getting the update frequence (snake speed)
        private int updateCount = 0;
        private int updateFrequence = 3;

        //Adding Body Features
        private List<Transform> _segments;
        public Transform segmentPrefab;

        //Method to start position and body
        private void Start()
        {
                _segments = new List<Transform>();
                _segments.Add(this.transform);
        }

        private void Update()
        {
                if (Input.GetKeyDown(KeyCode.W))
                {
                        _direction = Vector2.up;
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                        _direction = Vector2.down;
                }

                else if (Input.GetKeyDown(KeyCode.A))
                {
                        _direction = Vector2.left;
                }

                else if (Input.GetKeyDown(KeyCode.D))
                {
                        _direction = Vector2.right;
                }
        }

        private void FixedUpdate()
        {
                if (updateCount%updateFrequence == 0)
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
                else if (other.tag == "Food")
                {
                        Grow();
                } 

        }
}

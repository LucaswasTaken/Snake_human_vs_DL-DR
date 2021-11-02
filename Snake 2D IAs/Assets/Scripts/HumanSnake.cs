using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HumanSnake : MonoBehaviour
{
        private Vector2 _direction = Vector2.right;

        //UI data
        public Text pointText;

        //Data for speed 
        private int updateCount = 0;
        private int updateFrequence = 10;

        //Data for points
        public  int lives = 1;
        public  int points = 0;

        //Movement Limits
        public BoxCollider2D gridArea;
        

        //Adding Body Features
        private List<Transform> _segments;
        public Transform segmentPrefab;

        //Method to start position and body
        private void Start()
        {
                _segments = new List<Transform>();
                _segments.Add(this.transform);
                lives = 1;
                updateFrequence = 10;
                pointText.text = $"ENGINE POWER BLOCK: {points - lives + 1} \nBATTERING RAM BLOCK: {lives - 1}\nPoints: {points}";
        }
        //Update Direction
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

        //Update ditection corrected by speed
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

        //Reset Initial State
        private void ResetState()
        {
                for (int i = 1; i< _segments.Count; i++)
                {
                        Destroy(_segments[i].gameObject);
                }
                _segments.Clear();
                _segments.Add(this.transform);
                this.lives = 1;
                this.points = 0;
                this.updateFrequence = 10;
                pointText.text = $"ENGINE POWER BLOCK: {points - lives + 1} \nBATTERING RAM BLOCK: {lives - 1}\nPoints: {points}";
                this.transform.position = new Vector3(-10.0f, 0.0f,0.0f);

        }

        //Grow Snake
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

        //Trigger Contacts
        private void OnTriggerEnter2D(Collider2D other)
        {
                //Colisões com a parede
                Bounds bounds = this.gridArea.bounds;
                if (other.tag == "upWall") {
                        this.transform.position = new Vector3(_segments[0].position.x,bounds.min.y,0.0f);
 
                }

                else if (other.tag == "downWall") {
                        this.transform.position = new Vector3(_segments[0].position.x,bounds.max.y,0.0f);
 
                }

                else if (other.tag == "leftWall") {
                        this.transform.position = new Vector3(bounds.max.x,_segments[0].position.y,0.0f);
 
                }

                else if (other.tag == "rightWall") {
                        this.transform.position = new Vector3(bounds.min.x,_segments[0].position.y,0.0f);
 
                }

                //Colisões com a cobra IA
                else if (other.tag == "snakeIABody") {
                        if (lives > 1)
                        {
                                lives-=1;
                                Destroy(_segments[_segments.Count-1].gameObject);
                                _segments.RemoveAt(_segments.Count-1);
                        }
                        else
                        {
                                ResetState();    
                        }
                        this.points-=1;
                        pointText.text = $"ENGINE POWER BLOCK: {points - lives + 1} \nBATTERING RAM BLOCK: {lives - 1}\nPoints: {points}";
                }

                //Colisões com próprio corpo
                else if (other.tag == "snakeBody")
                {
                        ResetState();
                }

                //Comer comida de velocidade
                else if (other.tag == "fastFood")
                {
                        if (updateFrequence > 1)
                        {
                                updateFrequence-=1;
                        }
                        Grow();
                        this.points+=1;
                        pointText.text = $"ENGINE POWER BLOCK: {points - lives + 1} \nBATTERING RAM BLOCK: {lives - 1}\nPoints: {points}";
                }

                //Comer comida de vida
                else if (other.tag == "lifeFood")
                {
                        if (updateFrequence < 20)
                        {
                                updateFrequence+=1;
                        }
                        this.lives+=1;
                        this.points+=1;
                        pointText.text = $"ENGINE POWER BLOCK: {points - lives + 1} \nBATTERING RAM BLOCK: {lives - 1}\nPoints: {points}";
                        Grow();
                } 

        }
}

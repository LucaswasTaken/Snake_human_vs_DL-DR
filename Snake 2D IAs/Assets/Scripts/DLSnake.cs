using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
// using TensorFlow;
using System;
public class DLSnake : MonoBehaviour
{
        private Dictionary<string, int> dic_drl = new Dictionary<string, int>()
            {
                {"000-1-1-1-1", 1}, {"000-1-1-11", 1}, {"000-10-1-1", 1}, {"000-10-11", 1}, {"000-11-1-1", 1}, {"000-11-11", 1}, {"0000-1-1-1", 1}, {"0000-1-11", 1}, {"00000-1-1", 1}, {"00000-11", 1}, {"00001-1-1", 1}, {"00001-11", 1}, {"0001-1-1-1", 1}, {"0001-1-11", 1}, {"00010-1-1", 1}, {"00010-11", 1}, {"00011-1-1", 1}, {"00011-11", 1}, {"001-1-1-1-1", 1}, {"001-1-1-11", 1}, {"001-10-1-1", 1}, {"001-10-11", 1}, {"001-11-1-1", 1}, {"001-11-11", 1}, {"0010-1-1-1", 1}, {"0010-1-11", 1}, {"00100-1-1", 1}, {"00100-11", 1}, {"00101-1-1", 1}, {"00101-11", 1}, {"0011-1-1-1", 1}, {"0011-1-11", 1}, {"00110-1-1", 1}, {"00110-11", 1}, {"00111-1-1", 1}, {"00111-11", 1}, {"010-1-1-1-1", 1}, {"010-1-1-11", 1}, {"010-10-1-1", 1}, {"010-10-11", 1}, {"010-11-1-1", 1}, {"010-11-11", 1}, {"0100-1-1-1", 1}, {"0100-1-11", 1}, {"01000-1-1", 1}, {"01000-11", 1}, {"01001-1-1", 1}, {"01001-11", 1}, {"0101-1-1-1", 1}, {"0101-1-11", 1}, {"01010-1-1", 1}, {"01010-11", 1}, {"01011-1-1", 1}, {"01011-11", 1}, {"011-1-1-1-1", 1}, {"011-1-1-11", 1}, {"011-10-1-1", 1}, {"011-10-11", 1}, {"011-11-1-1", 1}, {"011-11-11", 1}, {"0110-1-1-1", 1}, {"0110-1-11", 1}, {"01100-1-1", 1}, {"01100-11", 1}, {"01101-1-1", 1}, {"01101-11", 1}, {"0111-1-1-1", 1}, {"0111-1-11", 1}, {"01110-1-1", 1}, {"01110-11", 1}, {"01111-1-1", 1}, {"01111-11", 1}, {"100-1-1-1-1", 1}, {"100-1-1-11", 1}, {"100-10-1-1", 1}, {"100-10-11", 1}, {"100-11-1-1", 1}, {"100-11-11", 1}, {"1000-1-1-1", 1}, {"1000-1-11", 1}, {"10000-1-1", 1}, {"10000-11", 1}, {"10001-1-1", 1}, {"10001-11", 1}, {"1001-1-1-1", 1}, {"1001-1-11", 1}, {"10010-1-1", 1}, {"10010-11", 1}, {"10011-1-1", 1}, {"10011-11", 1}, {"101-1-1-1-1", 1}, {"101-1-1-11", 1}, {"101-10-1-1", 1}, {"101-10-11", 1}, {"101-11-1-1", 1}, {"101-11-11", 1}, {"1010-1-1-1", 1}, {"1010-1-11", 1}, {"10100-1-1", 1}, {"10100-11", 1}, {"10101-1-1", 1}, {"10101-11", 1}, {"1011-1-1-1", 1}, {"1011-1-11", 1}, {"10110-1-1", 1}, {"10110-11", 1}, {"10111-1-1", 1}, {"10111-11", 1}, {"110-1-1-1-1", 1}, {"110-1-1-11", 1}, {"110-10-1-1", 1}, {"110-10-11", 1}, {"110-11-1-1", 1}, {"110-11-11", 1}, {"1100-1-1-1", 1}, {"1100-1-11", 1}, {"11000-1-1", 1}, {"11000-11", 1}, {"11001-1-1", 1}, {"11001-11", 1}, {"1101-1-1-1", 1}, {"1101-1-11", 1}, {"11010-1-1", 1}, {"11010-11", 1}, {"11011-1-1", 1}, {"11011-11", 1}, {"111-1-1-1-1", 1}, {"111-1-1-11", 1}, {"111-10-1-1", 1}, {"111-10-11", 1}, {"111-11-1-1", 1}, {"111-11-11", 1}, {"1110-1-1-1", 1}, {"1110-1-11", 1}, {"11100-1-1", 1}, {"11100-11", 1}, {"11101-1-1", 1}, {"11101-11", 1}, {"1111-1-1-1", 1}, {"1111-1-11", 1}, {"11110-1-1", 1}, {"11110-11", 1}, {"11111-1-1", 1}, {"11111-11", 1}
            };      
        private Vector2 _direction = Vector2.right;

        //UI data
        public Text pointText;

        //Getting the update frequence (snake speed)
        private int updateCount = 0;
        private int updateFrequence = 5;

        // Points
        public BoxCollider2D gridArea;
        public  int lives = 1;
        public  int points = 0;

        //Adding Body Features
        private List<Transform> _segments;
        public Transform segmentPrefab;

        //Method to start position and body
        private void Start()
        {
                _segments = new List<Transform>();
                _segments.Add(this.transform);
                lives = 1;
                updateFrequence = 5;
                pointText.text = $"ENGINE POWER BLOCK: {points - lives + 1} \nBATTERING RAM BLOCK: {lives - 1}\nPoints: {points}";
        }

        private void Update()
        {


                ///Skeletal code using TensorFlowSharp (not work due TensorFlowSharp update)
                ///
                // using (var graph = new TFGraph ())
                // {
                //         graph.Import(File.ReadAllBytes("./Assets/Scripts/model_tf/saved_model.pb"));
                // }
                // // 
                // var input = new TFTensor[1,0,0,0,1,0,1,0,1,0,1,0];
                // var output = new TFTensor[4];                
                //     // using (var session = new TFSession(graph))
                //     // {
                //     //     // Setup the runner
                //     //     var runner = session.GetRunner();
                //     //     runner.AddInput(graph["input"][0], input);
                //     //     runner.Fetch(graph["output"][0]);
                //     //     // Run the model
                //     //     output = runner.Run();
                //     //     // Fetch the result from output into `result`
                //     //     var result = output[0].GetValue();
                //     //     pointText.text = $"ENGINE POWER BLOCK: {result} \nBATTERING RAM BLOCK: {result}\nPoints: {result}";
                //     // }

                
                //Get food position
                var posFood = GameObject.Find("fastFood").transform.position;

                //State variables for AI (body relative position,food relative position,  movement direction)
                int[] inputState = new int[7];
                Array.Clear(inputState, 0, inputState.Length);

                //Body relative position
                for (int i = 2; i<_segments.Count - 1;i++)
                { 
                        var vec_aux = _segments[i].position - _segments[0].position;
                        if( vec_aux == new Vector3(_direction.x,_direction.y, 0.0f))
                        {
                                inputState[1] = 1;
                        }
                        else if(_direction == Vector2.down)
                        {
                                if(_segments[i].position - _segments[0].position == Vector3.left)
                                {
                                    inputState[2] = 1;
                                }

                                if(_segments[i].position - _segments[0].position == Vector3.right)
                                {
                                    inputState[0] = 1;
                                }
                        }

                        else if(_direction == Vector2.up)
                        {
                                if(_segments[i].position - _segments[0].position == Vector3.left)
                                {
                                    inputState[0] = 1;
                                }

                                if(_segments[i].position - _segments[0].position == Vector3.right)
                                {
                                    inputState[2] = 1;
                                }
                        }
                        else if(_direction == Vector2.right)
                        {
                                if(_segments[i].position - _segments[0].position == Vector3.up)
                                {
                                    inputState[0] = 1;
                                }

                                if(_segments[i].position - _segments[0].position == Vector3.down)
                                {
                                    inputState[2] = 1;
                                }
                        }

                        else if(_direction == Vector2.left)
                        {
                                if(_segments[i].position - _segments[0].position == Vector3.up)
                                {
                                    inputState[2] = 1;
                                }

                                if(_segments[i].position - _segments[0].position == Vector3.down)
                                {
                                    inputState[0] = 1;
                                }
                        }
                }

                //Food relative Position
                if (posFood.y > this.transform.position.y)
                {
                        inputState[4] = 1;
                }
                else if (posFood.y < this.transform.position.y)
                {
                        inputState[4] = -1;
                }
                else
                {
                        inputState[4] = 0;
                }

                if (posFood.x > this.transform.position.x)
                {
                        inputState[3] = 1;
                }
                else if (posFood.x < this.transform.position.x)
                {
                        inputState[3] = -1;
                }
                else
                {
                        inputState[3] = 0;
                }

                
                

                //Movement Direction
                if (_direction == Vector2.up)
                {
                        inputState[6] = 1;
                }
                else if (_direction == Vector2.right)
                {
                        inputState[5] = 1;
                }
                else if (_direction == Vector2.down)
                {
                        inputState[6] = -1;
                }
                else if (_direction == Vector2.left)
                {
                        inputState[5] = -1;
                }

                var state_string = "";
                for (int i = 0; i<7;i++)
                { 
                        state_string = $"{state_string}{inputState[i]}";
                }
                // update movement by direction
                int direction_move;
                dic_drl.TryGetValue(state_string, out direction_move);

                if (direction_move == 0)
                {       
                        if (_direction == Vector2.right)
                        {
                            if (((_segments.Count == 1)|| (_segments[0].position - _segments[1].position != Vector3.down)))
                                                {_direction = Vector2.up;}
                        }
                        else if (_direction == Vector2.down)
                        {
                            if (((_segments.Count == 1)|| (_segments[0].position - _segments[1].position != Vector3.left)))
                                                {_direction = Vector2.right;}
                        }
                        else if (_direction == Vector2.left)
                        {
                            if (((_segments.Count == 1)|| (_segments[0].position - _segments[1].position != Vector3.up)))
                                                {_direction = Vector2.down;}
                        }
                        else if (_direction == Vector2.up)
                        {
                            if (((_segments.Count == 1)|| (_segments[0].position - _segments[1].position != Vector3.right)))
                                                {_direction = Vector2.left;}
                        }

                }

                else if (direction_move == 2)
                {
                        if (_direction == Vector2.right)
                        {
                            if (((_segments.Count == 1)|| (_segments[0].position - _segments[1].position != Vector3.up)))
                                                {_direction = Vector2.down;}
                        }
                        else if (_direction == Vector2.down)
                        {
                            if (((_segments.Count == 1)|| (_segments[0].position - _segments[1].position != Vector3.right)))
                                                {_direction = Vector2.left;}
                        }
                        else if (_direction == Vector2.left)
                        {
                            if (((_segments.Count == 1)|| (_segments[0].position - _segments[1].position != Vector3.down)))
                                                {_direction = Vector2.up;}
                        }
                        else if (_direction == Vector2.up)
                        {
                            if (((_segments.Count == 1)|| (_segments[0].position - _segments[1].position != Vector3.left)))
                                                {_direction = Vector2.right;}
                        }
                }

        }

        private void FixedUpdate()
        {
                if ((updateCount%10)%updateFrequence == 0)
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
                this.lives = 1;
                this.points = 0;
                this.updateFrequence = 10;
                pointText.text = $"ENGINE POWER BLOCK: {points - lives + 1} \nBATTERING RAM BLOCK: {lives - 1}\nPoints: {points}";

                this.transform.position = new Vector3(10.0f, 0.0f,0.0f);

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

                else if (other.tag == "snakeIABody")
                {
                        ResetState();
                }

                else if (other.tag == "snakeBody") {
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
                else if (other.tag == "lifeFood")
                {
                        if (updateFrequence < 10)
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

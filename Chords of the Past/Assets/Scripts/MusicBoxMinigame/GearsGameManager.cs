    using System;
    using UnityEngine;
    using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GearsGameManager : MonoBehaviour
    {
        public InputSystem_Actions_Nick inputControls;
        public GameObject Gear;
        public GameObject canvas;
        public float intensity = 5f;
        public float cursorMax = 5.0f;
        public float cursorMin = -5.0f;
        public bool hasWon = false;
        
        private Vector3 cursor = new Vector3(-3.0f, -3.0f, 0);

        //create a new input action for each different key
        private InputAction playKey;
        private InputAction stopKey;
        private InputAction slider;
        private InputAction leftKey;
        private InputAction rightKey;
        private InputAction resetKey;

        private void Start()
        {
            Gear = Instantiate(Gear, cursor, Quaternion.identity);
        }
        private void Update()
        {
            Gear.transform.position = Vector3.MoveTowards(Gear.transform.position, cursor, 1.0f);
            
            if (!hasWon)
            {
                 if (Input.GetKey(KeyCode.W) && cursor.y < cursorMax) 
                {
                    cursor = new Vector3(cursor.x, cursor.y + 0.025f + Time.deltaTime, cursor.z);
                } else if (Input.GetKey(KeyCode.S) && cursor.y > cursorMin)
                {
                    cursor = new Vector3(cursor.x, cursor.y - 0.025f - Time.deltaTime, cursor.z);
                }
            }
        }

        public void PlaceGear()
        {
            if (Gear.GetComponent<Gear>().attemptToPlace())
            {
                hasWon = CheckWinCondition();
                if (!hasWon)
                {
                    Gear = Instantiate(Gear, cursor, Quaternion.identity);
                } else
                {
                    canvas.SetActive(true);
                    OnDisable();
                }
            }
        }

        public bool CheckWinCondition()
        {
            GameObject[] notches = GameObject.FindGameObjectsWithTag("Notch");
            foreach (GameObject obj in notches)
            {
                if (obj.GetComponent<Notch>().correctSolution == false)
                {
                    return false;
                }
            }
            return true;
        }

        

        private void Awake()
        {
            //initialize the input system
            inputControls = new InputSystem_Actions_Nick();
        }

        private void OnEnable()
        {
            playKey = inputControls.GearsPuzzle.Play;
            playKey.Enable();
            //register to the event
            playKey.performed += PlayKey;

            stopKey = inputControls.GearsPuzzle.Stop;
            stopKey.Enable();
            //register to the event
            stopKey.performed += StopKey;

            //enable the command (input system.map.command)
            slider = inputControls.GearsPuzzle.Slider;
            slider.Enable();
            //register to the event
            slider.performed += Slider;

            rightKey = inputControls.GearsPuzzle.Right;
            rightKey.Enable();
            //register to the event
            rightKey.performed += RightKey;

            leftKey = inputControls.GearsPuzzle.Left;
            leftKey.Enable();
            //register to the event
            leftKey.performed += LeftKey;

            resetKey = inputControls.GearsPuzzle.Reset;
            resetKey.Enable();
            //register to the event
            resetKey.performed += ResetKey;
    }

        private void OnDisable()
        {
            playKey.Disable();
            stopKey.Disable();
            slider.Disable();
            leftKey.Disable();
            rightKey.Disable();
            resetKey.Disable();
        }

        //the event I used to register it 
        public void PlayKey(InputAction.CallbackContext context)
        {
            Debug.Log("Pressed the play key");
            if (Gear.GetComponent<Gear>().type < 2)
            {
                Gear.GetComponent<Gear>().type++;
            } else
            {
                Gear.GetComponent<Gear>().type = 0;
            }
        }
        public void StopKey(InputAction.CallbackContext context)
        {
            Debug.Log("Pressed the stop key");
            PlaceGear();
        }

        public void RightKey(InputAction.CallbackContext context)
        {
            Debug.Log("Pressed the right key");
            if (cursor.x >= cursorMax) cursor.x = cursorMin;
            else cursor.x += 1;
        }

        public void LeftKey(InputAction.CallbackContext context)
        {
            Debug.Log("Pressed the left key");
            if (cursor.x <= cursorMin) cursor.x = cursorMax;
            else cursor.x -= 1;
        }

        public void ResetKey(InputAction.CallbackContext context)
        {
            Debug.Log("Pressed the reset key");
            Gear.GetComponent<Gear>().attemptToRemove();
        }


    public void Slider(InputAction.CallbackContext context)
        {   
            Debug.Log(context.ReadValue<float>());
            cursor = new Vector3(cursor.x, -3 + context.ReadValue<float>() * intensity, cursor.z);
            //Debug.Log("Slider1 being used");
        }
    }
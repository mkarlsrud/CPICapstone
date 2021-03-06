using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Timers;

public class RunningPresentationScript : MonoBehaviour 
{
	private RunningListener listener;
    public CharacterController player;
    public Graphics g;
    Timer timer;

    public float turnSpeed = 50f;
	public float runSpeed = 4.5f;
	
	void Start() 
	{
		// hide mouse cursor
		Screen.showCursor = false;
        
			
		// get the gestures listener
		listener = Camera.main.GetComponent<RunningListener>();
        timer = new System.Timers.Timer();
        timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
        timer.Interval = 3000;
        timer.Enabled = false;
    }

    void Update()
    {
        if (KinectGestures.running) {//listener.IsRunning())
			timer.Interval = 250;
			timer.Enabled = true;

		}
		if (KinectGestures.hurdling) {

		}
        if (timer.Enabled)// && !player.collider.)
        {
			CharacterMotor m = player.GetComponent<CharacterMotor>();
			//if(m.grounded)
			m.inputMoveDirection = transform.forward;


          //  player.transform.Translate(runSpeed * Time.deltaTime * Vector3.forward);

			//player.GetComponent<CharacterMotor>().movement.velocity = 
			//	runSpeed*transform.forward;

			// +
					//Vector3.up*player.GetComponent<CharacterMotor>().movement.velocity.y /2;
			


        }


        //if (listener.IsJumping())
        //{
            //motor.jumping.enabled = true;
                //jumping.jumpDir = Vector3.Slerp(Vector3.up, groundNormal, jumping.steepPerpAmount);
            
                //player.transform.Translate(150 * Time.deltaTime * (player.transform.forward + player.transform.up));
        
            //float jumpSpeed = 200.0F;
            //float gravity = 20.0F;
            //Vector3 moveDirection = Vector3.zero;
            //moveDirection.y = jumpSpeed;
            //moveDirection.y -= gravity * Time.deltaTime;
            //player.Move(moveDirection * Time.deltaTime);
        //}



        //TURN LEFT
        if (KinectGestures.turnLeft)//listener.IsLeftTurn())
        {
            if (KinectGestures.turnRight)
            {
                KinectGestures.hurdling = true;
            }
            else
                player.transform.Rotate(0, turnSpeed, 0);
        }
        //TURN RIGHT
        else if (KinectGestures.turnRight)//listener.IsRightTurn())
        {
            player.transform.Rotate(0, -turnSpeed, 0);
            //player.transform.Rotate(Vector3.down, -turnSpeed * Time.deltaTime);
        }
    }

    private void OnTimedEvent(object source, ElapsedEventArgs e)
    {
        //Debug.Log("timer event");

        timer.Enabled = false;
    }

}
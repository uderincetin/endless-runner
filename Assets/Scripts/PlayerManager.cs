using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

	private const float LANE_DISTANCE = 2.0f;
	private const float TURN_SPEED = 0.05f;

	//Booleanlar
	private bool isRunning = false;

	//Animasyon
	private Animator anim;
	
	//Hareket
	private CharacterController controller;
	public float jumpForce = 4.0f;
	private float gravity = 12.0f;
	private float verticalVelocity;
	private int desiredLane = 1;


	//Hýz

	public float originalSpeed = 7.0f;
	public float speed;
	public float speedIncreaseLastTick;
	public float speedIncreaseTime = 2.5f;
	public float speedIncreaseAmount = 0.1f;
	
	private void Start()
	{
		controller = GetComponent<CharacterController>();
		anim = GetComponent<Animator>();
	}
	private void Update()
	{
		if (!isRunning)
			return;

		if(Time.time - speedIncreaseLastTick > speedIncreaseTime)
		{
			speedIncreaseLastTick = Time.time;
			speed += speedIncreaseAmount;
			GameManager.Instance.UpdateModifier(speed);

		}

		//Lane Hareket
		if (MobileInput.Instance.SwipeLeft)
			MoveLane(false);
		if (MobileInput.Instance.SwipeRight)
			MoveLane(true);


		Vector3 targetPosition = transform.position.z * Vector3.forward;
		if (desiredLane == 0)
			targetPosition += Vector3.left * LANE_DISTANCE;
		else if (desiredLane == 2)
			targetPosition += Vector3.right * LANE_DISTANCE;

		Vector3 moveVector = Vector3.zero;
		moveVector.x = (targetPosition - transform.position).normalized.x * speed;

		//Zýplama
		bool isGrounded = IsGrounded();

		if (isGrounded)
		{
			verticalVelocity = -0.1f;

			if (MobileInput.Instance.SwipeUp)
			{
				verticalVelocity = jumpForce;
			}
		}

		else
		{
			verticalVelocity -= (gravity * Time.deltaTime);

			if (MobileInput.Instance.SwipeDown)
			{
				verticalVelocity = -jumpForce;
			}
		}

		moveVector.y = verticalVelocity;
		moveVector.z = speed;

		controller.Move(moveVector * Time.deltaTime);

		Vector3 dir = controller.velocity;
		if(dir != Vector3.zero) 
		{
			dir.y = 0;
			transform.forward = Vector3.Lerp(transform.forward, dir, TURN_SPEED);
		}
	}

	private void MoveLane(bool goingRight)
	{
		desiredLane += (goingRight) ? 1 : -1;
		desiredLane = Mathf.Clamp(desiredLane, 0, 2);
	}

	private bool IsGrounded()
	{
		Ray groundRay = new Ray(new Vector3(controller.bounds.center.x, (controller.bounds.center.y - controller.bounds.extents.y) +0.05f, controller.bounds.center.z), Vector3.down);
		Debug.DrawRay(groundRay.origin, groundRay.direction, Color.cyan,1.0f);

		return Physics.Raycast(groundRay, 0.2f + 0.1f) ;
	} 

	public void StartGame()
	{
		isRunning = true;
		anim.SetTrigger("Active");
	}

}

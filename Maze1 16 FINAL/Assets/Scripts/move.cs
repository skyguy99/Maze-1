using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class move : MonoBehaviour
{
	public static bool moveEnabled = false;
	public static bool moveBegun = false;

	public Camera cam;
	public float groundCheckDistance = 0.01f; // distance for checking if the controller is grounded ( 0.01f seems to work best for this )
	public float stickToGroundHelperDistance = 0.5f; // stops the character
	public float slowDownRate = 20f; // rate at which the controller comes to a stop when there is no input
	private Rigidbody m_RigidBody;
	private CapsuleCollider m_Capsule;
	private float m_YRotation;
	private Vector3 groundContactNormal;
	public float CurrentTargetSpeed = 0f;

	private CardboardHead head;

	public static bool firstMapFound = false;
	public static int numMapFound = 0;

	public Vector3 Velocity {
		get { return m_RigidBody.velocity; }
	}
	void Update()
	{
		
	}

	private void Start ()
	{

		head = Camera.main.GetComponent<StereoController>().Head;
		m_RigidBody = GetComponent<Rigidbody> ();
		m_Capsule = GetComponent<CapsuleCollider> ();
		//m_RigidBody.transform.rotation = Quaternion.Euler(8.0f,5.0f,0.0f);
	}

	private void FixedUpdate ()
	{

		if (moveEnabled) {
			Vector2 input = GetInput ();
			GroundCheck ();
			if ((Mathf.Abs (input.x) > float.Epsilon || Mathf.Abs (input.y) > float.Epsilon)) {
				//Debug.Log("got input "+input);
				// always move along the camera forward as it is the direction that it being aimed at
				Vector3 desiredMove = cam.transform.forward * input.y + cam.transform.right * input.x;
				desiredMove = Vector3.ProjectOnPlane (desiredMove, groundContactNormal).normalized;

				desiredMove.x = desiredMove.x * CurrentTargetSpeed;
				desiredMove.z = desiredMove.z * CurrentTargetSpeed;
				desiredMove.y = desiredMove.y * CurrentTargetSpeed;
				if (m_RigidBody.velocity.sqrMagnitude <
				   (CurrentTargetSpeed * CurrentTargetSpeed)) {
					m_RigidBody.AddForce (desiredMove, ForceMode.Impulse);
					Debug.Log ("moving!");
					moveBegun = true;
				}
				StickToGroundHelper ();
			}
		}
	}

	private void StickToGroundHelper ()
	{
		RaycastHit hitInfo;
		if (Physics.SphereCast (transform.position, m_Capsule.radius, Vector3.down, out hitInfo,
			((m_Capsule.height / 2f) - m_Capsule.radius) +
			stickToGroundHelperDistance)) {
			if (Mathf.Abs (Vector3.Angle (hitInfo.normal, Vector3.up)) < 85f) {
				m_RigidBody.velocity = Vector3.ProjectOnPlane (m_RigidBody.velocity, hitInfo.normal);
			}
		}
	}
	/// sphere cast down just beyond the bottom of the capsule to see if the capsule is colliding round the bottom
	private void GroundCheck()
	{
		RaycastHit hitInfo;
		if (Physics.SphereCast(transform.position, m_Capsule.radius, Vector3.down, out hitInfo,
			((m_Capsule.height/2f) - m_Capsule.radius) + groundCheckDistance))
		{
			groundContactNormal = hitInfo.normal;
		}
		else
		{
			groundContactNormal = Vector3.up;
		}
	}

	//Collision w/ cube
	void OnCollisionStay(Collision col){

		if (col.gameObject.tag == "blueCube" && GameLogic.timerOn && GameLogic.gameStarted) {

			//Destroy cube
			Destroy(col.gameObject);
			GameLogic.score++;
			Debug.Log ("Got a cube!");

		}
		if (col.gameObject.tag == "mazeCube" && GameLogic.timerOn && GameLogic.gameStarted) {

			//Destroy cube
			firstMapFound = true;

			Destroy(col.gameObject);
			GameLogic.mazeCreds++;
			Debug.Log ("Got a maze cube!");
			GameLogic.totalScore += 10; //overall score

		}
	}

	private Vector2 GetInput ()
	{
		float x,y;
		if (Input.GetMouseButton(0)){
			x=0;
			y = 1;
		} else {
			x = Input.GetAxis("Horizontal");
			y = Input.GetAxis("Vertical");
		}
		Vector2 input = new Vector2(x,y);
		return input;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


	Vector3 velocity;
	Rigidbody rb;  

	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	

	public void Move (Vector3 _velocity) {
		velocity = _velocity;
	}
		

	void FixedUpdate() {
		rb.MovePosition (rb.position + velocity * Time.fixedDeltaTime);
		Rotation ();
	}

	public void Rotation()
	{
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);  //Ray from mouse cursor in direction of camera 
		Plane groundPlane = new Plane (Vector3.up, Vector3.zero);
		float camRayLength;

		if (groundPlane.Raycast (camRay, out camRayLength)) {
			Vector3 point = camRay.GetPoint (camRayLength);

			Debug.DrawLine (camRay.origin, point, Color.red);
			LookAt (point);
		}
		
	}

	void LookAt(Vector3 lookPoint)
	{
		Vector3 heightCorrectedPoint = new Vector3 (lookPoint.x, transform.position.y, lookPoint.z);
		transform.LookAt (heightCorrectedPoint);
	}
}

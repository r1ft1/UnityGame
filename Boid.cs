using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour {

	private Rigidbody rb;
	public BoidManager flock;

	//Vector3 position;
	//Vector3 velocity;
	//Vector3 acceleration;

	//Vector3 centerOfMass;

	void Start () {
		rb = gameObject.GetComponent <Rigidbody> ();
		//flock = GetComponent <BoidManager> ();
	}

	void Seek () {
		
	}

	void Update () {
		//rb.velocity = new Vector3 (0, 0, 0);
		Vector3 vel = Cohesion() * Time.deltaTime + Separation() * Time.deltaTime + Alignment() * Time.deltaTime;
		rb.position += vel;
	}

	void FixedUpdate() {
	
	}

	Vector3 Cohesion() {
		float totalXPos = 0;
		float totalZPos = 0;
		for (int i = 0; i < flock.Boids.Length; i++) {
			if (gameObject != flock.Boids [i].gameObject) {
				totalXPos += flock.Boids [i].gameObject.transform.position.x;
				totalZPos += flock.Boids [i].gameObject.transform.position.z;
			}
		}
		Vector3 centerOfMass = new Vector3 (totalXPos / flock.Boids.Length, totalZPos / flock.Boids.Length, 0);
		return (centerOfMass - rb.position) / 100;
	}

	Vector3 Separation() {
		Vector3 c = new Vector3 (0, 0, 0);

		for (int i = 0; i < flock.Boids.Length; i++) {
			if (gameObject != flock.Boids [i].gameObject) {
				if (Vector3.Magnitude(gameObject.transform.position - flock.Boids [i].gameObject.transform.position) < 1f)
					c -= (transform.position - flock.Boids [i].transform.position);
			}
		}
		return c;
	}

	Vector3 Alignment() {
		Vector3 PVelocity = new Vector3 (0,0,0);
		for (int i = 0; i < flock.Boids.Length; i++) {
			if (gameObject != flock.Boids [i].gameObject) {
				PVelocity += flock.Boids [i].GetComponent<Boid> ().rb.velocity;
			}
			PVelocity = PVelocity / (flock.Boids.Length - 1);
		}
		return (PVelocity - rb.velocity) / 8;
	}

}

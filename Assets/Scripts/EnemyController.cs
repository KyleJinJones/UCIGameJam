using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EmenyStatus {
	Normal, Chasing, Backing
}

public class EnemyController : MonoBehaviour {
	public float speed;
	public float sightDistance;
	public float sightAngle;
	public float idleThreasholdDistance;
	public float chaseThreasholdDistance;
	public bool facingLeftAtBeginning;
	
	private Transform player;
	private SpriteRenderer spriteRenderer;
	private EmenyStatus status = EmenyStatus.Normal;
	private Vector3 defaultPosition;
	private bool facingLeft;

	private void ChangeDirection() {
		spriteRenderer.flipY = !spriteRenderer.flipY;
		facingLeft = !facingLeft;
	}

	private void Move() {
		if (facingLeft) {
			transform.Translate(-speed * Time.deltaTime, 0f, 0f);
		}
		else {
			transform.Translate(speed * Time.deltaTime, 0f, 0f);
		}
	}

	private void DefaultMove() {
		if (Vector3.Distance(transform.position, defaultPosition) > idleThreasholdDistance) {
			if (facingLeft != transform.position.x - defaultPosition.x > 0) {
				ChangeDirection();
			}
		}
		Move();
	}

	private void Chase() {
		if (facingLeft != player.transform.position.x - transform.position.x < 0) {
			ChangeDirection();
		}
		Move();
	}

	private void Back() {
		if (facingLeft != defaultPosition.x - transform.position.x < 0) {
			ChangeDirection();
		}
		Move();
	}

	private bool PlayerInSight() {
		Vector3 difference = transform.position - player.position;
		if (difference.magnitude < sightDistance) {
			float dx = player.transform.position.x - transform.position.x;
			float dy = player.transform.position.y - transform.position.y;
			float degree = Mathf.Rad2Deg * Mathf.Atan(dy/dx);
			if (facingLeft && dx < 0 && degree > -sightAngle && degree < sightAngle) {
//				Debug.Log("in sight");
				return true;
			} 
			if (!facingLeft && dx > 0 && degree > -sightAngle && degree < sightAngle) {
//				Debug.Log("in sight");
				return true;
			}
		}
//		Debug.Log("not in sight");
		return false;
	}

	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();
		defaultPosition = transform.position;
		player = GameObject.FindGameObjectWithTag("Player").transform;
		if (facingLeftAtBeginning) {
			ChangeDirection();
		}
	}
	
	void Update () {
		if (Vector3.Distance(defaultPosition, transform.position) < idleThreasholdDistance) {
			status = EmenyStatus.Normal;
		}
		if (PlayerInSight()) {
			status = EmenyStatus.Chasing;
		} 
		if (Vector3.Distance(defaultPosition, transform.position) >= chaseThreasholdDistance) {
			status = EmenyStatus.Backing;
		} 
		

		Debug.Log(status);
		switch (status) {
				case EmenyStatus.Normal:
					DefaultMove();
					break;
				case EmenyStatus.Chasing:
					Chase();
					break;
				case EmenyStatus.Backing:
					Back();
					break;
		}
	}
}

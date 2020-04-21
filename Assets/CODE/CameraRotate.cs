
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour {
	public Transform target;
	public float distance = 5;
	public float Speed = 30.0f;

	public float yMinLimit = -0f;
	public float yMaxLimit = 20f;

	public float smoothTime = 2f;

	public float rotationYAxis = 0.0f;
	public float rotationXAxis = 0.0f;

	public float velocityX = 0.0f;
	public float velocityY = 0.0f;

	public float minspeedx = .2f;
	private Touch touch;


	Vector3 CamPosition = new Vector3( 0,150,450);
	Vector3 CamRotation = new Vector3(0,180,000);

	void Awake(){
		setValuse ();
	}
	void Start() {
		Vector3 angles = transform.eulerAngles;
		rotationYAxis = angles.y;
		rotationXAxis = angles.x;
	}
	void LateUpdate() {
		{
			if (target) {
				//#if UNITY_EDITOR
//				if (Input.GetMouseButton (0)) {
//					velocityX += Speed * Input.GetAxis ("Mouse X") * 0.02f;
//					velocityY += Speed * Input.GetAxis ("Mouse Y") * 0.02f;
//					minspeedx = (velocityX > 10) ? .2f : -.2f;
//				}
				//#endif
				#if !UNITY_EDITOR
				if (Input.touchCount > 0 && (Input.GetTouch (0).phase == TouchPhase.Began || Input.GetTouch (0).phase == TouchPhase.Moved)) { //Input.touchCount == 1 && 
					touch = Input.GetTouch (0);
					velocityX += (Speed * touch.deltaPosition.x) / (Screen.width);
					velocityY += Speed * touch.deltaPosition.y / (Screen.height);
					minspeedx = (velocityX > 0) ? .2f : -.2f;
				}
				#else
				if (Input.GetMouseButton (0)) {
					velocityX += Speed * Input.GetAxis ("Mouse X") * 0.02f;
					velocityY += Speed * Input.GetAxis ("Mouse Y") * 0.02f;
					minspeedx = (velocityX > 10) ? .0f : -.0f;
				}
				#endif
				rotationYAxis += velocityX;
				rotationXAxis -= velocityY;

				rotationXAxis = ClampAngle (rotationXAxis, yMinLimit, yMaxLimit);
				Quaternion toRotation = Quaternion.Euler (rotationXAxis, rotationYAxis, 0);
				Vector3 negDistance = new Vector3 (0.0f, 2.0f, -distance);
				Vector3 position = toRotation * negDistance + target.position;
				velocityX = Mathf.Lerp (velocityX, minspeedx, Time.deltaTime * smoothTime);
				velocityY = Mathf.Lerp (velocityY, 0, Time.deltaTime * smoothTime);
				transform.rotation = toRotation;
				transform.position = position;
			}
		}
	}


	public static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360F)
			angle += 360F;
		if (angle > 360F)
			angle -= 360F;
		return Mathf.Clamp(angle, min, max);
	}

	public void setValuse(){
		transform.position = CamPosition;
		transform.rotation = Quaternion.Euler (CamRotation.x,CamRotation.y,CamRotation.z);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
	 float ang = 180;
	 public float Speed = 30;
	 float velocityX = 0.0f;
	 float smoothTime = 1f;
	 float minspeedx = 0;
	
	void Start()
    {
		

	}

    void Update()
    {
#if !UNITY_EDITOR
				//if (Input.touchCount > 0 && (Input.GetTouch (0).phase == TouchPhase.Began || Input.GetTouch (0).phase == TouchPhase.Moved)) {
                //  Speed = 10;
				//	velocityX += Speed * Input.GetAxis("Mouse X") * 0.008f;
				//}
        velocityX =0;
        if (Input.touchCount > 0) {
            if(Input.GetTouch (0).phase == TouchPhase.Moved)
				velocityX += Speed * Input.GetAxis("Mouse X") * 0.08f;
           
                
		}


#else
		velocityX = 0;
		if (Input.GetMouseButton(0))
		{
			//velocityX += Speed * Input.GetAxis("Mouse X") * 0.02f;
			velocityX = Speed * Input.GetAxis("Mouse X");
		}
		
#endif
		if (M.AppScreen == M.APP_HOME)
		{

			Vector3 localAngle = transform.localEulerAngles;
			localAngle.y -= velocityX;// ;
			transform.localEulerAngles = localAngle;


			//velocityX = Mathf.Lerp(velocityX, minspeedx, Time.deltaTime * smoothTime);
			//ang -= velocityX;
			//Quaternion toRotation = Quaternion.Euler(0, ang, 0);
			//transform.rotation = toRotation;
		}
        else
        {
            if(M.direction == 0)
			    transform.rotation = Quaternion.Euler(0, 180, 0);
            else
            {
				if (M.direction == 1)
					transform.rotation = Quaternion.Euler(0, 270, 0);
				if (M.direction == 2)
					transform.rotation = Quaternion.Euler(0, 90, 0);
				if (M.direction == 3)
					transform.rotation = Quaternion.Euler(0, 180, 0);

			}

			//Debug.Log("M.direction = " + M.direction);

		}

	}
}

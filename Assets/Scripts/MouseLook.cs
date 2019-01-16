using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Camera-Control/Mouse Look")]
public class MouseLook : MonoBehaviour {

	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;
	public float minimumX = -360F;
	public float maximumX = 360F;
	public float minimumY = -60F;
	public float maximumY = 60F;

	private float rotationX = 0F;
	private float rotationY = 0F;
	Quaternion originalRotation;

	private Quaternion RotateMouseX(){
		rotationX += Input.GetAxis("Mouse X") * sensitivityX;
		rotationX = ClampAngle (rotationX, minimumX, maximumX);
		Quaternion xQuaternion = Quaternion.AngleAxis (rotationX, Vector3.up);
		return xQuaternion;
	}

	private Quaternion RotateMouseY(){
		rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
		rotationY = ClampAngle (rotationY, minimumY, maximumY);
		Quaternion yQuaternion = Quaternion.AngleAxis (-rotationY, Vector3.right);
		return yQuaternion;
	}

	void Update ()
	{
		Quaternion q = Quaternion.identity;
		if (axes == RotationAxes.MouseX || axes == RotationAxes.MouseXAndY) {
			q *= RotateMouseX ();
		}
		if (axes == RotationAxes.MouseY || axes == RotationAxes.MouseXAndY) {
			q *= RotateMouseY ();
		}
		transform.localRotation = originalRotation * q;
	}

	void Start ()
	{
		// Make the rigidbody not change rotation
		if (GetComponent<Rigidbody>())
			GetComponent<Rigidbody>().GetComponent<Rigidbody>().freezeRotation = true;
		originalRotation = transform.localRotation;
	}

	public static float ClampAngle (float angle, float min, float max)
	{
		if (angle < -360F)
			angle += 360F;
		if (angle > 360F)
			angle -= 360F;
		return Mathf.Clamp (angle, min, max);
	}
}

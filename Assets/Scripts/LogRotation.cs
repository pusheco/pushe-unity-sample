using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogRotation : MonoBehaviour {

	[Serializable]
	class RotationElement
	{
#pragma warning disable 0649
		public float Speed;
		public float Duration;
		
#pragma warning restore 0649
	}

	[SerializeField]
	private RotationElement[] rotationPattern;

	private WheelJoint2D wheelJoint2D;
	private JointMotor2D motor;

	private void Awake()
	{
		wheelJoint2D = GetComponent<WheelJoint2D>();
		motor = new JointMotor2D();
		StartCoroutine("PlayRotationPattern");
	}

	private IEnumerator PlayRotationPattern()
	{
		int rotationIndex = 0;
		while (true)
		{
			yield return new WaitForFixedUpdate();

			motor.motorSpeed = rotationPattern[rotationIndex].Speed;
			motor.maxMotorTorque = 10000;
			wheelJoint2D.motor = motor;
			
			yield return new WaitForSecondsRealtime(rotationPattern[rotationIndex].Duration);
			rotationIndex++;
			rotationIndex = rotationIndex < rotationPattern.Length ? rotationIndex : 0;
		}
	}
}

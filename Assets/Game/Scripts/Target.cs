using System;
using UnityEngine;

public class Target : MonoBehaviour
{
	private Mug _currentStandingMug;
	
	private void OnTriggerEnter (Collider other)
	{
		Mug mug = other.transform.GetComponent<Mug>();

		if (mug)
		{
			if (mug.IsSliding)
			{
				_currentStandingMug = mug;
			}
		}
	}

	private void OnTriggerExit (Collider other)
	{
		Mug mug = other.transform.GetComponent<Mug>();

		if (mug == _currentStandingMug)
		{
			if (mug.IsSliding)
			{
				_currentStandingMug = mug;
			}
		}
	}
}
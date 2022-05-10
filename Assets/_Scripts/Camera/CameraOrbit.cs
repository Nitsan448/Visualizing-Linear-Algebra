using UnityEngine;
using System;

public class CameraOrbit : MonoBehaviour
{
	public static Action XAxisCircled;

	[SerializeField] private float _cameraDistance = 10f;
    [SerializeField] private float _mouseSensitivity = 4f;
	[SerializeField] private float _scrollSensitvity = 2f;
	[SerializeField] private float _orbitDampening = 10f;
	[SerializeField] private float _scrollDampening = 6f;
    [SerializeField] private Vector2 _distanceRange = new Vector2(1.5f, 10f);

	private Quaternion _quaternion;
    private Vector3 _localRotation;
	private float _startingCameraDistance;
	private bool _allowRotation;


	private void Start()
	{
		_localRotation = new Vector3(-45, 20, 0);
		_startingCameraDistance = _cameraDistance;
	}

	void LateUpdate()
    {
		if (Input.GetMouseButtonDown(0))
		{
			_allowRotation = true;
		}
		if (Input.GetMouseButtonUp(0))
		{
			_allowRotation = false;
		}
        if (Input.GetMouseButtonDown(2))
        {
			//Allow moving around
			//transform.parent.position = new Vector3(-1, 0, 0);
        }

		UpdateRotation();

		UpdateZoom();

		UpdateCameraTransform();
    }

	private void UpdateRotation()
	{
		if (_allowRotation && (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0))
		{
			_localRotation.x += Input.GetAxis("Mouse X") * _mouseSensitivity;
			_localRotation.y -= Input.GetAxis("Mouse Y") * _mouseSensitivity;
		}
	}

	private void UpdateZoom()
	{
		if (Input.GetAxis("Mouse ScrollWheel") != 0f)
		{
			float ScrollAmount = Input.GetAxis("Mouse ScrollWheel") * _scrollSensitvity;

			//Scroll faster the farther we are from the object
			ScrollAmount *= (_cameraDistance * 0.3f);

			_cameraDistance += ScrollAmount * -1f;
			_cameraDistance = Mathf.Clamp(_cameraDistance, _distanceRange.x, _distanceRange.y);
		}
	}

	private void UpdateCameraTransform()
	{
		float previousYRotation = transform.parent.eulerAngles.y;

		_quaternion = Quaternion.Euler(_localRotation.y, _localRotation.x, 0);
		transform.parent.localRotation = Quaternion.Lerp(transform.parent.localRotation, _quaternion, Time.deltaTime * _orbitDampening);

		//Only update if camera changed
		if (transform.localPosition.z != _cameraDistance * -1f)
		{
			transform.localPosition = new Vector3(0f, 0f, Mathf.Lerp(transform.localPosition.z, _cameraDistance * -1f, Time.deltaTime * _scrollDampening));
		}

		float newYRotation = transform.parent.eulerAngles.y;
		CheckIfXAxisWasCircled(previousYRotation, newYRotation);
	}

	private void CheckIfXAxisWasCircled(float previousYRotation, float newYRotation)
	{
		if (IsBehindXAxis(previousYRotation) ^ IsBehindXAxis(newYRotation))
		{
			XAxisCircled?.Invoke();
		}

	}

	private bool IsBehindXAxis(float yRotation)
	{
		return yRotation > 90 && yRotation < 270;
	}

	public void ResetCamera()
	{
		//Called from reset camera button.
		transform.parent.localRotation = Quaternion.Euler(0, 0, 0);
		_localRotation = Vector3.zero;
		_cameraDistance = _startingCameraDistance;
		transform.localPosition = new Vector3(0f, 0f, Mathf.Lerp(transform.localPosition.z, _cameraDistance * -1f, Time.deltaTime * _scrollDampening));
	}
}
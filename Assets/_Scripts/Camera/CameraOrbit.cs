using UnityEngine;

public class CameraOrbit : MonoBehaviour
{

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
		_quaternion = Quaternion.Euler(_localRotation.y, _localRotation.x, 0);
		transform.parent.localRotation = Quaternion.Lerp(transform.parent.localRotation, _quaternion, Time.deltaTime * _orbitDampening);

		//Only update if camera changed
		if (transform.localPosition.z != _cameraDistance * -1f)
		{
			transform.localPosition = new Vector3(0f, 0f, Mathf.Lerp(transform.localPosition.z, _cameraDistance * -1f, Time.deltaTime * _scrollDampening));
		}
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
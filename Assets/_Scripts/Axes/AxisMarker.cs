using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AxisMarker : MonoBehaviour
{
    [SerializeField] private GameObject _axisMarkPrefab;
    [SerializeField] private int markingIncrement = 1;
    [SerializeField] private Vector3 _positionTextOffset = new Vector3(0, 0.05f, 0);
    [SerializeField] private eAxes _axis;
    [SerializeField] private Transform _cameraParent;

    private LineRenderer _line;
    private bool _markingTextFlipped= false;
    // Start is called before the first frame update
    void Start()
    {
        _line = GetComponent<LineRenderer>();
        MarkAxis();
    }

	private void OnEnable()
	{
        if(_axis == eAxes.X)
		{
            CameraOrbit.XAxisCircled += FlipMarkingsTexts;
        }
    }

	private void OnDisable()
	{
        if (_axis == eAxes.X)
        {
            CameraOrbit.XAxisCircled += FlipMarkingsTexts;
        }
    }

	private void MarkAxis()
	{
        Vector3 startPoint = _line.GetPosition(0);
        Vector3 endPoint = _line.GetPosition(1);

        switch (_axis)
        {
            case eAxes.X:
                for (float i = startPoint.x; i < endPoint.x; i += markingIncrement)
                {
                    CreateMarker(new Vector3(i, -0.05f, 0), new Vector3(i, 0.05f, 0));
                }
                break;

            case eAxes.Y:
                for (float i = startPoint.y; i < endPoint.y; i += markingIncrement)
                {
                    CreateMarker(new Vector3(-0.05f, i, 0), new Vector3(0.05f, i, 0));
                }
                break;

            case eAxes.Z:
                for (float i = startPoint.z; i < endPoint.z; i += markingIncrement)
                {
                    CreateMarker(new Vector3(0, -0.05f, i), new Vector3(0, 0.05f, i));
                }
                break;
        }
    }

    private void CreateMarker(Vector3 startPoint, Vector3 endPoint)
	{
        GameObject axisMark = Instantiate(_axisMarkPrefab, gameObject.transform);
        LineRenderer axisMarkLine = axisMark.GetComponent<LineRenderer>();
        TextMeshPro positionText = axisMark.GetComponentInChildren<TextMeshPro>();
        axisMarkLine.SetPosition(0, startPoint);
        axisMarkLine.SetPosition(1, endPoint);
        positionText.transform.position = new Vector3(startPoint.x + _positionTextOffset.x, 
                                    endPoint.y + _positionTextOffset.y, startPoint.z + _positionTextOffset.z);
        if(_cameraParent.rotation.y < -0.5f)
		{
            positionText.rectTransform.eulerAngles = new Vector3(0, 180, 0);
		}

        switch (_axis)
        {
            case eAxes.X:
                positionText.text = startPoint.x.ToString();
                break;

            case eAxes.Y:
                positionText.text = startPoint.y.ToString();
                break;

            case eAxes.Z:
                positionText.text = startPoint.z.ToString();
                break;
        }
    }

    private void FlipMarkingsTexts()
	{
        RectTransform[] markings = GetComponentsInChildren<RectTransform>();

        int newYRotation = 180;
		if (_markingTextFlipped)
		{
            newYRotation = 0;
		}

        foreach (RectTransform marking in markings)
		{
            marking.eulerAngles = new Vector3(0, newYRotation, 0);
        }
        _markingTextFlipped = !_markingTextFlipped;
	}
}

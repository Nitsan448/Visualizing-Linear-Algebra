using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorMouseController : MonoBehaviour
{
    public eAxes DontControlAxis { get; set; }
    public int ControlledVectorIndex { get; set; }

    private Ray _cameraToMouseRay;
    private Plane _planeToCastOn;

    private void Update()
	{
        if (Input.GetKey(KeyCode.Mouse1))
        {
            ControlVectorsWithMouse();
        }
    }

	private void ControlVectorsWithMouse()
    {
        Vector3 worldPosition = Vector3.zero;
        _cameraToMouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        SetPlaneToCastOn();

        if (_planeToCastOn.Raycast(_cameraToMouseRay, out float distance))
        {
            worldPosition = _cameraToMouseRay.GetPoint(distance);
        }

        if (worldPosition != Vector3.zero)
		{
            Managers.Vectors.Vectors[ControlledVectorIndex] = worldPosition;
            Managers.Vectors.UpdateResult();
        }
    }

    private void SetPlaneToCastOn()
	{
        Vector3 controlledVector = Managers.Vectors.Vectors[ControlledVectorIndex];
		switch (DontControlAxis)
		{
            case eAxes.X:
                _planeToCastOn = new Plane(Vector3.left, controlledVector);
                break;
            case eAxes.Y:
                _planeToCastOn = new Plane(Vector3.up, controlledVector);
                break;
            case eAxes.Z:
                _planeToCastOn = new Plane(Vector3.forward, controlledVector);
                break;
            default:
                _planeToCastOn = new Plane(_cameraToMouseRay.direction, 0);
                break;
        }
	}
}

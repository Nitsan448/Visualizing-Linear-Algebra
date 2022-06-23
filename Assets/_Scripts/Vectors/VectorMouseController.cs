using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorMouseController : MonoBehaviour
{
    [SerializeField] private bool _controlRedVector;

	private void Update()
	{
        if (Input.GetKey(KeyCode.Mouse1))
        {
            ControlVectorsWithMouse();
        }
    }

	private void ControlVectorsWithMouse()
    {
        Vector3 worldPosition = GetMousePositionInWorld();
        
        if (_controlRedVector)
        {
            Managers.Vectors.Vectors[0] = worldPosition;
        }
        else
        {
            Managers.Vectors.Vectors[1] = worldPosition;
        }
        Managers.Vectors.UpdateResult();
    }

    private Vector3 GetMousePositionInWorld()
	{
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(ray.direction, 0);
        float distance;
        Vector3 worldPosition = Vector3.zero;
        if(plane.Raycast(ray, out distance))
		{
            worldPosition = ray.GetPoint(distance);
		}

		return worldPosition;
    }

    public void ChangeControlledVector(int newVectorToControl)
	{
        if(newVectorToControl == 0)
		{
            _controlRedVector = true;
		}
		else
		{
            _controlRedVector = false;
		}
	}
}

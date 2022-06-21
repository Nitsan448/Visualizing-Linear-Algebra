using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorMouseController : MonoBehaviour
{
    [SerializeField] private bool controlFirstVector;

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

        if (controlFirstVector)
        {
            Managers.Vectors.Vectors[1] = worldPosition;
        }
        else
        {
            Managers.Vectors.Vectors[2] = worldPosition;
        }
        Managers.Vectors.UpdateResult();
    }

    private Vector3 GetMousePositionInWorld()
	{
        Plane plane = new Plane(Vector3.back, 0);
        float distance;
        Vector3 worldPosition = Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out distance))
        {
            worldPosition = ray.GetPoint(distance);
            return worldPosition;
        }
        plane = new Plane(Vector3.up, 0);
        worldPosition = Vector3.zero;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out distance))
        {
            worldPosition = ray.GetPoint(distance);
        }

        return worldPosition;
    }

    public void ChangeControlledVector(int newVectorToControl)
	{
        if(newVectorToControl == 0)
		{
            controlFirstVector = true;
		}
		else
		{
            controlFirstVector = false;
		}
	}
}

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
        Plane plane = new Plane(Vector3.back, 0);
        float distance;
        Vector3 worldPosition = Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out distance))
        {
            worldPosition = ray.GetPoint(distance);
        }

        if (controlFirstVector)
        {
            Vector3 mousePos = Input.mousePosition;
            Vector2 vector = Camera.main.ScreenToWorldPoint(mousePos);
            Managers.Vectors.vectorByIndex[1] = worldPosition;
        }
        else
        {
            Vector3 mousePos = Input.mousePosition;
            Vector2 vector = Camera.main.ScreenToWorldPoint(mousePos);
            Managers.Vectors.vectorByIndex[2] = worldPosition;
        }
        Managers.Vectors.UpdateResult();
    }
}

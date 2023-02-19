using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneDrawer : MonoBehaviour
{
    [SerializeField] private GameObject quad;

	private void OnEnable()
	{
		Managers.Vectors.VectorsUpdated += DrawPlane;
		DrawPlane();
	}

	private void OnDisable()
	{
		Managers.Vectors.VectorsUpdated -= DrawPlane;
	}

	private void DrawPlane()
	{
		switch (Managers.Vectors.VectorOperation.Operation)
		{
			case eVectorOperations.CrossProduct:
				DrawCrossProductPlane();
				break;
			case eVectorOperations.Reflection:
				DrawReflectionPlane();
				break;
			default:
				quad.gameObject.SetActive(false);
				break;
		}
    }

	private void DrawCrossProductPlane()
	{
		quad.gameObject.SetActive(true);
		float magnitude = (Vector3.Magnitude(Managers.Vectors.Vectors[0]) + Vector3.Magnitude(Managers.Vectors.Vectors[1])) / 3;
		quad.transform.localScale = new Vector3(magnitude, magnitude, magnitude);
		quad.transform.LookAt((Vector3)Managers.Vectors.Result);
	}

	private void DrawReflectionPlane()
	{
		quad.gameObject.SetActive(true);
		float magnitude = Vector3.Magnitude(Managers.Vectors.Vectors[1]) / 2;
		quad.transform.localScale = new Vector3(magnitude, magnitude, magnitude);
		quad.transform.LookAt(Managers.Vectors.Vectors[1]);
	}
}

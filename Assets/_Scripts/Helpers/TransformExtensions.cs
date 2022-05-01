using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtensions
{
    public static void SetObjectTransform(Transform currentTransform, Transform newTransform)
	{
		currentTransform.position = newTransform.position;
		currentTransform.rotation = newTransform.rotation;
		currentTransform.localScale = newTransform.localScale;
	}

	public static Vector4 ConvertToVector4(Vector3 vectorToConvert)
	{
		Vector4 newVector = new Vector4(vectorToConvert.x, vectorToConvert.y, vectorToConvert.z, 1);
		return newVector;
	}
}

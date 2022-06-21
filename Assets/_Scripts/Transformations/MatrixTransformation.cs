using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixTransformation
{
	public Matrix4x4 Matrix;
	public eTransformValue TransformValue;

	public MatrixTransformation(Matrix4x4 matrix, eTransformValue transformValue)
	{
		Matrix = matrix;
		TransformValue = transformValue;
	}
}

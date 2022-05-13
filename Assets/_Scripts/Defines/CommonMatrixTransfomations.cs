using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CommonMatrixTransfomations
{
	public static MatrixTransformation Identity = new MatrixTransformation(Matrix4x4.identity, eTransformValue.Position);

	public static MatrixTransformation UniformScalingBy2 = new MatrixTransformation(new Matrix4x4(new Vector4(2, 0, 0, 0),
		new Vector4(0, 2, 0, 0), new Vector4(0, 0, 2, 0), new Vector4(0, 0, 0, 1)), eTransformValue.Scale);

	public static MatrixTransformation TranslationBy1 = new MatrixTransformation(new Matrix4x4(new Vector4(1, 0, 0, 0),
		new Vector4(0, 1, 0, 0), new Vector4(0, 0, 1, 0), new Vector4(1, 1, 1, 1)), eTransformValue.Position);

	public static MatrixTransformation ReflectionOnAllAxes = new MatrixTransformation(new Matrix4x4(new Vector4(-1, 0, 0, 0),
		new Vector4(0, -1, 0, 0), new Vector4(0, 0, -1, 0), new Vector4(0, 0, 0, 1)), eTransformValue.Position);

	public static MatrixTransformation Shearing = new MatrixTransformation(new Matrix4x4(new Vector4(1, 1, 0, 0),
		new Vector4(0, 1, 0, 0), new Vector4(0, -1, 1, 0), new Vector4(0, 0, 0, 1)), eTransformValue.Scale);

	//public static Matrix4x4 RotationAroundZAxis = new Matrix4x4(new Vector4(1, -0.5f, 0, 0),
	//	new Vector4(0.5f, 1, 0, 0), new Vector4(0, -1, 1, 0), new Vector4(0, 0, 0, 1));

	public static readonly Dictionary<int, MatrixTransformation> matrixByIndex = new Dictionary<int, MatrixTransformation>()
	{
		{0, Identity },
		{1, ReflectionOnAllAxes },
		{2, UniformScalingBy2 },
		{3, TranslationBy1 },
		{4, Shearing },
	};
}

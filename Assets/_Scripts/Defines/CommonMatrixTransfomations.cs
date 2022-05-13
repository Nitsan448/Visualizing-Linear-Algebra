using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CommonMatrixTransfomations
{
	public static Matrix4x4 Identity = Matrix4x4.identity;

	public static Matrix4x4 UniformScalingBy2 = new Matrix4x4(new Vector4(2, 0, 0, 0),
		new Vector4(0, 2, 0, 0), new Vector4(0, 0, 2, 0), new Vector4(0, 0, 0, 1));

	public static Matrix4x4 TranslationBy1 = new Matrix4x4(new Vector4(1, 0, 0, 0),
		new Vector4(0, 1, 0, 0), new Vector4(0, 0, 1, 0), new Vector4(1, 1, 1, 1));

	public static Matrix4x4 XAxisReflection = new Matrix4x4(new Vector4(-1, 0, 0, 0), 
		new Vector4(0, 1, 0, 0), new Vector4(0, 0, 1, 0), new Vector4(0, 0, 0, 1));

	public static Matrix4x4 YAxisReflection = new Matrix4x4(new Vector4(1, 0, 0, 0),
		new Vector4(0, -1, 0, 0), new Vector4(0, 0, 1, 0), new Vector4(0, 0, 0, 1));

	public static Matrix4x4 ZAxisReflection = new Matrix4x4(new Vector4(1, 0, 0, 0),
		new Vector4(0, 1, 0, 0), new Vector4(0, 0, -1, 0), new Vector4(0, 0, 0, 1));

	public static Matrix4x4 ReflectionOnAllAxes = new Matrix4x4(new Vector4(-1, 0, 0, 0),
		new Vector4(0, -1, 0, 0), new Vector4(0, 0, -1, 0), new Vector4(0, 0, 0, 1));

	public static Matrix4x4 Shearing = new Matrix4x4(new Vector4(1, 1, 0, 0),
		new Vector4(0, 1, 0, 0), new Vector4(0, -1, 1, 0), new Vector4(0, 0, 0, 1));

	//public static Matrix4x4 RotationAroundZAxis = new Matrix4x4(new Vector4(1, -0.5f, 0, 0),
	//	new Vector4(0.5f, 1, 0, 0), new Vector4(0, -1, 1, 0), new Vector4(0, 0, 0, 1));

	public static readonly Dictionary<int, Matrix4x4> matrixByIndex = new Dictionary<int, Matrix4x4>()
	{
		{0, Identity },
		{1, ReflectionOnAllAxes },
		{2, UniformScalingBy2 },
		{3, TranslationBy1 },
		{4, Shearing },
	};
}

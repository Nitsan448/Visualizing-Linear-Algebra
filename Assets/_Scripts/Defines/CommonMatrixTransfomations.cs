using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CommonMatrixTransfomations
{
	public static Matrix4x4 Identity = Matrix4x4.identity;
	
	public static Matrix4x4 XAxisReflection = new Matrix4x4(new Vector4(-1, 0, 0, 0), 
		new Vector4(0, 1, 0, 0), new Vector4(0, 0, 1, 0), new Vector4(0, 0, 0, 1));

	public static Matrix4x4 YAxisReflection = new Matrix4x4(new Vector4(1, 0, 0, 0),
		new Vector4(0, -1, 0, 0), new Vector4(0, 0, 1, 0), new Vector4(0, 0, 0, 1));

	public static Matrix4x4 ZAxisReflection = new Matrix4x4(new Vector4(1, 0, 0, 0),
		new Vector4(0, 1, 0, 0), new Vector4(0, 0, -1, 0), new Vector4(0, 0, 0, 1));

	public static Matrix4x4 RotationBy180 = new Matrix4x4(new Vector4(-1, 0, 0, 0),
		new Vector4(0, -1, 0, 0), new Vector4(0, 0, -1, 0), new Vector4(0, 0, 0, 1));

	public static readonly Dictionary<int, Matrix4x4> matrixByIndex = new Dictionary<int, Matrix4x4>()
	{
		{0, Identity },
		{1, XAxisReflection },
		{2, YAxisReflection },
		{3, ZAxisReflection },
		{4, RotationBy180 }
	};
}

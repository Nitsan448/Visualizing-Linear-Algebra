using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

public static class StringExtensions
{
    public static string NumberOfDecimals = "F" + Managers.UI.numberOfDecimals.ToString();

    public static string FloatToString(float number)
	{
        return number.ToString(NumberOfDecimals);
    }

    public static Matrix4x4 stringToMatrix(string transform)
    {

        //TODO: Refactor!!!
        string[] vectors = transform.Split('\n');

        Vector4 firstRow = StringToVector4(vectors[0]);
        Vector4 secondRow = StringToVector4(vectors[1]);
        Vector4 thirdRow = StringToVector4(vectors[2]);
        Vector4 fourthRow = StringToVector4(vectors[3]);

        Vector4 firstColumn = new Vector4(firstRow.x, secondRow.x, thirdRow.x, fourthRow.x);
        Vector4 secondColumn = new Vector4(firstRow.y, secondRow.y, thirdRow.y, fourthRow.y);
        Vector4 thirdColumn = new Vector4(firstRow.z, secondRow.z, thirdRow.z, fourthRow.z);
        Vector4 fourthColumn = new Vector4(firstRow.w, secondRow.w, thirdRow.w, fourthRow.w);
        Matrix4x4 result = new Matrix4x4(firstColumn, secondColumn, thirdColumn, fourthColumn);

        return result;
    }

    public static string matrixToString(Matrix4x4 matrix)
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(Vector4ToString(matrix.GetRow(0)));
        stringBuilder.Append('\n');
        stringBuilder.Append(Vector4ToString(matrix.GetRow(1)));
        stringBuilder.Append('\n');
        stringBuilder.Append(Vector4ToString(matrix.GetRow(2)));
        stringBuilder.Append('\n');
        stringBuilder.Append(Vector4ToString(matrix.GetRow(3)));

        return stringBuilder.ToString();
    }

    public static bool IsMatrixStringFormatValid(string transform)
    {
        string[] vectors = transform.Split('\n');
        foreach (string vector in vectors)
        {
            if (!IsVectorStringFormatValid(vector))
            {
                return false;
            }
        }
        return true;
    }

    public static bool IsVectorStringFormatValid(string vector)
    {
        RemoveParenthesis(ref vector);
        string[] vectorValues = vector.Split(',');

        for (int i = 0; i < vectorValues.Length; i++)
        {
            float nextValue;
            bool floatInputWasValid = float.TryParse(vectorValues[i], out nextValue);
            if (!floatInputWasValid)
            {
                return false;
            }
        }

        return true;
    }

    public static Vector3 StringToVector3(string vector)
    {
        List<float> floatList = VectorStringToFloatList(vector);
        return new Vector3(floatList[0], floatList[1], floatList[2]);
    }

    public static string Vector3ToString(Vector3 vector)
    {
        StringBuilder stringBuilder = new StringBuilder("(");
        stringBuilder.Append(vector.x.ToString(NumberOfDecimals));
        stringBuilder.Append(", ");
        stringBuilder.Append(vector.y.ToString(NumberOfDecimals));
        stringBuilder.Append(", ");
        stringBuilder.Append(vector.z.ToString(NumberOfDecimals));
        stringBuilder.Append(")");
        return stringBuilder.ToString();
    }

    public static Vector4 StringToVector4(string vector)
    {
        List<float> floatList = VectorStringToFloatList(vector);
        return new Vector4(floatList[0], floatList[1], floatList[2], floatList[3]);
    }

    public static string Vector4ToString(Vector4 vector)
    {
        StringBuilder stringBuilder = new StringBuilder(Vector3ToString(vector));
        stringBuilder.Remove(stringBuilder.Length - 1, 1);
        stringBuilder.Append(", ");
        stringBuilder.Append(vector.w.ToString(NumberOfDecimals));
        stringBuilder.Append(")");
        return stringBuilder.ToString();
    }

    public static List<float> VectorStringToFloatList(string vector)
    {
        RemoveParenthesis(ref vector);

        string[] vectorValues = vector.Split(',');

        List<float> result = new List<float>();
        for (int i = 0; i < vectorValues.Length; i++)
        {
            result.Add(float.Parse(vectorValues[i]));
        }
        return result;
    }

    private static void RemoveParenthesis(ref string vector)
	{
        if (vector.StartsWith("(") && vector.EndsWith(")"))
        {
            vector = vector.Substring(1, vector.Length - 2);
        }
        else if (vector.StartsWith("("))
        {
            vector = vector.Substring(1, vector.Length - 1);
        }
        else if (vector.EndsWith(")"))
        {
            vector = vector.Substring(0, vector.Length - 2);
        }
    }

    public static void UpdateNumberOfDecimals()
	{
        NumberOfDecimals = "F" + Managers.UI.numberOfDecimals.ToString();
    }
}

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
        string[] vectors = transform.Split('\n');
        Matrix4x4 result = new Matrix4x4(StringToVector4(vectors[0]), StringToVector4(vectors[1]),
                               StringToVector4(vectors[2]), StringToVector4(vectors[3]));
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
        stringBuilder.Append('\n');

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
        //if(Regex.IsMatch(vector, "[A-Z][a-z]!@#%&"))
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

    public static string UpdateNumberOfDecimalsShownVector3(string vectorString)
	{
        Vector3 vector = StringToVector3(vectorString);
        return Vector3ToString(vector);
    }
    
    public static string UpdateNumberOfDecimalsShownVector4(string vectorString)
    {
        Vector4 vector = StringToVector4(vectorString);
        return Vector4ToString(vector);
    }
    public static string UpdateNumberOfDecimalsShownMatrix(string matrixString)
    {
        Matrix4x4 matrix = stringToMatrix(matrixString);
        return matrixToString(matrix);
    }

    public static void UpdateNumberOfDecimals()
	{
        NumberOfDecimals = "F" + Managers.UI.numberOfDecimals.ToString();
    }
}

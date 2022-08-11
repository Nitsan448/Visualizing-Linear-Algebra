using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

public static class StringExtensions
{
    public static string NumberOfDecimals = "F" + Managers.UI.numberOfDecimals.ToString();

    public static void UpdateNumberOfDecimals()
    {
        NumberOfDecimals = "F" + Managers.UI.numberOfDecimals.ToString();
    }

    public static string FloatToString(float number)
	{
        return number.ToString(NumberOfDecimals);
    }

    public static Matrix4x4 StringToMatrix(string transform)
    {
        Matrix4x4 result = new Matrix4x4();
        string[] vectors = transform.Split('\n');

        result.SetRow(0, StringToVector4(vectors[0]));
        result.SetRow(1, StringToVector4(vectors[1]));
        result.SetRow(2, StringToVector4(vectors[2]));
        result.SetRow(3, StringToVector4(vectors[3]));

        return result;
    }

    public static Vector4 StringToVector4(string vector)
    {
        List<float> floatList = VectorStringToFloatList(vector);
        return new Vector4(floatList[0], floatList[1], floatList[2], floatList[3]);
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

    public static void RemoveParenthesis(ref string vector)
    {
        vector = vector.Replace("(", string.Empty);
        vector = vector.Replace(")", string.Empty);
    }

    public static string MatrixToString(Matrix4x4 matrix)
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

    public static string Vector4ToString(Vector4 vector)
    {
        StringBuilder stringBuilder = new StringBuilder(Vector3ToString(vector));
        stringBuilder.Remove(stringBuilder.Length - 1, 1);
        stringBuilder.Append(", ");
        stringBuilder.Append(vector.w.ToString(NumberOfDecimals));
        stringBuilder.Append(")");
        return stringBuilder.ToString();
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

    public static string LinearTransformationStringToMatrixString(string linearTransformation)
    {
        StringBuilder stringBuilder = new StringBuilder("(");

        string[] linearTransformationValues = linearTransformation.Split('\n');
        stringBuilder.Append(LinearTransformationValueToMatrixRow(linearTransformationValues[0]));
        stringBuilder.Append(Environment.NewLine);
        stringBuilder.Append(LinearTransformationValueToMatrixRow(linearTransformationValues[1]));
        stringBuilder.Append(Environment.NewLine);
        stringBuilder.Append(LinearTransformationValueToMatrixRow(linearTransformationValues[2]));
        stringBuilder.Append(Environment.NewLine);
        stringBuilder.Append("(0, 0, 0, 1)");

        return stringBuilder.ToString();
    }

    private static Vector4 LinearTransformationValueToMatrixRow(string row)
    {
        RemoveIrrelevantChars(ref row);
        Vector4 result;

        result.x = GetAndRemoveCoefficient(ref row, 'X');
        result.y = GetAndRemoveCoefficient(ref row, 'Y');
        result.z = GetAndRemoveCoefficient(ref row, 'Z');

        bool wValueExists = float.TryParse(row, out float wValue);
        if (wValueExists)
        {
            result.w = wValue;
        }
        else
        {
            result.w = 0;
        }

        return result;
    }

    private static void RemoveIrrelevantChars(ref string row)
	{
        row = row.Replace(" ", string.Empty);
        row = row.Replace("+", string.Empty);
        row = row.Replace("\n", string.Empty);
        RemoveParenthesis(ref row);
    }

    //Change method name
    private static float GetAndRemoveCoefficient(ref string row, char valueToFindCoefficientOf)
    {
        float result;
        int indexOfChar = row.IndexOf(valueToFindCoefficientOf);
        if(indexOfChar == -1)
		{
            indexOfChar = row.IndexOf(valueToFindCoefficientOf.ToString().ToLower());
		}
        if (indexOfChar == -1)
        {
            result = 0;
        }
        else if (indexOfChar == 0)
        {
            result = 1;
            row = row.Remove(0, indexOfChar + 1);
        }
        else
        {
            float.TryParse(row.Substring(0, indexOfChar), out result);
            row = row.Remove(0, indexOfChar + 1);
        }
        return result;
    }

    public static string MatrixToLinearTransformationString(Matrix4x4 matrix)
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(Vector4ToLinearTransformationRow(matrix.GetRow(0)));
        stringBuilder.Append(Environment.NewLine);
        stringBuilder.Append(Vector4ToLinearTransformationRow(matrix.GetRow(1)));
        stringBuilder.Append(Environment.NewLine);
        stringBuilder.Append(Vector4ToLinearTransformationRow(matrix.GetRow(2)));
        return stringBuilder.ToString();
    }

    private static string Vector4ToLinearTransformationRow(Vector4 vector)
	{
        StringBuilder stringBuilder = new StringBuilder("(");
        stringBuilder.Append(GetCoefficientStringFromValue(vector.x, "X"));
        if(vector.y != 0 && !stringBuilder.ToString().Equals("("))
		{
            stringBuilder.Append("+");
		}
        stringBuilder.Append(GetCoefficientStringFromValue(vector.y, "Y"));
        if (vector.z != 0 && !stringBuilder.ToString().Equals("("))
        {
            stringBuilder.Append("+");
        }
        stringBuilder.Append(GetCoefficientStringFromValue(vector.z, "Z"));
        if (vector.w != 0 && !stringBuilder.ToString().Equals("("))
        {
            stringBuilder.Append("+");
        }
        stringBuilder.Append(GetCoefficientStringFromValue(vector.w, string.Empty));
        stringBuilder.Append(")");
        return stringBuilder.ToString();
    }

    private static string GetCoefficientStringFromValue(float value, string valueToGetCoefficientOf)
	{
        if(value == 1 && valueToGetCoefficientOf != string.Empty)
		{
            return valueToGetCoefficientOf;
		}
        else if(value != 0)
		{
            return value.ToString(NumberOfDecimals) + valueToGetCoefficientOf;
        }
		else
		{
            return string.Empty;
		}
	}
}

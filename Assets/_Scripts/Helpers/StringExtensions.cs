using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Text;

public static class StringExtensions
{
    public static string NumberOfDecimals = "F" + Managers.UI.numberOfDecimals.ToString();

    public static string FloatToString(float number)
	{
        return number.ToString(NumberOfDecimals);
    }

    public static Vector3 StringToVector3(string vector)
    {
        List<float> floatList = VectorStringToFloatList(vector);
        return new Vector3(floatList[0], floatList[1], floatList[2]);
    }

    public static string Vector3ToString(Vector3 vector)
    {
        //TODO: optimize with string builder
        string newVectorText = "(" + vector.x.ToString(NumberOfDecimals) + ", " + 
            vector.y.ToString(NumberOfDecimals) + ", " + vector.z.ToString(NumberOfDecimals) + ")";
        return newVectorText;
    }

    public static Vector4 StringToVector4(string vector)
    {
        List<float> floatList = VectorStringToFloatList(vector);
        return new Vector4(floatList[0], floatList[1], floatList[2], floatList[3]);
    }

    public static string Vector4ToString(Vector4 vector)
    {
        string newVectorText = "(" + vector.x.ToString(NumberOfDecimals) + ", " + vector.y.ToString(NumberOfDecimals)
            + ", " + vector.z.ToString(NumberOfDecimals) + ", " + vector.w.ToString(NumberOfDecimals) + ")";
        return newVectorText;
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

    public static void UpdateNumberOfDecimals()
	{
        NumberOfDecimals = "F" + Managers.UI.numberOfDecimals.ToString();
    }
}

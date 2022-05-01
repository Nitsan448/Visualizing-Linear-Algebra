using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public static class StringExtensions
{
    public static Vector3 StringToVector3(string vector)
    {
        List<float> floatList = VectorStringToFloatList(vector);
        return new Vector3(floatList[0], floatList[1], floatList[2]);
    }

    public static string Vector3ToString(Vector3 vector)
    {
        //TODO: use string builder
        string newVectorText = "(" + vector.x.ToString("0.0") + ", " + vector.y.ToString("0.0") + ", " + vector.z.ToString("0.0") + ")";
        return newVectorText;
    }

    public static Vector4 StringToVector4(string vector)
    {
        List<float> floatList = VectorStringToFloatList(vector);
        return new Vector4(floatList[0], floatList[1], floatList[2], floatList[3]);
    }

    public static string Vector4ToString(Vector4 vector)
    {
        string newVectorText = "(" + vector.x.ToString("0.0") + ", " + vector.y.ToString("0.0") + ", " + vector.z.ToString("0.0") + vector.w.ToString("0.0") + ")";
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
}

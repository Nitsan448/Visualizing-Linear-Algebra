using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Explanations
{
    private static string redV = TextExtensions.GetColoredText(TextExtensions.RedVectorColor, "V");
    private static string greenW = TextExtensions.GetColoredText(TextExtensions.GreenVectorColor, "W");

    public static string VectorAdditionExplanation = "If we have two vectors " + redV + " and " + greenW + "." +
		" We draw " + greenW + " from the head(front) of " + redV + ".\n" 
        + TextExtensions.GetColoredText(TextExtensions.BlueVectorColor, "V + W") + 
        " is defined to be the vector that goes from the tail of " + redV +
		" to the head of the drawn " + greenW + ".";

    public static string DotProductExplanation = "The Dot product between " + redV + " and " + greenW +
        " is the cosine of the angle between them, multiplied by their length";

    public static string CrossProductExplanation = "The Cross product of two vectors " + redV + " and " + greenW + 
        " is a " + TextExtensions.GetColoredText(TextExtensions.BlueVectorColor, "vector") + 
        " that is perpendicular to both of them.\n And thus a normal to the plane containing them.";

    public static string ReflectionExplanation = "Reflection of " + redV + " off the plane defined by the " +
        TextExtensions.GetColoredText(TextExtensions.GreenVectorColor, "normal");

    public static string ProjectionExplanation = "The Projection from " + redV + " onto " + greenW + 
        " Can be thought of as the shadow of " + redV + " onto " + greenW + ".";

    public static string MatrixTransformationExplanation = "Transformations matrices represent linear transformations.\n" +
		"We can multiply them with vectors to perform many interesting operations on the vector with a single calculation.";

    public static readonly Dictionary<eVectorOperations, string> ExplanationByVectorOperation = new Dictionary<eVectorOperations, string>()
    {
        { eVectorOperations.Addition, VectorAdditionExplanation },
        { eVectorOperations.DotProduct, DotProductExplanation },
        { eVectorOperations.CrossProduct, CrossProductExplanation },
        { eVectorOperations.Reflection, ReflectionExplanation },
        { eVectorOperations.Projection, ProjectionExplanation }
    };
}
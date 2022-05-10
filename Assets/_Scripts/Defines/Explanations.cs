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
        " can be thought of as projecting " + greenW + " onto the line that passes " +
		"through the origin and the tip of " + redV + ", and multiplying the length of this projection by the length of " + redV + ".";

    public static string CrossProductExplanation = "The Cross product of two vectors is a vector that is perpendicular to both of them.\n" +
		"And thus a normal to the plane containing them.";

    public static string ReflectionExplanation = "Reflection of " + redV + " off the plane defined by the " +
        TextExtensions.GetColoredText(TextExtensions.GreenVectorColor, "normal");

    public static string ProjectionExplanation = "Can be thought of as the shadow of " + redV + " on " + greenW + ".";

    public static string MatrixTransformationExplanation = "Matrix transformation explanation";

    public static readonly Dictionary<eVectorOperations, string> ExplanationByVectorOperation = new Dictionary<eVectorOperations, string>()
    {
        { eVectorOperations.Addition, VectorAdditionExplanation },
        { eVectorOperations.DotProduct, DotProductExplanation },
        { eVectorOperations.CrossProduct, CrossProductExplanation },
        { eVectorOperations.Reflection, ReflectionExplanation },
        { eVectorOperations.Projection, ProjectionExplanation }
    };
}
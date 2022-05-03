using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Explanations
{
    public const string DotProductExplanation = "The Dot product between v and w, can be thought of as projecting w onto the line that passes " +
		                        "through the origin and the tip of v, and multiplying the length of this projection by the length of v";

    public const string CrossProductExplanation = "The Cross product of two vectors is a vector that is perpendicular to both of them.\n" +
		"And thus a normal to the plane containing them.";
    public const string ReflectionExplanation = "Reflection of the red vector on the line defined by the green vector and the origin";
    public const string ProjectionExplanation = "Can be thought of as the shadow of a vector onto another.";

    public const string MatrixTransformationExplanation = "Matrix transformation explanation";

    public static readonly Dictionary<eVectorOperations, string> ExplanationByVectorOperation = new Dictionary<eVectorOperations, string>()
    {
        { eVectorOperations.DotProduct, DotProductExplanation },
        { eVectorOperations.CrossProduct, CrossProductExplanation },
        { eVectorOperations.Reflection, ReflectionExplanation },
        { eVectorOperations.Projection, ProjectionExplanation }
    };
}
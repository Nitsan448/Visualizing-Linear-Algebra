using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LinearTransformationUI : MonoBehaviour
{
    private TMP_InputField _linearTransformationInput;

	private void Awake()
	{
        _linearTransformationInput = GetComponent<TMP_InputField>();
	}

	void Start()
    {
        _linearTransformationInput.onEndEdit.AddListener(delegate
        {
            Managers.Transformations.UpdateMatrixFromString
            (StringExtensions.LinearTransformationStringToMatrixString(_linearTransformationInput.text));
        });
    }

    private void OnEnable()
    {
        Managers.Transformations.MatrixUpdated += UpdateLinearTransfomationUI;
        UpdateLinearTransfomationUI();
    }

    private void OnDisable()
    {
        Managers.Transformations.MatrixUpdated -= UpdateLinearTransfomationUI;
    }


    private void UpdateLinearTransfomationUI()
	{
        _linearTransformationInput.text = StringExtensions.MatrixToLinearTransformationString(Managers.Transformations.Matrix);
        _linearTransformationInput.pointSize = Managers.UI.FontSizeByNumberOfDecimals[Managers.UI.numberOfDecimals];
    }
}

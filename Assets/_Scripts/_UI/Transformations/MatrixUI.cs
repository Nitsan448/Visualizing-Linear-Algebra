using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MatrixUI : MonoBehaviour
{
	[SerializeField] private Vector2 _inputFieldSize;
	[SerializeField] private int _decimalsSizeChange = 20;

	private RectTransform _rectTransform;
	private TMP_InputField _matrixInput;

	private void Awake()
	{
		_rectTransform = GetComponent<RectTransform>();
		_matrixInput = GetComponent<TMP_InputField>();
		_matrixInput.onValueChanged.AddListener(delegate { UpdateMatrix(); });
	}

	private void UpdateMatrix()
	{
		Managers.Transformations.UpdateMatrixFromString(_matrixInput.text);
	}

	private void OnEnable()
	{
		Managers.Transformations.MatrixUpdated += UpdateMatrixUI;
		UpdateMatrixUI();
	}

	private void OnDisable()
	{
		Managers.Transformations.MatrixUpdated -= UpdateMatrixUI;
	}

	private void UpdateMatrixUI()
    {
        _matrixInput.pointSize = Managers.UI.FontSizeByNumberOfDecimals[Managers.UI.numberOfDecimals];
        _matrixInput.text = StringExtensions.MatrixToString(Managers.Transformations.Matrix);

		int heightChange = Managers.UI.numberOfDecimals * _decimalsSizeChange;
		_rectTransform.sizeDelta = new Vector2(_inputFieldSize.x, _inputFieldSize.y - heightChange);
    }
}

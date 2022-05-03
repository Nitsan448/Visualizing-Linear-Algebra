using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ChooseOperationDropDown : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _explanationText;

    private TMP_Dropdown dropDown;

    // Start is called before the first frame update
    void Start()
    {
        dropDown = GetComponent<TMP_Dropdown>();
        dropDown.onValueChanged.AddListener(delegate { SetVectorOperation(dropDown.value); });
        dropDown.value = 0;
        SetVectorOperation(dropDown.value);
    }



    public void SetVectorOperation(int operationIndex)
    {
        eVectorOperations operation = (eVectorOperations)operationIndex;
        Managers.Vectors.vectorOperation.operation = operation;
        _explanationText.text = Explanations.ExplanationByVectorOperation[operation];
        Managers.Vectors.UpdateResult();
    }
}

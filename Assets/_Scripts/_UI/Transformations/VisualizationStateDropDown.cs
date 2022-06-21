using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class VisualizationStateDropDown : MonoBehaviour
{
    private TMP_Dropdown dropDown;

    // Start is called before the first frame update
    void Start()
    {
        dropDown = GetComponent<TMP_Dropdown>();
        dropDown.onValueChanged.AddListener(delegate { SetVisualizationState(dropDown.value); });
        dropDown.value = (int)VisualizationStateManager.VisualizationState;
    }



    public void SetVisualizationState(int operationIndex)
    {
        eVisualizationState newState = (eVisualizationState)operationIndex;
        Managers.VisualizationState.SetVisualizationState(newState);
    }
}

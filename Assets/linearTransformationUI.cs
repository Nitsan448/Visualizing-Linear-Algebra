using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class linearTransformationUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TMP_InputField>().onEndEdit.AddListener(delegate
        {
            Managers.Transformations.UpdateMatrix
            (StringExtensions.LinearTransformationStringToMatrix(GetComponent<TMP_InputField>().text));
            Debug.Log(Managers.Transformations.Matrix);
        });
    }

	private void OnEnable()
	{
		//parse matrix to function
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResetObjectToOriginButton : MonoBehaviour
{
    [SerializeField] private ObjectTransformInput _objectTransformInput;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate {
            Managers.Transformations.ResetObjectToOrigin();
            _objectTransformInput.UpdateVectorUI();
        });
    }
}

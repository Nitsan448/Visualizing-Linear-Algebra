using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NormalizeVectorButton : MonoBehaviour
{
    [SerializeField] private int _vectorIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate { Managers.Vectors.NormalizeVector(_vectorIndex); });
    }
}

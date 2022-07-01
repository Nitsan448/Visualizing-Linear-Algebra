using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    public static VectorsManager Vectors { get; private set; }
    public static TransformationsManager Transformations { get; private set; }
    public static VisualizationStateManager VisualizationState { get; private set; }
    public static UIManager UI { get; private set; }

    private List<IGameManager> startSequence;

    void Awake()
    {
        Application.targetFrameRate = 60;
        GetManagers();
        SetStartSequenceOrder();
        StartupManagers();
        Debug.Log("All managers started up");
        //StartCoroutine(StartupManagers());
    }

    private void GetManagers()
	{
        VisualizationState = GetComponentInChildren<VisualizationStateManager>();
        Vectors = GetComponentInChildren<VectorsManager>();
        Transformations = GetComponentInChildren<TransformationsManager>();
        UI = GetComponentInChildren<UIManager>();
    }

    private void SetStartSequenceOrder()
	{
        startSequence = new List<IGameManager>();
        startSequence.Add(UI);
        startSequence.Add(Vectors);
        startSequence.Add(Transformations);
        startSequence.Add(VisualizationState);
    }

    private void StartupManagers()
	{
        foreach (IGameManager manager in startSequence)
        {
            manager.Startup();
        }
	}
}

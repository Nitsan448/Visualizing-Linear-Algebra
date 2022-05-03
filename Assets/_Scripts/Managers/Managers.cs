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
        GetManagers();
        SetStartSequenceOrder();
        StartCoroutine(StartupManagers());
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

    private IEnumerator StartupManagers()
    {
        foreach (IGameManager manager in startSequence)
        {
            manager.Startup();
        }
        yield return null;
        int numModules = startSequence.Count;
        int numReady = 0;
        while (numReady < numModules)
        {
            int lastReady = numReady;
            numReady = 0;
            foreach (IGameManager manager in startSequence)
            {
                if (manager.status == eManagerStatus.Started)
                {
                    numReady++;
                }
            }
            if (numReady > lastReady)
                Debug.Log($"Progress: {numReady}/{numModules}");
            yield return null;
        }
        Debug.Log("All managers started up");
    }
}

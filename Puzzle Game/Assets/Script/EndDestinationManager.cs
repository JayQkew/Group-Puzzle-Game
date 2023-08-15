using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EndDestinationManager : MonoBehaviour
{
    public static EndDestinationManager Instance { get; private set; }

    [SerializeField] public bool boxA_OnDestination = false;
    [SerializeField] public bool boxB_OnDestination = false;

    [SerializeField] private GameObject levelEndPanel;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
    }

    private void Update()
    {
        if (boxA_OnDestination && boxB_OnDestination)
        {
            StartCoroutine(TriggerCheck());
        }
    }

    IEnumerator TriggerCheck()
    {
        yield return new WaitForSeconds(1);
        if (boxA_OnDestination && boxB_OnDestination)
        {
            Time.timeScale = 0;
            levelEndPanel.SetActive(true);
        }
    }

}

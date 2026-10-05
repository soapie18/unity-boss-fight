using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : Interaction
{
    [Space(10)]
    [Header("Level Loading Details")]
    [SerializeField] string levelToLoad = "EncounterHub";

    private void OnEnable()
    {
        OnLongInteraction += GoToLevel;
    }

    private void OnDisable()
    {
        OnLongInteraction -= GoToLevel;
    }

    private void GoToLevel(GameObject obj, InteractionPhase phase, float delta)
    {
        if (phase == InteractionPhase.COMPLETED)
        {
            SceneManager.LoadScene(levelToLoad);
        }
    }
}

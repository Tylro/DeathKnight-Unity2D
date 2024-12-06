using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public SceneFader sceneFader;

    public static UIManager Instance;

    [SerializeField] GameObject deathScreen;
    public GameObject mapHandler;
    public GameObject inventory;
    [SerializeField] GameObject halfMana, fullMana;


    public enum ManaState
    {
        FullMana,
        HalfMana
    }
    public ManaState manaState;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);

        sceneFader = GetComponentInChildren<SceneFader>();
    }
    public void SwitchMana(ManaState _manaState)
    {
        switch(_manaState)
        {
            case ManaState.FullMana:

                halfMana.SetActive(false);
                fullMana.SetActive(true);

                break;

            case ManaState.HalfMana:

                fullMana.SetActive(false);
                halfMana.SetActive(true);

                break;
        }
        manaState = _manaState;
    }
    public IEnumerator ActivateDeathScreen()
    {
        yield return new WaitForSeconds(0.8f);
        StartCoroutine(sceneFader.Fade(SceneFader.FadeDirection.In));

        yield return new WaitForSeconds(0.8f);
        deathScreen.SetActive(true);
    }
    public IEnumerator DeactivateDeathScreen()
    {
        yield return new WaitForSeconds(0.5f);
        deathScreen.SetActive(false);
        StartCoroutine(sceneFader.Fade(SceneFader.FadeDirection.Out));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleep : MonoBehaviour
{
    PlayerController playerController;
    DisableControls disableControls;
    DayTimeController dayTime;
    [SerializeField] GameObject loadImage;
    Player player;

    private void Awake()
    {
        disableControls = GetComponent<DisableControls>();
        playerController = GetComponent<PlayerController>();
        player = GetComponent<Player>();
        dayTime = GameManager.instance.timeController;
    }

    internal void DoSleep()
    {
        StartCoroutine(SleepRoutine());
    }

    IEnumerator SleepRoutine()
    {
        ScreenTint screenTint = GameManager.instance.screenTint;

        disableControls.DisableControl();

        screenTint.Tint();
        yield return new WaitForSeconds(1f);
        loadImage.SetActive(true);
        yield return new WaitForSeconds(3f);
        
        dayTime.SkipToMorning();
        player.FullRest();
        
        loadImage.SetActive(false);
        screenTint.UnTint();
        yield return new WaitForSeconds(2f);

        disableControls.EnableControl();

        yield return null;
    }
}

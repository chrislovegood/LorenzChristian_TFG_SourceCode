﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

public class DayTimeController : MonoBehaviour
{
    const float secondsInDay = 86400f;
    const float phaseLength = 900f; //15 minutes chunk of time
    const float phasesInDay = 96f; //secondsInDay divided by phaseLength
    
    [SerializeField] Color nightLightColor;
    [SerializeField] AnimationCurve nightTimeCurve;
    [SerializeField] Color dayLightColor = Color.white;
    [SerializeField] Text text;
    [SerializeField] Light2D globalLight;

    [SerializeField] float timeScale = 60f;
    [SerializeField] float startAtTime = 28800f;
    [SerializeField] float morningTime = 28800f;

    float time;
    private int days;

    List<TimeAgent> agents;

    private void Awake()
    {
        agents = new List<TimeAgent>();
    }

    private void Start()
    {
        time =  GameManager.instance.gameData.entries[2].value;
        days = (int)  GameManager.instance.gameData.entries[3].value;
        TimeValueCalculation();
    }

    public void Subscribe(TimeAgent timeAgent)
    {
        agents.Add(timeAgent);
    }

    public void Unsubscribe(TimeAgent timeAgent)
    {
        agents.Remove(timeAgent);
    }
    
    float Hours 
    {
        get { return time / 3600f; }
    }

    float Minutes 
    {
        get { return time % 3600f / 60f; }
    }

    private void Update()
    {
        time += Time.deltaTime * timeScale;
        GameManager.instance.gameData.entries[2].value = time;

        TimeValueCalculation();
        DayLight();

        if(time > secondsInDay )
        {
            NextDay();
            GameManager.instance.gameData.entries[3].value = days;
        }
        TimeAgents();

        if(Input.GetKeyDown(KeyCode.T))
        {   
            SkipTime(hours: 4);
        }
    }

    private void TimeValueCalculation()
    {
        int hh = (int) Hours;
        int mm = (int) Minutes;
        text.text = GameManager.instance.settings.dictionary["day"] + " " + days + " - " + hh.ToString("00") + ":" + mm.ToString("00");
    }

    private void DayLight()
    {
        float v = nightTimeCurve.Evaluate(Hours);
        Color c = Color.Lerp(dayLightColor, nightLightColor, v);
        globalLight.color = c;
    }

    int oldPhase = -1;

    private void TimeAgents()
    {

        if(oldPhase == -1)
        {
            oldPhase = CalculatePhase();
        }

        int currentPhase = CalculatePhase();
        
        while(oldPhase < currentPhase)
        {
            oldPhase += 1;
            for(int i=0; i < agents.Count; i++)
            {
                agents[i].Invoke();
            }
        }
    }

    private void NextDay()
    {
        time -= secondsInDay;
        days += 1;
    }

    private int CalculatePhase()
    {
        return (int) (time / phaseLength) + (int)(days * phasesInDay);
    }

    public void SkipTime(float seconds = 0, float minutes = 0, float hours = 0)
    {   
        float timeToSkip = seconds;
        timeToSkip += minutes * 60f;
        timeToSkip += hours * 3600f;

        time += timeToSkip;
    }

    public void SkipToMorning()
    {
        float secondsToSkip = 0f;
        
        if(time > morningTime)
        {
            secondsToSkip += secondsInDay - time + morningTime; 
        }
        else
        {
            secondsToSkip += morningTime - time;
        }

        SkipTime(secondsToSkip);
    }
}

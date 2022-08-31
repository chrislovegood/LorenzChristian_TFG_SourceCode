using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeatherStates
{
    Clear,
    Rain
}

public class WeatherManager : TimeAgent
{
    [Range(0f,1f)] [SerializeField] float chanceToChangeWeather = 0.02f;
    WeatherStates currentWeatherState = WeatherStates.Clear;
    [SerializeField] ParticleSystem rainObject;
    [SerializeField] TileMapCropsManager cropsManager;
    [SerializeField] AudioClip audioRaining;

    private int timer;

    private void Start()
    {
        Init();
        onTimeTick += RandomWeatherChangeCheck;
        currentWeatherState = GameManager.instance.gameData.entries[5].value == 1 ? WeatherStates.Rain : WeatherStates.Clear;
        UpdateWeather();
    }

    public void RandomWeatherChangeCheck()
    {
        ++timer;
        if(timer % 96 == 0)
        {
            if(UnityEngine.Random.value < chanceToChangeWeather)
            {
                RandomWeatherChange();
            }
        }
    }

    public void RandomWeatherChange()
    {
        WeatherStates newWeatherState = (WeatherStates) UnityEngine.Random.Range(0, Enum.GetNames(typeof(WeatherStates)).Length);
        ChangeWeather(newWeatherState);
    }

    public void ChangeWeather(WeatherStates newWeatherState)
    {
        currentWeatherState = newWeatherState;
        GameManager.instance.gameData.entries[5].value = currentWeatherState == WeatherStates.Rain ? 1 : 0;
        UpdateWeather();
    }

    private void UpdateWeather()
    {
        switch (currentWeatherState)
        {
            case WeatherStates.Clear:

                rainObject.gameObject.SetActive(false);

                if(AudioManager.instance.GetAudioSource(audioRaining) != null)
                {
                    AudioManager.instance.GetAudioSource(audioRaining).Stop();
                }
                cropsManager.setRaining(false);
                break;

            case WeatherStates.Rain:

                rainObject.gameObject.SetActive(true);

                if(AudioManager.instance.GetAudioSource(audioRaining) == null)
                {
                    AudioManager.instance.Play(audioRaining, true);
                }
                else
                {
                    AudioManager.instance.GetAudioSource(audioRaining).Play();
                }
                
                cropsManager.WaterAllCropTiles();
                break;
        }
    }


}

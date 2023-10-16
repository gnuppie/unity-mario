using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPowerUpWeek4 : BasePowerupWeek4
{
    // setup this object's type
    // instantiate variables
    protected override void Start()
    {
        base.Start(); // call base class Start()
        this.type = PowerupTypeWeek4.Coin;

    }
    public override void ApplyPowerup(MonoBehaviour i)
    {
        // throw new System.NotImplementedException();
    }

    public override void GameRestart()
    {
        base.GameRestart();

    }

    public void playSound()
    {
        AudioSource powerUpAudio = this.transform.GetComponent<AudioSource>();
        powerUpAudio.volume = UnityEngine.Random.Range(0.8f, 1.0f);
        powerUpAudio.pitch = UnityEngine.Random.Range(0.85f, 1.1f);
        powerUpAudio.PlayOneShot(powerUpAudio.clip);
    }

    public override void SpawnPowerup()
    {
        spawned = true;
        playSound();
        GameManagerWeek3.instance.IncreaseScore(1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPowerUp : BasePowerup
{
    // setup this object's type
    // instantiate variables
    protected override void Start()
    {
        base.Start(); // call base class Start()
        this.type = PowerupType.Coin;

    }
    public override void ApplyPowerup(MonoBehaviour i)
    {
        // throw new System.NotImplementedException();
    }

    public override void SpawnPowerup()
    {
        // Play Sound
        AudioSource coinAudio = this.transform.GetComponent<AudioSource>();
        coinAudio.volume = UnityEngine.Random.Range(0.8f, 1.0f);
        coinAudio.pitch = UnityEngine.Random.Range(0.85f, 1.1f);
        coinAudio.PlayOneShot(coinAudio.clip);
        GameManagerWeek3.instance.IncreaseScore(1);
    }
}

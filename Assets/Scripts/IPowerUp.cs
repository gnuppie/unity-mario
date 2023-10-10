using UnityEngine;

public interface IPowerup
{
    void DestroyPowerup();
    void GameRestart();
    void SpawnPowerup();
    void ApplyPowerup(MonoBehaviour i);


    PowerupType powerupType
    {
        get;
    }

    bool hasSpawned
    {
        get;
    }
}


public interface IPowerupApplicable
{
    public void RequestPowerupEffect(IPowerup i);
}
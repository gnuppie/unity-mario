using UnityEngine;

public interface IPowerupWeek4
{
    void DestroyPowerup();
    void GameRestart();
    void SpawnPowerup();
    void ApplyPowerup(MonoBehaviour i);


    PowerupTypeWeek4 powerupType
    {
        get;
    }

    bool hasSpawned
    {
        get;
    }
}


public interface IPowerupApplicableWeek4
{
    public void RequestPowerupEffect(IPowerupWeek4 i);
}
using UnityEngine;


public abstract class BasePowerup : MonoBehaviour, IPowerup
{
    public PowerupType type;
    public bool spawned = false;
    protected bool consumed = false;
    protected bool goRight = true;
    protected Rigidbody2D rigidBody;

    protected Vector3 startPosition;

    // base methods
    protected virtual void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        startPosition = new Vector3(0.0f, 0.85f, 0.0f);
    }

    // interface methods
    // 1. concrete methods
    public PowerupType powerupType
    {
        get // getter
        {
            return type;
        }
    }

    public bool hasSpawned
    {
        get // getter
        {
            return spawned;
        }
        set
        {
            spawned = value;
        }
    }

    public void DestroyPowerup()
    {
        Debug.Log("DestroyPowerup");
        rigidBody.velocity = Vector3.zero;
        this.gameObject.SetActive(false);
        // Destroy(this.gameObject);
    }

    public virtual void GameRestart()
    {
        rigidBody.velocity = Vector3.zero;
        this.gameObject.SetActive(true);
        this.gameObject.transform.localPosition = startPosition;
        this.gameObject.GetComponentInChildren<Animator>().SetTrigger("gameRestart");

    }

    // 2. abstract methods, must be implemented by derived classes
    public abstract void SpawnPowerup();
    public abstract void ApplyPowerup(MonoBehaviour i);
}

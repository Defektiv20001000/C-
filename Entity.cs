using UnityEngine;

public class Entity : MonoBehaviour
{
    public virtual void GetDam()
    {

    }
    public virtual void Death()
    {
        Destroy(this.gameObject);
    }
}

using UnityEngine;

public class CoinTake : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Dodik.Instance.gameObject)
        {
            Dodik.Instance.CoinTake();
            Destroy(this.gameObject);
        }
    }
}

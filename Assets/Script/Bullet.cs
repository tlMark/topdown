using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
	    if (collision.GetComponent<EnemyMovement>())
		    {
			    Destroy(collision.gameObject);
			    Destroy(gameObject);
		    }
    }
}

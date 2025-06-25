using UnityEngine;

public class Pipe : MonoBehaviour
{
    public float pipeSpeed;
    public bool move;


    // Update is called once per frame
    void Update()
    {
        if (!move) return;
        transform.Translate(new Vector3(-1, 0, 0) * pipeSpeed * Time.deltaTime);
        if (transform.position.x < -11) transform.gameObject.SetActive(false);

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.GetComponent<AlienMovement>() != null)
        {
            GameManager.Instance.AddScore();
        }
    }
}

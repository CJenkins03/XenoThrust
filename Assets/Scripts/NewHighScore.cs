using UnityEngine;

public class NewHighScore : MonoBehaviour
{
    public float speed;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, 1, 0) * speed * Time.deltaTime);
        if(transform.position.y >= 9) transform.gameObject.SetActive(false);
    }
}

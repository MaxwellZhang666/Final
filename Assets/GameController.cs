using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public float spawnSpeed = 3f;
    public GameObject Duck;
    public GameObject Duck2;
    public Texture2D crosshairCursor;
    public GameObject Table;
    public float duckDelay = 1.5f;
    public float duck2Delay = 1.5f;

    private bool notgameover = true;
    private bool mouseLeftDown = false;
    private Vector2 clickedPos;
    private float nextDuckTime = 0f;
    private float nextDuck2Time = 0f;

    float pos = 9.2f;
    float dir = 1f;

    // Update is called once per frame
    private void Update()
    {
        if (notgameover)
        {
            if (Time.time >= nextDuckTime)
            {
                SpawnDuck();
                nextDuckTime = Time.time + duckDelay;
                if (duckDelay >= 1.2f) duckDelay *= 0.9f;
                if (duckDelay < 1.2f && duckDelay > 0.5f) duckDelay *= 0.99f;
            }
            mouseLeftDown = Input.GetMouseButtonDown(0);
            if (Time.time >= nextDuck2Time)
            {
                SpawnDuck2();
                nextDuck2Time = Time.time + duck2Delay;
                if (duck2Delay >= 1.2f) duck2Delay *= 0.9f;
                if (duck2Delay < 1.2f && duck2Delay > 0.5f) duck2Delay *= 0.99f;
            }
            mouseLeftDown = Input.GetMouseButtonDown(0);

            if (mouseLeftDown)
            {
                GetComponent<AudioSource>().Play();

                clickedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(clickedPos, clickedPos, 0f);

                if (hit && hit.collider.gameObject.layer == LayerMask.NameToLayer("Duck"))
                {
                    hit.collider.SendMessage("GetDown", SendMessageOptions.DontRequireReceiver);
                }
                if (hit && hit.collider.gameObject.layer == LayerMask.NameToLayer("Duck2"))
                {
                    hit.collider.SendMessage("GetDown", SendMessageOptions.DontRequireReceiver);
                }
            }
        }
        else Debug.Log("Game Over!");
    }

    private void Start()
    {
        Cursor.SetCursor(crosshairCursor, new Vector2(crosshairCursor.width / 2, crosshairCursor.height / 2), CursorMode.Auto);
    }
    public void SpawnDuck()
    {
        float newScale = Random.Range(0.7f, 1.8f);
        Vector3 randomPositionRight = new Vector3(pos, Random.Range(-0.3f, 0.6f));
        GameObject newDuck = Instantiate(Duck, randomPositionRight, Quaternion.identity) as GameObject;
        newDuck.transform.localScale = new Vector3(newScale, newScale, 1f);
        newDuck.SendMessage("SetDirection", dir, SendMessageOptions.DontRequireReceiver);
        dir *= -1f;
        pos *= -1f;
    }

    public void SpawnDuck2()
    {
        float newScale = Random.Range(0.7f, 1.8f);
        Vector3 randomPositionRight = new Vector3(pos, Random.Range(-0.3f, 0.6f));
        GameObject newDuck2 = Instantiate(Duck2, randomPositionRight, Quaternion.identity) as GameObject;
        newDuck2.transform.localScale = new Vector3(newScale, newScale, 1f);
        newDuck2.SendMessage("SetDirection", dir, SendMessageOptions.DontRequireReceiver);
        dir *= 1f;
        pos *= 1f;
    }

}

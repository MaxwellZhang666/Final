using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckController : MonoBehaviour
{
    public float xSpeed = 0f;
    public float xSpeedrandom = 0.25f;
    private float ySpeed = 0f;
    private bool isAlive = true;
    private SpriteRenderer theSpriteRenderer;
    Collider2D m_Collider;

    void Start()
    {
        theSpriteRenderer = GetComponent<SpriteRenderer>();
        m_Collider = GetComponent<Collider2D>();
        xSpeed = xSpeed + Random.Range(-xSpeedrandom, xSpeedrandom);
        if (xSpeed >= 0) theSpriteRenderer.flipX = true;
        else theSpriteRenderer.flipX = false;
        StartCoroutine(ChangeDirection(Random.Range(2.5f, 4f)));
        Invoke("Disappear", 20);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.Translate(xSpeed * Time.deltaTime, ySpeed * Time.deltaTime, 0f);
    }

    public void SetDirection(float dir)
    {
        xSpeed *= dir;
    }

    public void GetDown()
    {
        isAlive = false;
        m_Collider.enabled = false;
        theSpriteRenderer.flipY = true;
        ySpeed = -1f;
        StartCoroutine(DisappearSec(0.7f));
        Destroy(GetComponent<Animator>());
        StartCoroutine(PlaySound(0.3f));
    }

    IEnumerator DisappearSec(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
    IEnumerator PlaySound(float time)
    {
        yield return new WaitForSeconds(time);
        GetComponent<AudioSource>().Play();
    }
    IEnumerator ChangeDirection(float time)
    {
        yield return new WaitForSeconds(time);
        if (isAlive) ySpeed = Mathf.Abs(xSpeed);
    }

}

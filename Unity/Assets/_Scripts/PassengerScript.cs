using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassengerScript : MonoBehaviour
{
    bool dragOn = false;
    bool touch = false;
    bool seated = false;
    bool moveY = true, moveX = false;
    Vector2 position;
    private SeatScript seat = null;
    int count = 0;
    private GameScript gameScript;
    private float startTime = 0.005f;
    public Image image;
    private float radius;
    private int audioIndex = 0;
    public AudioSource audioHit;
    public AudioSource[] audioDrag;
    public AudioSource audioDead;

    void Awake()
    {
        radius = GetComponent<RectTransform>().sizeDelta.x/2;
        image = GetComponent<Image>();
        audioHit = GameObject.Find("hit").GetComponent<AudioSource>();
        audioDrag = new AudioSource[3];
        audioDrag[0] = GameObject.Find("drag").GetComponent<AudioSource>();
        audioDrag[1] = GameObject.Find("drag2").GetComponent<AudioSource>();
        audioDrag[2] = GameObject.Find("drag3").GetComponent<AudioSource>();
        for (int i=0;i<audioDrag.Length;i++) {
            audioDrag[i].loop = true;
        }
        audioDead = GameObject.Find("dead").GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start()
    {
        GameObject gameController = GameObject.Find("GameController");
        if (gameController != null)
        {
            gameScript = gameController.GetComponent<GameScript>();
            seat = gameScript.getSeat();
            position = seat.GetComponent<RectTransform>().position;
            StartCoroutine(FindSeat());
        }
    }

    public void DestroyPassenger()
    {
        GameState.points++;
        GameState.checkLevel();
        seat.free = true;
        audioDrag[audioIndex].Stop();
        audioDead.Play();
        GameScript.countSeats--;
        Destroy(gameObject);

    }

    private IEnumerator FindSeat()
    {
        Vector2 startPosition = new Vector2(transform.position.x, transform.position.y);
        float rate = 0.5f;
        float t = 0.0f;
        while (t < 1.0)
        {
            // make the character desappear
            t += Time.deltaTime * rate;
            transform.position = Vector3.Lerp(startPosition, new Vector2(transform.position.x, position.y), Mathf.SmoothStep(0.0f, 1.0f, t));
            yield return null;
        }
        t = 0.0f;
        startPosition = new Vector2(transform.position.x, transform.position.y);
        while (t < 1.0)
        {
            t += Time.deltaTime * rate;
            // make the character desappear
            transform.position = Vector2.Lerp(startPosition, new Vector2(position.x, transform.position.y), Mathf.SmoothStep(0.0f, 1.0f, t));
            yield return null;
        }
        seated = true;
        yield return new WaitForSeconds(1f);
    }

    private void DragOnState(Vector3 position)
    {
        Vector2 deltaPos = position - transform.position;
        if (deltaPos.magnitude > radius)
        {
            GetComponent<Rigidbody2D>().velocity = deltaPos.normalized * 1500;

        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        }
    }

    // Update is called once per frame
    void Update()
    {

        float gb = Mathf.Clamp(count, 0, 3);
        image.color = new Color(1.0f, 1.0f - (0.3f * gb), 1.0f - (0.3f * gb));

        if (seated)
        {
#if UNITY_EDITOR
            //mouse event
            if (Input.GetMouseButtonDown(0))
            {
                if (count >= 3 && Vector3.Distance(Input.mousePosition, transform.position) < radius)
                {
                    dragOn = true;
                    audioIndex = (int)Mathf.Round(Random.value * (audioDrag.Length - 1));
                    audioDrag[audioIndex].Play();
                }
                //else if (count <= 3)
                //{
                //    touch = true;
                //}
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (count >= 3)
                {
                    dragOn = false;
                    audioDrag[audioIndex].Stop();
                }
                else
                {
                    if (Vector3.Distance(Input.mousePosition, transform.position) < radius)
                    {
                        count++;
                        audioHit.Play();

                    }
                    //touch = false;
                }
            }
            if (dragOn)
            {
                DragOnState(Input.mousePosition);

            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;


            }
#else
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (count >= 3 && Vector3.Distance(Input.GetTouch(0).position, transform.position) < radius)
                {
                    dragOn = true;
                    audioIndex= (int)(Random.value*(audioDrag.Length-1));
                    audioDrag[audioIndex].Play();

                }
                //else {
                //    touch = true;
                //}

            }
            else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                if (count >= 3)
                {
                    dragOn = false;
                    audioDrag[audioIndex].Stop();

                }
                else {
                    if(Vector3.Distance(Input.GetTouch(0).position, transform.position) < radius)
                    {
                        count++;
                        audioHit.Play();
                    }
                    //touch = false;
                }
            }

            if (dragOn) {
            Vector3 pos = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0);
            DragOnState(pos);

            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;


            }
#endif
        }
    }
}

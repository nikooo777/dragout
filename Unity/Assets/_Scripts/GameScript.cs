using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameScript : MonoBehaviour {
    public SeatScript[] seats;
    public GameObject passengerPrefab;
    public GameObject parent;
    public Text level, points;
    private const int WAIT_TIME = 500;
    private int time = 0;

    public QuitHandler qh;
    public static int countSeats = 0;

    // Use this for initialization
    void Start () {
        GameState.level = 1;
        GameState.points = 0;
        countSeats = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (time <= 0)
        {
            GameObject passenger = Instantiate(passengerPrefab) as GameObject;
            passenger.transform.SetParent(parent.transform, false);
            time = WAIT_TIME/GameState.level;
        }
        else {
            time--;
        }
        level.text = "Level: "+GameState.level;
        points.text = "Points: " + GameState.points;

        if (Input.GetKeyDown(KeyCode.Escape))
            qh.DoQuit();
            
    }

    public SeatScript getSeat() {
        bool searchSeat = true;
        int index = 0;

        if(countSeats>=seats.Length)
        {
            searchSeat = false;
            AdManager.Instance.HideBanner();
            SceneManager.LoadScene(3);
        }

        while (searchSeat) {
            index = (int)((seats.Length-1)*Random.value);
            if (seats[index].free)
            {
                seats[index].free = false;
                countSeats++;
                searchSeat = false;
            }

            
        }

            
        return seats[index];
    }
}

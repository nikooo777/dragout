using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showad : MonoBehaviour {

    public bool topNotBottom;

	// Use this for initialization
	void Start () {

        AdManager.Instance.ShowBanner(topNotBottom);
    }

}

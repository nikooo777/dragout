using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowVideoAd : MonoBehaviour {

    public void showVideo()
    {
        AdManager.Instance.ShowVideo();
    }
}

﻿using UnityEngine;
using System.Collections;

public class sanBarControl : MonoBehaviour {
		RectTransform progress;


		float maxpwidth;
	// Use this for initialization
	void Start () {
				progress = transform.FindChild("progress").gameObject.GetComponent<RectTransform>();
				maxpwidth = progress.rect.width;
	}
	
	


	// Update is called once per frame
	void Update () {
				float rito = (PlayerData.P_san / PlayerData.Maxsan) * maxpwidth;
				progress.sizeDelta = new Vector2 (rito,progress.rect.height);
	}



}

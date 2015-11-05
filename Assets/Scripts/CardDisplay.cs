using UnityEngine;
using System.Collections;
using System.Data;
using System.IO;
using System;

public class CardDisplay : MonoBehaviour {

	public GameObject[] GameObjs;
	public SpriteRenderer[] sprites;
	public Sprite defaultImage;

	private GameObject SearchObj;
	private ArrayList results;

	private int currentIndex = 0;
	private string sortMode = "cardname";
	private string sortDir = "asc";

	Search searchController;
	// Use this for initialization
	void Start () {
		results = new ArrayList ();
		GameObjs = new GameObject[12];
		sprites = new SpriteRenderer[12];

		for(int i = 0; i < 12; i++)
		{
			GameObjs[i] = GameObject.Find ("Card (" + (i).ToString() + ")");
			sprites[i] = GameObjs[i].GetComponent<SpriteRenderer>();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void InitCardDisplay(ArrayList resData)
	{
		results = resData;
		currentIndex = 0;

		//apply sorting here

		Debug.Log("num res: " + results.Count);

		for (int i = 0; i < 12; i++) 
		{
			if(i < results.Count)
			{
				sprites[i].color = new Color(255f, 255f, 255f, 255f);
				SetImage(i);
			}
			else //no more results
			{
				sprites[i].color = new Color(255f, 255f, 255f, 0f);
				sprites[i].sprite = defaultImage;
			}
		}
	}

	private void SetImage(int i)
	{
		Result c = (Result)results [i+currentIndex];

		if (c.sprite != null) {
			sprites [i].sprite = c.sprite;
		} 
		else 
		{
			string imgpath = Application.dataPath + "/" + c.ImagePath.Replace("\\","/");

			if (!File.Exists (imgpath)) 
			{
				StartCoroutine(FetchImage(i, updateImage));
			} 
			else
			{
				Texture2D tex =	LoadTexture(imgpath, i);

				updateImage(i, tex);
			}
		}
	}
		
	private Texture2D LoadTexture(string imgpath, int i) 
	{
		if (File.Exists (imgpath)) {
			byte[] FileData = File.ReadAllBytes (imgpath);
			Texture2D Tex2D = new Texture2D (2, 2);          
			if (Tex2D.LoadImage (FileData))           
				return Tex2D;    
		}
		return null;
	}

    IEnumerator FetchImage(int i, Action<int,Texture2D> updateCallback)
	{
		Result c = (Result)results [i+currentIndex];
		
		string URL = "";
		string gatherer = "http://gatherer.wizards.com/Handlers/Image.ashx?type=card&multiverseid=";
		string gathererRotated = "http://gatherer.wizards.com/Handlers/Image.ashx?type=card&options=rotate180&multiverseid=";
		
		if (c.Card_id > c.isSplit && c.isSplit != 0 && c.SetName.ToLower().Contains("kamigawa"))
		{
			//is 2nd part of flip card, get rotated image
			URL = gathererRotated;
		}
		else
		{
			URL = gatherer;
		}
		
		WWW www = new WWW (URL+c.multiverse_id);
		yield return www;
		
		updateImage (i, www.texture);
	}

	public void updateImage(int i, Texture2D tex)
	{
		Sprite cardImg = null;

		((Result)results [i+currentIndex]).texture = tex;
		
		Result c = (Result)results [i+currentIndex];
		
		if(c.texture != null)
		{
			cardImg = Sprite.Create(c.texture,new Rect(0, 0, c.texture.width, c.texture.height),new Vector2(0,0), 100.0f);
			//save card image
		}
		else
		{
			//TODO: build up image from text fields and a blank card
			cardImg = defaultImage;
		}

		((Result)results [i+currentIndex]).sprite = cardImg;
		sprites [i].color = new Color (255f, 255f, 255f, 0f);
		sprites[i].sprite = cardImg;
		float x = 0.68f;
		float y = 0.68f;
		sprites[i].transform.localScale = new Vector3(x,y);
		sprites [i].color = new Color (255f, 255f, 255f, 255f);

	}


	
}

using UnityEngine;
using System.Collections;

public class sceneButtonController : MonoBehaviour {


	public string targetScene;
	public Sprite hoverSprite;
	public Sprite pressSprite;
	public float transitionWaitTime;


	private Sprite m_defaultSprite;
	private SpriteRenderer m_SpriteRenderer;
	private AudioSource m_stoneSfx;
	private AudioSource m_glowSfx;
	private AudioSource[] srcs;

	void Start () {
		m_SpriteRenderer = gameObject.GetComponent<SpriteRenderer>(); 
		srcs = GetComponents<AudioSource>();
		m_stoneSfx = srcs [0];
		m_glowSfx = srcs [1];

		m_defaultSprite = m_SpriteRenderer.sprite;
	}

	void Update () {
	
	}

	void OnMouseEnter() { 
		m_SpriteRenderer.sprite = hoverSprite;
		m_stoneSfx.Play();
	} 

	void OnMouseExit() { 
		m_SpriteRenderer.sprite = m_defaultSprite;
	} 

	IEnumerator OnMouseDown() {
		m_SpriteRenderer.sprite = pressSprite;
		m_glowSfx.Play();

		yield return new WaitForSeconds(transitionWaitTime);

		if (targetScene != "") {
			Application.LoadLevel (targetScene);
		} else {
			Application.Quit();
		}
	}
}

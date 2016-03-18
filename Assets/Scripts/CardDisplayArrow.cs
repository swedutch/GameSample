using UnityEngine;
using System.Collections;

public class CardDisplayArrow : MonoBehaviour {

	public bool forward;

	public Color hoverColor;
	public Color pressColor;
	public Color inactiveColor;
	public Color activeColor;
	public float transitionWaitTime;
	public GameObject DisplayControllerObj;

	public bool isActive;
	private Color m_defaultColor;
	private SpriteRenderer m_SpriteRenderer;
	private AudioSource m_stoneSfx;
	private AudioSource m_glowSfx;
	private AudioSource[] m_srcs;
	private CardDisplay m_CardDisplay;
	
	void Start () {
		isActive = false;
		m_SpriteRenderer = gameObject.GetComponent<SpriteRenderer>(); 
		if (isActive) {
			m_SpriteRenderer.color = activeColor;
		} else {
			m_SpriteRenderer.color = inactiveColor;
		}
	//	m_srcs = GetComponents<AudioSource>();
	//	m_stoneSfx = m_srcs [0];
	//	m_glowSfx = m_srcs [1];
	
		m_CardDisplay = DisplayControllerObj.GetComponent<CardDisplay> ();
		m_defaultColor = m_SpriteRenderer.color;
	}

	public void Activate()
	{
		m_SpriteRenderer.color = activeColor;
		m_defaultColor = activeColor;
		isActive = true;
	}

	public void Deactivate()
	{
		Debug.Log ("Deactivate");
		m_SpriteRenderer.color = inactiveColor;
		m_defaultColor = inactiveColor;
		isActive = false;
	}
	
	void OnMouseEnter() { 
		Debug.Log ("over");
		if (isActive) {
			m_SpriteRenderer.color = hoverColor;
			m_defaultColor = hoverColor;
			//	m_stoneSfx.Play();
		}
	} 
	
	void OnMouseExit() {
		Debug.Log ("off");
		if (isActive) {
			m_SpriteRenderer.color = activeColor;
			m_defaultColor = activeColor;
		}
	} 
	
	IEnumerator OnMouseDown() {
		if (isActive) {
			m_SpriteRenderer.color = pressColor;
			//	m_glowSfx.Play();
		
			if (forward) {
				m_CardDisplay.Next();
			} else {
				m_CardDisplay.Prev();
			}

			yield return new WaitForSeconds (transitionWaitTime);

			m_SpriteRenderer.color = m_defaultColor;
		}
	}
}

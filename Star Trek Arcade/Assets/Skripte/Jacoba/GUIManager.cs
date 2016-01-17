using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {

	public RenderTexture MiniMapTexture;
	public Material MiniMapMaterial;
	public float size;
	
	float offset;

	// Use this for initialization
	void Awake () {
		offset = 10f;
	}

	void OnGUI () {
		if(Event.current.type == EventType.Repaint) {
			Graphics.DrawTexture(new Rect(Screen.width - size - offset, offset, size, size), MiniMapTexture, MiniMapMaterial);
		}
	}
}

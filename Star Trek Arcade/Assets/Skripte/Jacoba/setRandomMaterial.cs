using UnityEngine;
using System.Collections;

public class setRandomMaterial : MonoBehaviour {

	public Material[] materials;
    public Renderer rend;

	void Awake () {
		rend = GetComponent<Renderer>();
        rend.enabled = true;
	}

	void Start () {
		if (materials.Length == 0) {
            return;
		}
        int index =  Random.Range(0, materials.Length);

		// assign it to the renderer
        rend.sharedMaterial = materials[index];
	}
}

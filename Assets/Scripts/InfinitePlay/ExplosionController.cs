using UnityEngine;
using TMPro;

public class ExplosionController : MonoBehaviour {

    public TextMeshPro text;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 3f);
	}

    public void ShowPoints(int points)
    {
        text.SetText("+{0}", points);
        text.gameObject.SetActive(true);
    }
}

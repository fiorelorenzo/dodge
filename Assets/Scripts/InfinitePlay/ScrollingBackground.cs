using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ScrollingBackground : MonoBehaviour {

    public Rigidbody2D target;
    public float scrollSpeed = 1.0f;

    private MeshRenderer background;

    // Use this for initialization
    void Start () {
        //NewScale(this.gameObject, Camera.main.pixelWidth);
        background = GetComponent<MeshRenderer>();
        
	}

    private void LateUpdate()
    {
        if (GameManager.Instance.IsPlayerAlive())
        {
            Vector2 moving = (Vector2)target.transform.up;
            float horizontalScrollSpeed = Mathf.Abs(target.velocity.x);
            float verticalScrollSpeed = Mathf.Abs(target.velocity.y);
            //background.material.mainTextureOffset += new Vector2((moving.x * Time.deltaTime * horizontalScrollSpeed * scrollSpeed), (moving.y * Time.deltaTime * verticalScrollSpeed * scrollSpeed));
            background.material.mainTextureOffset += new Vector2((moving.x * Time.deltaTime * horizontalScrollSpeed * scrollSpeed) % 1, (moving.y * Time.deltaTime * verticalScrollSpeed * scrollSpeed) % 1);
        }
    }

    void NewScale(GameObject theGameObject, float newSize)
    {

        float size = theGameObject.GetComponent<Renderer>().bounds.size.y;

        Vector3 rescale = theGameObject.transform.localScale;

        rescale.y = newSize * rescale.y / size;

        theGameObject.transform.localScale = rescale;

    }
}

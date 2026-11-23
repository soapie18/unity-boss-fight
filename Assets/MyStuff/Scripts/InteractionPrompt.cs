using UnityEngine;

public class InteractionPrompt : MonoBehaviour
{
    public BoxCollider collider;
    private SpriteRenderer sprite;
    private Color off;
    private Color on;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        off = new Color(0, 0, 0, 0);
        on = sprite.color;

    }


    void Show()
    {
        sprite.color = on;
    }

    void Hide()
    {
        sprite.color = off;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hi");
        Show();
    }

    private void OnTriggerExit(Collider other)
    {
        Hide();
    }

    UPdate

}

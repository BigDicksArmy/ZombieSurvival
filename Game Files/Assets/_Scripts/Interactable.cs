using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public abstract class Interactable : MonoBehaviour
{
    new public string name = "";
    public float InteractionRadius = 0;

    protected SpriteRenderer Renderer;
    new protected CircleCollider2D collider;

    public virtual void Use()
    {
        Debug.Log("You have interacted with" + name);
    }
}
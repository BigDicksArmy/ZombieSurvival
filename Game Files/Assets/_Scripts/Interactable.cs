using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public abstract class Interactable : MonoBehaviour
{
	public float interactionRadius = 0;
	new public string name = "";

	new protected CircleCollider2D collider;

	public virtual void Use()
	{
		Debug.Log("You have interacted with" + name);
	}
}
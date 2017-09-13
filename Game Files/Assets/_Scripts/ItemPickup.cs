using UnityEngine;
using Utility;
using System;

[RequireComponent(typeof(AudioSource))]
public class ItemPickup : Interactable
{
    public AudioClip sound;

    private AudioSource source;
    private Weapon obj;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        collider = GetComponent<CircleCollider2D>();
        Renderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        source.clip = sound;
        collider.radius = InteractionRadius;

        try
        {
            obj = EquipmentController.Instance.Inventory.Find(name);
        }
        catch (NullReferenceException)
        {
            Debug.LogError("Attach the weapon to the player under WeaponHolder object");
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerHB"))
        {
            if (!obj.IsCollected)
            {
                obj.IsCollected = true;
                EquipmentController.Instance.SelectWeapon(obj);
                EquipmentController.Instance.WeaponsCount++;
                source.Play();
                Renderer.enabled = false;

                Destroy(gameObject, sound.length);
                return;
            }
            Destroy(gameObject);
        }
    }
}

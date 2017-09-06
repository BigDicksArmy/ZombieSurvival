using UnityEngine;

//This script only works when the camera attached is ortographic
[RequireComponent(typeof(SpriteRenderer))]
public class MouswiseRotationApplier : MonoBehaviour
{
    public float rotationOffset;

    private Camera mainCamera;
    private Transform ArmTransform;
    private SpriteRenderer spriteRend;
    private bool isFlipped = false;

    private void Awake()
    {
        mainCamera = Camera.main;
        spriteRend = GetComponent<SpriteRenderer>();
        ArmTransform = transform;
    }
    void Update()
    {
        //Vector3 difference = mainCamera.ScreenToWorldPoint(Input.mousePosition) - ArmTransform.localPosition;
        Vector3 difference = mainCamera.ScreenToWorldPoint(Input.mousePosition) - ArmTransform.position;
        difference.Normalize();
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg + rotationOffset;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
        //Flipping the sprites of the player and the weapon
        if (Mathf.Abs(rotationZ) > 90 && !isFlipped)
        {
            isFlipped = true;
            spriteRend.flipY = true;
        }
        if (Mathf.Abs(rotationZ) < 90 && isFlipped)
        {
            isFlipped = false;
            spriteRend.flipY = false;
        }
    }
}

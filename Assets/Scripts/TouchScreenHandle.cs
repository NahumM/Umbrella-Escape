using UnityEngine;

public class TouchScreenHandle : ITouchInput
{
    PlayerController playerC;
    public TouchScreenHandle(PlayerController playerController)
    {
        playerC = playerController;
    }
    public void OnTouchMove(Touch touch, Transform playerTransform, Rigidbody rb)
    {
        if (touch.phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            Plane plane = new Plane(Vector3.right, playerTransform.position);
            float distance;
            Vector3 positionToMove = Vector3.zero;
            if (plane.Raycast(ray, out distance))
                positionToMove = ray.GetPoint(distance);
            float directionZ = positionToMove.z - playerTransform.position.z;
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, Mathf.Sign(directionZ) * playerC.GetForce());
            playerC.GetAudioController().PlaySwingSound();
        }
    }
}

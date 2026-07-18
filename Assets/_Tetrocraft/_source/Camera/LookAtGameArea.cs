using UnityEngine;

public class LookAtGameArea : MonoBehaviour
{
    [SerializeField] private Transform pointLook;
    [SerializeField] private float minPosY = 4f;
    [SerializeField] private float maxPosY = 18f;  

    private Vector3 _targetPos;

    private void Start()
    {
        _targetPos = transform.position;
    }

    private void LateUpdate()
    {
        if (pointLook != null)
        {
            transform.LookAt(pointLook);
        }
        MovePosY(_targetPos);

    }

    public void SetPosYCamera(int posY)
    {
        Vector3 pos = transform.position;
        
        float t = Mathf.InverseLerp(0, 20, posY);
        pos.y = Mathf.Lerp(minPosY, maxPosY, t);
        _targetPos = pos;
    }

    void MovePosY(Vector3 pos)
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPos, Time.deltaTime);
    }
}
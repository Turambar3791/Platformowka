using UnityEngine;

public class ShowNpc : MonoBehaviour
{
    [SerializeField] private float x;
    [SerializeField] private float y;

    void Start()
    {
        if (SaveLoadSystem.Instance.data.is1stLevelCompeleted)
        {
            transform.SetPositionAndRotation(new Vector3(x, y, 0), transform.rotation);
        }
    }
}

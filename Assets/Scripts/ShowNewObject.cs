using UnityEngine;

public class ShowNewObject : MonoBehaviour
{
    [SerializeField] private float x;
    [SerializeField] private float y;
    [SerializeField] private int whichLevelCompleted;

    void Start()
    {
        if (whichLevelCompleted == 1 && SaveLoadSystem.Instance.data.is1stLevelCompeleted)
        {
            transform.SetPositionAndRotation(new Vector3(x, y, 0), transform.rotation);
        }
        
        if (whichLevelCompleted == 2 && SaveLoadSystem.Instance.data.is2ndLevelCompleted)
        {
            transform.SetPositionAndRotation(new Vector3(x, y, 0), transform.rotation);
        }

        if (whichLevelCompleted == 3 && SaveLoadSystem.Instance.data.is3rdLevelCompleted)
        {

        }
    }
}

using UnityEngine;

public class Animatable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

}

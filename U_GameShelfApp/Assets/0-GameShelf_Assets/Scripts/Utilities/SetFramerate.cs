using UnityEngine;


public class SetFramerate : MonoBehaviour
{

    public int targetFrameRate;
    public bool debugFPS;
    public TMPro.TextMeshProUGUI fpsCounter;

    // Start is called before the first frame update
    void Start()
    {
        SetApplicationFramerate();
    }


    int frameCount = 1;
    int maxFrames = 10;
    // Update is called once per frame
    void Update()
    {
        if(debugFPS && frameCount>= maxFrames)
        {
            frameCount = 1;
            fpsCounter.text = ((int)((float)1 / (float)Time.deltaTime)).ToString() + " FPS";
        }
        frameCount++;


    }

    private void SetApplicationFramerate()
    {
        Application.targetFrameRate = targetFrameRate;
    }
}

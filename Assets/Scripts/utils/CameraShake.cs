using UnityEngine;
using System.Collections;
public class CameraShake : MonoBehaviour
{
    public float shakeTime;
    public float fps;
    public float frameTime;
    public float shakeDelta;
    public Camera cam;
    public bool isshakeCamera = false;
    // Use this for initialization
    void Start()
    {
        reset();
        cam = gameObject.GetComponent<Camera>();
    }

    private void reset()
    {
        shakeTime = 0.5f;
        fps = 60.0f;
        frameTime = 0;
        shakeDelta = 0.01f;
    }

    public void Shake()
    {
        isshakeCamera = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isshakeCamera)
        {
            if (shakeTime > 0)
            {
                shakeTime -= Time.deltaTime;
                if (shakeTime <= 0)
                {
                    cam.rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
                    isshakeCamera = false;
                    reset();
                }
                else
                {
                    frameTime += Time.deltaTime;

                    if (frameTime > 1.0 / fps)
                    {
                        frameTime = 0;
                        cam.rect = new Rect(shakeDelta * (-1.0f + 2.0f * Random.value), shakeDelta * (-1.0f + 2.0f * Random.value), 1.0f, 1.0f);
                    }
                }
            }
        }
    }
}
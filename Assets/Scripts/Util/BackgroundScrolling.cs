using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScrolling : MonoBehaviour
{
    public float speed;
    public Transform[] backgrounds;
    public Camera BackgroundCam;

    float leftPosX = 0f;
    float backgroundWidth;

    void Start()
    { 
        if (BackgroundCam == null)
        {
            BackgroundCam = Camera.main; 
        }

        // 카메라의 크기 계산
        float yScreenHalfSize = BackgroundCam.orthographicSize;
        float xScreenHalfSize = yScreenHalfSize * BackgroundCam.aspect;

        backgroundWidth = backgrounds[0].GetComponent<SpriteRenderer>().bounds.size.x;
        leftPosX = -(xScreenHalfSize + (backgroundWidth / 2));
    }

    void Update()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);

            if (backgrounds[i].position.x < leftPosX)
            {
                Vector3 nextPos = backgrounds[i].position;
                nextPos.x += backgrounds.Length * backgroundWidth - 0.6f; 
                backgrounds[i].position = nextPos;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    public float speed;
    public Transform[] backgrounds;

    float leftPosX = 0f;
    float rightPosX = 0f;
    float xScreenHalfSize;
    float yScreenHalfSize;

    // 각 배경 이미지의 너비를 저장할 배열
    float[] backgroundWidths;

    // Start is called before the first frame update
    void Start()
    {
        // 카메라 크기 계산
        yScreenHalfSize = Camera.main.orthographicSize;
        xScreenHalfSize = yScreenHalfSize * Camera.main.aspect;

        // 각 배경 이미지의 너비 배열 초기화
        backgroundWidths = new float[backgrounds.Length];

        for (int i = 0; i < backgrounds.Length; i++)
        {
            // 배경 이미지의 SpriteRenderer에서 너비를 계산하여 배열에 저장
            backgroundWidths[i] = backgrounds[i].GetComponent<SpriteRenderer>().bounds.size.x;
        }

        // 왼쪽 끝 위치 설정 (첫 번째 배경을 기준으로)
        leftPosX = -(xScreenHalfSize + (backgroundWidths[0] / 2));  // 첫 번째 배경 이미지의 너비로 계산
        rightPosX = backgroundWidths[0] * backgrounds.Length;  // 첫 번째 배경 이미지의 총 길이 계산
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            // 현재 배경의 위치를 왼쪽으로 이동
            backgrounds[i].transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);

            // 배경이 화면의 왼쪽 끝을 벗어나면
            if (backgrounds[i].position.x < leftPosX)
            {
                // 배경을 가장 오른쪽 끝으로 이동 (각 배경 이미지의 너비 기준으로)
                Vector3 nextPos = backgrounds[i].position;
                nextPos.x += backgrounds.Length * backgroundWidths[i];  // 각 배경 이미지의 너비를 사용
                backgrounds[i].position = nextPos;
            }
        }
    }
}

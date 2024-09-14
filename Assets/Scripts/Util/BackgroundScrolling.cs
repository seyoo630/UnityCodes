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
    // Start is called before the first frame update
    void Start()
    {
        // 카메라 크기 계산
        yScreenHalfSize = Camera.main.orthographicSize;
        xScreenHalfSize = yScreenHalfSize * Camera.main.aspect;

        // 배경 이미지의 너비 계산 (backgrounds 배열 중 첫 번째 배경의 SpriteRenderer로 너비를 계산)
        float backgroundWidth = backgrounds[0].GetComponent<SpriteRenderer>().bounds.size.x;

        // 화면에서 왼쪽으로 벗어나는 기준값 설정
        leftPosX = -(xScreenHalfSize + (backgroundWidth / 2));  // 카메라 너비보다 배경 이미지 반 너비만큼 더 이동
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
                // 배경을 가장 오른쪽 끝으로 이동시킴 (배경 이미지의 x 길이 19를 기준으로)
                Vector3 nextPos = backgrounds[i].position;
                nextPos.x += backgrounds.Length * 19;  // backgrounds.Length는 배경의 개수
                backgrounds[i].position = nextPos;
            }
        }
    }

}
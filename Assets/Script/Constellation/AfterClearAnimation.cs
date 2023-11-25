using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterClearAnimation : MonoBehaviour
{
    public GameObject Line_O;
    public GameObject NextStar;
    public int numOfStars;

    public void AfterAnimation(){
        float[] colorSet = new float[3]; // {} 3개의 색(RGB) 세트 정하기
        Line_O.SetActive(false);
        float[] RandomStarSet = new float[3]; //1f 할거 정하기
        int RandomStar = 1;
        SpriteRenderer spriteRenderer = NextStar.GetComponent<SpriteRenderer>();
//(numOfStars -1), 1
        for(int k=0;k<3;k++){
            RandomStarSet[k] = (float) k;
            // Debug.Log("k" + RandomStarSet[k]);
        }
        RandomStar = (int)ChooseRandomNumber(RandomStarSet); // RGB 세트의 0,1,2 요소 중에 1f 할 요소 정하기
//(numOfStars -1), 2
        for(int i=0;i<3;i++){
            // Debug.Log("RandomStar 1f:"+RandomStar);
            if(RandomStar != i){
                float[] RandomValueSet = {0.3f, 0.5f};
                float RandomValue = ChooseRandomNumber(RandomValueSet); // 1f가 아닌 나머지 2개 요소의 색 정하기 
                colorSet[i] = RandomValue;
            }
            else{
                colorSet[i] = 1f;
            }
        }
        
        spriteRenderer.color = new Color(colorSet[0],colorSet[1],colorSet[2]);
    }
    float ChooseRandomNumber(float[] numbers){
        int randomIndex = Random.Range(0,numbers.Length);
        return numbers[randomIndex];
    }
}

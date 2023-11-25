using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashController : MonoBehaviour
{
    private Animator animator;
    public GameObject flashlight ; //자식 오브젝트 변수
    // flashlight player의 상하좌우로 위치하게 하는 변수 
    
    Vector3 pos; //vector3에 float 값 할당 불가능. new만들어줘야함.그냥 선언은 괜찮

    string bef_st = "stay"; //beforestate
    float moveX_p = 0, moveY_p = 0; //부모(player)의 X,Y좌표

    void Start()
    {
        animator = GetComponent<Animator>();
        flashlight = GameObject.Find("flashlightstay");
        pos = flashlight.transform.position;
        //Debug.Log("부모위치: " + flashlight.transform.parent.transform.position); //test용
        //Debug.Log("자식위치: " + flashlight.transform.position);
    }
    void Update()
    {
        moveX_p = flashlight.transform.parent.transform.position.x;
        moveY_p = flashlight.transform.parent.transform.position.y;
        //pos = flashlight.transform.position; //다시 손전등불빛 player 뒤로 원위치 //stay애니메이션 더 오래 나타남.쓰지X

        if (Input.GetKey(KeyCode.UpArrow))
        {
            if(Input.GetKey(KeyCode.LeftArrow)) //up,left키 동시에 눌렸을때
            {
                if(bef_st == "left") { animator.SetTrigger("LtoL_U");  }
                else if(bef_st == "up") { animator.SetTrigger("UtoL_U"); }
                pos.x = moveX_p - 1; pos.y = moveY_p + 1;
                bef_st = "l_u";
            }
            else if(Input.GetKey(KeyCode.RightArrow))
            {
                if(bef_st == "right") { animator.SetTrigger("RtoR_U"); }
                else if(bef_st =="up") { animator.SetTrigger("UtoR_U"); }
                pos.x = moveX_p + 1; pos.y = moveY_p + 1;
                bef_st = "r_u";
            }
            else
            {
                switch (bef_st)
                {
                    case "stay":
                        animator.SetTrigger("StoU"); break;

                    case "down":
                        animator.SetTrigger("DtoU"); break;

                    case "left":
                        animator.SetTrigger("LtoU"); break;

                    case "right":
                        animator.SetTrigger("RtoU"); break;

                    case "up": //이미 애니메이터 up상태임
                        break;

                    case "l_u":
                        animator.SetTrigger("L_UtoU"); break;

                    case "r_u":
                        animator.SetTrigger("R_UtoU"); break;

                    default: Debug.Log("상태가 이상합니다."); break;
                }
                pos.y = moveY_p + 1;
                bef_st = "up";
            }
            transform.position = pos; 
        }

        else if (Input.GetKey(KeyCode.DownArrow))
        {
            if (Input.GetKey(KeyCode.LeftArrow)) //down,left키 동시에 눌렸을때
            {
                if (bef_st == "left") { animator.SetTrigger("LtoL_D"); }
                else if (bef_st == "down") { animator.SetTrigger("DtoL_D"); }
                pos.x = moveX_p - 1; pos.y = moveY_p - 1;
                bef_st = "l_d";
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                if (bef_st == "right") { animator.SetTrigger("RtoR_D"); }
                else if (bef_st == "down") { animator.SetTrigger("DtoR_D"); }
                pos.x = moveX_p + 1; pos.y = moveY_p - 1;
                bef_st = "r_d";
            }
            else
            {
                switch (bef_st)
                {
                    case "stay":
                        animator.SetTrigger("StoD"); break;

                    case "up":
                        animator.SetTrigger("UtoD"); break;

                    case "left":
                        animator.SetTrigger("LtoD"); break;

                    case "right":
                        animator.SetTrigger("RtoD"); break;

                    case "down": //이미 애니메이터 down상태임
                        break;

                    case "l_d":
                        animator.SetTrigger("L_DtoD"); break;

                    case "r_d":
                        animator.SetTrigger("R_DtoD"); break;

                    default: Debug.Log("상태가 이상합니다."); break;
                }
                pos.y = moveY_p - 1;
                bef_st = "down";

            }
            transform.position = pos;
            
        }

        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (Input.GetKey(KeyCode.UpArrow)) //left, up키 동시에 눌렸을때
            {
                if (bef_st == "left") { animator.SetTrigger("LtoL_U"); }
                else if (bef_st == "up") { animator.SetTrigger("UtoL_U"); }
                pos.x = moveX_p - 1; pos.y = moveY_p + 1;
                bef_st = "l_u";
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                if (bef_st == "left") { animator.SetTrigger("LtoL_D"); }
                else if (bef_st == "down") { animator.SetTrigger("DtoL_D"); }
                pos.x = moveX_p - 1; pos.y = moveY_p - 1;
                bef_st = "l_d";
            }
            else
            {
                switch (bef_st)
                {
                    case "stay":
                        animator.SetTrigger("StoL"); break;

                    case "down":
                        animator.SetTrigger("DtoL"); break;

                    case "up":
                        animator.SetTrigger("UtoL"); break;

                    case "right":
                        animator.SetTrigger("RtoL"); break;

                    case "left": //이미 애니메이터 left상태임
                        break;

                    case "l_u":
                        animator.SetTrigger("L_UtoL"); break;

                    case "l_d":
                        animator.SetTrigger("L_DtoL"); break;

                    default: Debug.Log("상태가 이상합니다."); break;
                }
                pos.x = moveX_p - 1;
                bef_st = "left";
            }
            transform.position = pos;
            
            
        }

        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (Input.GetKey(KeyCode.UpArrow)) //left, up키 동시에 눌렸을때
            {
                if (bef_st == "right") { animator.SetTrigger("RtoR_U"); }
                else if (bef_st == "up") { animator.SetTrigger("UtoR_U"); }
                pos.x = moveX_p + 1; pos.y = moveY_p + 1;
                bef_st = "r_u";
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                if (bef_st == "right") { animator.SetTrigger("RtoR_D"); }
                else if (bef_st == "down") { animator.SetTrigger("DtoR_D"); }
                pos.x = moveX_p + 1; pos.y = moveY_p - 1;
                bef_st = "r_d";
            }
            else
            {
                switch (bef_st)
                {
                    case "stay":
                        animator.SetTrigger("StoR"); break;

                    case "down":
                        animator.SetTrigger("DtoR"); break;

                    case "left":
                        animator.SetTrigger("LtoR"); break;

                    case "up":
                        animator.SetTrigger("UtoR"); break;

                    case "right": //이미 애니메이터 right상태임
                        break;

                    case "r_u":
                        animator.SetTrigger("R_UtoR"); break;

                    case "r_d":
                        animator.SetTrigger("R_DtoR"); break;

                    default: Debug.Log("상태가 이상합니다."); break;
                }
                pos.x = moveX_p + 1;
                bef_st = "right";
            }
            transform.position = pos;
        }

        else
        {
            switch (bef_st) //stay상태로 바뀌는 경우
            {
                case "up":
                    animator.SetTrigger("UtoS"); break;

                case "down":
                    animator.SetTrigger("DtoS"); break;

                case "left":
                    animator.SetTrigger("LtoS"); break;

                case "right":
                    animator.SetTrigger("RtoS"); break;

                case "stay": //이미 애니메이터 stay상태임
                    break;

                default: Debug.Log("상태가 이상합니다."); break;
            }
            bef_st = "stay";
            transform.position = pos;      
        }
    }
    public void ReturnOriginPos()
    {
        pos = new Vector3(-35.5f, -37.5f, 0);
        transform.position = pos;
    }
}

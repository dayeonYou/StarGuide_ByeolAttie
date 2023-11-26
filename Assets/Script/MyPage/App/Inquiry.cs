using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inquiry : MonoBehaviour
{
    public void OnClickEvent() {
        string mailto = "byeolatti@gmail.com";
        string subject = EscapeURL("[문의사항] 제목을 입력해주세요.");
        string body = EscapeURL("계정 정보(이메일) : \n\n" +
                                "문의 내용 : " + "[문제 또는 질문에 대한 자세한 내용을 작성해주세요.]\n\n\n\n" +
                                "========" +
                                "\n\n" +
                                "Device Model : " +
                                SystemInfo.deviceModel +
                                "\n\n" +
                                "Device OS : " + SystemInfo.operatingSystem +
                                "\n\n" +
                                "========");
        Application.OpenURL("mailto:" + mailto + "?subject=" + subject + "&body=" + body);
    }

    private string EscapeURL(string url) {
        return WWW.EscapeURL(url).Replace("+", "%20");
    }
}
using UnityEngine;
using TMPro;
using VInspector.Libs;

public class PopUpSignUp : MonoBehaviour
{
    [Header("TMP")]
    [SerializeField] private TMP_InputField id;
    [SerializeField] private TMP_InputField userName;
    [SerializeField] private TMP_InputField pw;
    [SerializeField] private TMP_InputField pwConfirm;
    [SerializeField] private TextMeshProUGUI warningMsg;

    [SerializeField] private GameObject popupError;
    [SerializeField] private GameObject popupCheck;
    public UserData data { get => GameManager.Instance.data; }

    //signup 버튼 눌렀을때 빈칸이 있을 때 에러표시, 없다면 정상적으로 계정 생성
    public void OnClickSignUpCheck() // ID가 겹쳤을 때 중복 체크 기능도 넣어야 하나?
    {
        ErrorMsg(id, "ID를 확인해주세요.");
        ErrorMsg(userName, "Name을 확인해주세요.");
        ErrorMsg(pw, "PW를 확인해주세요.");
        ErrorMsg(pwConfirm, "PWConfirm를 확인해주세요.");

        //else if(id.text == GameManager.Instance.data.ID) // 중복체크 아닌 것 같은데 어케 수정?
        //{
        //    warningMsg.text = "중복된 ID입니다.";
        //    popupError.SetActive(true);
        //    return;
        //}
        if(pw.text != pwConfirm.text)
        {
            warningMsg.text = "비밀번호가 일치하지 않습니다.";
            return;
        }
            SuccessSignUp();
    }

    //singup버튼을 눌렀을 때 빈칸이 없고, 정상적으로 계정 생성에 성공했을 때
    public void SuccessSignUp()
    {
        GameManager.Instance.SaveUserData(id.text, pw.text, userName.text, 100000, 50000);
        id.text = "";
        userName.text = "";
        pw.text = "";
        pwConfirm.text = "";
        warningMsg.text = "";
        popupCheck.SetActive(true);
    }

    private void ErrorMsg(TMP_InputField input, string message)
    {
        if (input.text.IsNullOrEmpty())
        {
            warningMsg.text = message;
            popupError.SetActive(true);
            return;
        }
    }
}

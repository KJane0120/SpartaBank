using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PopUpLogin : MonoBehaviour
{
    [SerializeField] private GameObject popupBank;
    [SerializeField] private GameObject popupSignUp;
    [SerializeField] private Button logInBtn;


    [Header("InputField")]
    [SerializeField] private TMP_InputField inputField_ID;
    [SerializeField] private TMP_InputField inputField_PW;
    [SerializeField] private GameObject popupError;


    private void Start()
    {
        logInBtn.onClick.AddListener(OnClickLogIn);
    }
    //ID랑 PW 비교해서 저장된 데이터와 비교하기
    public UserData LogInCheck(string id, string pw)
    {
        //ID 존재 여부
        if (PlayerPrefs.HasKey(id + "/Password"))
        {
            string storePw = PlayerPrefs.GetString(id + "/Password");

            if (pw == storePw) //비밀번호 일치 여부 확인
            {
                GameManager.Instance.LoadUserData(id);
                return GameManager.Instance.data;
            }
            else
            {
                Debug.Log("비밀번호가 일치하지 않습니다.");
                return null;
            }
        }
        else
        {
            Debug.Log("존재하지 않는 ID입니다.");
            return null;
        }
    }

    public void OnClickLogIn()
    {
        UserData data = LogInCheck(inputField_ID.text, inputField_PW.text);

        if (data != null)
        {
            Debug.Log("로그인 성공!");
            inputField_ID.text = "";
            inputField_PW.text = "";
            this.gameObject.SetActive(false);
            popupBank.SetActive(true);
        }
        else
        {
            popupError.SetActive(true);
            popupError.GetComponentInChildren<TextMeshProUGUI>(true).text = "존재하지 않는 회원입니다.";
            Debug.Log("로그인 실패!");
        }
    }
}

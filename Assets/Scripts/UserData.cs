using UnityEngine;

public class UserData //: MonoBehaviour
{
    //[SerializeField] private TextMeshProUGUI userNameText;
    //[SerializeField] private TextMeshProUGUI cashText;
    //[SerializeField] private TextMeshProUGUI balanceText;
    public string userName;
    public int cash;
    public int balance;

    public UserData(string userName, int cash, int balance) //생성자
    {
        this.userName = userName;
        this.cash = cash;
        this.balance = balance;
    }




    // Start is called before the first frame update
    //void Start()
    //{
    //    userName = "염예찬";
    //    balance = 50000;
    //    cash = 100000;
    //}

    // Update is called once per frame
    //void Update()
    //{
    //    UpdateUserName();
    //    UpdateCash();
    //    UpdateBalance();
    //}

    //string UpdateUserName()
    //{
    //    userNameText.text = userName;
    //    return userNameText.text;
    //}
    //string UpdateCash()
    //{
    //    cashText.text = string.Format("{0:N0}", cash);
    //    return cashText.text;
    //}

    //string UpdateBalance()
    //{
    //    balanceText.text = string.Format("{0:N0}", balance);
    //    return balanceText.text;
    //}
}

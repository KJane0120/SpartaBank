using Unity.VisualScripting;

[System.Serializable]
public class UserData //: MonoBehaviour
{
    public string userName;
    public int cash;
    public int balance;

    public UserData(string userName, int cash, int balance) //생성자
    {
        this.userName = userName;
        this.cash = cash;
        this.balance = balance;
    }
}

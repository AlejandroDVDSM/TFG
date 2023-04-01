public class User
{
    public string UserID;
    public string UserName;
    public int HighestScore;

    public User(string userID, string userName, int highestScore)
    {
        UserID = userID;
        UserName = userName;
        HighestScore = highestScore;
    }
}

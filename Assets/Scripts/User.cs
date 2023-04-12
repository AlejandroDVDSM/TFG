public class User
{
    // This variables must be public to be stored in Realtime Database (Firebase)
    public string userName;
    
    public int highestScore;
    public int steps;

    public string firstPlayedInEpochMillis;
    public string lastPlayedInEpochMillis;

    public User(string userName, int highestScore, int steps, string firstPlayedInEpochMillis, string lastPlayedInEpochMillis)
    {
        this.userName = userName;
        
        this.highestScore = highestScore;
        this.steps = steps;

        this.firstPlayedInEpochMillis = firstPlayedInEpochMillis;
        this.lastPlayedInEpochMillis = lastPlayedInEpochMillis;
    }
}

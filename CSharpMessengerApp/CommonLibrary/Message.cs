namespace CommonLibrary;

public class Message
{
    public Client From { get; set; }
    public Client To { get; set; }  
    public string Text { get; set; } 
    public DateTime CreatedAt { get; set; }
}
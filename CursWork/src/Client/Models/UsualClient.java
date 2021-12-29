package Client.Models;

import java.net.Socket;

public class UsualClient extends Client
{
    public UsualClient(Socket clientN){
        client = clientN;
    }
}
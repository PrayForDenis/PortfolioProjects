package Server;

import javax.swing.*;
import java.io.IOException;
import java.net.ServerSocket;
import java.net.Socket;
import java.sql.SQLException;
import java.time.LocalDate;
import java.util.ArrayList;

public class Server implements Runnable
{
    private ServerSocket server;
    private ArrayList<ClientHandler> clientHandlers;
    private ArrayList<Thread> clientThreads;
    private JTextArea debugArea;
    private DatabaseController dbController;

    public Server(JTextArea debugArea) throws IOException
    {
        server = new ServerSocket(8000);
        clientHandlers = new ArrayList<ClientHandler>();
        clientThreads = new ArrayList<Thread>();
        this.debugArea = debugArea;

        try
        {
            dbController = new DatabaseController();
        }
        catch (SQLException e)
        {
            e.printStackTrace();
        }
    }

    @Override
    public void run() {
        Start();
    }

    public void Start()
    {
        Log("Server has been started");
        Socket client = null;
        try
        {
            while (true)
            {
                client = server.accept();
                Log("Client connected " + client.getInetAddress());

                ClientHandler clientHandler = new ClientHandler(client, this);
                clientHandlers.add(clientHandler);

                Thread clientThread = new Thread(clientHandler);
                clientThreads.add(clientThread);
                clientThread.start();
            }
        }
        catch (IOException e)
        {
            e.printStackTrace();
        }
    }

    public void Stop() throws IOException
    {
        for (Thread thread: clientThreads)
        {
            thread.interrupt();
        }

        for (ClientHandler handler: clientHandlers)
        {
            handler.Close();
        }

        server.close();
        Log("Server has been stopped");
    }

    public void Log(String str){
        debugArea.append(str+"\n");
    }

    public void CheckUser(String login, String password, ClientHandler whoAsk)
    {
        whoAsk.SendResponse(dbController.CheckUser(login,password));
    }

    public void RemoveClient(ClientHandler whoRemove)
    {
        int idx = clientHandlers.indexOf(whoRemove);
        whoRemove.Close();
        clientThreads.get(idx).interrupt();
        clientThreads.remove(idx);
        clientHandlers.remove(idx);
        Log("Client removed:" + whoRemove);
    }

    public void NewMedHist(String surnameNamePatronymic, int age,
                           String education, String familyStatus,
                           String gender, String homeAddress, String workPlace,
                           LocalDate arrivalDate, LocalDate dateOfDischarge,
                           String specialMarks, String complaints,
                           String presentDiseaseHist, String lifeStory,
                           String preDiagnosis, String treatmentPlan,
                           String clinicalDiagnosis, String epicrisis,
                           ClientHandler whoCreate)
    {
        whoCreate.SendResponse(dbController.NewMedHist(surnameNamePatronymic, age,
                                                        education, familyStatus,
                                                        gender, homeAddress, workPlace,
                                                        arrivalDate, dateOfDischarge,
                                                        specialMarks, complaints,
                                                        presentDiseaseHist, lifeStory,
                                                        preDiagnosis, treatmentPlan,
                                                        clinicalDiagnosis, epicrisis));
    }

    public void GetMedHist(ClientHandler whoAsk)
    {
        GetMedHistResult result = dbController.GetMedHist();
        whoAsk.SendResponse(String.valueOf(result.MedicalHistory.size())+"\n");

        for(int i=0;i<result.MedicalHistory.size();i++)
        {
            whoAsk.SendResponse(result.MedicalHistory.get(i)+"\n");
        }
    }

    public void GetMedHist(ClientHandler whoAsk, String surnameNamePatronymic)
    {
        GetMedHistResult result = dbController.GetMedHist(surnameNamePatronymic);
        whoAsk.SendResponse(String.valueOf(result.MedicalHistory.size())+"\n");

        for(int i=0;i<result.MedicalHistory.size();i++)
        {
            whoAsk.SendResponse(result.MedicalHistory.get(i)+"\n");
        }
    }

    public void DeleteMedHist(int id, ClientHandler whoDelete)
    {
        whoDelete.SendResponse(dbController.DeleteMedHist(id));
    }

    public void EditMedHist(int id, String surnameNamePatronymic, int age,
                           String education, String familyStatus,
                           String gender, String homeAddress, String workPlace,
                           LocalDate arrivalDate, LocalDate dateOfDischarge,
                           String specialMarks, String complaints,
                           String presentDiseaseHist, String lifeStory,
                           String preDiagnosis, String treatmentPlan,
                           String clinicalDiagnosis, String epicrisis,
                           ClientHandler whoEdit)
    {
        whoEdit.SendResponse(dbController.EditMedHist(id, surnameNamePatronymic, age,
                education, familyStatus,
                gender, homeAddress, workPlace,
                arrivalDate, dateOfDischarge,
                specialMarks, complaints,
                presentDiseaseHist, lifeStory,
                preDiagnosis, treatmentPlan,
                clinicalDiagnosis, epicrisis));
    }
}


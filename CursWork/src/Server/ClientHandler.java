package Server;

import java.io.*;
import java.net.Socket;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.time.Instant;
import java.time.LocalDate;
import java.time.ZoneId;
import java.util.Date;

public class ClientHandler implements Runnable
{
    private Socket client;
    private Server server;

    private BufferedWriter writer;
    private BufferedReader reader;

    public ClientHandler(Socket client, Server server)
    {
        this.client = client;
        this.server = server;
    }

    @Override
    public void run()
    {
        try
        {
            reader = new BufferedReader(
                    new InputStreamReader(client.getInputStream()));
            writer = new BufferedWriter(
                    new OutputStreamWriter(client.getOutputStream()));

        }
        catch (IOException e)
        {
            e.printStackTrace();
        }

        while (true)
        {
            try
            {
                String type = reader.readLine();

                if (type == null) continue;

                switch (type)
                {
                    case "CheckUser":
                    {
                        String login = reader.readLine();
                        String password = reader.readLine();
                        server.CheckUser(login,password,this);
                        break;
                    }
                    case "NewMedHist":
                    {
                        String surnameNamePatronymic = reader.readLine();
                        int age = Integer.parseInt(reader.readLine());
                        String education = reader.readLine();
                        String familyStatus = reader.readLine();
                        String gender = reader.readLine();
                        String homeAddress = reader.readLine();
                        String workPlace = reader.readLine();

                        SimpleDateFormat formatter = new SimpleDateFormat("yyyy-MM-dd");

                        Date arrivalDate = formatter.parse(reader.readLine());
                        Instant instant = arrivalDate.toInstant();
                        LocalDate localArrivalDate = instant.atZone(ZoneId.systemDefault()).toLocalDate();

                        Date dateOfDischarge = formatter.parse(reader.readLine());
                        Instant instant1 = dateOfDischarge.toInstant();
                        LocalDate localDateOfDischarge = instant1.atZone(ZoneId.systemDefault()).toLocalDate();

                        String specialMarks = reader.readLine();
                        String complaints = reader.readLine();
                        String presentDiseaseHist = reader.readLine();
                        String lifeStory = reader.readLine();
                        String preDiagnosis = reader.readLine();
                        String treatmentPlan = reader.readLine();
                        String clinicalDiagnosis = reader.readLine();
                        String epicrisis = reader.readLine();
                        server.NewMedHist(surnameNamePatronymic, age, education, familyStatus,
                                gender, homeAddress, workPlace, localArrivalDate,
                                localDateOfDischarge, specialMarks, complaints,
                                presentDiseaseHist, lifeStory,
                                preDiagnosis, treatmentPlan, clinicalDiagnosis,
                                epicrisis, this);
                        break;
                    }
                    case "GetMedHist":
                    {
                        server.GetMedHist(this);
                        break;
                    }
                    case "GetMedHistBySNP":
                    {
                        String surnameNamePatronymic = reader.readLine();
                        server.GetMedHist(this, surnameNamePatronymic);
                        break;
                    }
                    case "DeleteMedHist":
                    {
                        int id = Integer.parseInt(reader.readLine());
                        server.DeleteMedHist(id, this);
                        break;
                    }
                    case "EditMedHist":
                    {
                        int id = Integer.parseInt(reader.readLine());
                        String surnameNamePatronymic = reader.readLine();
                        int age = Integer.parseInt(reader.readLine());
                        String education = reader.readLine();
                        String familyStatus = reader.readLine();
                        String gender = reader.readLine();
                        String homeAddress = reader.readLine();
                        String workPlace = reader.readLine();

                        SimpleDateFormat formatter = new SimpleDateFormat("yyyy-MM-dd");
                        Date arrivalDate = formatter.parse(reader.readLine());
                        Instant instant = arrivalDate.toInstant();
                        LocalDate localArrivalDate = instant.atZone(ZoneId.systemDefault()).toLocalDate();

                        Date dateOfDischarge = formatter.parse(reader.readLine());
                        Instant instant1 = dateOfDischarge.toInstant();
                        LocalDate localDateOfDischarge = instant1.atZone(ZoneId.systemDefault()).toLocalDate();

                        String specialMarks = reader.readLine();
                        String complaints = reader.readLine();
                        String presentDiseaseHist = reader.readLine();
                        String lifeStory = reader.readLine();
                        String preDiagnosis = reader.readLine();
                        String treatmentPlan = reader.readLine();
                        String clinicalDiagnosis = reader.readLine();
                        String epicrisis = reader.readLine();
                        server.EditMedHist(id, surnameNamePatronymic, age, education, familyStatus,
                                gender, homeAddress, workPlace, localArrivalDate,
                                localDateOfDischarge, specialMarks, complaints,
                                presentDiseaseHist, lifeStory,
                                preDiagnosis, treatmentPlan, clinicalDiagnosis,
                                epicrisis, this);
                        break;
                    }
                    default:
                        break;
                }
            }
            catch (IOException | ParseException e)
            {
                e.printStackTrace();
                server.RemoveClient(this);
                return;
            }
        }
    }

    public void Close()
    {
        try
        {
            writer.close();
            reader.close();
            client.close();
        }
        catch (IOException e)
        {
            e.printStackTrace();
        }
    }

    public void SendResponse(String response)
    {
        try
        {
            writer.write(response);
            writer.flush();
        }
        catch (IOException e)
        {
            e.printStackTrace();
        }
    }
}

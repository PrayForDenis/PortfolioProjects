package Client.Models;

import java.io.*;
import java.net.Socket;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.time.Instant;
import java.time.LocalDate;
import java.time.ZoneId;
import java.util.ArrayList;
import java.util.Date;

public abstract class Client
{
    protected Socket client;
    protected BufferedWriter writer;
    protected BufferedReader reader;

    public final void Start() throws IOException
    {
        writer = new BufferedWriter(new OutputStreamWriter(client.getOutputStream()));
        reader = new BufferedReader(new InputStreamReader(client.getInputStream()));
    }

    public final void Stop() throws IOException
    {
        writer.close();
        reader.close();
        client.close();
    }

    public ArrayList<MedicalHistory> GetMedHist()
    {
        ArrayList<MedicalHistory> medicalHistories = new ArrayList<MedicalHistory>();
        try
        {
            writer.write("GetMedHist\n");
            writer.flush();
            int n = Integer.parseInt(reader.readLine());
            for(int i=0;i<n;i++)
            {
                String MedHistoryString = reader.readLine();
                SimpleDateFormat formatter = new SimpleDateFormat("yyyy-MM-dd");
                String[] MedHistoryAtt = MedHistoryString.split(";");

                Date arrivalDate = formatter.parse(MedHistoryAtt[8]);
                Instant instant = arrivalDate.toInstant();
                LocalDate localArrivalDate = instant.atZone(ZoneId.systemDefault()).toLocalDate();

                Date dateOfDischarge = formatter.parse(MedHistoryAtt[9]);
                Instant instant1 = dateOfDischarge.toInstant();
                LocalDate localDateOfDischarge = instant1.atZone(ZoneId.systemDefault()).toLocalDate();

                MedicalHistory MedHist = new MedicalHistory(Integer.parseInt(MedHistoryAtt[0]),
                        MedHistoryAtt[1],
                        Integer.parseInt(MedHistoryAtt[2]),
                        MedHistoryAtt[3],
                        MedHistoryAtt[4],
                        MedHistoryAtt[5],
                        MedHistoryAtt[6],
                        MedHistoryAtt[7],
                        localArrivalDate,
                        localDateOfDischarge,
                        MedHistoryAtt[10],
                        MedHistoryAtt[11],
                        MedHistoryAtt[12],
                        MedHistoryAtt[13],
                        MedHistoryAtt[14],
                        MedHistoryAtt[15],
                        MedHistoryAtt[16],
                        MedHistoryAtt[17]);
                medicalHistories.add(MedHist);
            }
        }
        catch (IOException | ParseException e)
        {
            e.printStackTrace();
        }
        return medicalHistories;
    }

    public ArrayList<MedicalHistory> GetMedHist(String surnameNamePatronymic)
    {
        ArrayList<MedicalHistory> medicalHistories = new ArrayList<MedicalHistory>();
        try
        {
            writer.write("GetMedHistBySNP\n" +
                    surnameNamePatronymic + "\n");
            writer.flush();
            int n = Integer.parseInt(reader.readLine());
            for(int i=0;i<n;i++)
            {
                String MedHistoryString = reader.readLine();
                SimpleDateFormat formatter = new SimpleDateFormat("yyyy-MM-dd");
                String[] MedHistoryAtt = MedHistoryString.split(";");

                Date arrivalDate = formatter.parse(MedHistoryAtt[8]);
                Instant instant = arrivalDate.toInstant();
                LocalDate localArrivalDate = instant.atZone(ZoneId.systemDefault()).toLocalDate();

                Date dateOfDischarge = formatter.parse(MedHistoryAtt[9]);
                Instant instant1 = dateOfDischarge.toInstant();
                LocalDate localDateOfDischarge = instant1.atZone(ZoneId.systemDefault()).toLocalDate();

                MedicalHistory MedHist = new MedicalHistory(Integer.parseInt(MedHistoryAtt[0]),
                        MedHistoryAtt[1],
                        Integer.parseInt(MedHistoryAtt[2]),
                        MedHistoryAtt[3],
                        MedHistoryAtt[4],
                        MedHistoryAtt[5],
                        MedHistoryAtt[6],
                        MedHistoryAtt[7],
                        localArrivalDate,
                        localDateOfDischarge,
                        MedHistoryAtt[10],
                        MedHistoryAtt[11],
                        MedHistoryAtt[12],
                        MedHistoryAtt[13],
                        MedHistoryAtt[14],
                        MedHistoryAtt[15],
                        MedHistoryAtt[16],
                        MedHistoryAtt[17]);
                medicalHistories.add(MedHist);
            }
        }
        catch (IOException | ParseException e)
        {
            e.printStackTrace();
        }
        return medicalHistories;
    }
}

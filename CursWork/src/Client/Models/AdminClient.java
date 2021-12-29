package Client.Models;

import javax.swing.*;
import java.awt.*;
import java.io.IOException;
import java.net.Socket;
import java.time.LocalDate;

public class AdminClient extends Client
{
    public AdminClient(Socket clientN){
        client = clientN;
    }

    public void NewMedHist(String surnameNamePatronymic, int age,
                           String education, String familyStatus,
                           String gender, String homeAddress, String workPlace,
                           LocalDate arrivalDate, LocalDate dateOfDischarge,
                           String specialMarks, String complaints,
                           String presentDiseaseHist, String lifeStory,
                           String preDiagnosis, String treatmentPlan,
                           String clinicalDiagnosis, String epicrisis, Component cmp) throws IOException
    {
        writer.write("NewMedHist\n" +
                surnameNamePatronymic +"\n" +
                age +"\n" +
                education +"\n" +
                familyStatus +"\n" +
                gender +"\n" +
                homeAddress +"\n" +
                workPlace +"\n" +
                arrivalDate.toString() +"\n" +
                dateOfDischarge.toString() +"\n" +
                specialMarks +"\n" +
                complaints +"\n" +
                presentDiseaseHist +"\n" +
                lifeStory +"\n" +
                preDiagnosis +"\n" +
                treatmentPlan +"\n" +
                clinicalDiagnosis +"\n" +
                epicrisis +"\n");
        writer.flush();

        if (reader.readLine().contains("Success"))
        {
            JOptionPane.showMessageDialog(cmp,"Успешно");
        }
        else
        {
            JOptionPane.showMessageDialog(cmp,"Ошибка");
        }
    }

    public void DeleteMedHist(int id, Component cmp) throws IOException
    {
        writer.write("DeleteMedHist\n" +
                id +"\n");
        writer.flush();

        if (reader.readLine().contains("Success"))
        {
            JOptionPane.showMessageDialog(cmp,"Успешно");
        }
        else
        {
            JOptionPane.showMessageDialog(cmp,"Ошибка");
        }
    }

    public void EditMedHist(int id, String surnameNamePatronymic, int age,
                            String education, String familyStatus,
                            String gender, String homeAddress, String workPlace,
                            LocalDate arrivalDate, LocalDate dateOfDischarge,
                            String specialMarks, String complaints,
                            String presentDiseaseHist, String lifeStory,
                            String preDiagnosis, String treatmentPlan,
                            String clinicalDiagnosis, String epicrisis, Component cmp) throws IOException
    {
        writer.write("EditMedHist\n" +
                id + "\n" +
                surnameNamePatronymic +"\n" +
                age +"\n" +
                education +"\n" +
                familyStatus +"\n" +
                gender +"\n" +
                homeAddress +"\n" +
                workPlace +"\n" +
                arrivalDate.toString() +"\n" +
                dateOfDischarge.toString() +"\n" +
                specialMarks +"\n" +
                complaints +"\n" +
                presentDiseaseHist +"\n" +
                lifeStory +"\n" +
                preDiagnosis +"\n" +
                treatmentPlan +"\n" +
                clinicalDiagnosis +"\n" +
                epicrisis +"\n");
        writer.flush();

        if (reader.readLine().contains("Success"))
        {
            JOptionPane.showMessageDialog(cmp,"Успешно");
        }
        else
        {
            JOptionPane.showMessageDialog(cmp,"Ошибка");
        }
    }
}

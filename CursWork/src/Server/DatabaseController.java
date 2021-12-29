package Server;

import java.io.IOException;
import java.io.InputStream;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.sql.*;
import java.time.LocalDate;
import java.util.Properties;

public class DatabaseController
{
    Connection dbConnection;

    public DatabaseController() throws SQLException, IOException
    {
        String[] connParams = {"", "", ""};
        getConnectionParameters(connParams);
        dbConnection = DriverManager.getConnection(connParams[0], connParams[1], connParams[2]);
    }

    public String CheckUser(String login, String password)
    {
        String query = "SELECT login, password, role " +
                "FROM users " +
                "WHERE login = '" + login + "' AND password = '" + password + "'";
        try
        {
            PreparedStatement prSt = dbConnection.prepareStatement(query);
            ResultSet result =  prSt.executeQuery();
            if(result.next())
            {
                return "Success\n" + result.getString("Role")+"\n";
            }
            return "Error\n";
        }
        catch (SQLException e)
        {
            e.printStackTrace();
            return "Error\n";
        }
    }

    public String NewMedHist(String surnameNamePatronymic, int age,
                             String education, String familyStatus,
                             String gender, String homeAddress, String workPlace,
                             LocalDate arrivalDate, LocalDate dateOfDischarge,
                             String specialMarks, String complaints,
                             String presentDiseaseHist, String lifeStory,
                             String preDiagnosis, String treatmentPlan,
                             String clinicalDiagnosis, String epicrisis)
    {
        try
        {
            String query = "INSERT INTO medical_history" +
                    "(surnameNamePatronymic, age, education, familyStatus, gender, " +
                    "homeAddress, workPlace, arrivalDate, dateOfDischarge, specialMarks, " +
                    "complaints, presentDiseaseHist, lifeStory, preDiagnosis, treatmentPlan, " +
                    "clinicalDiagnosis, epicrisis) " +
                    "VALUES" +
                    "(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
            PreparedStatement prSt = dbConnection.prepareStatement(query);
            prSt.setString(1, surnameNamePatronymic);
            prSt.setInt(2, age);
            prSt.setString(3, education);
            prSt.setString(4, familyStatus);
            prSt.setString(5, gender);
            prSt.setString(6, homeAddress);
            prSt.setString(7, workPlace);
            prSt.setDate(8, Date.valueOf(arrivalDate.toString()));
            prSt.setDate(9, Date.valueOf(dateOfDischarge.toString()));
            prSt.setString(10, specialMarks);
            prSt.setString(11, complaints);
            prSt.setString(12, presentDiseaseHist);
            prSt.setString(13, lifeStory);
            prSt.setString(14, preDiagnosis);
            prSt.setString(15, treatmentPlan);
            prSt.setString(16, clinicalDiagnosis);
            prSt.setString(17, epicrisis);
            prSt.execute();
            return "Success\n";
        }
        catch (SQLException e)
        {
            e.printStackTrace();
            return "Error\n";
        }
    }

    public void getConnectionParameters(String[] connParams) throws IOException
    {
        Properties props = new Properties();
        try(InputStream in = Files.newInputStream(Paths.get("database.properties")))
        {
            props.load(in);
        }
        connParams[0] = props.getProperty("url");
        connParams[1] = props.getProperty("username");
        connParams[2] = props.getProperty("password");
    }

    public GetMedHistResult GetMedHist()
    {
        GetMedHistResult gdr = new GetMedHistResult();
        String query = "SELECT * FROM medical_history";

        try
        {
            PreparedStatement prSt = dbConnection.prepareStatement(query);
            ResultSet result = prSt.executeQuery();
            while (result.next())
            {
                String MedHist = "";
                MedHist = MedHist.concat(result.getString(1)) +";";
                MedHist = MedHist.concat(result.getString(2)) +";";
                MedHist = MedHist.concat(result.getString(3)) +";";
                MedHist = MedHist.concat(result.getString(4)) +";";
                MedHist = MedHist.concat(result.getString(5)) +";";
                MedHist = MedHist.concat(result.getString(6)) +";";
                MedHist = MedHist.concat(result.getString(7)) +";";
                MedHist = MedHist.concat(result.getString(8)) +";";
                MedHist = MedHist.concat(result.getString(9)) +";";
                MedHist = MedHist.concat(result.getString(10)) +";";
                MedHist = MedHist.concat(result.getString(11)) +";";
                MedHist = MedHist.concat(result.getString(12)) +";";
                MedHist = MedHist.concat(result.getString(13)) +";";
                MedHist = MedHist.concat(result.getString(14)) +";";
                MedHist = MedHist.concat(result.getString(15)) +";";
                MedHist = MedHist.concat(result.getString(16)) +";";
                MedHist = MedHist.concat(result.getString(17)) +";";
                MedHist = MedHist.concat(result.getString(18));
                gdr.MedicalHistory.add(MedHist);
            }
        }
        catch (SQLException e)
        {
            e.printStackTrace();
            return gdr;
        }
        return gdr;
    }

    public GetMedHistResult GetMedHist(String surnameNamePatronymic)
    {
        GetMedHistResult gdr = new GetMedHistResult();
        String query = "SELECT * FROM medical_history WHERE surnameNamePatronymic = '" +
                surnameNamePatronymic + "'";

        try
        {
            PreparedStatement prSt = dbConnection.prepareStatement(query);
            ResultSet result = prSt.executeQuery();
            while (result.next())
            {
                String MedHist = "";
                MedHist = MedHist.concat(result.getString(1)) +";";
                MedHist = MedHist.concat(result.getString(2)) +";";
                MedHist = MedHist.concat(result.getString(3)) +";";
                MedHist = MedHist.concat(result.getString(4)) +";";
                MedHist = MedHist.concat(result.getString(5)) +";";
                MedHist = MedHist.concat(result.getString(6)) +";";
                MedHist = MedHist.concat(result.getString(7)) +";";
                MedHist = MedHist.concat(result.getString(8)) +";";
                MedHist = MedHist.concat(result.getString(9)) +";";
                MedHist = MedHist.concat(result.getString(10)) +";";
                MedHist = MedHist.concat(result.getString(11)) +";";
                MedHist = MedHist.concat(result.getString(12)) +";";
                MedHist = MedHist.concat(result.getString(13)) +";";
                MedHist = MedHist.concat(result.getString(14)) +";";
                MedHist = MedHist.concat(result.getString(15)) +";";
                MedHist = MedHist.concat(result.getString(16)) +";";
                MedHist = MedHist.concat(result.getString(17)) +";";
                MedHist = MedHist.concat(result.getString(18));
                gdr.MedicalHistory.add(MedHist);
            }
        }
        catch (SQLException e)
        {
            e.printStackTrace();
            return gdr;
        }
        return gdr;
    }

    public String DeleteMedHist(int id)
    {
        String query = "DELETE FROM medical_history WHERE id =  ?";
        try
        {
            PreparedStatement prSt = dbConnection.prepareStatement(query);
            prSt.setInt(1, id);
            prSt.execute();
            return "Success\n";
        }
        catch (SQLException e)
        {
            e.printStackTrace();
            return "Error\n";
        }
    }

    public String EditMedHist(int id, String surnameNamePatronymic, int age,
                              String education, String familyStatus,
                              String gender, String homeAddress, String workPlace,
                              LocalDate arrivalDate, LocalDate dateOfDischarge,
                              String specialMarks, String complaints,
                              String presentDiseaseHist, String lifeStory,
                              String preDiagnosis, String treatmentPlan,
                              String clinicalDiagnosis, String epicrisis)
    {
        try
        {
            String query = "UPDATE medical_history " +
                    "SET surnameNamePatronymic = ?, age = ?, education = ?, familyStatus = ?, " +
                    "gender = ?, homeAddress = ?, workPlace = ?, arrivalDate = ?, dateOfDischarge = ?, " +
                    "specialMarks = ?, complaints = ?, presentDiseaseHist = ?, lifeStory = ?, " +
                    "preDiagnosis = ?, treatmentPlan = ?, clinicalDiagnosis = ?, epicrisis = ? " +
                    "WHERE id = ?";
            PreparedStatement prSt = dbConnection.prepareStatement(query);
            prSt.setString(1, surnameNamePatronymic);
            prSt.setInt(2, age);
            prSt.setString(3, education);
            prSt.setString(4, familyStatus);
            prSt.setString(5, gender);
            prSt.setString(6, homeAddress);
            prSt.setString(7, workPlace);
            prSt.setDate(8, Date.valueOf(arrivalDate.toString()));
            prSt.setDate(9, Date.valueOf(dateOfDischarge.toString()));
            prSt.setString(10, specialMarks);
            prSt.setString(11, complaints);
            prSt.setString(12, presentDiseaseHist);
            prSt.setString(13, lifeStory);
            prSt.setString(14, preDiagnosis);
            prSt.setString(15, treatmentPlan);
            prSt.setString(16, clinicalDiagnosis);
            prSt.setString(17, epicrisis);
            prSt.setInt(18, id);
            prSt.execute();
            return "Success\n";
        }
        catch (SQLException e)
        {
            e.printStackTrace();
            return "Error\n";
        }
    }
}

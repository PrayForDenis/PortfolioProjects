package Client.Models;

import java.time.LocalDate;

public class MedicalHistory
{
    public int Id;
    public String SurnameNamePatronymic;
    public int Age;
    public String Education;
    public String FamilyStatus;
    public String Gender;
    public String HomeAddress;
    public String WorkPlace;
    public LocalDate ArrivalDate;
    public LocalDate DateOfDischarge;
    public String SpecialMarks;
    public String Complaints;
    public String PresentDiseaseHist;
    public String LifeStory;
    public String PreDiagnosis;
    public String TreatmentPlan;
    public String ClinicalDiagnosis;
    public String Epicrisis;

    public MedicalHistory(int id, String surnameNamePatronymic, int age,
                          String education, String familyStatus,
                          String gender, String homeAddress, String workPlace,
                          LocalDate arrivalDate, LocalDate dateOfDischarge,
                          String specialMarks, String complaints,
                          String presentDiseaseHist, String lifeStory,
                          String preDiagnosis, String treatmentPlan,
                          String clinicalDiagnosis, String epicrisis)
    {
        Id = id;
        SurnameNamePatronymic = surnameNamePatronymic;
        Age = age;
        Education = education;
        FamilyStatus = familyStatus;
        Gender = gender;
        HomeAddress = homeAddress;
        WorkPlace = workPlace;
        ArrivalDate = arrivalDate;
        DateOfDischarge = dateOfDischarge;
        SpecialMarks = specialMarks;
        Complaints = complaints;
        PresentDiseaseHist = presentDiseaseHist;
        LifeStory = lifeStory;
        PreDiagnosis = preDiagnosis;
        TreatmentPlan = treatmentPlan;
        ClinicalDiagnosis = clinicalDiagnosis;
        Epicrisis = epicrisis;
    }
}

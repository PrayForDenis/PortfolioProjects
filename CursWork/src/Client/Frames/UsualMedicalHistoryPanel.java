package Client.Frames;

import Client.Models.UsualClient;
import Client.Models.MedicalHistory;

import javax.swing.*;
import java.awt.*;

public class UsualMedicalHistoryPanel extends JPanel
{
    private MedicalHistory medHist;

    private JLabel surnameNamePatronymicLabel;
    private JLabel ageLabel;
    private JLabel educationLabel;
    private JLabel familyStatusLabel;
    private JLabel genderLabel;
    private JLabel homeAddressLabel;
    private JLabel workPlaceLabel;
    private JLabel arrivalDateLabel;
    private JLabel dateOfDischargeLabel;
    private JLabel specialMarksLabel;
    private JLabel complaintsLabel;
    private JLabel presentDiseaseHistLabel;
    private JLabel lifeStoryLabel;
    private JLabel preDiagnosisLabel;
    private JLabel treatmentPlanLabel;
    private JLabel clinicalDiagnosisLabel;
    private JLabel epicrisisLabel;

    public UsualMedicalHistoryPanel(MedicalHistory medicalHistory, UsualClient client, UsualClientFrame parent)
    {
        medHist = medicalHistory;

        setLayout(new GridLayout(0, 1, 10, 10));
        setBorder(BorderFactory.createEmptyBorder(10, 20, 20, 20));

        surnameNamePatronymicLabel = new JLabel(medicalHistory.SurnameNamePatronymic);
        ageLabel = new JLabel(String.valueOf(medicalHistory.Age));
        educationLabel = new JLabel(medicalHistory.Education);
        familyStatusLabel = new JLabel(medicalHistory.FamilyStatus);
        genderLabel = new JLabel(medicalHistory.Gender);
        homeAddressLabel = new JLabel(medicalHistory.HomeAddress);
        workPlaceLabel = new JLabel(medicalHistory.WorkPlace);
        arrivalDateLabel = new JLabel(medicalHistory.ArrivalDate.toString());
        dateOfDischargeLabel = new JLabel(medicalHistory.DateOfDischarge.toString());
        specialMarksLabel = new JLabel(medicalHistory.SpecialMarks);
        complaintsLabel = new JLabel(medicalHistory.Complaints);
        presentDiseaseHistLabel = new JLabel(medicalHistory.PresentDiseaseHist);
        lifeStoryLabel = new JLabel(medicalHistory.LifeStory);
        preDiagnosisLabel = new JLabel(medicalHistory.PreDiagnosis);
        treatmentPlanLabel = new JLabel(medicalHistory.TreatmentPlan);
        clinicalDiagnosisLabel = new JLabel(medicalHistory.ClinicalDiagnosis);
        epicrisisLabel = new JLabel(medicalHistory.Epicrisis);

        add(surnameNamePatronymicLabel);
        add(ageLabel);
        add(educationLabel);
        add(familyStatusLabel);
        add(genderLabel);
        add(homeAddressLabel);
        add(workPlaceLabel);
        add(arrivalDateLabel);
        add(dateOfDischargeLabel);
        add(specialMarksLabel);
        add(complaintsLabel);
        add(presentDiseaseHistLabel);
        add(lifeStoryLabel);
        add(preDiagnosisLabel);
        add(treatmentPlanLabel);
        add(clinicalDiagnosisLabel);
        add(epicrisisLabel);

        setVisible(true);
    }
}

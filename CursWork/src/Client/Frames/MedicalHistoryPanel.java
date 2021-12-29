package Client.Frames;

import Client.Models.AdminClient;
import Client.Models.MedicalHistory;

import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.io.IOException;

public class MedicalHistoryPanel extends JPanel
{
    private MedicalHistory medicalHistory;

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

    public MedicalHistoryPanel(MedicalHistory MedHist, AdminClient admin, AdminClientFrame parent)
    {
        medicalHistory = MedHist;

        setLayout(new GridLayout(0,1,5,5));
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

        JButton deleteBut = new JButton("Удалить");
        deleteBut.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                try
                {
                    admin.DeleteMedHist(medicalHistory.Id, parent);
                    parent.UpdateMedHist();
                }
                catch (IOException ex)
                {
                    ex.printStackTrace();
                }
            }
        });

        JButton editBut = new JButton("Изменить");
        editBut.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                EditMedHistDialog editMedHistDialog = new EditMedHistDialog(admin, parent, medicalHistory);
            }
        });

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
        add(deleteBut);
        add(editBut);

        setVisible(true);
    }
}

package Client.Frames;

import Client.Models.AdminClient;

import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.io.IOException;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.time.Instant;
import java.time.LocalDate;
import java.time.ZoneId;
import java.util.Calendar;
import java.util.Date;
import java.util.Properties;
import org.jdatepicker.impl.JDatePanelImpl;
import org.jdatepicker.impl.JDatePickerImpl;
import org.jdatepicker.impl.UtilDateModel;

class NewMedHistoryDialog extends JDialog
{
    private JPanel panLabels;
    private JPanel panFields;
    private JPanel panButtons;

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

    private JTextField surnameNamePatronymicField;
    private JSpinner ageSpinner;
    private JTextField educationField;
    private JTextField familyStatusField;
    private JTextField genderField;
    private JTextField homeAddressField;
    private JTextField workPlaceField;
    private JDatePickerImpl arrivalDatePicker;
    private JDatePickerImpl dateOfDischargePicker;
    private JTextArea specialMarksArea;
    private JTextArea complaintsArea;
    private JTextArea presentDiseaseHistArea;
    private JTextArea lifeStoryArea;
    private JTextArea preDiagnosisArea;
    private JTextArea treatmentPlanArea;
    private JTextArea clinicalDiagnosisArea;
    private JTextArea epicrisisArea;

    public NewMedHistoryDialog(AdminClient admin, AdminClientFrame parent)
    {
        setTitle("Новая история болезни");
        setModal(true);
        setSize(900, 700);
        setResizable(false);

        surnameNamePatronymicLabel = new JLabel("Фамилия Имя Отчество");
        ageLabel = new JLabel("Возраст");
        educationLabel = new JLabel("Образование");
        familyStatusLabel = new JLabel("Семейный статус");
        genderLabel = new JLabel("Пол");
        homeAddressLabel = new JLabel("Домашний адрес");
        workPlaceLabel = new JLabel("Место работы");
        arrivalDateLabel = new JLabel("Дата поступления");
        dateOfDischargeLabel = new JLabel("Дата выписки");
        specialMarksLabel = new JLabel("Особые отметки");
        complaintsLabel = new JLabel("Жалобы");
        presentDiseaseHistLabel = new JLabel("История настоящего заболевания");
        lifeStoryLabel = new JLabel("Анамнез жизни");
        preDiagnosisLabel = new JLabel("Предварительный диагноз");
        treatmentPlanLabel = new JLabel("План лечения");
        clinicalDiagnosisLabel = new JLabel("Клинический диагноз");
        epicrisisLabel = new JLabel("Эпикриз");

        surnameNamePatronymicField = new JTextField(50);
        ageSpinner = new JSpinner(new SpinnerNumberModel(0,0,100,1));
        educationField = new JTextField(50);
        familyStatusField = new JTextField(50);
        genderField = new JTextField(50);
        homeAddressField = new JTextField(50);
        workPlaceField = new JTextField(50);

        UtilDateModel model = new UtilDateModel();
        Properties p = new Properties();
        p.put("text.today", "Today");
        p.put("text.month", "Month");
        p.put("text.year", "Year");
        JDatePanelImpl datePanel = new JDatePanelImpl(model, p);
        JFormattedTextField.AbstractFormatter formatter = new JFormattedTextField.AbstractFormatter() {
            private final SimpleDateFormat dateFormatter = new SimpleDateFormat("yyyy-MM-dd");
            @Override
            public Object stringToValue(String text) throws ParseException {
                return dateFormatter.parseObject(text);
            }

            @Override
            public String valueToString(Object value) throws ParseException {
                if (value != null)
                {
                    Calendar cal = (Calendar) value;
                    return dateFormatter.format(cal.getTime());
                }
                return "";
            }
        };
        arrivalDatePicker = new JDatePickerImpl(datePanel, formatter);

        UtilDateModel model1 = new UtilDateModel();
        Properties p1 = new Properties();
        p1.put("text.today", "Today");
        p1.put("text.month", "Month");
        p1.put("text.year", "Year");
        JDatePanelImpl datePanel1 = new JDatePanelImpl(model1, p1);
        dateOfDischargePicker = new JDatePickerImpl(datePanel1, formatter);

        specialMarksArea = new JTextArea();
        complaintsArea = new JTextArea();
        presentDiseaseHistArea = new JTextArea();
        lifeStoryArea = new JTextArea();
        preDiagnosisArea = new JTextArea();
        treatmentPlanArea = new JTextArea();
        clinicalDiagnosisArea = new JTextArea();
        epicrisisArea = new JTextArea();

        JButton create = new JButton("Добавить");
        create.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e)
            {
                try
                {
                    Date arrivalDate = (Date)arrivalDatePicker.getModel().getValue();
                    Instant instant = arrivalDate.toInstant();
                    LocalDate localArrivalDate = instant.atZone(ZoneId.systemDefault()).toLocalDate();

                    Date dateOfDischarge = (Date)dateOfDischargePicker.getModel().getValue();
                    Instant instant1 = dateOfDischarge.toInstant();
                    LocalDate localDateOfDischarge = instant1.atZone(ZoneId.systemDefault()).toLocalDate();

                    admin.NewMedHist(
                            surnameNamePatronymicField.getText(),
                            (int)ageSpinner.getValue(),
                            educationField.getText(),
                            familyStatusField.getText(),
                            genderField.getText(),
                            homeAddressField.getText(),
                            workPlaceField.getText(),
                            localArrivalDate,
                            localDateOfDischarge,
                            specialMarksArea.getText(),
                            complaintsArea.getText(),
                            presentDiseaseHistArea.getText(),
                            lifeStoryArea.getText(),
                            preDiagnosisArea.getText(),
                            treatmentPlanArea.getText(),
                            clinicalDiagnosisArea.getText(),
                            epicrisisArea.getText(), NewMedHistoryDialog.this);
                    parent.UpdateMedHist();
                    setVisible(false);
                    dispose();
                }
                catch (IOException ex)
                {
                    ex.printStackTrace();
                }
            }
        });

        panLabels = new JPanel();
        panFields = new JPanel();
        panButtons = new JPanel();

        panLabels.setLayout(new GridLayout(0,1,10,10));
        panFields.setLayout(new GridLayout(0,1,10,10));
        panButtons.setLayout(new GridLayout(0,1,10,10));
        panButtons.setBorder(BorderFactory.createEmptyBorder(15,0,0,0));

        panLabels.add(surnameNamePatronymicLabel);
        panFields.add(surnameNamePatronymicField);

        panLabels.add(ageLabel);
        panFields.add(ageSpinner);

        panLabels.add(educationLabel);
        panFields.add(educationField);

        panLabels.add(familyStatusLabel);
        panFields.add(familyStatusField);

        panLabels.add(genderLabel);
        panFields.add(genderField);

        panLabels.add(homeAddressLabel);
        panFields.add(homeAddressField);

        panLabels.add(workPlaceLabel);
        panFields.add(workPlaceField);

        panLabels.add(arrivalDateLabel);
        panFields.add(arrivalDatePicker);

        panLabels.add(dateOfDischargeLabel);
        panFields.add(dateOfDischargePicker);

        panLabels.add(specialMarksLabel);
        panFields.add(new JScrollPane(specialMarksArea));

        panLabels.add(complaintsLabel);
        panFields.add(new JScrollPane(complaintsArea));

        panLabels.add(presentDiseaseHistLabel);
        panFields.add(new JScrollPane(presentDiseaseHistArea));

        panLabels.add(lifeStoryLabel);
        panFields.add(new JScrollPane(lifeStoryArea));

        panLabels.add(preDiagnosisLabel);
        panFields.add(new JScrollPane(preDiagnosisArea));

        panLabels.add(treatmentPlanLabel);
        panFields.add(new JScrollPane(treatmentPlanArea));

        panLabels.add(clinicalDiagnosisLabel);
        panFields.add(new JScrollPane(clinicalDiagnosisArea));

        panLabels.add(epicrisisLabel);
        panFields.add(new JScrollPane(epicrisisArea));

        panButtons.add(create);

        add(panLabels, BorderLayout.WEST);
        add(panFields, BorderLayout.EAST);
        add(panButtons, BorderLayout.SOUTH);

        getRootPane().setBorder(BorderFactory.createEmptyBorder(20, 20, 10, 20));
        setVisible(true);
    }
}
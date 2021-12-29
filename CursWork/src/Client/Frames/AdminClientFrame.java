package Client.Frames;

import Client.Models.AdminClient;
import Client.Models.MedicalHistory;

import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.WindowEvent;
import java.awt.event.WindowListener;
import java.io.IOException;
import java.net.Socket;
import java.util.ArrayList;

public class AdminClientFrame extends JFrame
{
    private AdminClient admin;
    private JPanel MedHistPanel;

    public AdminClientFrame()
    {
        try
        {
            admin = new AdminClient(new Socket("127.0.0.1",8000));
            admin.Start();
        }
        catch (IOException e)
        {
            e.printStackTrace();
        }

        JPanel search = new JPanel(new GridLayout(2, 2, 5, 5));

        setTitle("Admin Client");
        setLayout(new BorderLayout());
        setDefaultCloseOperation(WindowConstants.EXIT_ON_CLOSE);

        JTextField searchPacient = new JTextField("Введите ФИО пациента", 50);
        search.add(searchPacient, BorderLayout.WEST);

        JButton searchByLabelPacient = new JButton("Поиск в базе");
        searchByLabelPacient.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e)
            {
                UpdateMedHist(searchPacient);
            }
        });
        search.add(searchByLabelPacient, BorderLayout.EAST);

        JButton addDishButton = new JButton("Новая история болезни");
        addDishButton.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                NewMedHistoryDialog dishDialog = new NewMedHistoryDialog(admin, AdminClientFrame.this);
            }
        });
        search.add(addDishButton, BorderLayout.SOUTH);

        JButton viewAllBase = new JButton("Просмотр полной базы");
        viewAllBase.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                UpdateMedHist();
            }
        });
        search.add(viewAllBase, BorderLayout.SOUTH);

        add(search, BorderLayout.NORTH);

        addWindowListener(new WindowListener() {
            @Override
            public void windowClosed(WindowEvent e)
            {
                try
                {
                    admin.Stop();
                }
                catch (IOException ex)
                {
                    ex.printStackTrace();
                }
            }

            @Override
            public void windowOpened(WindowEvent e)
            {
                //
            }

            @Override
            public void windowClosing(WindowEvent e)
            {
                //
            }

            @Override
            public void windowIconified(WindowEvent e)
            {
                //
            }

            @Override
            public void windowDeiconified(WindowEvent e)
            {
                //
            }

            @Override
            public void windowActivated(WindowEvent e)
            {
                //
            }

            @Override
            public void windowDeactivated(WindowEvent e)
            {
                //
            }
        });

        MedHistPanel = new JPanel(new GridLayout(0,2,10,10));
        ShowMedHist(MedHistPanel);
        add(new JScrollPane(MedHistPanel),BorderLayout.CENTER);

        pack();
        setVisible(true);
    }

    private void ShowMedHist(JPanel panel)
    {
        ArrayList<MedicalHistory> MedHists = admin.GetMedHist();
        for (MedicalHistory MedHist: MedHists)
        {
            MedicalHistoryPanel MedHistFrame = new MedicalHistoryPanel(MedHist, admin, this);
            panel.add(MedHistFrame);
        }
    }

    private void ShowMedHist(JPanel panel, String surnameNamePatronymic)
    {
        ArrayList<MedicalHistory> MedHists = admin.GetMedHist(surnameNamePatronymic);
        for (MedicalHistory MedHist: MedHists)
        {
            MedicalHistoryPanel MedHistFrame = new MedicalHistoryPanel(MedHist, admin, this);
            panel.add(MedHistFrame);
        }
    }

    public void UpdateMedHist()
    {
        MedHistPanel.removeAll();
        ShowMedHist(MedHistPanel);
        revalidate();
        repaint();
    }

    public void UpdateMedHist(JTextField searchPacient)
    {
        MedHistPanel.removeAll();
        ShowMedHist(MedHistPanel, searchPacient.getText());
        revalidate();
        repaint();
    }
}

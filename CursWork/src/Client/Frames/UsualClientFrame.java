package Client.Frames;

import Client.Models.UsualClient;
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

public class UsualClientFrame extends JFrame
{
    private UsualClient client;
    private JPanel MedHistPanel;

    public UsualClientFrame()
    {
        try
        {
            client = new UsualClient(new Socket("127.0.0.1",8000));
            client.Start();
        }
        catch (IOException e)
        {
            e.printStackTrace();
        }

        JPanel search = new JPanel(new GridLayout(3, 1, 5, 5));

        setTitle("Usual Client");
        setLayout(new BorderLayout());
        setDefaultCloseOperation(WindowConstants.EXIT_ON_CLOSE);

        JTextField searchPacient = new JTextField("Введите ФИО пациента", 50);
        search.add(searchPacient);

        JButton searchByLabelPacient = new JButton("Поиск в базе");
        searchByLabelPacient.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e)
            {
                UpdateMedHist(searchPacient);
            }
        });
        search.add(searchByLabelPacient);

        JButton viewAllBase = new JButton("Просмотр полной базы");
        viewAllBase.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                UpdateMedHist();
            }
        });
        search.add(viewAllBase);

        add(search, BorderLayout.PAGE_START);

        addWindowListener(new WindowListener() {
            @Override
            public void windowClosed(WindowEvent e)
            {
                try
                {
                    client.Stop();
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
        add(MedHistPanel,BorderLayout.CENTER);

        pack();
        setVisible(true);
    }

    private void ShowMedHist(JPanel panel)
    {
        ArrayList<MedicalHistory> MedHists = client.GetMedHist();
        for (MedicalHistory MedHist: MedHists)
        {
            UsualMedicalHistoryPanel MedHistoryPanel = new UsualMedicalHistoryPanel(MedHist, client, this);
            panel.add(MedHistoryPanel);
        }
    }

    private void ShowMedHist(JPanel panel, String surnameNamePatronymic)
    {
        ArrayList<MedicalHistory> MedHists = client.GetMedHist(surnameNamePatronymic);
        for (MedicalHistory MedHist: MedHists)
        {
            UsualMedicalHistoryPanel MedHistFrame = new UsualMedicalHistoryPanel(MedHist, client, this);
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

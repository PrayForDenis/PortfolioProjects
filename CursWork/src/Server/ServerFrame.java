package Server;

import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.WindowEvent;
import java.awt.event.WindowListener;
import java.io.IOException;

public class ServerFrame extends JFrame
{
    private static Server Server;
    private static Thread ServerThread;

    private static JTextArea Debug;

    public ServerFrame()
    {
        setLayout(new BorderLayout());

        setTitle("Server");
        setSize(400, 400);
        setResizable(false);
        Debug = new JTextArea(10,8);
        Debug.setFont(new Font("Arial", Font.PLAIN,14));
        Debug.setTabSize(10);
        Debug.setLineWrap(true);
        Debug.setWrapStyleWord(true);
        Debug.setEditable(false);

        JButton startButton = new JButton("Start");
        startButton.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e)
            {
                try
                {
                    Server = new Server(Debug);
                    ServerThread = new Thread(Server);
                    ServerThread.start();
                }
                catch (IOException ex)
                {
                    ex.printStackTrace();
                    System.out.println("Невозможно запустить сервер");
                }
            }
        });

        JButton stopButton = new JButton("Stop");
        stopButton.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                try
                {
                    ServerThread.interrupt();
                    Server.Stop();
                }
                catch (IOException ex)
                {
                    ex.printStackTrace();
                }
            }
        });

        addWindowListener(new WindowListener() {
            @Override
            public void windowClosed(WindowEvent e) {
                try
                {
                    ServerThread.interrupt();
                    Server.Stop();
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

        add(startButton, BorderLayout.PAGE_START);
        add(stopButton, BorderLayout.PAGE_END);
        add(new JScrollPane(Debug),BorderLayout.CENTER);

        setDefaultCloseOperation(WindowConstants.EXIT_ON_CLOSE);
        setVisible(true);
    }

    public static void main(String[] args)
    {
        new ServerFrame();
    }
}

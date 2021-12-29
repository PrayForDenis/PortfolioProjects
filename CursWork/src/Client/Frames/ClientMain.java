package Client.Frames;

import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.WindowEvent;
import java.awt.event.WindowListener;
import java.io.*;
import java.net.Socket;

public class ClientMain extends JFrame
{
    private static Socket client;

    private static LoginFrame logInPanel;

    public ClientMain()
    {
        setTitle("MedicalHistory");
        setLayout(new BorderLayout());
        setSize(400, 500);
        setResizable(false);
        setDefaultCloseOperation(WindowConstants.EXIT_ON_CLOSE);
        client = null;
        try
        {
            client = new Socket("127.0.0.1",8000);
        }
        catch (IOException e)
        {
            e.printStackTrace();
        }

        logInPanel = new LoginFrame(client,this);

        add(logInPanel,BorderLayout.CENTER);

        addWindowListener(new WindowListener() {
            @Override
            public void windowClosed(WindowEvent e) {
                try
                {
                    client.close();
                }
                catch (IOException ex)
                {
                    ex.printStackTrace();
                }
            }
            @Override
            public void windowOpened(WindowEvent e) {
                //
            }
            @Override
            public void windowClosing(WindowEvent e) {
                //
            }
            @Override
            public void windowIconified(WindowEvent e) {
                //
            }
            @Override
            public void windowDeiconified(WindowEvent e) {
                //
            }
            @Override
            public void windowActivated(WindowEvent e) {
                //
            }
            @Override
            public void windowDeactivated(WindowEvent e) {
                //
            }
        });
        setVisible(true);
    }

    public static void main(String[] args) {
        new ClientMain();
    }
}

class LoginFrame extends JPanel implements ActionListener
{
    private Socket client;
    private ClientMain parent;

    JLabel userLabel = new JLabel("USERNAME");
    JLabel passwordLabel = new JLabel("PASSWORD");
    JTextField userTextField = new JTextField();
    JPasswordField passwordField = new JPasswordField();
    JButton loginButton = new JButton("LOGIN");
    JButton resetButton = new JButton("RESET");
    JCheckBox showPassword = new JCheckBox("Show Password");

    LoginFrame(Socket clientN, ClientMain parentN)
    {
        client = clientN;
        parent = parentN;

        setLayout(null);

        userLabel.setBounds(50, 150, 100, 30);
        passwordLabel.setBounds(50, 220, 100, 30);
        userTextField.setBounds(150, 150, 150, 30);
        passwordField.setBounds(150, 220, 150, 30);
        showPassword.setBounds(150, 250, 150, 30);
        loginButton.setBounds(50, 300, 100, 30);
        resetButton.setBounds(200, 300, 100, 30);

        add(userLabel);
        add(passwordLabel);
        add(userTextField);
        add(passwordField);
        add(showPassword);
        add(loginButton);
        add(resetButton);

        loginButton.addActionListener(this);
        resetButton.addActionListener(this);
        showPassword.addActionListener(this);

    }

    @Override
    public void actionPerformed(ActionEvent e)
    {
        //LOGIN button
        if (e.getSource() == loginButton)
        {
            TryToSignIn();
        }

        //RESET button
        if (e.getSource() == resetButton)
        {
            userTextField.setText("");
            passwordField.setText("");
        }

        //showPassword JCheckBox
        if (e.getSource() == showPassword)
        {
            if (showPassword.isSelected())
            {
                passwordField.setEchoChar((char) 0);
            }
            else
            {
                passwordField.setEchoChar('*');
            }
        }
    }

    private void TryToSignIn()
    {
        String login = userTextField.getText().trim();
        char[] password = passwordField.getPassword();

        try
        {
            BufferedWriter writer = new BufferedWriter(new OutputStreamWriter(client.getOutputStream()));
            BufferedReader reader = new BufferedReader(new InputStreamReader(client.getInputStream()));

            writer.write("CheckUser\n" +
                    login+"\n"+
                    new String(password)+"\n");
            writer.flush();

            String response = reader.readLine();
            String role = null;
            if (response.contains("Error"))
            {
                JOptionPane.showMessageDialog(this,"Непраильный логин или пароль");
            }
            else
            {
                role = reader.readLine();
                JOptionPane.showMessageDialog(this,"Успешно "+ role);
            }

            switch (role)
            {
                case "admin":
                {
                    AdminClientFrame admin = new AdminClientFrame();
                    break;
                }
                case "usual":
                    UsualClientFrame usual = new UsualClientFrame();
                    break;
                default:
                    break;
            }
            parent.setVisible(false);
            parent.dispose();
            return;
        }
        catch (IOException | NullPointerException e)
        {
            e.printStackTrace();
        }
    }
}
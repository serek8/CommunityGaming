package ab.analogonfragment.core;

/**
 * Created by adam on 01.11.2015.
 */
/*
 * @author Jan Seredyński
 * @version 1 November 2015 r.
 */

import android.os.AsyncTask;
import android.util.Log;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;


public class WarriorSocketClientUsageExample extends AsyncTask implements SocketClientDelegate
{
    private SocketClient socketClient;
    private  BufferedReader inFromUser;
    @Override
    public void clientSocketDidDisconnectFromServer() {
        Log.d("closed", "Connection Closed");
    }

    public void runThis()
    {
        WarriorSocketClientUsageExample client = new WarriorSocketClientUsageExample();
        try {
             socketClient = new SocketClient(5555, "192.168.0.11", client);
             inFromUser = new BufferedReader( new InputStreamReader(System.in));
        } catch (IOException e) {
            // Connection ERROR
            String s = "CE__"+ e.getMessage();
          Log.d("error",s);
        }
    }

    @Override
    protected Object doInBackground(Object[] params) {
        this.runThis();
        return null;
    }

    public void getSendedData(int x, int y){
        WarriorSerializer warrior = new WarriorSerializer();
        int motion = x ;
        int rotation = y;
        int action = 0;
        warrior.movement=motion;
        warrior.roatation=rotation;
        warrior.action=action;

        warrior.writeToStream(socketClient);

    }
}

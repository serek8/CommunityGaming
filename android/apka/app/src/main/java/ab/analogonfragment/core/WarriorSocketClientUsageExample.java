package ab.analogonfragment.core;

/**
 * Created by adam on 01.11.2015.
 */
/*
 * @author Jan Seredyński
 * @version 1 November 2015 r.
 */

import android.app.Activity;
import android.app.Fragment;
import android.os.AsyncTask;
import android.util.Log;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;

import ab.analogonfragment.LS_analog;


public class WarriorSocketClientUsageExample extends AsyncTask implements SocketClientDelegate
{
    private SocketClient socketClient;
    private  BufferedReader inFromUser;
    private Activity activity;

    public WarriorSocketClientUsageExample(Activity fm){
        activity = fm;
    }

    @Override
    public void clientSocketDidDisconnectFromServer() {
        Log.d("closed", "Connection Closed");
    }

    public void runThis()
    {
        try {
             socketClient = new SocketClient(5555, "192.168.0.11", this);
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

    public void getSendedData(double motionDegree, double rotationDegree, int fire ){
        WarriorSerializer warrior = new WarriorSerializer();
        if(socketClient.isIfIsConnected()){
            double motion = motionDegree ;
            double rotation = rotationDegree;
            int action = fire;
            warrior.movement=(int)Math.round(motion);
            warrior.roatation=(int)Math.round(rotation);
            warrior.action=action;
            warrior.writeToStream(socketClient);
        }
        else{
            Log.d("AnalogOnFragment-->>","Nie ustanowiono połączenia z serwerem,");
        }
    }
}

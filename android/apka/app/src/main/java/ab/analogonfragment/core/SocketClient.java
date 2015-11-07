package ab.analogonfragment.core;

/**
 * Created by adam on 01.11.2015.
 */
/*
 * @author Jan Seredyński
 * @version 1 November 2015 r.
 */

import android.util.Log;

import java.io.*;
import java.net.*;
import java.nio.ByteBuffer;
import java.nio.ByteOrder;

interface SocketClientDelegate
{
    void clientSocketDidDisconnectFromServer();
}

interface BaseSerializable
{
    void writeToStream(SocketClient socketClient);
}


public class SocketClient
{
    private Socket clientSocket;
    private DataOutputStream outputStream;
    private SocketClientDelegate delegate;
    private String s ;
    private boolean ifIsConnected = false;

    public boolean isIfIsConnected() {
        return ifIsConnected;
    }

    public SocketClient(int port, String host, SocketClientDelegate delegate) throws IOException
    {
        try {
            this.clientSocket = new Socket(host, port);  //("localhost", 5555);
            this.delegate = delegate;
            this.outputStream = new DataOutputStream(this.clientSocket.getOutputStream());
            ifIsConnected = true;
//			this.buffer4bytes = ByteBuffer.allocate(4);
//			buffer4bytes.order(ByteOrder.LITTLE_ENDIAN);

        } catch (IOException e) {
            e.printStackTrace();
            throw new IOException();


        }
    }

    public void writeInt(int variable)
    {
        ByteBuffer buffer4bytes = ByteBuffer.allocate(4);
        buffer4bytes.order(ByteOrder.LITTLE_ENDIAN);

        try {
            outputStream.write(buffer4bytes.putInt(variable).array(), 0, 4);
        } catch (IOException e) {
            this.delegate.clientSocketDidDisconnectFromServer();
            //e.printStackTrace();
        }

    }

    public String toString(){
        return s;
    }




    public static void main(String[] args) {
        // TODO Auto-generated method stub

    }








}

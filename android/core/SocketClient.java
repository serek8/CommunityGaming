/*
 * @author Jan Seredyński
 * @version 1 November 2015 r.
 */

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
    private DatagramSocket udpClientSocket;
	private DataOutputStream outputStream;
	private SocketClientDelegate delegate;
	private String host;
	
	public SocketClient(int port, String host, SocketClientDelegate delegate) throws IOException
	{
		try {
			this.clientSocket = new Socket(host, port);  //("localhost", 5555);
			this.delegate = delegate;
            this.udpClientSocket = new DatagramSocket();
			this.outputStream = new DataOutputStream(this.clientSocket.getOutputStream());
			this.host = host;
//			this.buffer4bytes = ByteBuffer.allocate(4);
//			buffer4bytes.order(ByteOrder.LITTLE_ENDIAN);
			//Runtime.getRuntime().addShutdownHook(new Thread{public void run(){System.out.println("narazie");}});
			
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
    
    public void sendObjectAsUdp(byte[] data)
    {
    	try {
        DatagramPacket sendPacket = new DatagramPacket(data, data.length,  InetAddress.getByName(host), 5557);
        udpClientSocket.send(sendPacket);
		} catch (IOException e) {
			this.delegate.clientSocketDidDisconnectFromServer();
			//e.printStackTrace();
		}
    }
	
	
	
	
	public static void main(String[] args) {
		// TODO Auto-generated method stub

	}

	
	
	
	
	
	
	
}

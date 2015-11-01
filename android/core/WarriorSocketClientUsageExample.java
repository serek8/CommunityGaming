/*
 * @author Jan Seredyński
 * @version 1 November 2015 r.
 */

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;


public class WarriorSocketClientUsageExample implements SocketClientDelegate
{
	@Override
	public void clientSocketDidDisconnectFromServer() {
		System.out.println("Connection Closed");
		
	}
	
	public static void main(String[] args) throws Exception
	{
		// TODO Auto-generated method stub
		
		WarriorSocketClientUsageExample client = new WarriorSocketClientUsageExample();
		try {
			SocketClient socketClient = new SocketClient(5555,"localhost", client);

		
		BufferedReader inFromUser = new BufferedReader( new InputStreamReader(System.in));
		  String motion, rotation, action;
		while(true)
		{
			  WarriorSerializer warrior = new WarriorSerializer();
			  System.out.println("Type movement:");
			  motion = inFromUser.readLine();
			  
			  System.out.println("Type rotation:");
			  rotation = inFromUser.readLine();
			  
			  System.out.println("Type action:");
			  action = inFromUser.readLine();
			  
			  warrior.movement=Integer.parseInt(motion);
			  warrior.roatation=Integer.parseInt(rotation);
			  warrior.action=Integer.parseInt(action);
			  
			  warrior.writeToStream(socketClient);
			  
			
		}
		} catch (IOException e) {
			// Connection ERROR
			System.out.println("Connection Errror");
		}

		
	}

}

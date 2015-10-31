import java.io.*;
import java.net.*;
import java.nio.ByteBuffer;
import java.nio.ByteOrder;
class TCPClient
{
 public static void main(String argv[]) throws Exception
 {
  String motion, rotation, action;
  int m,r,a;
  String modifiedSentence;
  BufferedReader inFromUser = new BufferedReader( new InputStreamReader(System.in));
  Socket clientSocket = new Socket("localhost", 5555);
  DataOutputStream outToServer = new   DataOutputStream(clientSocket.getOutputStream());
  BufferedReader inFromServer = new BufferedReader(new InputStreamReader(clientSocket.getInputStream()));
  while(true)
  {
//	  int a =1;
//	  a = Integer.reverse(a);
	  System.out.println("Type movement:");
	  motion = inFromUser.readLine();
	  m=Integer.parseInt(motion);
	  System.out.println("Type rotation:");
	  rotation = inFromUser.readLine();
	  r=Integer.parseInt(rotation);
	  System.out.println("Type action:");
	  action = inFromUser.readLine();
	  a=Integer.parseInt(action);
	  
	  ByteBuffer buf = ByteBuffer.allocate(4);
	  buf.order(ByteOrder.LITTLE_ENDIAN);
	  outToServer.write(buf.putInt(m).array(), 0, 4);
	  
	  
	  buf = ByteBuffer.allocate(4);
	  buf.order(ByteOrder.LITTLE_ENDIAN);
	  outToServer.write(buf.putInt(r).array(), 0, 4);
	  
	  
	  buf = ByteBuffer.allocate(4);
	  buf.order(ByteOrder.LITTLE_ENDIAN);
	  outToServer.write(buf.putInt(a).array(), 0, 4);
	  System.out.println("Package has been sent");
  }
  //clientSocket.close();
 }
 
 
 public static final byte[] intToByteArray(int value) {
	 value = Integer.reverse(value);
	    return new byte[] {
	            (byte)(value >>> 24),
	            (byte)(value >>> 16),
	            (byte)(value >>> 8),
	            (byte)value};
	}
}
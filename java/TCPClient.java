import java.io.*;
import java.net.*;
import java.nio.ByteBuffer;
import java.nio.ByteOrder;
class TCPClient
{
 public static void main(String argv[]) throws Exception
 {
  String sentence;
  String modifiedSentence;
  BufferedReader inFromUser = new BufferedReader( new InputStreamReader(System.in));
  Socket clientSocket = new Socket("192.168.0.110", 5555);
  DataOutputStream outToServer = new   DataOutputStream(clientSocket.getOutputStream());
  BufferedReader inFromServer = new BufferedReader(new InputStreamReader(clientSocket.getInputStream()));
  while(true)
  {
	  //int a =1;
	 // a = Integer.reverse(a);
	  sentence = inFromUser.readLine();
	  ByteBuffer buf = ByteBuffer.allocate(4);
	  buf.order(ByteOrder.LITTLE_ENDIAN);
	  
	  outToServer.write(buf.putInt(7).array(), 0, 4);
	  
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
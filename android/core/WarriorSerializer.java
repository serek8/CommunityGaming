/*
 * @author Jan Seredyński
 * @version 1 November 2015 r.
 */


public class WarriorSerializer implements BaseSerializable
{
	public int movement;
	public int roatation;
	public int action;
	


	@Override
	public void writeToStream(SocketClient socketClient) {
		// TODO Auto-generated method stub
		socketClient.writeInt(movement);
		socketClient.writeInt(roatation);
		socketClient.writeInt(action);
	}
	
	
	
	

}

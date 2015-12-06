/*
 * @author Jan Seredyński
 * @version 1 November 2015 r.
 */
import java.nio.ByteBuffer;
import java.nio.ByteOrder;

public class WarriorSerializer implements BaseSerializable
{
	public int movement;
    public int speed;
	public int roatation;
	public int action;
	


	@Override
	public void writeToStream(SocketClient socketClient) {
		// TODO Auto-generated method stub
		socketClient.writeInt(movement);
        socketClient.writeInt(speed);
		socketClient.writeInt(roatation);
		socketClient.writeInt(action);
	}
	
    public void sendAsUdpRoSever(SocketClient socketClient) {
        // TODO Auto-generated method stub
//        socketClient.writeInt(movement);
//        socketClient.writeInt(roatation);
//        socketClient.writeInt(action);
        
        ByteBuffer buffer4bytes = ByteBuffer.allocate(12);
        buffer4bytes.order(ByteOrder.LITTLE_ENDIAN);
        buffer4bytes.putInt(movement);
        buffer4bytes.putInt(roatation);
        buffer4bytes.putInt(action);
        
        socketClient.sendObjectAsUdp(buffer4bytes.array());
        
    }
	
	

}

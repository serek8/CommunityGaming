package ab.analogonfragment;

import android.app.Activity;
import android.graphics.Point;
import android.os.Bundle;
import android.app.Fragment;
import android.util.Log;
import android.view.Display;
import android.view.LayoutInflater;
import android.view.MotionEvent;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import ab.analogonfragment.core.WarriorSocketClientUsageExample;

public class LS_analog extends Fragment {

    private ImageView analogImage, center;
    private float positionX, positionY, centerXPoint, centerYPoint,   maxX, minX, maxY, minY;
    private TextView tv_positionX, tv_positionY;
    private WarriorSocketClientUsageExample w;
    private int width, height;

    private Activity activity;


    public LS_analog(Activity activity){
        this.activity = activity;
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        View v =inflater.inflate(R.layout.fragment_ls_analog, container, false);

        Button b1 = (Button) v.findViewById(R.id.btn_reset);

        b1.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Log.d("Click", "Click");
                autoSetAnalogOnStartPosition();
                autoSetPosition();
                setCenterPoints();
                analogImage.setVisibility(View.VISIBLE);
                center.setVisibility(View.VISIBLE);
                ((MainActivity)activity).runConnection();
                v.setVisibility(View.INVISIBLE);
            }
        });

        analogImage = (ImageView) v.findViewById(R.id.analogImage);
        analogImage.setVisibility(View.INVISIBLE);
        center = (ImageView) v.findViewById(R.id.iVcenter);
        center.setVisibility(View.INVISIBLE);
        positionX = analogImage.getX();
        positionY = analogImage.getY();
        tv_positionX = (TextView) v.findViewById(R.id.tv_X);
        tv_positionY = (TextView) v.findViewById(R.id.tv_Y);
        this.setSizeOfScreen();
        autoSetAnalogOnStartPosition();
        autoSetPosition();
        setCetnerPosition();


        analogImage.setOnTouchListener(new View.OnTouchListener() {
            @Override
            public boolean onTouch(View v, MotionEvent event) {
                switch (event.getAction()) {
                    case MotionEvent.ACTION_DOWN:
                        // movingAnalog = true;
                        break;
                    case MotionEvent.ACTION_MOVE:
                        float x = event.getRawX();
                        float y = event.getRawY();

                        if (x < maxX && x > minX && y > minY && y < maxY) {
                            positionX = event.getRawX() - analogImage.getMeasuredWidth()* 1 / 2;
                            positionY = event.getRawY() - analogImage.getMeasuredHeight() * 1 / 2;
                        } else {
                            positionX = (float) Math.floor(event.getRawX()- analogImage.getMeasuredWidth()* 1 / 2);
                            while (positionX > maxX + analogImage.getMeasuredHeight() * 1 / 2) {
                                positionX = maxX+ analogImage.getMeasuredWidth()* 1 / 2;
                            }
                            while (positionX < minX - analogImage.getMeasuredHeight() * 1 / 2) {
                                positionX = minX- analogImage.getMeasuredWidth()* 1 / 2;
                            }

                            positionY = (float) Math.floor(event.getRawY()- analogImage.getMeasuredHeight() * 1 / 2);
                            while (positionY > maxY+ analogImage.getMeasuredHeight() * 1 / 2) {
                                positionY = maxY+ analogImage.getMeasuredHeight() * 1 / 2;
                            }
                            while (positionY < minY- analogImage.getMeasuredHeight() * 1 / 2) {
                                positionY = minY- analogImage.getMeasuredHeight() * 1 / 2;
                            }
                        }
                        analogImage.setX(positionX);
                        analogImage.setY(positionY);
                        ((MainActivity)activity).getW().getSendedData(getDegreeFromXY(Math.round(positionX), Math.round(positionY)),0,0);
                        autoSetPosition();
                        break;
                    case MotionEvent.ACTION_UP:
                        autoSetAnalogOnStartPosition();
                        autoSetPosition();
                        break;
                }
                return true;
            }
        });
        return v;
    }

    private void setCetnerPosition(){
        center.setX(analogImage.getX() - (center.getDrawable().getIntrinsicWidth() * 1 / 2)); //800
        center.setY(analogImage.getY() - (center.getDrawable().getIntrinsicHeight()  * 1 / 2));  //600
    }
    private void autoSetPosition() {
        tv_positionX.setText(String.valueOf(analogImage.getX())); //700
        tv_positionY.setText(String.valueOf(analogImage.getY())); //400
    }

    private void autoSetAnalogOnStartPosition() {
        analogImage.setX((width * 60 / 100) - (analogImage.getMeasuredWidth() * 1 / 2));
        analogImage.setY((height * 20 / 100) - (analogImage.getMeasuredHeight() * 1 / 2));


        //analogImage.setX(800- analogImage.getWidth()/2);
        //analogImage.setY(500- analogImage.getHeight()*6/10);
    }
    private void setCenterPoints(){
        centerXPoint = (width * 60 / 100) - (analogImage.getMeasuredWidth() * 1 / 2);
        centerYPoint = (height * 20 / 100) - (analogImage.getMeasuredHeight() * 1 / 2);
        maxX = Math.round(centerXPoint) + (width*2/100) ;
        minX = Math.round(centerXPoint) -(width*2/100)  ;
        maxY = Math.round(centerYPoint) +(width*2/100)  ;
        minY = Math.round(centerYPoint) -(width*2/100)  ;
    }

    private double getDegreeFromXY(int x, int y){
        float xFromCenter = x-centerXPoint;
        float yFromCenter = y-centerYPoint;
        double rad = Math.atan2(yFromCenter, xFromCenter); // In radians
        double deg = rad * (180 / Math.PI);
        if(deg<0){
            deg = deg*(-1);
            deg = deg + 90;
        }else if(deg>90 ){
            deg = deg*(-1);
            deg = deg +450;
        }
        else if(deg<=90 ){
            deg=90-deg;
        }
        Log.d("MATH", "    phi= " + deg);
        return deg;
    }

    private void setSizeOfScreen(){
        Display display = getActivity().getWindowManager().getDefaultDisplay();
        Point size = new Point();
        display.getSize(size);
        width = size.x;
        height = size.y;
    }

    public void creatDialog(){
        Toast.makeText(getContext().getApplicationContext(), "Nie udało się nawiązać połączenia z serwerem",Toast.LENGTH_SHORT).show();
    }
}

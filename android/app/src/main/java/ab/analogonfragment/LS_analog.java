package ab.analogonfragment;

import android.os.Bundle;
import android.app.Fragment;
import android.view.LayoutInflater;
import android.view.MotionEvent;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;

public class LS_analog extends Fragment {

    private ImageView analogImage, center;
    private float positionX, positionY;
    private TextView tv_positionX, tv_positionY;

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        View v =inflater.inflate(R.layout.fragment_ls_analog, container, false);
        analogImage = (ImageView) v.findViewById(R.id.analogImage);
        analogImage.setOnTouchListener(new View.OnTouchListener() {
            @Override
            public boolean onTouch(View v, MotionEvent event) {
                switch (event.getAction()) {
                    case MotionEvent.ACTION_DOWN:
                        // movingAnalog = true;
                        break;
                    case MotionEvent.ACTION_MOVE:
                        // if(movingAnalog){
                        float x = analogImage.getX();
                        float y = analogImage.getY();

                        if (x < 800 && x > 600 && y > 300 && y < 500) {
                            positionX = event.getRawX() - analogImage.getWidth() / 2;
                            positionY = event.getRawY() - analogImage.getHeight() * 6 / 10;
                            analogImage.setX(positionX);
                            analogImage.setY(positionY);
                            autoSetPosition();
                        } else {
                            positionX = (float) (Math.floor(event.getRawX() / 100) * 100);
                            while (positionX > 800) {
                                positionX = 800;
                            }
                            while (positionX < 600) {
                                positionX = 600;
                            }

                            positionY = (float) (Math.floor(event.getRawY() / 100) * 100);
                            while (positionY > 500) {
                                positionY = 500;
                            }
                            while (positionY < 300) {
                                positionY = 300;
                            }


                            analogImage.setX(positionX);
                            analogImage.setY(positionY);
                            autoSetPosition();
                        }

                        // }
                        break;
                    case MotionEvent.ACTION_UP:
                        autoSetAnalogOnStartPosition();
                        autoSetPosition();
                        break;


                }
                return true;
            }
        });
        positionX = analogImage.getX();
        positionY = analogImage.getY();
        tv_positionX = (TextView) v.findViewById(R.id.tv_X);
        tv_positionY = (TextView) v.findViewById(R.id.tv_Y);
        autoSetAnalogOnStartPosition();
        autoSetPosition();
        center = (ImageView) v.findViewById(R.id.iVcenter);
        center.setX(800);
        center.setY(500);
        return v;
    }
    private void autoSetPosition() {
        tv_positionX.setText(String.valueOf(analogImage.getX()));
        tv_positionY.setText(String.valueOf(analogImage.getY()));
    }

    private void autoSetAnalogOnStartPosition(){
        analogImage.setX(700);
        analogImage.setY(400);

        //analogImage.setX(800- analogImage.getWidth()/2);
        //analogImage.setY(500- analogImage.getHeight()*6/10);
    }


}

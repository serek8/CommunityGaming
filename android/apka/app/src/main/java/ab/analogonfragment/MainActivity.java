package ab.analogonfragment;

import android.app.Activity;
import android.app.FragmentManager;
import android.app.FragmentTransaction;
import android.content.Context;
import android.media.Image;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuItem;
import android.view.MotionEvent;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.FrameLayout;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.RelativeLayout;
import android.widget.TextView;

import org.w3c.dom.Text;

import ab.analogonfragment.core.WarriorSocketClientUsageExample;

public class MainActivity extends Activity  {

    private ImageView analogImage, center;
    private float positionX, positionY;
    private TextView tv_positionX, tv_positionY;
    private Button reset;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
     /*   analogImage = (ImageView) findViewById(R.id.analogImage);
        analogImage.setOnTouchListener(this);
        positionX = analogImage.getX();
        positionY = analogImage.getY();
        tv_positionX = (TextView) findViewById(R.id.tv_X);
        tv_positionY = (TextView) findViewById(R.id.tv_Y);
        autoSetAnalogOnStartPosition();
        autoSetPosition();
        center = (ImageView) findViewById(R.id.iVcenter);
        center.setX(400);
        center.setY(200);
        reset = (Button) findViewById(R.id.btn_reset);
        reset.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                autoSetAnalogOnStartPosition();
            }
        });

*/
        LS_analog ls_analog = new LS_analog();
        FragmentManager fragmentManager = getFragmentManager();
        FragmentTransaction fragmentTransaction = fragmentManager.beginTransaction();
        fragmentTransaction.replace(R.id.leftSide, ls_analog );
        fragmentTransaction.commit();



   /*
    }

    private void autoSetPosition() {
        tv_positionX.setText(String.valueOf(analogImage.getX()));
        tv_positionY.setText(String.valueOf(analogImage.getY()));
    }

    private void autoSetAnalogOnStartPosition(){
        analogImage.setX(400- analogImage.getWidth()/2);
        analogImage.setY(200- analogImage.getHeight()*6/10);
    }

*/
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.menu_main, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        //noinspection SimplifiableIfStatement
        if (id == R.id.action_settings) {
            return true;
        }

        return super.onOptionsItemSelected(item);
    }

/*
    @Override
    public boolean onTouch(View v, MotionEvent event) {

        switch (event.getAction()){
            case MotionEvent.ACTION_DOWN:
               // movingAnalog = true;
                break;
            case  MotionEvent.ACTION_MOVE:
                   // if(movingAnalog){
                    float x = analogImage.getX();
                    float y = analogImage.getY();

                    if(x < 500 && x > 300  && y>50 && y <250){
                        positionX = event.getRawX() - analogImage.getWidth()/2;
                        positionY = event.getRawY() - analogImage.getHeight()*6/10;
                        analogImage.setX(positionX);
                        analogImage.setY(positionY);
                        autoSetPosition();
                    }else{
                        positionX = (float) (Math.floor(event.getRawX()/100)*100);
                        while (positionX>500 ) {
                            positionX = 500;
                        }
                        while (positionX<300) {
                            positionX = 300;
                        }

                        positionY = (float) (Math.floor(event.getRawY()/100)*100);
                        while (positionY>250 ) {
                            positionY = 250;
                        }
                        while (positionY<50 ) {
                            positionY = 50;
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
    */
}

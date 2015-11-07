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
import android.widget.ImageButton;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.RelativeLayout;
import android.widget.TextView;

import org.w3c.dom.Text;

import ab.analogonfragment.core.WarriorSocketClientUsageExample;

public class MainActivity extends Activity  {

    private WarriorSocketClientUsageExample w;
    private boolean ifIsRun = false;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        LS_analog ls_analog = new LS_analog( this);
        PS_analogButtons ps_analogButtons = new PS_analogButtons(this);
        FragmentManager fragmentManager = getFragmentManager();
        FragmentTransaction fragmentTransaction = fragmentManager.beginTransaction();
        fragmentTransaction.replace(R.id.leftSide, ls_analog);
        fragmentTransaction.replace(R.id.rideSide, ps_analogButtons);
        fragmentTransaction.commit();
        ImageButton b1 = (ImageButton) findViewById(R.id.fireBtn);
        b1.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                runConnection();
                w.getSendedData(0,0,1);
            }
        });


    }

    public WarriorSocketClientUsageExample getW() {
        return w;
    }

    public void runConnection(){
        if(!ifIsRun){
            w = new WarriorSocketClientUsageExample(this);
            w.execute();
            ifIsRun = true;
        }
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

}

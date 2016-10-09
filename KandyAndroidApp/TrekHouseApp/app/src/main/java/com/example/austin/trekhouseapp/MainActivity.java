package com.example.austin.trekhouseapp;

import android.content.Context;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.Gravity;
import android.view.View;
import android.widget.Toast;

public class MainActivity extends AppCompatActivity {
    public static final String TAG = "MainActivity";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
    }

    public void ToastButtonSend(View view) {
        CharSequence text = "Yeah Toast!!!";
        MakeAToast(text);
    }

    private void MakeAToast(CharSequence theSequence) {
        Context context = getApplicationContext();
        int duration = Toast.LENGTH_SHORT;

        Toast toast = Toast.makeText(context, theSequence, duration);
        toast.setGravity(Gravity.BOTTOM, 0, 50);
        toast.show();
    }
}
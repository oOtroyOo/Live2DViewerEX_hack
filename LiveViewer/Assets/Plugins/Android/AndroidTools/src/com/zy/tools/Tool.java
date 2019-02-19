package com.zy.tools;

import java.io.*;
import java.lang.Process;
import java.net.*;
import java.util.*;

import android.app.*;
import android.app.ActivityManager.*;
import android.content.*;
import android.content.pm.*;
import android.content.pm.PackageManager.*;
import android.content.res.*;
import android.net.*;
import android.os.*;
import android.provider.*;
import android.text.format.Formatter;
import android.util.*;
import android.view.*;
import android.widget.*;

public class Tool {
    Activity currentActivity;

    public Activity GetCurrentActivity() {       
        return currentActivity;
    }

    public Context GetCurrentActivityContext() {
        return (Context) GetCurrentActivity();
    }

    public Tool(Activity activity) {
        this.currentActivity = activity;
    }

   
    /**
     * 应用程序运行命令获取 Root权限，设备必须已破解(获得ROOT权限)
     *
     * @return 应用程序是/否获取Root权限
     */
    /**
     * 应用程序运行命令获取 Root权限，设备必须已破解(获得ROOT权限)
     *
     * @return 应用程序是/否获取Root权限
     */
    public boolean upgradeRootPermission() {

        //GetCurrentActivity().runOnUiThread(new Runnable() {
        //    @Override
        //    public void run() {
        //        RootCommand("cd /data/data ; ls -a");
        //    }
        //});
        return RootCommand("chmod 777 " + GetCurrentActivity().getPackageCodePath());
    }

    public boolean RootCommand(String command)
    {

        Process process = null;
        DataOutputStream os = null;
        try {
            process = Runtime.getRuntime().exec("su");
            os = new DataOutputStream(process.getOutputStream());
            os.writeBytes(command + "\n");
            os.writeBytes("exit\n");
            os.flush();
            process.waitFor();
        } catch (Exception e) {
            Log.d("*** DEBUG ***", "ROOT REE" + e.getMessage());
            return false;
        } finally {
            try {
                if (os != null) {
                    os.close();
                }
                process.destroy();
            } catch (Exception e) {
            }
        }
        Log.d("*** DEBUG ***", "Root SUC " + command);
        return true;
    }

    public byte[] ReadFile(String path) throws IOException {
        File file = new File(path);
        FileInputStream in = null;
        try {
            in = new FileInputStream(file);
            byte[] bytes = new byte[(int) file.length()];
            in.read(bytes);
            in.close();
            return bytes;
        } finally {
            if (in != null) {
                try {
                    in.close();
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }
        }
    }

    public void WriteFile(String path, byte[] data) throws IOException {

        try {
            File file = new File(path);
            if (file.exists()) {
                file.delete();
            }
            FileOutputStream out = new FileOutputStream(file);
            out.write(data);
			out.close();
        } finally {

        }
    }
}
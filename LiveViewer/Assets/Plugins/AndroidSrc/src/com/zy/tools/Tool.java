package com.zy.tools;

import android.app.Activity;
import android.app.Fragment;
import android.content.Context;
import android.content.Intent;
import android.content.UriPermission;
import android.net.Uri;
import android.os.Bundle;
import android.os.Environment;
import android.util.Log;
import android.widget.Toast;

import java.io.DataOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;

import androidx.documentfile.provider.DocumentFile;


public class Tool {


    Activity currentActivity;

    public String pkg;

    public Activity GetCurrentActivity() {
        return currentActivity;
    }

    public Context GetCurrentActivityContext() {
        return (Context) GetCurrentActivity();
    }

    public Tool(Activity activity) {
        this.currentActivity = activity;
    }

    public void RequestDataPermission(Runnable callback) {
        ToolFragment.callback = callback;
        boolean granted = false;
        for (UriPermission persistedUriPermission : currentActivity.getContentResolver().getPersistedUriPermissions()) {
            if (persistedUriPermission.isReadPermission() && persistedUriPermission.getUri().toString().contains("content://com.android.externalstorage.documents/tree/primary%3AAndroid%2Fdata")) {
                granted = true;
            }
        }
        if (granted) {
            callback.run();
        } else {
            final Fragment request = new ToolFragment();
            Bundle bundle = new Bundle();
            bundle.putString("pkg", pkg);
            request.setArguments(bundle);
            ((Activity) currentActivity).getFragmentManager().beginTransaction().add(0, request).commit();
        }
    }

    public void CopyAllFiles() {
        Toast.makeText(currentActivity, "读取文件中", Toast.LENGTH_LONG).show();
        DocumentFile documentFile = DocumentFile.fromTreeUri(currentActivity, Uri.parse(changeToUri3("Android/data/" + pkg)));
        CopyFiles(documentFile);
    }

    //遍历示例，不进行额外逻辑处理
    void CopyFiles(DocumentFile documentFile) {
        Log.d("文件:", documentFile.getName());
        if (documentFile.isDirectory()) {
            for (DocumentFile file : documentFile.listFiles()) {
                Log.d("子文件", file.getName());
                if (file.isFile()) {
                    CopyStream(file);
                }
                if (file.isDirectory()) {
                    CopyFiles(file);//递归调用
                }
            }
        }
        if (documentFile.isFile()) {
            CopyStream(documentFile);
        }

    }

    void CopyStream(DocumentFile documentFile) {
        //content://com.android.externalstorage.documents/tree/primary%3AAndroid%2Fdata%2Fcom.pavostudio.live2dviewerex/document/primary%3AAndroid%2Fdata%2Fcom.pavostudio.live2dviewerex%2Ffiles
        try {
            String targetPath = currentActivity.getExternalFilesDir("").getAbsolutePath() + UriToFile(documentFile.getUri()).substring(("Android/data" + pkg + "/files/").length());
            InputStream inputStream = currentActivity.getContentResolver().openInputStream(documentFile.getUri());
            File targetFile = new File(targetPath);
            if (!targetFile.getParentFile().exists()) {
                targetFile.getParentFile().mkdirs();
            }
            OutputStream outputStream = new FileOutputStream(targetPath);
            byte[] buffer = new byte[4096];
            int count;
            while ((count = inputStream.read(buffer)) > 0) {
                outputStream.write(buffer, 0, count);
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public boolean CopyBack(String localFile) {
//        String localFullPath = currentActivity.getExternalFilesDir("").getAbsolutePath() + '/' + localFile;
        String targetPath = localFile.replace(currentActivity.getPackageName(), pkg);
        DocumentFile documentFile = DocumentFile.fromTreeUri(currentActivity, Uri.parse(changeToUri3(targetPath)));
        boolean canwrite = documentFile.canWrite();
        Uri targetUri = documentFile.getUri();
        currentActivity.grantUriPermission("com.android.externalstorage.ExternalStorageProvider", targetUri, Intent.FLAG_GRANT_READ_URI_PERMISSION);
        currentActivity.grantUriPermission("com.android.externalstorage.ExternalStorageProvider", targetUri, Intent.FLAG_GRANT_WRITE_URI_PERMISSION);
        currentActivity.grantUriPermission("com.android.externalstorage.ExternalStorageProvider", targetUri, Intent.FLAG_GRANT_PERSISTABLE_URI_PERMISSION);

        try {
            InputStream inputStream = new FileInputStream(localFile);
            OutputStream outputStream = currentActivity.getContentResolver().openOutputStream(targetUri);
            byte[] buffer = new byte[4096];
            int count;
            while ((count = inputStream.read(buffer)) > 0) {
                outputStream.write(buffer, 0, count);
            }
            return true;
        } catch (Exception e) {
            e.printStackTrace();
            if (upgradeRootPermission()) {
                RootCommand("cp -f " + localFile + " " + targetPath);
                return true;
            }
        }
        return false;
    }

    String UriToFile(Uri uri) {
        //content://com.android.externalstorage.documents/tree/primary%3AAndroid%2Fdata%2Fcom.pavostudio.live2dviewerex/document/primary%3AAndroid%2Fdata%2Fcom.pavostudio.live2dviewerex%2Ffiles
        String path = uri.toString();
        String search = pkg + "/document/primary";
        path = path.substring(path.indexOf(search) + search.length() + 3);
        path = path.replace("%2F", "/");
        return path;
    }

    //转换至uriTree的路径
    public static String changeToUri3(String path) {
        path = path.replace("/storage/emulated/0/", "").replace("/", "%2F");
        return ("content://com.android.externalstorage.documents/tree/primary%3A" + path);

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

    public boolean RootCommand(String command) {

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
# Live2DViewerEX_SaveEdit

# 介绍
仅供学习交流

# 软件架构

手机版 [前往releases页面下载](../../releases)

使用 Unity 2019 打开 [目录LiveViewer](LiveViewer) 进行打包

# 使用说明

## 用户数据路径

```
存储/Android/data/com.pavostudio.live2dviewerex/files/save/perf.dat
```
用户模型存档路径

```
存储/Android/data/com.pavostudio.live2dviewerex/files/save/cha/[模型id]
```
 **如果是Android 11** ，存储/Android/data已无法用默认文件管理器打开，可以使用RS文件浏览器v1.7.2查看

## 使用

运行`Live修改器`，选择功能 ，打开存档，以json格式进行编辑。

- 用户数据的积分

    计算方法应该是 `积分=randomKey-userPointEnc` 修改`userPointEnc`即可

- 模型存档

    有一些模型是有积分制度的。如果打开其中的文件，同时修改 `intimacy` 和 `intimacyAmount`

- 保存
  
    由于Android 10、11 的新文件权限的限制。
    其中Android 11保存文件需要root权限，Android 10可能也要root，但我手里没有手机。
    我暂时没有查询到免root保存的方式。如果有参考文档可以联系我
    

## 加密算法

```
json文本 互相转 UTF-8 byte[]
使用以下的算法进行简单的加密

  for (int i = 0; i < data.Length; i++)
  {
       data[i] = (byte)((data.Length & 255) ^ (int)data[i]); 
  }
```

# 控制台测试

打开`Live2DViewerEX.sln`。这是一个 .Net Console项目，用来测试一些算法，可以复制以上文件到电脑。运行代码

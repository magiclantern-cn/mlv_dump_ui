﻿using mlv_dump_ui.Models.BindingView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;
using CheckBox = System.Windows.Controls.CheckBox;
using MessageBox = System.Windows.MessageBox;

namespace mlv_dump_ui
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        //定义常量  
        public const int WM_DEVICECHANGE = 0x219;
        public const int DBT_DEVICEARRIVAL = 0x8000;
        public const int DBT_DEVICEREMOVECOMPLETE = 0x8004;

        private HwndSource hwndSource = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            ScanRemovableDriver();
            //_syncContext = SynchronizationContext.Current;

            hwndSource = PresentationSource.FromVisual(this) as HwndSource; //窗口过程
            if (hwndSource != null)
                hwndSource.AddHook(new HwndSourceHook(WndProc)); //挂钩
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (hwndSource != null)
                hwndSource.RemoveHook(WndProc);
            base.OnClosing(e);
        }

        private void AddMlvFileForListView(MLVFileInfo fileInfo)
        {
            if (fileInfo == null)
                return;
            if (listView1.ItemContainerGenerator.Items.Any(a => a.ToString() == fileInfo.ToString()) == false)
                listView1.Items.Add(fileInfo);
        }

        private void ScanRemovableDriver()
        {
            DriveInfo[] s = DriveInfo.GetDrives();
            foreach (DriveInfo drive in s)
            {
                if (drive.DriveType == DriveType.Removable && drive.IsReady)
                {
                    ScanMlvFile(drive.Name);
                }
            }
        }
        /// <summary>
        /// 扫描MLV文件
        /// </summary>
        /// <param name="path"></param>
        private void ScanMlvFile(string path)
        {
            string folderFullName = Path.Combine(path, "DCIM");
            if (Directory.Exists(folderFullName))
            {
                
                DirectoryInfo TheFolder = new DirectoryInfo(folderFullName);
                foreach (DirectoryInfo NextFolder in TheFolder.GetDirectories())
                {
                    foreach (var file in Directory.GetFiles(NextFolder.FullName, "*.MLV"))
                    {
                        var fileInfo = new FileInfo(file);
                        AddMlvFileForListView(new MLVFileInfo()
                        {
                            Name = fileInfo.Name,
                            Path = fileInfo.DirectoryName,
                            CreateTime = fileInfo.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"),
                            Size = Math.Round((decimal)fileInfo.Length / (1024 * 1024 * 1024), 2)
                        });
                    }
                }
            }
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            try
            {
                if (msg == WM_DEVICECHANGE)
                {
                    switch (wParam.ToInt32())
                    {
                        case WM_DEVICECHANGE:
                            break;
                        case DBT_DEVICEARRIVAL:
                            ScanRemovableDriver();
                            //this.textBox2.AppendText("U盘已插入，盘符是" + drive.Name.ToString() + "\r\n");
                            break;
                        case DBT_DEVICEREMOVECOMPLETE:
                            //this.textBox2.AppendText("U盘已卸载\r\n");
                            listView1.Items.Clear();
                            break;
                        default:
                            break;
                    }
                }
                return IntPtr.Zero;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return IntPtr.Zero;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("选择导出目录");
                return;
            }

            if (listView1.ItemContainerGenerator.Items.Any(a => ((MLVFileInfo) a).Select) == false)
            {
                MessageBox.Show("选择导出视频");
                return;
            }

            foreach (MLVFileInfo item in listView1.ItemContainerGenerator.Items)
            {
                if (item.Select)
                {
                    string file = item.ToString();
                    var fileInfo = new FileInfo(file);
                    string path = Path.Combine(textBox1.Text, Path.GetFileNameWithoutExtension(item.ToString()));
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string cmdLine = string.Format("--batch --dng {0} -o {1}\\", file, path);
                    new Thread(() => { MlvToDng(cmdLine,item.Id); }).Start();
                }

                //textBox2.Text += item.ToString() + "\r\n";
            }
        }

        //private Task UpdateUiAsync(int i)
        //{
        //    return Task.Run(() =>
        //    {
        //        this.Dispatcher.Invoke(async () =>
        //        {
        //            while (true)
        //            {
        //                listView1.Items.Add(new MLVFileInfo
        //                {
        //                    Name = i.ToString() + ".MLV",
        //                    CreateTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"),
        //                    Path = "C:/",
        //                    Size = i
        //                });
        //                await Task.Delay(300);
        //            }
        //        });
        //    });
        //}

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog m_Dialog = new FolderBrowserDialog();
            DialogResult result = m_Dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            textBox1.Text = m_Dialog.SelectedPath.Trim();
        }

        private void MlvToDng(string cmdLine,Guid? id)
        {
            string exeName = Directory.GetCurrentDirectory() + "\\mlv_dump.exe";

            //实例化一个进程类
            Process cmd = new Process();

            //cmd.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();

            //获得系统信息，使用的是 systeminfo.exe 这个控制台程序
            cmd.StartInfo.FileName = exeName;

            cmd.StartInfo.Arguments = cmdLine;

            //将cmd的标准输入和输出全部重定向到.NET的程序里

            cmd.StartInfo.UseShellExecute = false; //此处必须为false否则引发异常

            cmd.StartInfo.RedirectStandardInput = true; //标准输入
            cmd.StartInfo.RedirectStandardOutput = true; //标准输出
            cmd.StartInfo.RedirectStandardError = true;

            //不显示命令行窗口界面
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            cmd.OutputDataReceived += (sender, args) => Display(args.Data,id);
            cmd.ErrorDataReceived +=  (sender, args) => Display(args.Data,id);

            cmd.Start(); //启动进程

            cmd.BeginOutputReadLine();
            cmd.BeginErrorReadLine();

            cmd.WaitForExit(); //you need this in order to flush the output buffer
        }

        private Task Display(string output,Guid? id)
        {
            
            return Task.Run(() =>
            {
                this.Dispatcher.Invoke(async () =>
                {
                    if (!string.IsNullOrWhiteSpace(output))
                    {
                        int startIndex = output.IndexOf("V:");
                        int endIndex = output.IndexOf("A:");
                        if (startIndex > 0 && endIndex > 0)
                        {
                            string taskPro = output.Substring(startIndex + 2, endIndex - (startIndex + 2) - 1);
                            listView1.ItemContainerGenerator.Items.Select(s => (MLVFileInfo) s)
                                .First(f => f.Id == id).TaskProgress = taskPro;
                            listView1.Items.Refresh();
                        }
                    }

                    await Task.Delay(1);
                });
            });
        }

        private void Check_ALL_OnClick(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            if (cb.IsChecked == true)
            {
                foreach (MLVFileInfo fileInfo in listView1.ItemContainerGenerator.Items)
                {
                    fileInfo.Select = true;
                }
            }
            else
            {
                foreach (MLVFileInfo fileInfo in listView1.ItemContainerGenerator.Items)
                {
                    fileInfo.Select = false;
                }
            }
        }

        private void CheckBox1_OnClick(object sender, RoutedEventArgs e)
        {
            CheckBox box = (CheckBox) sender;
            listView1.ItemContainerGenerator.Items.Select(s => (MLVFileInfo) s)
                .First(f => f.Id.ToString() == box.Tag.ToString()).Select = box.IsChecked.Value;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            listView1.Items.Refresh();
        }
    }
}

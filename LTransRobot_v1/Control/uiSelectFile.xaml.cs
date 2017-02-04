using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using log4net;
using Microsoft.Win32;
using Word = Microsoft.Office.Interop.Word;
using LTransDeal_v1;

namespace LTransRobot_v1.Control
{
    /// <summary>
    /// uiSelectFile.xaml 的交互逻辑
    /// </summary>
    public partial class uiSelectFile : UserControl
    {
        private ILog log;
        public uiSelectFile()
        {
            InitializeComponent();
            log = LogManager.GetLogger("log");
            button_source_file.Click += Button_source_file_Click;
            button_dest_file.Click += Button_dest_file_Click;
            button_todo.Click += Button_todo_Click;
        }

        private void Button_todo_Click(object sender, RoutedEventArgs e)
        {
            if (source_fn.Equals("")) return;
            ReadDoc();
        }

        private void asnyc_showinfo(string info)
        {
            System.Windows.Application.Current.Dispatcher.BeginInvoke(new Action<string>(showinfo), info);
        }
        private void showinfo(string info)
        {
            textBox_info.AppendText(info + '\n');
        }

        private void ReadDoc()
        {
            //if (dest_file.Equals(""))
            //{
            //    MessageBox.Show("结果保存文件未设置");
            //    return;
            //}
            Word.Application wordapp = new Word.Application();
            Word.Document doc = null;
            try
            {
                doc = wordapp.Documents.Open(source_fn);
                int pc = doc.Paragraphs.Count;
                int sc = doc.Sentences.Count;
                
                showinfo("段落总数：" + pc);
                showinfo("句子总数：" + sc);
               

                showinfo("示例");
                int n = pc > 50 ? 50 : pc;

                StringBuilder sb = new StringBuilder();
                for (int i = 1; i < n; i++)
                {
                    string s = doc.Paragraphs[i].Range.Text;
                    //if (s == null||s.Equals("")) continue;
                    //sb.Append(s);
                    s = s.Trim().Replace("\a", "").Replace("\r","").Replace("\n", "");
                    if (s.Equals("")) continue;
                    sb.Append(s + "\n");
                    s = LTransDeal_v1.LTransDeal.getTransResult(s);
                    //if (s == null) continue;
                    sb.Append(s+"\n");
                }
                    

                showinfo(sb.ToString());
            }
            catch (Exception ex)
            {
                log.Error(ex);
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if(doc != null)
                {
                    doc.Close();
                    wordapp.Quit();
                    doc = null;
                    wordapp = null;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
            }

        }

        private string dest_file = "";
        private void Button_dest_file_Click(object sender, RoutedEventArgs e)
        {
            if (source_fn.Equals("")) return;
            string ext = System.IO.Path.GetExtension(source_fn);
            string name = System.IO.Path.GetFileNameWithoutExtension(source_fn);
            SaveFileDialog sf = new SaveFileDialog();
            string fn = name + System.DateTime.Now.ToString("-[yyMMddHHmmss]")+ext;
            sf.FileName = fn;
            if (sf.ShowDialog() == false)
            {
                dest_file = "";
                return;
            }
            dest_file = sf.FileName;
            textBox_Save.Text = dest_file;
            System.IO.File.Copy(source_fn, dest_file);
            
        }

        private string source_fn = "";
        private void Button_source_file_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "Word文件|*.doc;*.docx|所有文件|*.*";
            of.FilterIndex = 0;
            if (of.ShowDialog() == false)
            {
                source_fn = "";
                return;
            }
            source_fn = of.FileName;
            textBox_file.Text = source_fn;
        }
    }
}

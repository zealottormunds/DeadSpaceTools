using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Zealot_s_Dead_Space_Tools
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // When drag&dropping the file whose name will be changed
        private void listBox1_DragDrop(object sender, DragEventArgs e)
        {
            string n = e.Data.GetData(DataFormats.Text).ToString();
            MessageBox.Show(n);
        }

        List<string> original_paths = new List<string>();
        List<string> original_filenames = new List<string>();
        List<string> new_filenames = new List<string>();

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Multiselect = true;
            o.ShowDialog();

            if (o.FileNames.Length == 0) return;

            string[] files = o.FileNames;
            for (int x = 0; x < files.Length; x++)
            {
                string actual = files[x];
                original_paths.Add(Path.GetDirectoryName(actual));
                original_filenames.Add(Path.GetFileName(actual));
            }

            UpdateSourceList();
        }

        void UpdateSourceList()
        {
            listBox1.Items.Clear();

            for(int x = 0; x < original_filenames.Count; x++)
            {
                string dest = original_filenames[x];
                listBox1.Items.Add(x.ToString() + " : " + dest);
            }
        }

        // Add files to target names
        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Multiselect = true;
            o.ShowDialog();

            if (o.FileNames.Length == 0) return;

            string[] files = o.FileNames;
            for (int x = 0; x < files.Length; x++)
            {
                string actual = files[x];
                new_filenames.Add(Path.GetFileName(actual));
            }

            UpdateTargetList();
        }

        void UpdateTargetList()
        {
            listBox2.Items.Clear();

            for (int x = 0; x < new_filenames.Count; x++)
            {
                string dest = new_filenames[x];
                listBox2.Items.Add(x.ToString() + " : " + dest);
            }
        }

        // Move Source Up
        private void button8_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex <= 0 || listBox1.Items.Count == 1) return;

            int i = listBox1.SelectedIndex;
            int target = i - 1;

            string newn = original_filenames[target];
            string newn_p = original_paths[target];

            string orin = original_filenames[i];
            string orin_p = original_paths[i];

            original_filenames[target] = orin;
            original_paths[target] = orin_p;

            original_filenames[i] = newn;
            original_paths[i] = newn_p;

            UpdateSourceList();
            listBox1.SelectedIndex = target;
        }

        // Move Source Down
        private void button9_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1 || listBox1.SelectedIndex == listBox1.Items.Count - 1 || listBox1.Items.Count == 1) return;

            int i = listBox1.SelectedIndex;
            int target = i + 1;

            string newn = original_filenames[target];
            string newn_p = original_paths[target];

            string orin = original_filenames[i];
            string orin_p = original_paths[i];

            original_filenames[target] = orin;
            original_paths[target] = orin_p;

            original_filenames[i] = newn;
            original_paths[i] = newn_p;

            UpdateSourceList();
            listBox1.SelectedIndex = target;
        }

        // Remove Source
        private void button7_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1) return;

            int i = listBox1.SelectedIndex;
            original_paths.RemoveAt(i);
            original_filenames.RemoveAt(i);
            UpdateSourceList();

            if (listBox1.Items.Count > i) listBox1.SelectedIndex = i;
            else listBox1.SelectedIndex = listBox1.Items.Count - 1;
        }

        // Move Target Up
        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex <= 0 || listBox2.Items.Count == 1) return;

            int i = listBox2.SelectedIndex;
            string newn = new_filenames[listBox2.SelectedIndex - 1];
            string orin = new_filenames[listBox2.SelectedIndex];
            new_filenames[listBox2.SelectedIndex - 1] = orin;
            new_filenames[listBox2.SelectedIndex] = newn;

            UpdateTargetList();
            listBox2.SelectedIndex = i - 1;
        }

        // Move Target Down
        private void button5_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex == -1 || listBox2.SelectedIndex == listBox2.Items.Count - 1 || listBox2.Items.Count == 1) return;

            int i = listBox2.SelectedIndex;
            string orin = new_filenames[listBox2.SelectedIndex];
            string newn = new_filenames[listBox2.SelectedIndex + 1];
            new_filenames[listBox2.SelectedIndex] = newn;
            new_filenames[listBox2.SelectedIndex + 1] = orin;

            UpdateTargetList();
            listBox2.SelectedIndex = i + 1;
        }

        // Remove Target
        private void button6_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex == -1) return;

            int i = listBox2.SelectedIndex;
            new_filenames.RemoveAt(i);
            UpdateTargetList();

            if (listBox2.Items.Count > i) listBox2.SelectedIndex = i;
            else listBox2.SelectedIndex = listBox2.Items.Count - 1;
        }

        // Convert
        private void button1_Click(object sender, EventArgs e)
        {
            for(int x = 0; x < original_filenames.Count; x++)
            {
                if(x >= new_filenames.Count)
                {
                    MessageBox.Show("Files not renamed: " + (original_filenames.Count - new_filenames.Count).ToString());
                    return;
                }

                string ofile = original_paths[x] + "/" + original_filenames[x];
                string nfile = original_paths[x] + "/" + new_filenames[x];
                File.Move(ofile, nfile);
            }
        }

        private void fileConverterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            File_Converter.FileConverter f = new File_Converter.FileConverter();
            f.ShowDialog();
        }
    }
}

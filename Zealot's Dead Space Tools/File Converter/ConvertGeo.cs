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

namespace Zealot_s_Dead_Space_Tools.File_Converter
{
    public partial class ConvertGeo : Form
    {
        FileConverter fc;
        FileParser fp = new FileParser();
        string geoPath = "";
        Geo.GeoData geo;
        byte[] geoBytes;

        List<int> sectionList = new List<int>();

        public ConvertGeo(string path, byte[] fileBytes, FileConverter f)
        {
            fc = f;

            InitializeComponent();
            geoPath = path;
            geoBytes = fileBytes;

            unsafe
            {
                fixed (byte* db1 = fileBytes)
                {
                    bool[] inverted = new bool[fileBytes.Length];

                    geo = *(Geo.GeoData*)db1;

                    for (int x = 0; x < geo.tableCount; x++)
                    {
                        int offset = geo.datatable_offset + (x * 0xC0);
                        Geo.GeoHeaderData* sec = (Geo.GeoHeaderData*)(((int)db1) + offset);
                        sectionList.Add(((int)db1) + offset);

                        string meshName = x.ToString("00") + " : " + fp.ReadString(fileBytes, sec->meshNameOffset) + "(vType: " + Geo.GetVertexLength(sec).ToString("X2") + ")";
                        listBox1.Items.Add(meshName);
                    }
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int s = listBox1.SelectedIndex;
            if (s == -1) return;

            unsafe
            {
                fixed (byte* db1 = geoBytes)
                {
                    int offset = geo.datatable_offset + (s * 0xC0);
                    Geo.GeoHeaderData* sec = (Geo.GeoHeaderData*)(((int)db1) + offset);
                    string meshName = fp.ReadString(geoBytes, sec->meshNameOffset);
                    textBox1.Text = meshName;

                    string textureHash = sec->textureHash.ToString("X2").PadLeft(8, '0');
                    textBox2.Text = textureHash;

                    numericUpDown1.Value = (decimal)(sec->faceCount);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int s = listBox1.SelectedIndex;
            if (s == -1) return;

            unsafe
            {
                fixed (byte* db1 = geoBytes)
                {
                    int offset = geo.datatable_offset + (s * 0xC0);
                    Geo.GeoHeaderData* sec = (Geo.GeoHeaderData*)(((int)db1) + offset);

                    if (textBox2.Text != "")
                    {
                        sec->textureHash = Convert.ToInt32(textBox2.Text, 16);
                    }
                    sec->textureHash = sec->textureHash;

                    sec->faceCount = (int)(numericUpDown1.Value);
                }
            }
        }

        private void ConvertGeo_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult r = MessageBox.Show("Do you want to save the current file?", "", MessageBoxButtons.YesNoCancel);

            if(r == DialogResult.Yes)
            {
                Save(null, null);
            }
            else if(r == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        private unsafe byte[] ClearNormals()
        {
            byte[] a = new byte[geoBytes.Length];
            Array.Copy(geoBytes, a, geoBytes.Length);

            fixed (byte* db1 = a)
            {
                for (int x = 0; x < geo.tableCount; x++)
                {
                    int offset = geo.datatable_offset + (x * 0xC0);
                    Geo.GeoHeaderData* sec = (Geo.GeoHeaderData*)(((int)db1) + offset);

                    int vertexSection = sec->vertexOffset;
                    int vertexLength = 0;
                    if (sec->vertexLenSetting == -1) vertexLength = 0x20;
                    else
                    {
                        if (sec->lodType == 4) vertexLength = 0x14;
                        else vertexLength = 0xC;
                    }

                    int db1i = (int)db1;

                    for (int i = 0; i < sec->vertexCount; i++)
                    {
                        int index = vertexSection + (i * vertexLength);
                        int v4i = db1i + index + 4 + 4 + 0x4;

                        if (vertexLength >= 0x10)
                        {
                            *(short*)(v4i) = 0;
                            *(short*)(v4i + 2) = 0;
                        }
                    }
                }
            }

            return a;
        }

        private void Save(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                File.WriteAllBytes(geoPath, ClearNormals());
                return;
            }

            File.WriteAllBytes(geoPath, geoBytes);
        }

        private void SaveAs(object sender, EventArgs e)
        {
            SaveFileDialog s = new SaveFileDialog();
            s.ShowDialog();

            if (s.FileName == "") return;

            geoPath = s.FileName;

            if (checkBox1.Checked)
            {
                File.WriteAllBytes(geoPath, ClearNormals());
                return;
            }
            File.WriteAllBytes(geoPath, geoBytes);
        }

        private void transferTextureHashesFromAnotherGeoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Title = "Select a .geo file";
            o.DefaultExt = "geo";
            o.ShowDialog();

            if (o.FileName == "") return;

            byte[] fileBytes = File.ReadAllBytes(o.FileName);
            string magic = fp.ReadString(fileBytes, 0, 4);
            string[] th = new string[0];

            switch (magic)
            {
                case "MGAE":
                    {
                        th = GetTextureHashes(fileBytes);
                    }
                    break;
                case "EAGM":
                    {
                        byte[] converted = fc.ProcessPS3Geo(fileBytes, o.FileName);
                        th = GetTextureHashes(converted);
                    }
                    break;
            }

            if(th.Length == 0)
            {
                MessageBox.Show("No hashes found.");
                return;
            }
            
            if(th.Length == geo.tableCount) // Same number of hashes (replace each hash)
            {
                for(int x = 0; x < geo.tableCount; x++)
                {

                }
            }
            else if(th.Length < geo.tableCount) // New has less than actual (fill hashes in order and then repeat the last one)
            {

            }
            else if(th.Length > geo.tableCount) // New has more than actual (fill hashes in order)
            {

            }
        }

        public string[] GetTextureHashes(byte[] fb)
        {
            List<string> th = new List<string>();

            unsafe
            {
                fixed (byte* db1 = fb)
                {
                    Geo.GeoData* geo2 = (Geo.GeoData*)db1;
                    int db1i = ((int)db1);

                    for(int x = 0; x < geo2->tableCount; x++)
                    {
                        int offset = geo2->datatable_offset + (x * 0xC0);
                        Geo.GeoHeaderData* sec = (Geo.GeoHeaderData*)(((int)db1) + offset);

                        string hash = sec->textureHash.ToString("X2").PadLeft(8, '0');
                        th.Add(hash);
                    }
                }
            }

            return th.ToArray();
        }
    }
}

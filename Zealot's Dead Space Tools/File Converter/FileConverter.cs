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
    public partial class FileConverter : Form
    {
        public FileConverter()
        {
            InitializeComponent();
        }

        FileParser fp = new FileParser();

        // Geo Converter
        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Title = "Select a .geo file to analyze";
            o.DefaultExt = "geo";
            o.ShowDialog();

            if (o.FileName == "") return;

            ConvertGeo c = new ConvertGeo(o.FileName);
            c.ShowDialog();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Multiselect = true;
            o.ShowDialog();

            if (o.FileNames.Length == 0) return;

            for (int x = 0; x < o.FileNames.Length; x++)
            {
                byte[] fileBytes = File.ReadAllBytes(o.FileNames[x]);
                string magic = fp.ReadString(fileBytes, 0, 4);

                switch (magic)
                {
                    case "MGAE":
                        {
                            ProcessPCGeo(fileBytes, o.FileNames[x]);
                        }
                        break;
                    case "EAGM":
                        {
                            byte[] converted = ProcessPS3Geo(fileBytes, o.FileNames[x]);
                            string fileName = o.FileNames[x];
                            File.WriteAllBytes(fileName.Replace(".geo", ".win.geo"), fileBytes);
                        }
                        break;
                }
            }
        }

        private void meshVolatilePS3ToPCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.ShowDialog();

            if (o.FileName == "") return;
            string fileName = o.FileName;

            byte[] fileBytes = File.ReadAllBytes(o.FileName);

            unsafe
            {
                fixed (byte* db1 = fileBytes)
                {
                    bool[] inverted = new bool[fileBytes.Length];

                    Geo.GeoData* geo = (Geo.GeoData*)db1;
                    int db1i = ((int)db1);

                    int totalCount = fileBytes.Length / 4;
                    for (int x = 0; x < totalCount; x++)
                    {
                        float* a = (float*)(db1i + x * 4);
                        *a = InvertBytes(*a);
                    }
                }
            }

            File.WriteAllBytes(fileName.Replace(".geo", ".win.geo"), fileBytes);
            MessageBox.Show("Written file to: " + fileName.Replace(".geo", ".win.geo"));
        }

        public int InvertBytes(int v)
        {
            int val = v;
            byte[] o = BitConverter.GetBytes(val);
            Array.Reverse(o);
            return BitConverter.ToInt32(o, 0);
        }

        public short InvertBytes(short v)
        {
            short val = v;
            byte[] o = BitConverter.GetBytes(val);
            Array.Reverse(o);
            return BitConverter.ToInt16(o, 0);
        }

        public float InvertBytes(float v)
        {
            float val = v;
            byte[] o = BitConverter.GetBytes(val);
            Array.Reverse(o);
            return BitConverter.ToSingle(o, 0);
        }

        private byte[] ProcessPS3Geo(byte[] fileBytes, string fileName)
        {
            // Convert endianess
            unsafe
            {
                fixed (byte* db1 = fileBytes)
                {
                    bool[] inverted = new bool[fileBytes.Length];

                    Geo.GeoData* geo = (Geo.GeoData*)db1;
                    int db1i = ((int)db1);

                    void ConvertShort(int index)
                    {
                        int shortIndex = index;
                        if (inverted[shortIndex - db1i] == false)
                        {
                            short* val = (short*)(shortIndex);
                            *val = InvertBytes(*val);
                            inverted[shortIndex - db1i] = true;
                        }
                    }

                    void ConvertFloat(int index)
                    {
                        if (inverted[index - db1i] == false)
                        {
                            float* val = (float*)(index);
                            *val = InvertBytes(*val);
                            inverted[index - db1i] = true;
                        }
                    }

                    // Ints
                    geo->magic = InvertBytes(geo->magic);
                    geo->unk_0x04 = InvertBytes(geo->unk_0x04);

                    geo->unk_0x08 = InvertBytes(geo->unk_0x08);
                    if (geo->unk_0x08 != 3) geo->unk_0x08 = 3;

                    geo->unk_0x0C = InvertBytes(geo->unk_0x0C);
                    for (int x = 0; x < 4; x++) geo->data00_0x10[x] = InvertBytes(geo->data00_0x10[x]);
                    geo->meshNameOffset = InvertBytes(geo->meshNameOffset);
                    geo->unk_0x24 = InvertBytes(geo->unk_0x24);
                    geo->unk_0x28 = InvertBytes(geo->unk_0x28);
                    geo->unk_0x2C = InvertBytes(geo->unk_0x2C);
                    geo->unk_0x30 = InvertBytes(geo->unk_0x30);
                    geo->tableCount = InvertBytes(geo->tableCount);

                    // Shorts
                    geo->unk_0x38 = InvertBytes(geo->unk_0x38);
                    geo->unk_0x3A = InvertBytes(geo->unk_0x3A);
                    geo->unk_0x3C = InvertBytes(geo->unk_0x3C);
                    geo->unk_0x3E = InvertBytes(geo->unk_0x3E);
                    geo->unk_0x40 = InvertBytes(geo->unk_0x40);
                    geo->unk_0x42 = InvertBytes(geo->unk_0x42);
                    geo->unk_0x44 = InvertBytes(geo->unk_0x44);
                    geo->unk_0x46 = InvertBytes(geo->unk_0x46);

                    // Int
                    geo->unktbl1_count_0x48 = InvertBytes(geo->unktbl1_count_0x48);
                    geo->unktbl1_offset_0x4C = InvertBytes(geo->unktbl1_offset_0x4C);
                    geo->datatable_offset = InvertBytes(geo->datatable_offset);
                    geo->unktbl2_count_0x54 = InvertBytes(geo->unktbl2_count_0x54);
                    geo->unk_0x58 = InvertBytes(geo->unk_0x58);
                    geo->unk_0x5C = InvertBytes(geo->unk_0x5C);
                    geo->uv_offset = InvertBytes(geo->uv_offset);
                    geo->unk_0x64 = InvertBytes(geo->unk_0x64);
                    geo->unktbl2_offset_0x68 = InvertBytes(geo->unktbl2_offset_0x68);
                    geo->unktbl2_offset_0x6C = InvertBytes(geo->unktbl2_offset_0x6C);
                    geo->unk_0x70 = InvertBytes(geo->unk_0x70);
                    geo->unk_0x74 = InvertBytes(geo->unk_0x74);

                    geo->blankarea1_offset_0x78 = InvertBytes(geo->blankarea1_offset_0x78);
                    geo->blankarea2_offset_0x7C = InvertBytes(geo->blankarea2_offset_0x7C);
                    geo->unk_0x80 = InvertBytes(geo->unk_0x80);
                    geo->unk_0x84 = InvertBytes(geo->unk_0x84);
                    geo->unk_0x88 = InvertBytes(geo->unk_0x88);
                    geo->unk_0x8C = InvertBytes(geo->unk_0x8C);
                    geo->unk_0x90 = InvertBytes(geo->unk_0x90);
                    geo->unk_0x94 = InvertBytes(geo->unk_0x94);
                    geo->unk_0x98 = InvertBytes(geo->unk_0x98);
                    geo->filesize_0x9C = InvertBytes(geo->filesize_0x9C);

                    // Fix sections
                    for (int x = 0; x < geo->tableCount; x++)
                    {
                        int offset = geo->datatable_offset + (x * 0xC0);
                        Geo.GeoHeaderData* sec = (Geo.GeoHeaderData*)(((int)db1) + offset);

                        sec->meshNameOffset = InvertBytes(sec->meshNameOffset);
                        sec->unk_0x8 = InvertBytes(sec->unk_0x8);

                        if (textBox1.Text != "")
                        {
                            sec->textureHash = Convert.ToInt32(textBox1.Text, 16);
                        }
                        sec->textureHash = InvertBytes(sec->textureHash);

                        for (int i = 0; i < 4; i++) sec->fdata_0x10[i] = InvertBytes(sec->fdata_0x10[i]);
                        sec->tableOffset2 = InvertBytes(sec->tableOffset2);
                        sec->unk_0x28 = InvertBytes(sec->unk_0x28);
                        sec->unk_0x2A = InvertBytes(sec->unk_0x2A);
                        sec->unk_0x2C = InvertBytes(sec->unk_0x2C);
                        sec->faceCount = InvertBytes(sec->faceCount);
                        sec->unk_0x34 = InvertBytes(sec->unk_0x34);
                        sec->vertexCount = InvertBytes(sec->vertexCount);
                        sec->unk_0x3A = InvertBytes(sec->unk_0x3A);
                        sec->lodType = InvertBytes(sec->lodType);

                        if (sec->lodType == 5) sec->lodType = 4;
                        else if (sec->lodType == 6) sec->lodType = 5;

                        sec->unk_0x3E = InvertBytes(sec->unk_0x3E);
                        sec->unk_0x40 = InvertBytes(sec->unk_0x40);
                        sec->vertexCountSub1 = InvertBytes(sec->vertexCountSub1);
                        sec->unkoff_0x44 = InvertBytes(sec->unkoff_0x44);

                        for (int i = 0; i < 7; i++) sec->unkdata_0x48[i] = InvertBytes(sec->unkdata_0x48[i]);
                        sec->unk_0x68 = InvertBytes(sec->unk_0x68);
                        sec->unk_0x6C = InvertBytes(sec->unk_0x6C);
                        sec->unk_0x70 = InvertBytes(sec->unk_0x70);
                        sec->unk_0x74 = InvertBytes(sec->unk_0x74);
                        sec->unk_0x78 = InvertBytes(sec->unk_0x78);
                        sec->unk_0x7C = InvertBytes(sec->unk_0x7C);
                        sec->unk_0x80 = InvertBytes(sec->unk_0x80);
                        sec->vertexOffset = InvertBytes(sec->vertexOffset);
                        sec->faceOffset = InvertBytes(sec->faceOffset);
                        for (int i = 0; i < 8; i++) sec->data_0x8C[i] = InvertBytes(sec->data_0x8C[i]);
                        sec->unk_0xAC = InvertBytes(sec->unk_0xAC);

                        sec->unk_0xAE = InvertBytes(sec->unk_0xAE);
                        sec->unk_0xAE--;

                        sec->unk_0xB0 = InvertBytes(sec->unk_0xB0);
                        sec->unk_0xB4 = InvertBytes(sec->unk_0xB4);
                        sec->unk_0xB8 = InvertBytes(sec->unk_0xB8);
                        sec->unk_0xBC = InvertBytes(sec->unk_0xBC);

                        // Fix subsection A
                        int subsectOffset = sec->tableOffset2;
                        if (subsectOffset != 0)
                        {
                            Geo.GeoSubsectionA* ssa = (Geo.GeoSubsectionA*)(((int)db1) + subsectOffset);
                            for (int i = 0; i < 32; i++) ssa->data[i] = InvertBytes(ssa->data[i]);
                        }

                        // Fix subsection B (0xC0 offset)
                        subsectOffset = sec->data_0x8C[0];
                        if (subsectOffset != 0)
                        {
                            Geo.GeoSubsectionA* ssa = (Geo.GeoSubsectionA*)(((int)db1) + subsectOffset);
                            for (int i = 0; i < 12; i++) ssa->data[i] = InvertBytes(ssa->data[i]);
                        }

                        // Fix vertex section
                        int vertexSection = sec->vertexOffset;
                        int vertexLength = 0;
                        if (sec->vertexLenSetting == -1) vertexLength = 0x20;
                        else
                        {
                            if (sec->lodType == 4) vertexLength = 0x14;
                            else vertexLength = 0xC;
                        }

                        for (int i = 0; i < sec->vertexCount; i++)
                        {
                            int index = vertexSection + (i * vertexLength);
                            int v1i = db1i + index;
                            int v2i = v1i + 4;
                            int v3i = v2i + 4;

                            if (inverted[v1i - db1i] == false)
                            {
                                float* vx = (float*)(v1i);
                                *vx = InvertBytes(*vx);
                                inverted[v1i - db1i] = true;
                            }

                            if (inverted[v2i - db1i] == false)
                            {
                                float* vy = (float*)(v2i);
                                *vy = InvertBytes(*vy);
                                inverted[v2i - db1i] = true;
                            }

                            if (inverted[v3i - db1i] == false)
                            {
                                float* vz = (float*)(v3i);
                                *vz = InvertBytes(*vz);
                                inverted[v3i - db1i] = true;
                            }

                            if (vertexLength >= 0x20)
                            {
                                for (int j = 0; j < 4; j++)
                                {
                                    /*int shortIndex = (db1i + index + 0xC + (j * 2));

                                    if (inverted[shortIndex - db1i] == false)
                                    {
                                        short* val = (short*)(shortIndex);
                                        *val = InvertBytes(*val);
                                        inverted[shortIndex - db1i] = true;
                                    }*/

                                    int shortIndex = (db1i + index + 0x18 + (j * 2));

                                    if (inverted[shortIndex - db1i] == false)
                                    {
                                        short* val = (short*)(shortIndex);
                                        *val = InvertBytes(*val);
                                        inverted[shortIndex - db1i] = true;
                                    }
                                }
                            }

                            if (vertexLength >= 0x10)
                            {
                                int v4i = v3i + 0x4;
                                ConvertShort(v4i);
                                ConvertShort(v4i + 2);
                                ConvertShort(v4i + 4);
                                ConvertShort(v4i + 6);
                                /*short* val = (short*)(v4i);
                                *val = 0;
                                val = (short*)(v4i + 2);
                                *val = 0;*/
                                //ConvertFloat(v4i);
                            }

                            /*if (vertexLength >= 0x14)
                            {
                                int v4i = v3i + 0x8;

                                if (inverted[v4i - db1i] == false)
                                {
                                    float* vna = (float*)(v4i);
                                    *vna = InvertBytes(*vna);
                                    //*vna = 0;
                                    inverted[v4i - db1i] = true;
                                }
                            }

                            if (vertexLength >= 0x10)
                            {
                                int v4i = v3i + 0x4;

                                if (inverted[v4i - db1i] == false)
                                {
                                    float* vna = (float*)(v4i);
                                    *vna = InvertBytes(*vna);
                                    //*vna = 0;
                                    inverted[v4i - db1i] = true;
                                }
                            }*/
                        }

                        // Fix triangle section
                        int triangleSection = sec->faceOffset;
                        int triangleCount = sec->faceCount / 3;

                        for (int i = 0; i < triangleCount; i++)
                        {
                            int triIndex = i * 0x6;
                            short* a = (short*)(db1i + triangleSection + triIndex);
                            short* b = (short*)(db1i + triangleSection + triIndex + 2);
                            short* c = (short*)(db1i + triangleSection + triIndex + 4);

                            if (inverted[triangleSection + triIndex] == false)
                            {
                                *a = InvertBytes(*a);
                                inverted[triangleSection + triIndex] = true;
                            }

                            if (inverted[triangleSection + triIndex + 4] == false)
                            {
                                *b = InvertBytes(*b);
                                inverted[triangleSection + triIndex + 4] = true;
                            }

                            if (inverted[triangleSection + triIndex + 8] == false)
                            {
                                *c = InvertBytes(*c);
                                inverted[triangleSection + triIndex + 8] = true;
                            }
                        }
                    }

                    // Fix C0 data
                    for (int x = 0; x < 8; x++)
                    {
                        float* a = (float*)(db1i + 0xC0 + (x * 4));
                        *a = InvertBytes(*a);
                    }

                    // Fix top header A
                    short topHeaderACount = geo->unk_0x3C;
                    int topHeaderAOffset = geo->unktbl2_offset_0x68;
                    for (int x = 0; x < topHeaderACount; x++)
                    {
                        int index = topHeaderAOffset + (x * 0x10);
                        int* a = (int*)(db1i + index);
                        int* b = (int*)(db1i + index + 4);
                        short* ca = (short*)(db1i + index + 8);
                        int* da = (int*)(db1i + index + 0xC);

                        *a = InvertBytes(*a);
                        *b = InvertBytes(*b);
                        *ca = InvertBytes(*ca);
                        *da = InvertBytes(*da);
                    }

                    // Fix top header B
                    short topHeaderBCount = geo->unk_0x3E;
                    int topHeaderBOffset = geo->unktbl2_offset_0x6C;
                    for (int x = 0; x < topHeaderBCount; x++)
                    {
                        int index = topHeaderBOffset + (x * 0x10);
                        int* a = (int*)(db1i + index);
                        int* b = (int*)(db1i + index + 4);
                        short* ca = (short*)(db1i + index + 8);
                        int* da = (int*)(db1i + index + 0xC);

                        *a = InvertBytes(*a);
                        *b = InvertBytes(*b);
                        *ca = InvertBytes(*ca);
                        *da = InvertBytes(*da);
                    }

                    // Fix bottom header A 
                    int bottomHeaderCount = geo->unk_0x38;
                    int bottomHeaderIndex = geo->uv_offset;
                    for (int x = 0; x < bottomHeaderCount; x++)
                    {
                        int index = bottomHeaderIndex + (x * 8);
                        int* a = (int*)(db1i + index);
                        int* b = (int*)(db1i + index + 4);

                        *a = InvertBytes(*a);
                        *b = InvertBytes(*b);
                    }

                    // Fix bottom header B
                    int bottomHeaderBCount = geo->unktbl1_count_0x48;
                    int bottomHeaderBIndex = geo->unktbl1_offset_0x4C;
                    for (int x = 0; x < bottomHeaderBCount; x++)
                    {
                        int index = bottomHeaderBIndex + (x * 8);
                        int* a = (int*)(db1i + index);
                        int* b = (int*)(db1i + index + 4);

                        *a = InvertBytes(*a);
                        *b = InvertBytes(*b);
                    }

                    return fileBytes;
                    //File.WriteAllBytes(fileName.Replace(".geo", ".win.geo"), fileBytes);
                    //MessageBox.Show("Written file to: " + fileName.Replace(".geo", ".win.geo"));
                }
            }
        }

        private void ProcessPCGeo(byte[] fileBytes, string fileName)
        {
            MessageBox.Show("Target file is already converted.");
            return;

            unsafe
            {
                fixed (byte* db1 = fileBytes)
                {
                    Geo.GeoData* ptest = (Geo.GeoData*)db1;

                    if (ptest->magic == 1296515397)
                    {
                        MessageBox.Show("Not a valid file");
                        return;
                    }
                }
            }
        }

        private void tToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.ShowDialog();

            if (o.FileName == "") return;
            string fileName = o.FileName;

            byte[] fileBytes = File.ReadAllBytes(o.FileName);

            unsafe
            {
                fixed (byte* db1 = fileBytes)
                {
                    int db1i = ((int)db1);

                    int totalCount = fileBytes.Length / 2;
                    for (int x = 0; x < totalCount; x++)
                    {
                        short* a = (short*)(db1i + x * 2);
                        *a = InvertBytes(*a);
                    }
                }
            }

            File.WriteAllBytes(fileName.Replace(".geo", ".win.geo"), fileBytes);
            MessageBox.Show("Written file to: " + fileName.Replace(".geo", ".win.geo"));
        }

        public static float toFloat(int hbits)
        {
            int mant = hbits & 0x03ff;            // 10 bits mantissa
            int exp = hbits & 0x7c00;            // 5 bits exponent
            if (exp == 0x7c00)                   // NaN/Inf
                exp = 0x3fc00;                    // -> NaN/Inf
            else if (exp != 0)                   // normalized value
            {
                exp += 0x1c000;                   // exp - 15 + 127
                if (mant == 0 && exp > 0x1c400)  // smooth transition
                    return BitConverter.ToSingle(BitConverter.GetBytes((hbits & 0x8000) << 16
                                                    | exp << 13 | 0x3ff), 0);
            }
            else if (mant != 0)                  // && exp==0 -> subnormal
            {
                exp = 0x1c400;                    // make it normal
                do
                {
                    mant <<= 1;                   // mantissa * 2
                    exp -= 0x400;                 // decrease exp by 1
                } while ((mant & 0x400) == 0); // while not normal
                mant &= 0x3ff;                    // discard subnormal bit
            }                                     // else +/-0 -> +/-0
            return BitConverter.ToSingle(BitConverter.GetBytes(          // combine all parts
                (hbits & 0x8000) << 16          // sign  << ( 31 - 15 )
                | (exp | mant) << 13), 0);         // value << ( 23 - 10 )
        }

        /*private void button1_Click(object sender, EventArgs e)
        {
            float f = toFloat((int)(numericUpDown1.Value));
            label1.Text = f.ToString();
        }*/
    }
}

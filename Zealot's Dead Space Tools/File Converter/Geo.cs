using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Zealot_s_Dead_Space_Tools.File_Converter
{
    public unsafe class Geo
    {
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public unsafe struct GeoData
        {
            public int magic; // 0x0
            public int unk_0x04; // 0x4
            public int unk_0x08; // 0x8
            public int unk_0x0C; // 0xC
            public fixed int data00_0x10[4]; // 0x10
            public int meshNameOffset; // 0x20
            public int unk_0x24; // 0x24
            public int unk_0x28; // 0x28
            public int unk_0x2C; // 0x2C
            public int unk_0x30; // 0x30
            public int tableCount; // 0x34
            public short unk_0x38; // 0x38
            public short unk_0x3A; // 0x3A
            public short unk_0x3C; // 0x3C
            public short unk_0x3E; // 0x3E
            public short unk_0x40; // 0x40
            public short unk_0x42; // 0x42
            public short unk_0x44; // 0x44
            public short unk_0x46; // 0x46
            public int unktbl1_count_0x48; // 0x48
            public int unktbl1_offset_0x4C; // 0x4C
            public int datatable_offset; // 0x50
            public int unktbl2_count_0x54; // 0x54
            public int unk_0x58; // 0x58
            public int unk_0x5C; // 0x5C
            public int uv_offset; // 0x60
            public int unk_0x64; // 0x64
            public int unktbl2_offset_0x68; // 0x68
            public int unktbl2_offset_0x6C; // 0x6C
            public int unk_0x70; // 0x70
            public int unk_0x74; // 0x74
            public int blankarea1_offset_0x78; // 0x78
            public int blankarea2_offset_0x7C; // 0x7C
            public int unk_0x80; // 0x80
            public int unk_0x84; // 0x84
            public int unk_0x88; // 0x88
            public int unk_0x8C; // 0x8C
            public int unk_0x90; // 0x90
            public int unk_0x94; // 0x94
            public int unk_0x98; // 0x98
            public int filesize_0x9C; // 0x9C
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public unsafe struct GeoHeaderData
        {
            public int meshNameOffset; // 0x0
            public fixed byte flags_0x4[4]; // 0x4;
            public int unk_0x8; // 0x8
            public int textureHash; // 0xC
            public fixed float fdata_0x10[4]; // 0x10
            public int tableOffset2; // 0x20
            public fixed byte flags_0x24[3]; // 0x24
            public byte uvType; // 0x27
            public short unk_0x28; // 0x28
            public short unk_0x2A; // 0x2A
            public int unk_0x2C; // 0x2C
            public int faceCount; // 0x30
            public int unk_0x34; // 0x34
            public short vertexCount; // 0x38
            public short unk_0x3A; // 0x3A
            public short lodType; // 0x3C
            public short unk_0x3E; // 0x3E
            public short unk_0x40; // 0x40
            public short vertexCountSub1; // 0x42
            public int unkoff_0x44; // 0x44
            public fixed int unkdata_0x48[7]; // 0x48
            public int vertexLenSetting; // 0x64
            public int unk_0x68; // 0x68
            public int unk_0x6C; // 0x6C
            public int unk_0x70; // 0x70
            public int unk_0x74; // 0x74
            public int unk_0x78; // 0x78
            public int unk_0x7C; // 0x7C
            public int unk_0x80; // 0x80
            public int vertexOffset; // 0x84
            public int faceOffset; // 0x88
            public fixed int data_0x8C[8]; // 0x8C
            public short unk_0xAC; // 0xAC
            public short unk_0xAE; // 0xAE
            public float unk_0xB0; // 0xB0
            public float unk_0xB4; // 0xB4
            public float unk_0xB8; // 0xB8
            public float unk_0xBC; // 0xBC
        }

        public static int GetVertexLength(GeoHeaderData* sec)
        {
            int vertexLength = 0;
            if (sec->vertexLenSetting == -1) vertexLength = 0x20;
            else
            {
                if (sec->lodType == 4) vertexLength = 0x14;
                else vertexLength = 0xC;
            }

            return vertexLength;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public unsafe struct GeoSubsectionA
        {
            public fixed float data[32]; // 0x0
        }

        /*public static void Main(string[] args)
        {
            //managed byte array
            byte[] DB1 = new byte[7]; //7B more than we need. byte buffer usually is greater.
            DB1[0] = 2;//test data |> LITTLE ENDIAN
            DB1[1] = 0;//test data |
            DB1[2] = 3;//test data
            DB1[3] = 4;//test data

            unsafe
            {
                fixed (byte* db1 = DB1)
                {
                    GeoData* ptest = (GeoData*)db1;   //does CHANGE DB1/db1
                    MessageBox.Show(ptest->unk00.ToString("X2"));
                }
            }
        }*/
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Timers;

namespace KeySAV2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            FileSystemWatcher fsw = new FileSystemWatcher();
            fsw.SynchronizingObject = this; // Timer Threading Related fix to cross-access control.
            InitializeComponent();
            myTimer.Elapsed += new ElapsedEventHandler(DisplayTimeEvent);
            this.tab_Main.AllowDrop = true;
            this.DragEnter += new DragEventHandler(tabMain_DragEnter);
            this.DragDrop += new DragEventHandler(tabMain_DragDrop);
            tab_Main.DragEnter += new DragEventHandler(tabMain_DragEnter);
            tab_Main.DragDrop += new DragEventHandler(tabMain_DragDrop);

            myTimer.Interval = 400; // milliseconds per trigger interval (0.4s)
            myTimer.Start();
            CB_Game.SelectedIndex = 0;
            CB_MainLanguage.SelectedIndex = 0;
            CB_BoxStart.SelectedIndex = 1;
            changeboxsetting(null, null);
            CB_Team.SelectedIndex = 0;
            CB_ExportStyle.SelectedIndex = 0;
            CB_BoxColor.SelectedIndex = 0;
            loadINI();
            this.FormClosing += onFormClose;
            InitializeStrings();
        }
        
        // Drag & Drop Events // 
        private void tabMain_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }
        private void tabMain_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            string path = files[0]; // open first D&D
            long len = new FileInfo(files[0]).Length;
            if (len == 0x100000 || len == 0x10009C)
            {
                tab_Main.SelectedIndex = 1;
                openSAV(path);
            }
            else if (len == 28256)
            {
                tab_Main.SelectedIndex = 0;
                openVID(path);
            }
            else MessageBox.Show("Dropped file is not supported.", "Error");
        }
        public void DisplayTimeEvent(object source, ElapsedEventArgs e)
        {
            find3DS();
        }
        #region Global Variables
        // Finding the 3DS SD Files
        public bool pathfound = false;
        public System.Timers.Timer myTimer = new System.Timers.Timer();
        public static string path_exe = System.Windows.Forms.Application.StartupPath;
        public static string datapath = path_exe + "\\data";
        public static string dbpath = path_exe + "\\db";
        public static string bakpath = path_exe + "\\backup";
        public string path_3DS = "";
        public string path_POW = "";

        // Language
        public string[] natures;
        public string[] types;
        public string[] abilitylist;
        public string[] movelist;
        public string[] itemlist;
        public string[] specieslist;
        public string[] balls;
        public string[] formlist;
        public string[] vivlist;

        // Blank File Egg Names
        public string[] eggnames = { "タマゴ", "Egg", "Œuf", "Uovo", "Ei", "", "Huevo", "알" };

        // Inputs
        public byte[] savefile = new Byte[0x10009C];
        public byte[] savkey = new Byte[0x80000];
        public byte[] batvideo = new Byte[0x100000]; // whatever
        
        private byte[] zerobox = new Byte[232 * 30];

        // Dumping Usage
        public string vidpath = "";
        public string savpath = "";
        public string savkeypath = "";
        public string vidkeypath = "";
        public string custom1 = ""; public string custom2 = ""; public string custom3 = "";
        public bool custom1b = false; public bool custom2b = false; public bool custom3b = false;
        public string[] boxcolors = new string[] { "", "###", "####", "#####", "######" };
        private string csvdata = "";
        private string csvheader = "";
        public int dumpedcounter = 0;
        private int slots = 0;
        public bool ghost = false;

        // Breaking Usage
        public string file1 = "";
        public string file2 = "";
        public byte[] break1 = new Byte[0x10009C];
        public byte[] break2 = new Byte[0x10009C];
        public byte[] video1 = new Byte[28256];
        public byte[] video2 = new Byte[28256];

        #endregion

        // Utility
        private void onFormClose(object sender, FormClosingEventArgs e)
        {
            // Save the ini file
            saveINI();
        }
        private void loadINI()
        {
            try
            {
                // Detect startup path and data path.
                if (!Directory.Exists(datapath)) // Create data path if it doesn't exist.
                    Directory.CreateDirectory(datapath);
                if (!Directory.Exists(dbpath)) // Create db path if it doesn't exist.
                    Directory.CreateDirectory(dbpath);
                if (!Directory.Exists(bakpath)) // Create backup path if it doesn't exist.
                    Directory.CreateDirectory(bakpath);
            
                // Load .ini data.
                if (!File.Exists(datapath + "\\config.ini"))
                    File.Create(datapath + "\\config.ini");
                else
                {
                    TextReader tr = new StreamReader(datapath + "\\config.ini");
                    try
                    {
                        // Load the data
                        tab_Main.SelectedIndex = Convert.ToInt16(tr.ReadLine());
                        custom1 = tr.ReadLine();
                        custom2 = tr.ReadLine();
                        custom3 = tr.ReadLine();
                        custom1b = Convert.ToBoolean(Convert.ToInt16(tr.ReadLine()));
                        custom2b = Convert.ToBoolean(Convert.ToInt16(tr.ReadLine()));
                        custom3b = Convert.ToBoolean(Convert.ToInt16(tr.ReadLine()));
                        CB_ExportStyle.SelectedIndex = Convert.ToInt16(tr.ReadLine());
                        CB_MainLanguage.SelectedIndex = Convert.ToInt16(tr.ReadLine());
                        CB_Game.SelectedIndex = Convert.ToInt16(tr.ReadLine());
                        CHK_MarkFirst.Checked = Convert.ToBoolean(Convert.ToInt16(tr.ReadLine()));
                        CHK_Split.Checked = Convert.ToBoolean(Convert.ToInt16(tr.ReadLine()));
                        CHK_BoldIVs.Checked = Convert.ToBoolean(Convert.ToInt16(tr.ReadLine()));
                        CB_BoxColor.SelectedIndex = Convert.ToInt16(tr.ReadLine());
                        CHK_ColorBox.Checked = Convert.ToBoolean(Convert.ToInt16(tr.ReadLine()));
                        CHK_HideFirst.Checked = Convert.ToBoolean(Convert.ToInt16(tr.ReadLine()));
                        this.Height = Convert.ToInt16(tr.ReadLine());
                        this.Width = Convert.ToInt16(tr.ReadLine());
                        tr.Close();
                    }
                    catch
                    {
                        tr.Close();
                    }
                }
            }
            catch (Exception e) { MessageBox.Show("Ini config file loading failed.\n\n" + e, "Error"); }
        }
        private void saveINI()
        {
            try
            {
                // Detect startup path and data path.
                if (!Directory.Exists(datapath)) // Create data path if it doesn't exist.
                    Directory.CreateDirectory(datapath);
            
                // Load .ini data.
                if (!File.Exists(datapath + "\\config.ini"))
                    File.Create(datapath + "\\config.ini");
                else
                {
                    TextWriter tr = new StreamWriter(datapath + "\\config.ini");
                    try
                    {
                        // Load the data
                        tr.WriteLine(tab_Main.SelectedIndex.ToString());
                        tr.WriteLine(custom1.ToString());
                        tr.WriteLine(custom2.ToString());
                        tr.WriteLine(custom3.ToString());
                        tr.WriteLine(Convert.ToInt16(custom1b).ToString());
                        tr.WriteLine(Convert.ToInt16(custom2b).ToString());
                        tr.WriteLine(Convert.ToInt16(custom3b).ToString());
                        tr.WriteLine(CB_ExportStyle.SelectedIndex.ToString());
                        tr.WriteLine(CB_MainLanguage.SelectedIndex.ToString());
                        tr.WriteLine(CB_Game.SelectedIndex.ToString());
                        tr.WriteLine(Convert.ToInt16(CHK_MarkFirst.Checked).ToString());
                        tr.WriteLine(Convert.ToInt16(CHK_Split.Checked).ToString());
                        tr.WriteLine(Convert.ToInt16(CHK_BoldIVs.Checked).ToString());
                        tr.WriteLine(CB_BoxColor.SelectedIndex.ToString());
                        tr.WriteLine(Convert.ToInt16(CHK_ColorBox.Checked).ToString());
                        tr.WriteLine(Convert.ToInt16(CHK_HideFirst.Checked).ToString());
                        tr.WriteLine(this.Height.ToString());
                        tr.WriteLine(this.Width.ToString());
                        tr.Close();
                    }
                    catch
                    {
                        tr.Close();
                    }
                }
            }
            catch (Exception e) { MessageBox.Show("Ini config file saving failed.\n\n" + e, "Error"); }
        }
        public volatile int game;

        // RNG
        private static uint LCRNG(uint seed)
        {
            return (seed * 0x41C64E6D + 0x00006073) & 0xFFFFFFFF;
        }
        private static Random rand = new Random();
        private static uint rnd32()
        {
            return (uint)(rand.Next(1 << 30)) << 2 | (uint)(rand.Next(1 << 2));
        }

        // PKX Struct Manipulation
        private byte[] shuffleArray(byte[] pkx, uint sv)
        {
            byte[] ekx = new Byte[260]; Array.Copy(pkx, ekx, 8);

            // Now to shuffle the blocks

            // Define Shuffle Order Structure
            var aloc = new byte[] { 0, 0, 0, 0, 0, 0, 1, 1, 2, 3, 2, 3, 1, 1, 2, 3, 2, 3, 1, 1, 2, 3, 2, 3 };
            var bloc = new byte[] { 1, 1, 2, 3, 2, 3, 0, 0, 0, 0, 0, 0, 2, 3, 1, 1, 3, 2, 2, 3, 1, 1, 3, 2 };
            var cloc = new byte[] { 2, 3, 1, 1, 3, 2, 2, 3, 1, 1, 3, 2, 0, 0, 0, 0, 0, 0, 3, 2, 3, 2, 1, 1 };
            var dloc = new byte[] { 3, 2, 3, 2, 1, 1, 3, 2, 3, 2, 1, 1, 3, 2, 3, 2, 1, 1, 0, 0, 0, 0, 0, 0 };

            // Get Shuffle Order
            var shlog = new byte[] { aloc[sv], bloc[sv], cloc[sv], dloc[sv] };

            // UnShuffle Away!
            for (int b = 0; b < 4; b++)
                Array.Copy(pkx, 8 + 56 * shlog[b], ekx, 8 + 56 * b, 56);

            // Fill the Battle Stats back
            if (pkx.Length > 232)
                Array.Copy(pkx, 232, ekx, 232, 28);
            return ekx;
        }
        private byte[] decryptArray(byte[] ekx)
        {
            byte[] pkx = new Byte[0xE8]; Array.Copy(ekx, pkx, 0xE8);
            uint pv = BitConverter.ToUInt32(pkx, 0);
            uint sv = (((pv & 0x3E000) >> 0xD) % 24);

            uint seed = pv;

            // Decrypt Blocks with RNG Seed
            for (int i = 8; i < 232; i += 2)
            {
                int pre = pkx[i] + ((pkx[i + 1]) << 8);
                seed = LCRNG(seed);
                int seedxor = (int)((seed) >> 16);
                int post = (pre ^ seedxor);
                pkx[i] = (byte)((post) & 0xFF);
                pkx[i + 1] = (byte)(((post) >> 8) & 0xFF);
            }

            // Deshuffle
            pkx = shuffleArray(pkx, sv);
            return pkx;
        }
        private byte[] encryptArray(byte[] pkx)
        {
            // Shuffle
            uint pv = BitConverter.ToUInt32(pkx, 0);
            uint sv = (((pv & 0x3E000) >> 0xD) % 24);

            byte[] ekxdata = new Byte[pkx.Length]; Array.Copy(pkx, ekxdata, pkx.Length);

            // If I unshuffle 11 times, the 12th (decryption) will always decrypt to ABCD.
            // 2 x 3 x 4 = 12 (possible unshuffle loops -> total iterations)
            for (int i = 0; i < 11; i++)
                ekxdata = shuffleArray(ekxdata, sv);

            uint seed = pv;
            // Encrypt Blocks with RNG Seed
            for (int i = 8; i < 232; i += 2)
            {
                int pre = ekxdata[i] + ((ekxdata[i + 1]) << 8);
                seed = LCRNG(seed);
                int seedxor = (int)((seed) >> 16);
                int post = (pre ^ seedxor);
                ekxdata[i] = (byte)((post) & 0xFF);
                ekxdata[i + 1] = (byte)(((post) >> 8) & 0xFF);
            }

            // Encrypt the Party Stats
            seed = pv;
            for (int i = 232; i < 260; i += 2)
            {
                int pre = ekxdata[i] + ((ekxdata[i + 1]) << 8);
                seed = LCRNG(seed);
                int seedxor = (int)((seed) >> 16);
                int post = (pre ^ seedxor);
                ekxdata[i] = (byte)((post) & 0xFF);
                ekxdata[i + 1] = (byte)(((post) >> 8) & 0xFF);
            }

            // Done
            return ekxdata;
        }
        private int getDloc(uint ec)
        {
            // Define Shuffle Order Structure
            var dloc = new byte[] { 3, 2, 3, 2, 1, 1, 3, 2, 3, 2, 1, 1, 3, 2, 3, 2, 1, 1, 0, 0, 0, 0, 0, 0 };
            uint sv = (((ec & 0x3E000) >> 0xD) % 24);

            return dloc[sv];
        }
        private bool verifyCHK(byte[] pkx)
        {
            ushort chk = 0;
            for (int i = 8; i < 232; i += 2) // Loop through the entire PKX
                chk += BitConverter.ToUInt16(pkx, i);

            ushort actualsum = BitConverter.ToUInt16(pkx, 0x6);
            if ((BitConverter.ToUInt16(pkx, 0x8) > 750) || (BitConverter.ToUInt16(pkx, 0x90) != 0)) 
                return false;
            return (chk == actualsum);
        }

        // File Type Loading
        private void B_OpenSAV_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.InitialDirectory = savpath;
            ofd.RestoreDirectory = true;
            ofd.Filter = "SAV|*.sav;*.bin";
            if (ofd.ShowDialog() == DialogResult.OK)
                openSAV(ofd.FileName);
        }
        private void B_OpenVid_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.InitialDirectory = vidpath;
            ofd.RestoreDirectory = true;
            ofd.Filter = "Battle Video|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
                openVID(ofd.FileName);
        }
        private void openSAV(string path)
        {
            // check to see if good input file
            long len = new FileInfo(path).Length;
            if (len != 0x100000 && len != 0x10009C)
            { MessageBox.Show("Incorrect File Size"); return; }
            
            TB_SAV.Text = path;

            // Go ahead and load the save file into RAM...
            byte[] input = File.ReadAllBytes(path);
            Array.Copy(input, input.Length % 0x100000, savefile, 0, 0x100000);
            // Fetch Stamp
            ulong stamp = BitConverter.ToUInt64(savefile, 0x10);
            string keyfile = fetchKey(stamp, 0x80000);
            if (keyfile == "")
            {
                L_KeySAV.Text = "Key not found. Please break for this SAV first.";
                B_GoSAV.Enabled = false;
                return;
            }
            else
            {
                B_GoSAV.Enabled = true;
                L_KeySAV.Text = new FileInfo(keyfile).Name;
                savkeypath = keyfile;
            }

            B_GoSAV.Enabled = CB_BoxEnd.Enabled = CB_BoxStart.Enabled = B_BKP_SAV.Visible = !(keyfile == "");
            byte[] key = File.ReadAllBytes(keyfile);
            byte[] empty = new Byte[232];
            // Save file is already loaded.

            // Get our empty file set up.
            Array.Copy(key, 0x10, empty, 0xE0, 0x4);
            string nick = eggnames[empty[0xE3] - 1];
            // Stuff in the nickname to our blank EKX.
            byte[] nicknamebytes = Encoding.Unicode.GetBytes(nick);
            Array.Resize(ref nicknamebytes, 24);
            Array.Copy(nicknamebytes, 0, empty, 0x40, nicknamebytes.Length);
            // Fix CHK
            uint chk = 0;
            for (int i = 8; i < 232; i += 2) // Loop through the entire PKX
                chk += BitConverter.ToUInt16(empty, i);

            // Apply New Checksum
            Array.Copy(BitConverter.GetBytes(chk), 0, empty, 06, 2);
            empty = encryptArray(empty);
            Array.Resize(ref empty, 0xE8);
            scanSAV(savefile, key, empty);
            File.WriteAllBytes(keyfile, key); // Key has been scanned for new slots, re-save key.
        }
        private void openVID(string path)
        {
            // check to see if good input file
            B_GoBV.Enabled = CB_Team.Enabled = false;
            long len = new FileInfo(path).Length;
            if (len != 28256)
            { MessageBox.Show("Incorrect File Size"); return; }

            TB_BV.Text = path;

            // Go ahead and load the save file into RAM...
            batvideo = File.ReadAllBytes(path);
            // Fetch Stamp
            ulong stamp = BitConverter.ToUInt64(batvideo, 0x10);
            string keyfile = fetchKey(stamp, 0x1000);
            B_GoBV.Enabled = CB_Team.Enabled = B_BKP_BV.Visible = (keyfile != "");
            if (keyfile == "")
            { L_KeyBV.Text = "Key not found. Please break for this BV first."; return; }
            else
            {
                string name = new FileInfo(keyfile).Name;
                L_KeyBV.Text = "Key: " + name;
                vidkeypath = keyfile;
            }
            // Check up on the key file...
            CB_Team.Items.Clear();
            CB_Team.Items.Add("My Team");
            byte[] bvkey = File.ReadAllBytes(vidkeypath);
            if (BitConverter.ToUInt64(bvkey, 0x800) != 0)
                CB_Team.Items.Add("Enemy Team");

            CB_Team.SelectedIndex = 0;
        }
        private string fetchKey(ulong stamp, int length)
        {
            // Find the Key in the datapath (program//data folder)
            string[] files = Directory.GetFiles(datapath,"*.bin", SearchOption.AllDirectories);
            byte[] data = new Byte[length];
            for (int i = 0; i < files.Length; i++)
            {
                FileInfo fi = new FileInfo(files[i]);
                {
                    if (fi.Length == length)
                    {
                        data = File.ReadAllBytes(files[i]);
                        ulong newstamp = BitConverter.ToUInt64(data, 0x0);
                        if (newstamp == stamp)
                            return files[i];
                    }
                }
            }
            // else return nothing
            return "";
        }

        // File Dumping
        // SAV
        private byte[] fetchpkx(byte[] input, byte[] keystream, int pkxoffset, int key1off, int key2off, byte[] blank)
        {
            // Auto updates the keystream when it dumps important data!
            ghost = true;
            byte[] ekx = new Byte[232];
            byte[] key1 = new Byte[232]; Array.Copy(keystream, key1off, key1, 0, 232);
            byte[] key2 = new Byte[232]; Array.Copy(keystream, key2off, key2, 0, 232);
            byte[] encrypteddata = new Byte[232]; Array.Copy(input, pkxoffset, encrypteddata, 0, 232);

            byte[] zeros = new Byte[232];
            byte[] ezeros = encryptArray(zeros); Array.Resize(ref ezeros, 0xE8);
            if (zeros.SequenceEqual(key1) && zeros.SequenceEqual(key2))
                return null;
            else if (zeros.SequenceEqual(key1))
            {
                // Key2 is confirmed to dump the data.
                ekx = xortwos(key2, encrypteddata);
                ghost = false;
            }
            else if (zeros.SequenceEqual(key2))
            {
                // Haven't dumped from this slot yet.
                if (key1.SequenceEqual(encrypteddata))
                {
                    // Slot hasn't changed.
                    return null;
                }
                else
                {
                    // Try and decrypt the data...
                    ekx = xortwos(key1, encrypteddata);
                    if (verifyCHK(decryptArray(ekx)))
                    {
                        // Data has been dumped!
                        // Fill keystream data with our log.
                        Array.Copy(encrypteddata, 0, keystream, key2off, 232);
                    }
                    else
                    {
                        // Try xoring with the empty data.
                        if (verifyCHK(decryptArray(xortwos(ekx, blank))))
                        {
                            ekx = xortwos(ekx, blank);
                            Array.Copy(xortwos(encrypteddata, blank), 0, keystream, key2off, 232);
                        }
                        else if (verifyCHK(decryptArray(xortwos(ekx, ezeros))))
                        {
                            ekx = xortwos(ekx, ezeros);
                            Array.Copy(xortwos(encrypteddata, ezeros), 0, keystream, key2off, 232);
                        }
                        else return null; // Not a failed decryption; we just haven't seen new data here yet.
                    }
                }
            }
            else
            {
                // We've dumped data at least once.
                if (key1.SequenceEqual(encrypteddata) || key1.SequenceEqual(xortwos(encrypteddata,blank)) || key1.SequenceEqual(xortwos(encrypteddata,ezeros)))
                {
                    // Data is back to break state, but we can still dump with the other key.
                    ekx = xortwos(key2, encrypteddata);
                    if (!verifyCHK(decryptArray(ekx)))
                    {
                        if (verifyCHK(decryptArray(xortwos(ekx, blank))))
                        {
                            ekx = xortwos(ekx, blank);
                            Array.Copy(xortwos(key2, blank), 0, keystream, key2off, 232);
                        }
                        else if (verifyCHK(decryptArray(xortwos(ekx, ezeros))))
                        {
                            // Key1 decrypts our data after we remove encrypted zeros.
                            // Copy Key1 to Key2, then zero out Key1.
                            ekx = xortwos(ekx, ezeros);
                            Array.Copy(xortwos(key2, ezeros), 0, keystream, key2off, 232);
                        }
                        else return null; // Decryption Error
                    }
                }
                else if (key2.SequenceEqual(encrypteddata) || key2.SequenceEqual(xortwos(encrypteddata, blank)) || key2.SequenceEqual(xortwos(encrypteddata, ezeros)))
                {
                    // Data is changed only once to a dumpable, but we can still dump with the other key.
                    ekx = xortwos(key1, encrypteddata); 
                    if (!verifyCHK(decryptArray(ekx)))
                    {
                        if (verifyCHK(decryptArray(xortwos(ekx, blank))))
                        {
                            ekx = xortwos(ekx, blank);
                            Array.Copy(xortwos(key1, blank), 0, keystream, key1off, 232);
                        }
                        else if (verifyCHK(decryptArray(xortwos(ekx, ezeros))))
                        {
                            ekx = xortwos(ekx, ezeros);
                            Array.Copy(xortwos(key1, ezeros), 0, keystream, key1off, 232);
                        }
                        else return null; // Decryption Error
                    }
                }
                else
                {
                    // Data has been observed to change twice! We can get our exact keystream now!
                    // Either Key1 or Key2 or Save is empty. Whichever one decrypts properly is the empty data.
                    // Oh boy... here we go...
                    ghost = false;
                    bool keydata1, keydata2 = false;
                    byte[] data1 = xortwos(encrypteddata, key1);
                    byte[] data2 = xortwos(encrypteddata, key2);

                    keydata1 = 
                        (verifyCHK(decryptArray(data1))
                        ||
                        verifyCHK(decryptArray(xortwos(data1, ezeros)))
                        ||
                        verifyCHK(decryptArray(xortwos(data1, blank)))
                        );
                    keydata2 = 
                        (verifyCHK(decryptArray(data2))
                        ||
                        verifyCHK(decryptArray(xortwos(data2, ezeros)))
                        ||
                        verifyCHK(decryptArray(xortwos(data2, blank)))
                        );
                    if (!keydata1 && !keydata2) 
                        return null; // All 3 are occupied.
                    if (keydata1 && keydata2)
                    {
                        // Save file is currently empty...
                        // Copy key data from save file if it decrypts with Key1 data properly.

                        if (verifyCHK(decryptArray(data1)))
                        {
                            // No modifications necessary.
                            ekx = data1;
                            Array.Copy(encrypteddata, 0, keystream, key2off, 232);
                            Array.Copy(zeros, 0, keystream, key1off, 232);
                        }
                        else if (verifyCHK(decryptArray(xortwos(data1, ezeros))))
                        {
                            ekx = ezeros;
                            Array.Copy(xortwos(encrypteddata,ezeros), 0, keystream, key2off, 232);
                            Array.Copy(zeros, 0, keystream, key1off, 232);
                        }
                        else if (verifyCHK(decryptArray(xortwos(data1, blank))))
                        {
                            ekx = ezeros;
                            Array.Copy(xortwos(encrypteddata, blank), 0, keystream, key2off, 232);
                            Array.Copy(zeros, 0, keystream, key1off, 232);
                        }
                        else return null; // unreachable
                    }
                    else if (keydata1) // Key 1 data is empty
                    {
                        if (verifyCHK(decryptArray(data1)))
                        {
                            ekx = data1;
                            Array.Copy(key1, 0, keystream, key2off, 232);
                            Array.Copy(zeros, 0, keystream, key1off, 232);
                        }
                        else if (verifyCHK(decryptArray(xortwos(data1, ezeros))))
                        {
                            ekx = xortwos(data1, ezeros);
                            Array.Copy(xortwos(key1, ezeros), 0, keystream, key2off, 232);
                            Array.Copy(zeros, 0, keystream, key1off, 232);
                        }
                        else if (verifyCHK(decryptArray(xortwos(data1, blank))))
                        {
                            ekx = xortwos(data1, blank);
                            Array.Copy(xortwos(key1, blank), 0, keystream, key2off, 232);
                            Array.Copy(zeros, 0, keystream, key1off, 232);
                        }
                        else return null; // unreachable
                    }
                    else if (keydata2)
                    {
                        if (verifyCHK(decryptArray(data2)))
                        {
                            ekx = data2;
                            Array.Copy(key2, 0, keystream, key2off, 232);
                            Array.Copy(zeros, 0, keystream, key1off, 232);
                        }
                        else if (verifyCHK(decryptArray(xortwos(data2, ezeros))))
                        {
                            ekx = xortwos(data2, ezeros);
                            Array.Copy(xortwos(key2, ezeros), 0, keystream, key2off, 232);
                            Array.Copy(zeros, 0, keystream, key1off, 232);
                        }
                        else if (verifyCHK(decryptArray(xortwos(data2, blank))))
                        {
                            ekx = xortwos(data2, blank);
                            Array.Copy(xortwos(key2, blank), 0, keystream, key2off, 232);
                            Array.Copy(zeros, 0, keystream, key1off, 232);
                        }
                        else return null; // unreachable
                    }
                }
            }
            byte[] pkx = decryptArray(ekx);
            if (verifyCHK(pkx))
            {
                slots++;
                return pkx;
            }
            else 
                return null; // Slot Decryption error?!
        }
        private void scanSAV(byte[] input, byte[] keystream, byte[] blank, bool setLable = true)
        {
            slots = 0;
            int boxoffset = BitConverter.ToInt32(keystream, 0x1C);
            for (int i = 0; i < 930; i++)
                fetchpkx(input, keystream, boxoffset + i * 232, 0x100 + i * 232, 0x40000 + i * 232, blank);

            if(setLable)
                L_SAVStats.Text = String.Format("{0}/930", slots);
            //MessageBox.Show("Unlocked: " + unlockedslots + " Soft: " + softslots);
        }
        private void dumpPKX_SAV(byte[] pkx, int dumpnum, int dumpstart)
        {
            if (ghost && CHK_HideFirst.Checked) return;
            if (pkx == null || !verifyCHK(pkx)) //RTB_SAV.AppendText("SLOT LOCKED\r\n");
                return;

            Structures.PKX data = new Structures.PKX(pkx);

            // Printout Parsing
            if (data.species == 0) //RTB_SAV.AppendText("SLOT EMPTY");
                return;

            string box = "B"+(dumpstart + (dumpnum/30)).ToString("00");
            string slot = (((dumpnum%30) / 6 + 1).ToString() + "," + (dumpnum % 6 + 1).ToString());
            string species = specieslist[data.species];
            string gender = data.genderstring;
            string nature = natures[data.nature];
            string ability = abilitylist[data.ability];
            string hp = data.HP_IV.ToString();
            string atk = data.ATK_IV.ToString();
            string def = data.DEF_IV.ToString();
            string spa = data.SPA_IV.ToString();
            string spd = data.SPD_IV.ToString();
            string spe = data.SPE_IV.ToString();
            string hptype = types[data.hptype];
            string ESV = data.ESV.ToString("0000");
            string TSV = data.TSV.ToString("0000");
            string ball = balls[data.ball];
            string nickname = data.nicknamestr;
            string otname = data.ot;
            string TID = data.TID.ToString("00000");
            string SID = data.SID.ToString("00000");
            string move1 = movelist[data.move1];
            string move2 = movelist[data.move2];
            string move3 = movelist[data.move3];
            string move4 = movelist[data.move4];
            string ev_hp = data.HP_EV.ToString();
            string ev_at = data.ATK_EV.ToString();
            string ev_de = data.DEF_EV.ToString();
            string ev_sa = data.SPA_EV.ToString();
            string ev_sd = data.SPD_EV.ToString();
            string ev_se = data.SPE_EV.ToString();

            // Bonus
            string relearn1 = movelist[data.eggmove1].ToString();
            string relearn2 = movelist[data.eggmove2].ToString();
            string relearn3 = movelist[data.eggmove3].ToString();
            string relearn4 = movelist[data.eggmove4].ToString();
            string isshiny = ""; if (data.isshiny) isshiny = "★";
            string isegg = ""; if (data.isegg) isegg = "✓";

            bool statisfiesFilters = true;

            while (CHK_Enable_Filtering.Checked)
            {
                bool checkHp = false;
                if (CB_HP_Type.SelectedIndex > 0)
                {
                    if (CB_HP_Type.SelectedIndex != data.hptype) { statisfiesFilters = false; break; }
                    checkHp = true;
                }

                int perfects = Convert.ToInt16(CB_No_IVs.SelectedItem);
                bool ivsSelected = CHK_IV_HP.Checked || CHK_IV_Atk.Checked || CHK_IV_Def.Checked || CHK_IV_SpAtk.Checked || CHK_IV_SpDef.Checked || CHK_IV_Spe.Checked;
                if (hp == "31" ||checkHp && hp == "30") --perfects;
                else if (ivsSelected && CHK_IV_HP.Checked != RAD_IVs_Miss.Checked) { statisfiesFilters = false; break; }
                if ((atk == "31" || checkHp && atk == "30") && !CHK_Special_Attacker.Checked || (atk == "0" || checkHp && atk == "1") && CHK_Special_Attacker.Checked) --perfects;
                else if (ivsSelected && CHK_IV_Atk.Checked != RAD_IVs_Miss.Checked) { statisfiesFilters = false; break; }
                if (def == "31" || checkHp && def == "30") --perfects;
                else if (ivsSelected && CHK_IV_Def.Checked != RAD_IVs_Miss.Checked) { statisfiesFilters = false; break; }
                if (spa == "31" || checkHp && spa == "30") --perfects;
                else if (ivsSelected && CHK_IV_SpAtk.Checked != RAD_IVs_Miss.Checked) { statisfiesFilters = false; break; }
                if (spd == "31" || checkHp && spd == "30") --perfects;
                else if (ivsSelected && CHK_IV_SpDef.Checked != RAD_IVs_Miss.Checked) { statisfiesFilters = false; break; }
                if ((spe == "31" || checkHp && spe == "30") && !CHK_Trickroom.Checked || (spe == "0" || checkHp && spe == "1") && CHK_Trickroom.Checked) --perfects;
                else if (ivsSelected && CHK_IV_Spe.Checked != RAD_IVs_Miss.Checked) { statisfiesFilters = false; break; }
                if (perfects > 0) { statisfiesFilters = false; break; }

                if (CHK_Is_Shiny.Checked || CHK_Hatches_Shiny_For_Me.Checked || CHK_Hatches_Shiny_For.Checked)
                {
                    // TODO: Should probably cache this somewhere...
                    ushort[] acceptedTSVs = (TB_SVs.Text != "" ? Array.ConvertAll(TB_SVs.Text.Split(','), Convert.ToUInt16) : new ushort[0]); 
                    if (!(CHK_Is_Shiny.Checked && data.isshiny ||
                        data.isegg && CHK_Hatches_Shiny_For_Me.Checked && ESV == TSV ||
                        data.isegg && CHK_Hatches_Shiny_For.Checked && Array.IndexOf(acceptedTSVs, data.ESV) > -1))
                    { statisfiesFilters = false; break; }
                }

                break;
            }

            if (statisfiesFilters)
            {
                if (!data.isegg) ESV = "";

                // Vivillon Forms...
                if (data.species >= 664 && data.species <= 666)
                    species += "-" + vivlist[data.altforms];

                if (((CB_ExportStyle.SelectedIndex == 1 || CB_ExportStyle.SelectedIndex == 2 || (CB_ExportStyle.SelectedIndex != 0 && CB_ExportStyle.SelectedIndex < 6)) && CHK_BoldIVs.Checked))
                {
                    if (hp == "31") hp = "**31**";
                    if (atk == "31") atk = "**31**";
                    if (def == "31") def = "**31**";
                    if (spa == "31") spa = "**31**";
                    if (spd == "31") spd = "**31**";
                    if (spe == "31") spe = "**31**";
                }

                string format = RTB_OPTIONS.Text;
                if (CB_ExportStyle.SelectedIndex >= 6)
                    format = "{0} - {1} - {2} ({3}) - {4} - {5} - {6}.{7}.{8}.{9}.{10}.{11} - {12} - {13}";

                if (CB_ExportStyle.SelectedIndex == 6)
                {
                    csvdata += String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30},{31},{32},{33},{34},{35}\r\n",
                        box, slot, species, gender, nature, ability, hp, atk, def, spa, spd, spe, hptype, ESV, TSV, nickname, otname, ball, TID, SID, ev_hp, ev_at, ev_de, ev_sa, ev_sd, ev_se, move1, move2, move3, move4, relearn1, relearn2, relearn3, relearn4, isshiny, isegg);
                }
                if (CB_ExportStyle.SelectedIndex == 7)
                {
                    isshiny = "";
                    if (data.isshiny)
                        isshiny = " ★";
                    if (data.isnick)
                        data.nicknamestr += String.Format(" ({0})", specieslist[data.species]);

                    string savedname =
                        data.species.ToString("000") + isshiny + " - "
                        + data.nicknamestr + " - "
                        + data.chk.ToString("X4") + data.EC.ToString("X8");
                    File.WriteAllBytes(dbpath + "\\" + CleanFileName(savedname) + ".pk6", pkx);
                }
                if (!(CB_ExportStyle.SelectedIndex == 1 || CB_ExportStyle.SelectedIndex == 2 || (CB_ExportStyle.SelectedIndex != 0 && CB_ExportStyle.SelectedIndex < 6 && CHK_R_Table.Checked)))
                {
                    if (ESV != "")
                        ESV = "[" + ESV + "]";
                }
                string result = String.Format(format, box, slot, species, gender, nature, ability, hp, atk, def, spa, spd, spe, hptype, ESV, TSV, nickname, otname, ball, TID, SID, ev_hp, ev_at, ev_de, ev_sa, ev_sd, ev_se, move1, move2, move3, move4, relearn1, relearn2, relearn3, relearn4, isshiny, isegg);

                if (ghost && CHK_MarkFirst.Checked) result = "~" + result;
                dumpedcounter++;
                RTB_SAV.AppendText(result + "\r\n");
            }
        }
        private void DumpSAV(object sender, EventArgs e)
        {
            csvheader = "Box,Row,Column,Species,Gender,Nature,Ability,HP IV,ATK IV,DEF IV,SPA IV,SPD IV,SPE IV,HP Type,ESV,TSV,Nickname,OT,Ball,TID,SID,HP EV,ATK EV,DEF EV,SPA EV,SPD EV,SPE EV,Move 1,Move 2,Move 3,Move 4,Relearn 1, Relearn 2, Relearn 3, Relearn 4, Shiny, Egg";
            csvdata = csvheader + "\r\n";
            RTB_SAV.Clear();
            dumpedcounter = 0;
            // Load our Keystream file.
            byte[] keystream = File.ReadAllBytes(savkeypath);
            byte[] empty = new Byte[232];
            // Save file is already loaded.

            // Get our empty file set up.
            Array.Copy(keystream, 0x10, empty, 0xE0, 0x4);
            string nick = eggnames[empty[0xE3] - 1];
            // Stuff in the nickname to our blank EKX.
            byte[] nicknamebytes = Encoding.Unicode.GetBytes(nick);
            Array.Resize(ref nicknamebytes, 24);
            Array.Copy(nicknamebytes, 0, empty, 0x40, nicknamebytes.Length);
            // Fix CHK
            uint chk = 0;
            for (int i = 8; i < 232; i += 2) // Loop through the entire PKX
                chk += BitConverter.ToUInt16(empty, i);

            // Apply New Checksum
            Array.Copy(BitConverter.GetBytes(chk), 0, empty, 06, 2);
            empty = encryptArray(empty);
            Array.Resize(ref empty, 0xE8);

            // Get our dumping parameters.
            int boxoffset = BitConverter.ToInt32(keystream, 0x1C);
            int offset = 0;
            int count = 30;
            int boxstart = 1;
            if (CB_BoxStart.Text == "All")
                count = 30 * 31;
            else
            {
                boxoffset += (Convert.ToInt16(CB_BoxStart.Text) - 1) * 30 * 232;
                offset += (Convert.ToInt16(CB_BoxStart.Text) - 1) * 30 * 232;
                count = (Convert.ToInt16(CB_BoxEnd.Text) - Convert.ToInt16(CB_BoxStart.Text) + 1) * 30;
                boxstart = Convert.ToInt16(CB_BoxStart.Text);
            }

            string header = String.Format(RTB_OPTIONS.Text, "Box", "Slot", "Species", "Gender", "Nature", "Ability", "HP", "ATK", "DEF", "SPA", "SPD", "SPE", "HiddenPower", "ESV", "TSV", "Nick", "OT", "Ball", "TID", "SID", "HP EV", "ATK EV", "DEF EV", "SPA EV", "SPD EV", "SPE EV", "Move 1", "Move 2", "Move 3", "Move 4", "Relearn 1", "Relearn 2", "Relearn 3", "Relearn 4", "Shiny", "Egg");
            if (CB_ExportStyle.SelectedIndex == 1 || CB_ExportStyle.SelectedIndex == 2 || (CB_ExportStyle.SelectedIndex != 0 && CB_ExportStyle.SelectedIndex < 6 && CHK_R_Table.Checked))
            {
                int args = Regex.Split(RTB_OPTIONS.Text, "{").Length;
                header += "\r\n|";
                for (int i = 0; i < args; i++)
                    header += ":---:|";

                if (!CHK_Split.Checked) // Still append the header if we aren't doing it for every box.
                {
                    // Add header if reddit
                    if (CHK_ColorBox.Checked)
                    {
                        if (CB_BoxColor.SelectedIndex == 0)
                            RTB_SAV.AppendText(boxcolors[1 + (rnd32() % 4)]);
                        else RTB_SAV.AppendText(boxcolors[CB_BoxColor.SelectedIndex - 1]);
                    }
                    // Append Box Name then Header
                    RTB_SAV.AppendText("B" + (boxstart).ToString("00") + "+\r\n\r\n");
                    RTB_SAV.AppendText(header + "\r\n");
                } 
            }

            for (int i = 0; i < count; i++)
            {
                if (i % 30 == 0 && CHK_Split.Checked)
                {
                    RTB_SAV.AppendText("\r\n");
                    // Add box header
                    if ((CB_ExportStyle.SelectedIndex == 1 || CB_ExportStyle.SelectedIndex == 2 || ((CB_ExportStyle.SelectedIndex != 0 && CB_ExportStyle.SelectedIndex < 6)) && CHK_R_Table.Checked))
                    {
                        if (CHK_ColorBox.Checked)
                        {
                            // Add Reddit Coloring
                            if (CB_BoxColor.SelectedIndex == 0)
                                RTB_SAV.AppendText(boxcolors[1 + ((i / 30 + boxstart) % 4)]);
                            else RTB_SAV.AppendText(boxcolors[CB_BoxColor.SelectedIndex - 1]);
                        }
                    }
                    // Append Box Name then Header
                    RTB_SAV.AppendText("B" + (i / 30 + boxstart).ToString("00") + "\r\n\r\n");
                    RTB_SAV.AppendText(header + "\r\n");
                }
                byte[] pkx = fetchpkx(savefile, keystream, boxoffset + i * 232, 0x100 + offset + i * 232, 0x40000 + offset + i * 232, empty);
                dumpPKX_SAV(pkx, i, boxstart);
            }

            // Copy Results to Clipboard
            try { Clipboard.SetText(RTB_SAV.Text); }
            catch { };
            RTB_SAV.AppendText("\r\nData copied to clipboard!\r\nDumped: " + dumpedcounter);
            RTB_SAV.Select(RTB_SAV.Text.Length - 1, 0);
            RTB_SAV.ScrollToCaret();

            if (CB_ExportStyle.SelectedIndex == 6)
            {
                SaveFileDialog savecsv = new SaveFileDialog();
                savecsv.Filter = "Spreadsheet|*.csv";
                savecsv.FileName = "KeySAV Data Dump.csv";
                if (savecsv.ShowDialog() == DialogResult.OK)
                    System.IO.File.WriteAllText(savecsv.FileName, csvdata, Encoding.UTF8);
            }
        }
        // BV
        private void dumpPKX_BV(byte[] pkx, int slot)
        {
            if (pkx == null || !verifyCHK(pkx)) //RTB_SAV.AppendText("SLOT LOCKED\r\n");
                return;

            Structures.PKX data = new Structures.PKX(pkx);

            // Printout Parsing
            if (data.species == 0) //RTB_SAV.AppendText("SLOT EMPTY");
                return;

            string box = "~";
            string species = specieslist[data.species];
            string gender = data.genderstring;
            string nature = natures[data.nature];
            string ability = abilitylist[data.ability];
            string hp = data.HP_IV.ToString();
            string atk = data.ATK_IV.ToString();
            string def = data.DEF_IV.ToString();
            string spa = data.SPA_IV.ToString();
            string spd = data.SPD_IV.ToString();
            string spe = data.SPE_IV.ToString();
            string hptype = types[data.hptype];
            string ESV = data.ESV.ToString("0000");
            string TSV = data.TSV.ToString("0000");
            string ball = balls[data.ball];
            string nickname = data.nicknamestr;
            string otname = data.ot;
            string TID = data.TID.ToString("00000");
            string SID = data.SID.ToString("00000");
            // if (!data.isegg) ESV = "";
            string move1 = movelist[data.move1];
            string move2 = movelist[data.move2];
            string move3 = movelist[data.move3];
            string move4 = movelist[data.move4];
            string ev_hp = data.HP_EV.ToString();
            string ev_at = data.ATK_EV.ToString();
            string ev_de = data.DEF_EV.ToString();
            string ev_sa = data.SPA_EV.ToString();
            string ev_sd = data.SPD_EV.ToString();
            string ev_se = data.SPE_EV.ToString();

            // Bonus
            string relearn1 = movelist[data.eggmove1].ToString();
            string relearn2 = movelist[data.eggmove2].ToString();
            string relearn3 = movelist[data.eggmove3].ToString();
            string relearn4 = movelist[data.eggmove4].ToString();
            string isshiny = ""; if (data.isshiny) isshiny = "★";
            string isegg = ""; if (data.isegg) isegg = "✓";

            // Vivillon Forms...
            if (data.species >= 664 && data.species <= 666)
                species += "-" + vivlist[data.altforms];

            if (((CB_ExportStyle.SelectedIndex == 1 || CB_ExportStyle.SelectedIndex == 2 || (CB_ExportStyle.SelectedIndex != 0 && CB_ExportStyle.SelectedIndex < 6)) && CHK_BoldIVs.Checked))
            {
                if (hp == "31") hp = "**31**";
                if (atk == "31") atk = "**31**";
                if (def == "31") def = "**31**";
                if (spa == "31") spa = "**31**";
                if (spd == "31") spd = "**31**";
                if (spe == "31") spe = "**31**";
            }
            string format = RTB_OPTIONS.Text;
            if (CB_ExportStyle.SelectedIndex >= 6)
                format = "{0} - {1} - {2} ({3}) - {4} - {5} - {6}.{7}.{8}.{9}.{10}.{11} - {12} - {13}";

            if (CB_ExportStyle.SelectedIndex == 6)
            {
                csvdata += String.Format("{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30},{31},{32},{33},{34},{35}\r\n",
                    box, slot, species, gender, nature, ability, hp, atk, def, spa, spd, spe, hptype, ESV, TSV, nickname, otname, ball, TID, SID, ev_hp, ev_at, ev_de, ev_sa, ev_sd, ev_se, move1, move2, move3, move4, relearn1, relearn2, relearn3, relearn4, isshiny, isegg);
            }
            if (CB_ExportStyle.SelectedIndex == 7)
            {
                isshiny = "";
                if (data.isshiny)
                    isshiny = " ★";
                if (data.isnick) 
                    data.nicknamestr += String.Format(" ({0})",specieslist[data.species]);
                string savedname =
                    data.species.ToString("000") + isshiny + " - "
                    + data.nicknamestr + " - "
                    + data.chk.ToString("X4") + data.EC.ToString("X8");
                File.WriteAllBytes(dbpath + "\\" + CleanFileName(savedname) + ".pk6", pkx);
            }
            if (!(CB_ExportStyle.SelectedIndex == 1 || CB_ExportStyle.SelectedIndex == 2 || (CB_ExportStyle.SelectedIndex != 0 && CB_ExportStyle.SelectedIndex < 6 && CHK_R_Table.Checked)))
            {
                if (ESV != "")
                    ESV = "[" + ESV + "]";
            }
            string result = String.Format(format, box, slot, species, gender, nature, ability, hp, atk, def, spa, spd, spe, hptype, ESV, TSV, nickname, otname, ball, TID, SID, ev_hp, ev_at, ev_de, ev_sa, ev_sd, ev_se, move1, move2, move3, move4, relearn1, relearn2, relearn3, relearn4, isshiny, isegg);

            RTB_VID.AppendText(result + "\r\n");
        } // BV
        private void dumpBV(object sender, EventArgs e)
        {
            csvheader = "Position,Species,Gender,Nature,Ability,HP IV,ATK IV,DEF IV,SPA IV,SPD IV,SPE IV,HP Type,ESV,TSV,Nickname,OT,Ball,TID,SID,HP EV,ATK EV,DEF EV,SPA EV,SPD EV,SPE EV,Move 1,Move 2,Move 3,Move 4,Relearn 1, Relearn 2, Relearn 3, Relearn 4, Shiny, Egg";
            csvdata = csvheader + "\r\n";
            RTB_VID.Clear();
            // player @ 0xX100, opponent @ 0x1800;
            byte[] keystream = File.ReadAllBytes(vidkeypath);
            byte[] key = new Byte[260];
            byte[] empty = new Byte[260];
            byte[] emptyekx = encryptArray(empty);
            byte[] ekx = new Byte[260];
            int offset = 0x4E18;
            int keyoff = 0x100;
            if (CB_Team.SelectedIndex == 1)
            {
                offset = 0x5438;
                keyoff = 0x800;
            }

            string header = String.Format(RTB_OPTIONS.Text, "Box", "Slot", "Species", "Gender", "Nature", "Ability", "HP", "ATK", "DEF", "SPA", "SPD", "SPE", "HiddenPower", "ESV", "TSV", "Nick", "OT", "Ball", "TID", "SID", "HP EV", "ATK EV", "DEF EV", "SPA EV", "SPD EV", "SPE EV", "Move 1", "Move 2", "Move 3", "Move 4", "Relearn 1", "Relearn 2", "Relearn 3", "Relearn 4", "Shiny", "Egg");

            // Add header if reddit
            if (CB_ExportStyle.SelectedIndex == 1 || CB_ExportStyle.SelectedIndex == 2 || ((CB_ExportStyle.SelectedIndex != 0 && CB_ExportStyle.SelectedIndex < 6) && CHK_R_Table.Checked))
            {
                // Add Reddit Coloring
                if (CHK_ColorBox.Checked)
                {
                    if (CB_BoxColor.SelectedIndex == 0)
                    {
                        RTB_VID.AppendText(boxcolors[1 + (rnd32() % 4)]);
                    }
                    else RTB_VID.AppendText(boxcolors[CB_BoxColor.SelectedIndex - 1]);
                }
                RTB_VID.AppendText(CB_Team.Text + "\r\n\r\n");
                
                int args = Regex.Split(RTB_OPTIONS.Text, "{").Length;
                header += "\r\n|";
                for (int i = 0; i < args; i++)
                    header += ":---:|";

                RTB_VID.AppendText(header + "\r\n");
            }



            for (int i = 0; i < 6; i++)
            {
                Array.Copy(batvideo, offset + 260 * i, ekx, 0, 260);
                Array.Copy(keystream, keyoff + 260 * i, key, 0, 260);
                ekx = xortwos(ekx, key);
                if (verifyCHK(decryptArray(ekx)))
                    dumpPKX_BV(decryptArray(ekx),i+1);
                else
                    dumpPKX_BV(null,i);
            }

            // Copy Results to Clipboard
            try { Clipboard.SetText(RTB_VID.Text); }
            catch { };
            RTB_VID.AppendText("\r\nData copied to clipboard!"); 
            
            RTB_VID.Select(RTB_VID.Text.Length - 1, 0);
            RTB_VID.ScrollToCaret(); 
            if (CB_ExportStyle.SelectedIndex == 6)
            {
                SaveFileDialog savecsv = new SaveFileDialog();
                savecsv.Filter = "Spreadsheet|*.csv";
                savecsv.FileName = "KeySAV Data Dump.csv";
                if (savecsv.ShowDialog() == DialogResult.OK)
                {
                    string path = savecsv.FileName;
                    System.IO.File.WriteAllText(path, csvdata, Encoding.UTF8);
                }
            }
        }

        // File Keystream Breaking
        private void loadBreak1(object sender, EventArgs e)
        {
            // Open Save File
            OpenFileDialog boxsave = new OpenFileDialog();
            boxsave.Filter = "Save/BV File|*.*";

            if (boxsave.ShowDialog() == DialogResult.OK)
            {
                string path = boxsave.FileName;
                byte[] input = File.ReadAllBytes(path);
                if ((input.Length == 0x10009C) || input.Length == 0x100000)
                {
                    Array.Copy(input, input.Length % 0x100000, break1, 0, 0x100000);
                    TB_File1.Text = path;
                    file1 = "SAV";
                }
                else if (input.Length == 28256)
                {
                    Array.Copy(input, video1, 28256);
                    TB_File1.Text = path;
                    file1 = "BV";
                }
                else
                {
                    file1 = "";
                    MessageBox.Show("Incorrect File Loaded: Neither a SAV (1MB) or Battle Video (~27.5KB).", "Error");
                }
            } 
            togglebreak();
        }
        private void loadBreak2(object sender, EventArgs e)
        {
            // Open Save File
            OpenFileDialog boxsave = new OpenFileDialog();
            boxsave.Filter = "Save/BV File|*.*";

            if (boxsave.ShowDialog() == DialogResult.OK)
            {
                string path = boxsave.FileName;
                byte[] input = File.ReadAllBytes(path);
                if ((input.Length == 0x10009C) || input.Length == 0x100000)
                {
                    Array.Copy(input, input.Length % 0x100000, break2, 0, 0x100000); // Force save to 0x100000
                    TB_File2.Text = path;
                    file2 = "SAV";
                }
                else if (input.Length == 28256)
                {
                    Array.Copy(input, video2, 28256);
                    TB_File2.Text = path;
                    file2 = "BV";
                }
                else
                {
                    file2 = "";
                    MessageBox.Show("Incorrect File Loaded: Neither a SAV (1MB) or Battle Video (~27.5KB).", "Error");
                }
            }
            togglebreak();
        }

        private void loadBreakFolder(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            if (folder.ShowDialog() == DialogResult.OK)
            {
                TB_Folder.Text = folder.SelectedPath;
                B_BreakFolder.Enabled = true;
            }
        }

        private void togglebreak()
        {
            B_Break.Enabled = false;
            if (TB_File1.Text != "" && TB_File2.Text != "")
                if ((file1 == "SAV" && file2 == "SAV") || (file1 == "BV" && file2 == "BV"))
                   B_Break.Enabled = true;
        }

        // Specific Breaking Branch
        private void B_Break_Click(object sender, EventArgs e)
        {
            if (file1 == file2)
            {
                if (file1 == "SAV")
                    breakSAV();
                else if (file1 == "BV")
                    breakBV();
                else
                    return;
            }
        }
        private void breakBV()
        {
            // Do Trick
            {
                byte[] ezeros = encryptArray(new Byte[260]);
                byte[] xorstream = new Byte[260 * 6];
                byte[] breakstream = new Byte[260 * 6];
                byte[] bvkey = new Byte[0x1000];
                #region Old Exploit to ensure that the usage is correct
                // Validity Check to see what all is participating...

                Array.Copy(video1, 0x4E18, breakstream, 0, 260 * 6);
                // XOR them together at party offset
                for (int i = 0; i < (260 * 6); i++)
                    xorstream[i] = (byte)(breakstream[i] ^ video2[i + 0x4E18]);

                // Retrieve EKX_1's data
                byte[] ekx1 = new Byte[260];
                for (int i = 0; i < (260); i++)
                    ekx1[i] = (byte)(xorstream[i + 260] ^ ezeros[i]);
                for (int i = 0; i < 260; i++)
                    xorstream[i] ^= ekx1[i];

                #endregion
                // If old exploit does not properly decrypt slot1...
                byte[] pkx = decryptArray(ekx1);
                if (!verifyCHK(pkx))
                { MessageBox.Show("Improperly set up Battle Videos. Please follow directions and try again", "Error"); return; }
                // 

                // Start filling up our key...
                #region Key Filling (bvkey)
                // Copy in the unique CTR encryption data to ID the video...
                Array.Copy(video1, 0x10, bvkey, 0, 0x10);

                // Copy unlocking data
                byte[] key1 = new Byte[260]; Array.Copy(video1, 0x4E18, key1, 0, 260);
                Array.Copy(xortwos(ekx1, key1), 0, bvkey, 0x100, 260);
                Array.Copy(video1, 0x4E18 + 260, bvkey, 0x100 + 260, 260*5); // XORstream from save1 has just keystream.
                
                // See if Opponent first slot can be decrypted...

                Array.Copy(video1, 0x5438, breakstream, 0, 260 * 6);
                // XOR them together at party offset
                for (int i = 0; i < (260 * 6); i++)
                    xorstream[i] = (byte)(breakstream[i] ^ video2[i + 0x5438]);
                // XOR through the empty data for the encrypted zero data.
                for (int i = 0; i < (260 * 5); i++)
                    bvkey[0x100 + 260 + i] ^= ezeros[i % 260];

                // Retrieve EKX_2's data
                byte[] ekx2 = new Byte[260];
                for (int i = 0; i < (260); i++)
                    ekx2[i] = (byte)(xorstream[i + 260] ^ ezeros[i]);
                for (int i = 0; i < 260; i++)
                    xorstream[i] ^= ekx2[i];
                byte[] key2 = new Byte[260]; Array.Copy(video1,0x5438,key2,0,260);
                byte[] pkx2 = decryptArray(ekx2);
                if (verifyCHK(decryptArray(ekx2)) && (BitConverter.ToUInt16(pkx2,0x8) != 0))
                {
                    Array.Copy(xortwos(ekx2,key2), 0, bvkey, 0x800, 260);
                    Array.Copy(video1, 0x5438 + 260, bvkey, 0x800 + 260, 260 * 5); // XORstream from save1 has just keystream.

                    for (int i = 0; i < (260 * 5); i++)
                        bvkey[0x800 + 260 + i] ^= ezeros[i % 260];

                    MessageBox.Show("Can dump from Opponent Data on this key too!");
                }
                #endregion

                string ot = TrimFromZero(Encoding.Unicode.GetString(pkx, 0xB0, 24));
                ushort tid = BitConverter.ToUInt16(pkx, 0xC);
                ushort sid = BitConverter.ToUInt16(pkx, 0xE);
                ushort tsv = (ushort)((tid ^ sid) >> 4);
                // Finished, allow dumping of breakstream
                MessageBox.Show(String.Format("Success!\r\nYour first Pokemon's TSV: {0}\r\nOT: {1}\r\n\r\nPlease save your keystream.", tsv.ToString("0000"),ot));


                FileInfo fi = new FileInfo(TB_File1.Text);
                string bvnumber = Regex.Split(fi.Name, "(-)")[0];
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = CleanFileName(String.Format("BV Key - {0}.bin", bvnumber));
                string ID = sfd.InitialDirectory;
                sfd.InitialDirectory = path_exe + "\\data";
                sfd.RestoreDirectory = true;
                sfd.Filter = "Video Key|*.bin";
                if (sfd.ShowDialog() == DialogResult.OK)
                    File.WriteAllBytes(sfd.FileName, bvkey);
                else
                    MessageBox.Show("Chose not to save keystream.", "Alert");
                sfd.InitialDirectory = ID; sfd.RestoreDirectory = true;
            }
        }
        private void breakSAV()
        {
            int[] offset = new int[2];
            byte[] empty = new Byte[232];
            byte[] emptyekx = new Byte[232];
            byte[] ekxdata = new Byte[232];
            byte[] pkx = new Byte[232];
            #region Finding the User Specific Data: Using Valid to keep track of progress...
            // Do Break. Let's first do some sanity checking to find out the 2 offsets we're dumping from.
            // Loop through save file to find
            int fo = savefile.Length / 2 + 0x20000; // Initial Offset, can tweak later.
            int success = 0;
            string result = "";

            for (int d = 0; d < 2; d++)
            {
                // Do this twice to get both box offsets.
                for (int i = fo; i < 0xEE000; i++)
                {
                    int err = 0;
                    // Start at findoffset and see if it matches pattern
                    if ((break1[i + 4] == break2[i + 4]) && (break1[i + 4 + 232] == break2[i + 4 + 232]))
                    {
                        // Sanity Placeholders are the same
                        for (int j = 0; j < 4; j++)
                            if (break1[i + j] == break2[i + j])
                                err++;

                        if (err < 4)
                        {
                            // Keystream ^ PID doesn't match entirely. Keep checking.
                            for (int j = 8; j < 232; j++)
                                if (break1[i + j] == break2[i + j])
                                    err++;

                            if (err < 20)
                            {
                                // Tolerable amount of difference between offsets. We have a result.
                                offset[d] = i;
                                break;
                            }
                        }
                    }
                }
                fo = offset[d] + 232 * 30;  // Fast forward out of this box to find the next.
            }

            // Now that we have our two box offsets...
            // Check to see if we actually have them.

            if ((offset[0] == 0) || (offset[1] == 0))
            {
                // We have a problem. Don't continue.
                result = "Unable to Find Box.\r\n";
            }
            else
            {
                // Let's go deeper. We have the two box offsets.
                // Chunk up the base streams.
                byte[,] estream1 = new Byte[30, 232];
                byte[,] estream2 = new Byte[30, 232];
                // Stuff 'em.
                for (int i = 0; i < 30; i++)    // Times we're iterating
                {
                    for (int j = 0; j < 232; j++)   // Stuff the Data
                    {
                        estream1[i, j] = break1[offset[0] + 232 * i + j];
                        estream2[i, j] = break2[offset[1] + 232 * i + j];
                    }
                }

                // Okay, now that we have the encrypted streams, formulate our EKX.
                string nick = eggnames[1];
                // Stuff in the nickname to our blank EKX.
                byte[] nicknamebytes = Encoding.Unicode.GetBytes(nick);
                Array.Resize(ref nicknamebytes, 24);
                Array.Copy(nicknamebytes, 0, empty, 0x40, nicknamebytes.Length);

                // Encrypt the Empty PKX to EKX.
                Array.Copy(empty, emptyekx, 232);
                emptyekx = encryptArray(emptyekx);
                // Not gonna bother with the checksum, as this empty file is temporary.

                // Sweet. Now we just have to find the E0-E3 values. Let's get our polluted streams from each.
                // Save file 1 has empty box 1. Save file 2 has empty box 2.
                byte[,] pstream1 = new Byte[30, 232]; // Polluted Keystream 1
                byte[,] pstream2 = new Byte[30, 232]; // Polluted Keystream 2
                for (int i = 0; i < 30; i++)    // Times we're iterating
                {
                    for (int j = 0; j < 232; j++)   // Stuff the Data
                    {
                        pstream1[i, j] = (byte)(estream1[i, j] ^ emptyekx[j]);
                        pstream2[i, j] = (byte)(estream2[i, j] ^ emptyekx[j]);
                    }
                }

                // Cool. So we have a fairly decent keystream to roll with. We now need to find what the E0-E3 region is.
                // 0x00000000 Encryption Constant has the D block last. 
                // We need to make sure our Supplied Encryption Constant Pokemon have the D block somewhere else (Pref in 1 or 3).

                // First, let's get out our polluted EKX's.
                byte[,] polekx = new Byte[6, 232];
                for (int i = 0; i < 6; i++)
                    for (int j = 0; j < 232; j++) // Save file 1 has them in the second box. XOR them out with the Box2 Polluted Stream
                        polekx[i, j] = (byte)(break1[offset[1] + 232 * i + j] ^ pstream2[i, j]);

                uint[] encryptionconstants = new uint[6]; // Array for all 6 Encryption Constants. 
                int valid = 0;
                for (int i = 0; i < 6; i++)
                {
                    encryptionconstants[i] = (uint)polekx[i, 0];
                    encryptionconstants[i] += (uint)polekx[i, 1] * 0x100;
                    encryptionconstants[i] += (uint)polekx[i, 2] * 0x10000;
                    encryptionconstants[i] += (uint)polekx[i, 3] * 0x1000000;
                    // EC Obtained. Check to see if Block D is not last.
                    if (getDloc(encryptionconstants[i]) != 3)
                    {
                        valid++;
                        // Find the Origin/Region data.
                        byte[] encryptedekx = new Byte[232];
                        byte[] decryptedpkx = new Byte[232];
                        for (int z = 0; z < 232; z++)
                            encryptedekx[z] = polekx[i, z];

                        decryptedpkx = decryptArray(encryptedekx);

                        // finalize data

                        // Okay, now that we have the encrypted streams, formulate our EKX.
                        nick = eggnames[decryptedpkx[0xE3] - 1];
                        // Stuff in the nickname to our blank EKX.
                        nicknamebytes = Encoding.Unicode.GetBytes(nick);
                        Array.Resize(ref nicknamebytes, 24);
                        Array.Copy(nicknamebytes, 0, empty, 0x40, nicknamebytes.Length);

                        // Dump it into our Blank EKX. We have won!
                        empty[0xE0] = decryptedpkx[0xE0];
                        empty[0xE1] = decryptedpkx[0xE1];
                        empty[0xE2] = decryptedpkx[0xE2];
                        empty[0xE3] = decryptedpkx[0xE3];
                        break;
                    }
                }
            #endregion

                if (valid == 0) // We didn't get any valid EC's where D was not in last. Tell the user to try again with different specimens.
                    result = "The 6 supplied Pokemon are not suitable. \r\nRip new saves with 6 different ones that originated from your save file.\r\n";

                else
                {
                    #region Fix up our Empty File
                    // We can continue to get our actual keystream.
                    // Let's calculate the actual checksum of our empty pkx.
                    uint chk = 0;
                    for (int i = 8; i < 232; i += 2) // Loop through the entire PKX
                        chk += BitConverter.ToUInt16(empty, i);

                    // Apply New Checksum
                    Array.Copy(BitConverter.GetBytes(chk), 0, empty, 06, 2);

                    // Okay. So we're now fixed with the proper blank PKX. Encrypt it!
                    Array.Copy(empty, emptyekx, 232);
                    emptyekx = encryptArray(emptyekx);
                    Array.Resize(ref emptyekx, 232); // ensure it's 232 bytes.

                    // Empty EKX obtained. Time to set up our key file.
                    savkey = new Byte[0x80000];
                    // Copy over 0x10-0x1F (Save Encryption Unused Data so we can track data).
                    Array.Copy(break1, 0x10, savkey, 0, 0x10);
                    // Include empty data
                    savkey[0x10] = empty[0xE0]; savkey[0x11] = empty[0xE1]; savkey[0x12] = empty[0xE2]; savkey[0x13] = empty[0xE3];
                    // Copy over the scan offsets.
                    Array.Copy(BitConverter.GetBytes(offset[0]), 0, savkey, 0x1C, 4);

                    for (int i = 0; i < 30; i++)    // Times we're iterating
                    {
                        for (int j = 0; j < 232; j++)   // Stuff the Data temporarily...
                        {
                            savkey[0x100 + i * 232 + j] = (byte)(estream1[i, j] ^ emptyekx[j]);
                            savkey[0x100 + (30 * 232) + i * 232 + j] = (byte)(estream2[i, j] ^ emptyekx[j]);
                        }
                    }
                    #endregion
                    // Let's extract some of the information now for when we set the Keystream filename.
                    #region Keystream Naming
                    byte[] data1 = new Byte[232];
                    byte[] data2 = new Byte[232];
                    for (int i = 0; i < 232; i++)
                    {
                        data1[i] = (byte)(savkey[0x100 + i] ^ break1[offset[0] + i]);
                        data2[i] = (byte)(savkey[0x100 + i] ^ break2[offset[0] + i]);
                    }
                    byte[] data1a = new Byte[232]; byte[] data2a = new Byte[232];
                    Array.Copy(data1, data1a, 232); Array.Copy(data2, data2a, 232);
                    byte[] pkx1 = decryptArray(data1);
                    byte[] pkx2 = decryptArray(data2);
                    ushort chk1 = 0;
                    ushort chk2 = 0;
                    for (int i = 8; i < 232; i += 2)
                    {
                        chk1 += BitConverter.ToUInt16(pkx1, i);
                        chk2 += BitConverter.ToUInt16(pkx2, i);
                    }
                    if (verifyCHK(pkx1) && Convert.ToBoolean(BitConverter.ToUInt16(pkx1, 8)))
                    {
                        // Save 1 has the box1 data
                        pkx = pkx1;
                        success = 1;
                    }
                    else if (verifyCHK(pkx2) && Convert.ToBoolean(BitConverter.ToUInt16(pkx2, 8)))
                    {
                        // Save 2 has the box1 data
                        pkx = pkx2;
                        success = 1;
                    }
                    else
                    {
                        // Data isn't decrypting right...
                        for (int i = 0; i < 232; i++)
                        {
                            data1a[i] ^= empty[i];
                            data2a[i] ^= empty[i];
                        }
                        pkx1 = decryptArray(data1a); pkx2 = decryptArray(data2a);
                        if (verifyCHK(pkx1) && Convert.ToBoolean(BitConverter.ToUInt16(pkx1, 8)))
                        {
                            // Save 1 has the box1 data
                            pkx = pkx1;
                            success = 1;
                        }
                        else if (verifyCHK(pkx2) && Convert.ToBoolean(BitConverter.ToUInt16(pkx2, 8)))
                        {
                            // Save 2 has the box1 data
                            pkx = pkx2;
                            success = 1;
                        }
                        else
                        {
                            // Sigh...
                        }
                    }
                    #endregion
                }
            }
            if (success == 1)
            {
                // Markup the save to know that boxes 1 & 2 are dumpable.
                savkey[0x20] = 3; // 00000011 (boxes 1 & 2)

                // Clear the keystream file...
                for (int i = 0; i < 31; i++)
                {
                    Array.Copy(zerobox, 0, savkey, 0x00100 + i * (232 * 30), 232 * 30);
                    Array.Copy(zerobox, 0, savkey, 0x40000 + i * (232 * 30), 232 * 30);
                }

                // Since we don't know if the user put them in in the wrong order, let's just markup our keystream with data.
                byte[] data1 = new Byte[232];
                byte[] data2 = new Byte[232];
                for (int i = 0; i < 31; i++)
                {
                    for (int j = 0; j < 30; j++)
                    {
                        Array.Copy(break1, offset[0] + i * (232 * 30) + j * 232, data1, 0, 232);
                        Array.Copy(break2, offset[0] + i * (232 * 30) + j * 232, data2, 0, 232);
                        if (data1.SequenceEqual(data2))
                        {
                            // Just copy data1 into the key file.
                            Array.Copy(data1, 0, savkey, 0x00100 + i * (232 * 30) + j * 232, 232);
                        }
                        else
                        {
                            // Copy both datas into their keystream spots.
                            Array.Copy(data1, 0, savkey, 0x00100 + i * (232 * 30) + j * 232, 232);
                            Array.Copy(data2, 0, savkey, 0x40000 + i * (232 * 30) + j * 232, 232);
                        }
                    }
                }

                // Save file diff is done, now we're essentially done. Save the keystream.

                // Success
                result = "Keystreams were successfully bruteforced!\r\n\r\n";
                result += "Save your keystream now...";
                MessageBox.Show(result);

                // From our PKX data, fetch some details to name our key file...
                string ot = TrimFromZero(Encoding.Unicode.GetString(pkx, 0xB0, 24));
                ushort tid = BitConverter.ToUInt16(pkx, 0xC);
                ushort sid = BitConverter.ToUInt16(pkx, 0xE);
                ushort tsv = (ushort)((tid ^ sid) >> 4);
                SaveFileDialog sfd = new SaveFileDialog();
                string ID = sfd.InitialDirectory;
                sfd.InitialDirectory = path_exe + "\\data";
                sfd.RestoreDirectory = true;
                sfd.FileName = CleanFileName(String.Format("SAV Key - {0} - ({1}.{2}) - TSV {3}.bin", ot, tid.ToString("00000"), sid.ToString("00000"), tsv.ToString("0000")));
                sfd.Filter = "Save Key|*.bin";
                if (sfd.ShowDialog() == DialogResult.OK)
                    File.WriteAllBytes(sfd.FileName, savkey);
                else
                    MessageBox.Show("Chose not to save keystream.", "Alert");

                sfd.InitialDirectory = ID; sfd.RestoreDirectory = true;
            }
            else // Failed
                MessageBox.Show(result + "Keystreams were NOT bruteforced!\r\n\r\nStart over and try again :(");
        }

        // Utility
        private byte[] xortwos(byte[] arr1, byte[] arr2)
        {
            if (arr1.Length != arr2.Length) return null;
            byte[] arr3 = new Byte[arr1.Length];
            for (int i = 0; i < arr1.Length; i++)
                arr3[i] = (byte)(arr1[i] ^ arr2[i]);
            return arr3;
        }
        public static string TrimFromZero(string input)
        {
            int index = input.IndexOf('\0');
            if (index < 0)
                return input;

            return input.Substring(0, index);
        }
        private static string CleanFileName(string fileName)
        {
            return Path.GetInvalidFileNameChars().Aggregate(fileName, (current, c) => current.Replace(c.ToString(), string.Empty));
        }
        public static FileInfo GetNewestFile(DirectoryInfo directory)
        {
            return directory.GetFiles()
                .Union(directory.GetDirectories().Select(d => GetNewestFile(d)))
                .OrderByDescending(f => (f == null ? DateTime.MinValue : f.LastWriteTime))
                .FirstOrDefault();
        }

        // SD Detection
        private void changedetectgame(object sender, EventArgs e)
        {
            game = CB_Game.SelectedIndex;
            myTimer.Start();
        }
        private void detectMostRecent()
        {
            // Fetch the selected save file and video
            //try
            {
                if (game == 0)
                {
                    // X
                    savpath = path_3DS + "\\title\\00040000\\00055d00\\";
                    vidpath = path_3DS + "\\extdata\\00000000\\0000055d\\00000000\\";
                }
                else if (game == 1)
                {
                    // Y
                    savpath = path_3DS + "\\title\\00040000\\00055e00\\";
                    vidpath = path_3DS + "\\extdata\\00000000\\0000055e\\00000000\\";
                }
                else if (game == 2) 
                {
                    // OR
                    savpath = path_3DS + "\\title\\00040000\\0011c400\\";
                    vidpath = path_3DS + "\\extdata\\00000000\\000011c4\\00000000\\";
                }
                else if (game == 3)
                {
                    // AS
                    savpath = path_3DS + "\\title\\00040000\\0011c500\\";
                    vidpath = path_3DS + "\\extdata\\00000000\\000011c5\\00000000\\";
                }

                if (Directory.Exists(savpath))
                {
                    if (File.Exists(savpath + "00000001.sav"))
                        this.Invoke(new MethodInvoker(delegate { openSAV(savpath + "00000001.sav"); }));
                }
                // Fetch the latest video
                if (Directory.Exists(vidpath))
                {
                    try
                    {
                        FileInfo BV = GetNewestFile(new DirectoryInfo(vidpath));
                        if (BV.Length == 28256)
                        { this.Invoke(new MethodInvoker(delegate { openVID(BV.FullName); })); }
                    }
                    catch { }
                }
            }
            //catch { }
        }
        private void find3DS()
        {
            // start by checking if the 3DS file path exists or not.
            string[] DriveList = Environment.GetLogicalDrives();
            for (int i = 1; i < DriveList.Length; i++)
            {
                path_3DS = DriveList[i] + "Nintendo 3DS";
                if (Directory.Exists(path_3DS))
                    break;

                path_3DS = null;
            }
            if (path_3DS == null) // No 3DS SD Card Detected
                return;
            else
            {
                // 3DS data found in SD card reader. Let's get the title folder location!
                string[] folders = Directory.GetDirectories(path_3DS, "*", System.IO.SearchOption.AllDirectories);

                // Loop through all the folders in the Nintendo 3DS folder to see if any of them contain 'title'.
                for (int i = 0; i < folders.Length; i++)
                {
                    DirectoryInfo di = new DirectoryInfo(folders[i]);
                    if (di.Name == "title" || di.Name == "extdata")
                    {
                        path_3DS = di.Parent.FullName.ToString();
                        myTimer.Stop();
                        detectMostRecent();
                        pathfound = true;
                        return;
                    }
                }
            }
        }

        // UI Prompted Updates
        private void changeboxsetting(object sender, EventArgs e)
        {
            CB_BoxEnd.Visible = CB_BoxEnd.Enabled = L_BoxThru.Visible = !(CB_BoxStart.Text == "All");
            if (CB_BoxEnd.Enabled)
            {
                int start = Convert.ToInt16(CB_BoxStart.Text);
                int oldValue = Convert.ToInt16(CB_BoxEnd.SelectedItem);
                CB_BoxEnd.Items.Clear();
                for (int i = start; i < 32; i++)
                    CB_BoxEnd.Items.Add(i.ToString());
                CB_BoxEnd.SelectedIndex = (start >= oldValue ? 0 : oldValue-start);
            }
        }
        private void B_ShowOptions_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                 "{0} - Box\r\n"
                +"{1} - Slot\r\n"
                +"{2} - Species\r\n"
                +"{3} - Gender\r\n"
                +"{4} - Nature\r\n"
                +"{5} - Ability\r\n"
                +"{6} - HP IV\r\n"
                +"{7} - ATK IV\r\n"
                +"{8} - DEF IV\r\n"
                +"{9} - SPA IV\r\n"
                +"{10} - SPE IV\r\n"
                +"{11} - SPD IV\r\n"
                +"{12} - Hidden Power Type\r\n"
                +"{13} - ESV\r\n"
                +"{14} - TSV\r\n"
                +"{15} - Nickname\r\n"
                +"{16} - OT Name\r\n"
                +"{17} - Ball\r\n"
                +"{18} - TID\r\n"
                +"{19} - SID\r\n"
                +"{20} - HP EV\r\n"
                +"{21} - ATK EV\r\n"
                +"{22} - DEF EV\r\n"
                +"{23} - SPA EV\r\n"
                +"{24} - SPD EV\r\n"
                +"{25} - SPE EV\r\n"
                +"{26} - Move 1\r\n"
                +"{27} - Move 2\r\n"
                +"{28} - Move 3\r\n"
                +"{29} - Move 4\r\n"
                +"{30} - Relearn 1\r\n"
                +"{31} - Relearn 2\r\n"
                +"{32} - Relearn 3\r\n"
                +"{33} - Relearn 4\r\n"
                +"{34} - Is Shiny\r\n"
                +"{35} - Is Egg\r\n"
                ,"Help"
                );
        }
        private void changeExportStyle(object sender, EventArgs e)
        {
            /*
                Default
                Reddit
                TSV
                Custom 1
                Custom 2
                Custom 3
                CSV
                To .PK6 File 
             */
            CHK_BoldIVs.Visible = CHK_ColorBox.Visible = CB_BoxColor.Visible = false;
            if (CB_ExportStyle.SelectedIndex == 0) // Default
            {
                CHK_R_Table.Visible = false;
                RTB_OPTIONS.ReadOnly = true; RTB_OPTIONS.Text =
                    "{0} - {1} - {2} ({3}) - {4} - {5} - {6}.{7}.{8}.{9}.{10}.{11} - {12} - {13}";
            }
            else if (CB_ExportStyle.SelectedIndex == 1) // Reddit
            {
                CHK_R_Table.Visible = false;
                CHK_BoldIVs.Visible = CHK_ColorBox.Visible = CB_BoxColor.Visible = true;
                RTB_OPTIONS.ReadOnly = true; RTB_OPTIONS.Text =
                "{0} | {1} | {2} ({3}) | {4} | {5} | {6}.{7}.{8}.{9}.{10}.{11} | {12} | {13} |";
            }
            else if (CB_ExportStyle.SelectedIndex == 2) // TSV
            {
                CHK_R_Table.Visible = false;
                CHK_BoldIVs.Visible = CHK_ColorBox.Visible = CB_BoxColor.Visible = true;
                RTB_OPTIONS.ReadOnly = true; RTB_OPTIONS.Text =
                "{0} | {1} | {16} | {18} | {14} |";
            }
            else if (CB_ExportStyle.SelectedIndex == 3) // Custom 1
            {
                CHK_R_Table.Visible = true; CHK_R_Table.Checked = custom1b;
                CHK_BoldIVs.Visible = CHK_ColorBox.Visible = CB_BoxColor.Visible = true;
                RTB_OPTIONS.ReadOnly = false;
                RTB_OPTIONS.Text = custom1;
            }
            else if (CB_ExportStyle.SelectedIndex == 4) // Custom 2
            {
                CHK_R_Table.Visible = true; CHK_R_Table.Checked = custom2b;
                CHK_BoldIVs.Visible = CHK_ColorBox.Visible = CB_BoxColor.Visible = true;
                RTB_OPTIONS.ReadOnly = false;
                RTB_OPTIONS.Text = custom2;
            }
            else if (CB_ExportStyle.SelectedIndex == 5) // Custom 3
            {
                CHK_R_Table.Visible = true; CHK_R_Table.Checked = custom3b;
                CHK_BoldIVs.Visible = CHK_ColorBox.Visible = CB_BoxColor.Visible = true;
                RTB_OPTIONS.ReadOnly = false;
                RTB_OPTIONS.Text = custom3;
            }
            else if (CB_ExportStyle.SelectedIndex == 6) // CSV
            {
                CHK_R_Table.Visible = false;
                RTB_OPTIONS.ReadOnly = true; RTB_OPTIONS.Text =
                "CSV will output everything imagineable to the specified location.";
            }
            else if (CB_ExportStyle.SelectedIndex == 7) // PK6
            {
                CHK_R_Table.Visible = false;
                RTB_OPTIONS.ReadOnly = true; RTB_OPTIONS.Text =
                "Files will be saved in .PK6 format, and the default method will display.";
            }
        }
        private void changeFormatText(object sender, EventArgs e)
        {
            if (CB_ExportStyle.SelectedIndex == 3) // Custom 1
                custom1 = RTB_OPTIONS.Text;
            else if (CB_ExportStyle.SelectedIndex == 4) // Custom 2
                custom2 = RTB_OPTIONS.Text;
            else if (CB_ExportStyle.SelectedIndex == 5) // Custom 3
                custom3 = RTB_OPTIONS.Text;
        }
        private void changeTableStatus(object sender, EventArgs e)
        {
            if (CB_ExportStyle.SelectedIndex == 3) // Custom 1
                custom1b = CHK_R_Table.Checked;
            else if (CB_ExportStyle.SelectedIndex == 4) // Custom 2
                custom2b = CHK_R_Table.Checked;
            else if (CB_ExportStyle.SelectedIndex == 5) // Custom 3
                custom3b = CHK_R_Table.Checked;
        }
        private void changeReadOnly(object sender, EventArgs e)
        {
            RichTextBox rtb = sender as RichTextBox;
            if (rtb.ReadOnly) rtb.BackColor = Color.FromKnownColor(KnownColor.Control);
            else rtb.BackColor = Color.FromKnownColor(KnownColor.White);
        }

        // Translation
        private void changeLanguage(object sender, EventArgs e)
        {
            InitializeStrings();
        }
        private string[] getStringList(string f, string l)
        {
            object txt = Properties.Resources.ResourceManager.GetObject("text_" + f + "_" + l); // Fetch File, \n to list.
            List<string> rawlist = ((string)txt).Split(new char[] { '\n' }).ToList();

            string[] stringdata = new string[rawlist.Count];
            for (int i = 0; i < rawlist.Count; i++)
                stringdata[i] = rawlist[i].Trim();
            return stringdata;
        }
        private void InitializeStrings()
        {
            string[] lang_val = { "en", "ja", "fr", "it", "de", "es", "ko" };
            string curlanguage = lang_val[CB_MainLanguage.SelectedIndex];

            string l = curlanguage;
            natures = getStringList("Natures", l);
            types = getStringList("Types", l);
            abilitylist = getStringList("Abilities", l);
            movelist = getStringList("Moves", l);
            itemlist = getStringList("Items", l);
            specieslist = getStringList("Species", l);
            formlist = getStringList("Forms", l);

            abilitylist[0] = itemlist[0] = movelist[0] = "(" + itemlist[0] + ")";

            int[] ballindex = {
                                  0,1,2,3,4,5,6,7,8,9,0xA,0xB,0xC,0xD,0xE,0xF,0x10,
                                  0x1EC,0x1ED,0x1EE,0x1EF,0x1F0,0x1F1,0x1F2,0x1F3,
                                  0x240 
                              };
            balls = new string[ballindex.Length];
            for (int i = 0; i < ballindex.Length; i++)
                balls[i] = itemlist[ballindex[i]];

            // vivillon pattern list
            vivlist = new string[20];
            vivlist[0] = formlist[666];
            for (int i = 1; i < 20; i++)
                vivlist[i] = formlist[835+i];
        }

        // Structs
        public class Structures
        {
            public struct PKX
            {
                public uint EC, PID, IV32,

                    exp,
                    HP_EV, ATK_EV, DEF_EV, SPA_EV, SPD_EV, SPE_EV,
                    HP_IV, ATK_IV, DEF_IV, SPE_IV, SPA_IV, SPD_IV,
                    cnt_cool, cnt_beauty, cnt_cute, cnt_smart, cnt_tough, cnt_sheen,
                    markings, hptype;

                public string
                    nicknamestr, notOT, ot, genderstring;

                public int
                    ability, abilitynum, nature, feflag, genderflag, altforms, PKRS_Strain, PKRS_Duration,
                    metlevel, otgender;

                public bool
                    isegg, isnick, isshiny;

                public ushort
                    species, helditem, TID, SID, TSV, ESV,
                    move1, move2, move3, move4,
                    move1_pp, move2_pp, move3_pp, move4_pp,
                    move1_ppu, move2_ppu, move3_ppu, move4_ppu,
                    eggmove1, eggmove2, eggmove3, eggmove4,
                    chk,

                    OTfriendship, OTaffection,
                    egg_year, egg_month, egg_day,
                    met_year, met_month, met_day,
                    eggloc, metloc,
                    ball, encountertype,
                    gamevers, countryID, regionID, dsregID, otlang;

                public PKX(byte[] pkx)
                {
                    nicknamestr = "";
                    notOT = "";
                    ot = "";
                    EC = BitConverter.ToUInt32(pkx, 0);
                    chk = BitConverter.ToUInt16(pkx, 6);
                    species = BitConverter.ToUInt16(pkx, 0x08);
                    helditem = BitConverter.ToUInt16(pkx, 0x0A);
                    TID = BitConverter.ToUInt16(pkx, 0x0C);
                    SID = BitConverter.ToUInt16(pkx, 0x0E);
                    exp = BitConverter.ToUInt32(pkx, 0x10);
                    ability = pkx[0x14];
                    abilitynum = pkx[0x15];
                    // 0x16, 0x17 - unknown
                    PID = BitConverter.ToUInt32(pkx, 0x18);
                    nature = pkx[0x1C];
                    feflag = pkx[0x1D] % 2;
                    genderflag = (pkx[0x1D] >> 1) & 0x3;
                    altforms = (pkx[0x1D] >> 3);
                    HP_EV = pkx[0x1E];
                    ATK_EV = pkx[0x1F];
                    DEF_EV = pkx[0x20];
                    SPA_EV = pkx[0x22];
                    SPD_EV = pkx[0x23];
                    SPE_EV = pkx[0x21];
                    cnt_cool = pkx[0x24];
                    cnt_beauty = pkx[0x25];
                    cnt_cute = pkx[0x26];
                    cnt_smart = pkx[0x27];
                    cnt_tough = pkx[0x28];
                    cnt_sheen = pkx[0x29];
                    markings = pkx[0x2A];
                    PKRS_Strain = pkx[0x2B] >> 4;
                    PKRS_Duration = pkx[0x2B] % 0x10;

                    // Block B
                    nicknamestr = TrimFromZero(Encoding.Unicode.GetString(pkx, 0x40, 24));
                    // 0x58, 0x59 - unused
                    move1 = BitConverter.ToUInt16(pkx, 0x5A);
                    move2 = BitConverter.ToUInt16(pkx, 0x5C);
                    move3 = BitConverter.ToUInt16(pkx, 0x5E);
                    move4 = BitConverter.ToUInt16(pkx, 0x60);
                    move1_pp = pkx[0x62];
                    move2_pp = pkx[0x63];
                    move3_pp = pkx[0x64];
                    move4_pp = pkx[0x65];
                    move1_ppu = pkx[0x66];
                    move2_ppu = pkx[0x67];
                    move3_ppu = pkx[0x68];
                    move4_ppu = pkx[0x69];
                    eggmove1 = BitConverter.ToUInt16(pkx, 0x6A);
                    eggmove2 = BitConverter.ToUInt16(pkx, 0x6C);
                    eggmove3 = BitConverter.ToUInt16(pkx, 0x6E);
                    eggmove4 = BitConverter.ToUInt16(pkx, 0x70);

                    // 0x72 - Super Training Flag - Passed with pkx to new form

                    // 0x73 - unused/unknown
                    IV32 = BitConverter.ToUInt32(pkx, 0x74);
                    HP_IV = IV32 & 0x1F;
                    ATK_IV = (IV32 >> 5) & 0x1F;
                    DEF_IV = (IV32 >> 10) & 0x1F;
                    SPE_IV = (IV32 >> 15) & 0x1F;
                    SPA_IV = (IV32 >> 20) & 0x1F;
                    SPD_IV = (IV32 >> 25) & 0x1F;
                    isegg = Convert.ToBoolean((IV32 >> 30) & 1);
                    isnick = Convert.ToBoolean((IV32 >> 31));

                    // Block C
                    notOT = TrimFromZero(Encoding.Unicode.GetString(pkx, 0x78, 24));
                    bool notOTG = Convert.ToBoolean(pkx[0x92]);
                    // Memory Editor edits everything else with pkx in a new form

                    // Block D
                    ot = TrimFromZero(Encoding.Unicode.GetString(pkx, 0xB0, 24));
                    // 0xC8, 0xC9 - unused
                    OTfriendship = pkx[0xCA];
                    OTaffection = pkx[0xCB]; // Handled by Memory Editor
                    // 0xCC, 0xCD, 0xCE, 0xCF, 0xD0
                    egg_year = pkx[0xD1];
                    egg_month = pkx[0xD2];
                    egg_day = pkx[0xD3];
                    met_year = pkx[0xD4];
                    met_month = pkx[0xD5];
                    met_day = pkx[0xD6];
                    // 0xD7 - unused
                    eggloc = BitConverter.ToUInt16(pkx, 0xD8);
                    metloc = BitConverter.ToUInt16(pkx, 0xDA);
                    ball = pkx[0xDC];
                    metlevel = pkx[0xDD] & 0x7F;
                    otgender = (pkx[0xDD]) >> 7;
                    encountertype = pkx[0xDE];
                    gamevers = pkx[0xDF];
                    countryID = pkx[0xE0];
                    regionID = pkx[0xE1];
                    dsregID = pkx[0xE2];
                    otlang = pkx[0xE3];

                    if (genderflag == 0)
                        genderstring = "♂";
                    else if (genderflag == 1)
                        genderstring = "♀";
                    else genderstring = "-";

                    hptype = (15 * ((HP_IV & 1) + 2 * (ATK_IV & 1) + 4 * (DEF_IV & 1) + 8 * (SPE_IV & 1) + 16 * (SPA_IV & 1) + 32 * (SPD_IV & 1))) / 63 + 1;

                    TSV = (ushort)((TID ^ SID) >> 4);
                    ESV = (ushort)(((PID >> 16) ^ (PID & 0xFFFF)) >> 4);

                    isshiny = (TSV == ESV);
                }
            }
        }

        private void B_BKP_SAV_Click(object sender, EventArgs e)
        {
            TextBox tb = TB_SAV;

            FileInfo fi = new FileInfo(tb.Text);
            DateTime dt = fi.LastWriteTime;
            int year = dt.Year;
            int month = dt.Month;
            int day = dt.Day;
            int hour = dt.Hour;
            int minute = dt.Minute;
            int second = dt.Second;

            string bkpdate = year.ToString("0000") + month.ToString("00") + day.ToString("00") + hour.ToString("00") + minute.ToString("00") + second.ToString("00") + " ";
            string newpath = bakpath + "\\" + bkpdate + fi.Name;
            if (File.Exists(newpath))
            {
                DialogResult sdr = MessageBox.Show("File already exists!\r\n\r\nOverwrite?", "Prompt", MessageBoxButtons.YesNo);
                if (sdr == DialogResult.Yes)
                    File.Delete(newpath);
                else 
                    return;
            }

            File.Copy(tb.Text, newpath);
            MessageBox.Show("Copied to Backup Folder.\r\n\r\nFile named:\r\n" + newpath, "Alert");
        }
        private void B_BKP_BV_Click(object sender, EventArgs e)
        {
            TextBox tb = TB_BV;

            FileInfo fi = new FileInfo(tb.Text);
            DateTime dt = fi.LastWriteTime;
            int year = dt.Year;
            int month = dt.Month;
            int day = dt.Day;
            int hour = dt.Hour;
            int minute = dt.Minute;
            int second = dt.Second;

            string bkpdate = year.ToString("0000") + month.ToString("00") + day.ToString("00") + hour.ToString("00") + minute.ToString("00") + second.ToString("00") + " ";
            string newpath = bakpath + "\\" + bkpdate + fi.Name;
            if (File.Exists(newpath))
            {
                DialogResult sdr = MessageBox.Show("File already exists!\r\n\r\nOverwrite?", "Prompt", MessageBoxButtons.YesNo);
                if (sdr == DialogResult.Yes)
                    File.Delete(newpath);
                else 
                    return;
            }

            File.Copy(tb.Text, newpath);
            MessageBox.Show("Copied to Backup Folder.\r\n\r\nFile named:\r\n" + newpath, "Alert");
        }

        private void B_BreakFolder_Click(object sender, EventArgs e)
        {
            byte[] savefile = new byte[0x100000];
            string savkeypath;
            foreach (string path in Directory.GetFiles(TB_Folder.Text))
            {
                // check to see if good input file
                long len = new FileInfo(path).Length;
                if (len != 0x100000 && len != 0x10009C)
                    continue;

                // Go ahead and load the save file into RAM...
                byte[] input = File.ReadAllBytes(path);
                Array.Copy(input, input.Length % 0x100000, savefile, 0, 0x100000);
                // Fetch Stamp
                ulong stamp = BitConverter.ToUInt64(savefile, 0x10);
                string keyfile = fetchKey(stamp, 0x80000);
                if (keyfile == "")
                    continue;
                else
                    savkeypath = keyfile;

                byte[] key = File.ReadAllBytes(keyfile);
                byte[] empty = new Byte[232];
                // Save file is already loaded.

                // Get our empty file set up.
                Array.Copy(key, 0x10, empty, 0xE0, 0x4);
                string nick = eggnames[empty[0xE3] - 1];
                // Stuff in the nickname to our blank EKX.
                byte[] nicknamebytes = Encoding.Unicode.GetBytes(nick);
                Array.Resize(ref nicknamebytes, 24);
                Array.Copy(nicknamebytes, 0, empty, 0x40, nicknamebytes.Length);

                // Fix CHK
                uint chk = 0;
                for (int i = 8; i < 232; i += 2) // Loop through the entire PKX
                    chk += BitConverter.ToUInt16(empty, i);

                // Apply New Checksum
                Array.Copy(BitConverter.GetBytes(chk), 0, empty, 06, 2);
                empty = encryptArray(empty);
                Array.Resize(ref empty, 0xE8);
                scanSAV(savefile, key, empty, false);
                File.WriteAllBytes(keyfile, key); // Key has been scanned for new slots, re-save key.
            }
            MessageBox.Show("Processed all files in folder...");
        }
    }
}

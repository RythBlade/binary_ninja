using binary_viewer.Script;
using binary_viewer.Spec;
using System;
using System.IO;
using System.Windows.Forms;

namespace binary_viewer
{
    public partial class MainWindow : Form
    {
        private FileSpec m_fileSpec;

        private string m_scriptBuffer;

        private byte[] m_targetFileBuffer;

        private string m_loadedSpecFileName;
        private string m_loadedTargetFileName;

        public MainWindow()
        {
            m_fileSpec = null;
            m_scriptBuffer = string.Empty;
            m_targetFileBuffer = null;
            m_loadedSpecFileName = string.Empty;
            m_loadedTargetFileName = string.Empty;

            // setup the window text
            Text = "Binary Ninja";

            InitializeComponent();
        }

        private void openFileSpecMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = fileSpecOpenFileDialogue.ShowDialog();

            if( result == DialogResult.OK )
            {
                m_loadedSpecFileName = fileSpecOpenFileDialogue.FileName;
            }
            else
            {
                m_loadedSpecFileName = string.Empty;
            }

            LoadScriptFile();
        }

        private void openTargetFileMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = targetFileOpenFileDialogue.ShowDialog();

            if (result == DialogResult.OK)
            {
                m_loadedTargetFileName = targetFileOpenFileDialogue.FileName;
            }
            else
            {
                m_loadedTargetFileName = string.Empty;
            }

            LoadTargetFile();
        }

        private void refreshDataMenuItem_Click(object sender, EventArgs e)
        {
            ResetAllLoadedData();
            LoadScriptFile();
            LoadTargetFile();
        }

        private void writeTestItemMenuItem_Click_1(object sender, EventArgs e)
        {
            DialogResult result = saveTestFileDialogue.ShowDialog();

            if (result == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(saveTestFileDialogue.FileName))
                {
                    using (BinaryWriter binaryWriter = new BinaryWriter(File.Open(saveTestFileDialogue.FileName, FileMode.Create)))
                    {
                        uint testValueOffset = 36; binaryWriter.Write(testValueOffset);
                        testValueOffset = 52; binaryWriter.Write(testValueOffset);

                        testValueOffset = 555; binaryWriter.Write(testValueOffset);
                        testValueOffset = 555; binaryWriter.Write(testValueOffset);
                        testValueOffset = 555; binaryWriter.Write(testValueOffset);
                        testValueOffset = 555; binaryWriter.Write(testValueOffset);
                        testValueOffset = 555; binaryWriter.Write(testValueOffset);
                        testValueOffset = 555; binaryWriter.Write(testValueOffset);
                        testValueOffset = 555; binaryWriter.Write(testValueOffset);

                        float value = 1.0f;  binaryWriter.Write(value);
                        testValueOffset = 16; binaryWriter.Write(testValueOffset);
                        testValueOffset = 555; binaryWriter.Write(testValueOffset);
                        testValueOffset = 555; binaryWriter.Write(testValueOffset);
                        value = 2.0f; binaryWriter.Write(value);
                        value = 3.0f; binaryWriter.Write(value);
                        testValueOffset = 16; binaryWriter.Write(testValueOffset);
                        testValueOffset = 555; binaryWriter.Write(testValueOffset);
                        testValueOffset = 555; binaryWriter.Write(testValueOffset);
                        value = 4.0f; binaryWriter.Write(value);

                        int nextValue = 5; binaryWriter.Write(nextValue);
                        testValueOffset = 16; binaryWriter.Write(testValueOffset);
                        testValueOffset = 555; binaryWriter.Write(testValueOffset);
                        testValueOffset = 555; binaryWriter.Write(testValueOffset);
                        nextValue = 6; binaryWriter.Write(nextValue);
                        nextValue = 7; binaryWriter.Write(nextValue);
                        testValueOffset = 16; binaryWriter.Write(testValueOffset);
                        testValueOffset = 555; binaryWriter.Write(testValueOffset);
                        testValueOffset = 555; binaryWriter.Write(testValueOffset);
                        nextValue = 8; binaryWriter.Write(nextValue);
                        nextValue = 9; binaryWriter.Write(nextValue);
                        testValueOffset = 16; binaryWriter.Write(testValueOffset);
                        testValueOffset = 555; binaryWriter.Write(testValueOffset);
                        testValueOffset = 555; binaryWriter.Write(testValueOffset);
                        nextValue = 9; binaryWriter.Write(nextValue);

                        string outputString = "Some test string";
                        binaryWriter.Write(outputString);

                        binaryWriter.Close();
                    }
                }
                else
                {
                    WriteMessageToErrorOutputWindow($"Opened target file: {m_loadedTargetFileName}");
                    MessageBox.Show("Filename was empty. Couldn't write out the test file!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void toHardCodedTestToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            // clear everything for the hard coded test!
            ResetAllLoadedData();

            DialogResult result = fileSpecOpenFileDialogue.ShowDialog();

            if (result == DialogResult.OK)
            {
                m_loadedSpecFileName = fileSpecOpenFileDialogue.FileName;

                // write out the hard coded buffer
                int lengthOfFakeBuffer = 500;
                m_targetFileBuffer = new byte[lengthOfFakeBuffer];

                using (MemoryStream stream = new MemoryStream(m_targetFileBuffer, 0, lengthOfFakeBuffer))
                {
                    using (BinaryWriter binaryWriter = new BinaryWriter(stream))
                    {
                        int nextInt = 0;
                        nextInt = -0; binaryWriter.Write(nextInt);
                        nextInt = 1; binaryWriter.Write(nextInt);
                        nextInt = -2; binaryWriter.Write(nextInt);
                        nextInt = -3; binaryWriter.Write(nextInt);
                        nextInt = 4; binaryWriter.Write(nextInt);

                        float nextFloat = 10.0f;
                        nextFloat = 10.0f; binaryWriter.Write(nextFloat);
                        nextFloat = 11.0f; binaryWriter.Write(nextFloat);
                        nextFloat = 12.0f; binaryWriter.Write(nextFloat);
                        nextFloat = 13.0f; binaryWriter.Write(nextFloat);
                        nextFloat = 14.0f; binaryWriter.Write(nextFloat);

                        for( int i = 0; i < 2; ++i)
                        {
                            double nextDouble = 3.0; binaryWriter.Write(nextDouble);

                            uint nextUint = 55; binaryWriter.Write(nextUint);

                            long nextLong = -56; binaryWriter.Write(nextLong);
                            ulong nextULong = 57; binaryWriter.Write(nextULong);

                            char nextChar = 'c'; binaryWriter.Write(Convert.ToByte(nextChar));

                            int arrayLength = 7; binaryWriter.Write(arrayLength);

                            // dynamic length array
                            nextChar = 'm'; binaryWriter.Write(Convert.ToByte(nextChar));
                            nextChar = 'u'; binaryWriter.Write(Convert.ToByte(nextChar));
                            nextChar = 'z'; binaryWriter.Write(Convert.ToByte(nextChar));
                            nextChar = 'z'; binaryWriter.Write(Convert.ToByte(nextChar));
                            nextChar = 'i'; binaryWriter.Write(Convert.ToByte(nextChar));
                            nextChar = 'l'; binaryWriter.Write(Convert.ToByte(nextChar));
                            nextChar = 'y'; binaryWriter.Write(Convert.ToByte(nextChar));

                            // fixed length array
                            nextChar = 'd'; binaryWriter.Write(Convert.ToByte(nextChar));
                            nextChar = 'e'; binaryWriter.Write(Convert.ToByte(nextChar));
                            nextChar = 'f'; binaryWriter.Write(Convert.ToByte(nextChar));
                            nextChar = 'g'; binaryWriter.Write(Convert.ToByte(nextChar));
                            nextChar = 'h'; binaryWriter.Write(Convert.ToByte(nextChar));
                            nextChar = 'i'; binaryWriter.Write(Convert.ToByte(nextChar));
                            nextChar = 'j'; binaryWriter.Write(Convert.ToByte(nextChar));
                            nextChar = 'k'; binaryWriter.Write(Convert.ToByte(nextChar));
                            nextChar = 'l'; binaryWriter.Write(Convert.ToByte(nextChar));
                            nextChar = 'm'; binaryWriter.Write(Convert.ToByte(nextChar));
                        }
                    }
                }

                LoadScriptFile();
            }
        }

        private void ResetAllLoadedData()
        {
            m_fileSpec = null;
            m_scriptBuffer = string.Empty;
            m_targetFileBuffer = null;
            outputWindowTextBox.Text = string.Empty;
            consoleOutputWindow.Text = string.Empty;
            fileDisplayPropertyGrid.SelectedObject = null;
            scriptViewTextBox.Text = string.Empty;

            Text = "Binary Ninja";
        }

        private void LoadTargetFile()
        {
            if (!string.IsNullOrEmpty(m_loadedTargetFileName))
            {
                using (FileStream fileStream = File.OpenRead(m_loadedTargetFileName))
                {
                    m_targetFileBuffer = new byte[fileStream.Length];
                    fileStream.Read(m_targetFileBuffer, 0, m_targetFileBuffer.Length);
                }
                
                WriteMessageToErrorOutputWindow($"Opened target file: {m_loadedTargetFileName}");

                FinaliseLoad();
            }
            else
            {
                WriteMessageToErrorOutputWindow("Filename was empty. No target file loaded!!");
                MessageBox.Show("Filename was empty. No target file loaded!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadScriptFile()
        {
            if (!string.IsNullOrEmpty(m_loadedSpecFileName))
            {
                m_scriptBuffer = string.Empty;

                using (StreamReader newFile = File.OpenText(m_loadedSpecFileName))
                {
                    m_scriptBuffer = newFile.ReadToEnd();
                }
                
                WriteMessageToErrorOutputWindow($"Opened script file: {m_loadedSpecFileName}");

                FinaliseLoad();
            }
            else
            {
                WriteMessageToErrorOutputWindow("Filename was empty. No script file loaded!!");
                MessageBox.Show("Filename was empty. No script file loaded!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FinaliseLoad()
        {
            Text = $"Binar Ninja - [Target File: {Path.GetFileName(m_loadedTargetFileName)}] - [Target Script: {Path.GetFileName(m_loadedSpecFileName)}]";

            if (m_targetFileBuffer != null && !string.IsNullOrEmpty(m_loadedSpecFileName))
            {
                ScriptConsumer consumer = null;

                //try
                {
                    // create everything!
                    m_fileSpec = new FileSpec(m_targetFileBuffer, 0);
                    consumer = new ScriptConsumer(m_scriptBuffer, m_fileSpec);

                    // this is where the magic happens - read the script and create a view onto the file!
                    consumer.ParseScript();

                    // output the results of the script!
                    outputWindowTextBox.Text = m_fileSpec.PrintFile();
                    fileDisplayPropertyGrid.SelectedObject = m_fileSpec;
                    scriptViewTextBox.Text = m_scriptBuffer;

                    WriteMessageToErrorOutputWindow(consumer.ErrorOutput);
                }
                /*catch (Exception ex)
                {
                    if (consumer != null)
                    {
                        WriteMessageToErrorOutputWindow(consumer.ErrorOutput);
                    }
#if DEBUG
                    throw ex;
#else
                    WriteMessageToErrorOutputWindow($"Ok...there was an exception parsing the script file: {ex.Message}");
                    MessageBox.Show($"Ok...there was an exception parsing the script file: {ex.Message}");
#endif
                }*/
            }
        }

        private void WriteMessageToErrorOutputWindow(string stringToWrite)
        {
            consoleOutputWindow.Text += stringToWrite + "\n";
        }
    }
}

﻿using binary_viewer.Script;
using binary_viewer.Spec;
using binary_viewer.Threading;
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
        private LoadingDialogue m_newLoadingDialogue;

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

        private void newLoadingDialogue_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_newLoadingDialogue = null;
            MainMenuStrip.Enabled = true;
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
                        float value = 1.0f;  binaryWriter.Write(value);
                        value = 2.0f; binaryWriter.Write(value);
                        value = 3.0f; binaryWriter.Write(value);
                        value = 4.0f; binaryWriter.Write(value);

                        int nextValue = 5; binaryWriter.Write(nextValue);
                        nextValue = 6; binaryWriter.Write(nextValue);
                        nextValue = 7; binaryWriter.Write(nextValue);
                        nextValue = 8; binaryWriter.Write(nextValue);
                        nextValue = 9; binaryWriter.Write(nextValue);
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
                TargetFileLoadPayLoad targetFilePayload = new TargetFileLoadPayLoad();
                targetFilePayload.TargetFileName = m_loadedTargetFileName;
                
                ShowLoadingDialogue("Loading target file...");

                backgroundWorker.RunWorkerAsync(targetFilePayload);
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

                ScriptFilePayLoad scriptPayload = new ScriptFilePayLoad();
                scriptPayload.ScriptFileName = m_loadedSpecFileName;
                
                ShowLoadingDialogue("Loading script file...");

                backgroundWorker.RunWorkerAsync(scriptPayload);
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
                //try
                {
                    FileSpec fileSpec = new FileSpec(m_targetFileBuffer, 0);
                    ScriptConsumer consumer = new ScriptConsumer(m_scriptBuffer, fileSpec);
                    ParserPayload workerPayload = new ParserPayload(fileSpec, consumer);

                    ShowLoadingDialogue("Parsing the file...");

                    backgroundWorker.RunWorkerAsync(workerPayload);
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

        private void showSpinnerToolStripMenuItem_Click(object sender, EventArgs e)
        {
//             if (m_targetFileBuffer != null && !string.IsNullOrEmpty(m_loadedSpecFileName))
//             {
//                 ShowLoadingDialogue
// 
//                 FileSpec fileSpec = new FileSpec(m_targetFileBuffer, 0);
//                 ScriptConsumer consumer = new ScriptConsumer(m_scriptBuffer, fileSpec);
//                 ParserPayload workerPayload = new ParserPayload(fileSpec, consumer);
// 
//                 m_newLoadingDialogue.Show(this);
// 
//                 backgroundWorker.RunWorkerAsync(workerPayload);
//             }
//             else
//             {
//                 MessageBox.Show("Some error");
//             }
        }

        private void backgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            ParserPayload parserPayload = e.Argument as ParserPayload;
            ScriptFilePayLoad scriptFilePayload = e.Argument as ScriptFilePayLoad;
            TargetFileLoadPayLoad targetFilePayload = e.Argument as TargetFileLoadPayLoad;

            if (parserPayload != null)
            {
                //try
                {
                    // this is where the magic happens - read the script and create a view onto the file!
                    parserPayload.ScriptConsumerToUse.ParseScript();

                    e.Result = parserPayload;
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
            else if (scriptFilePayload != null)
            {
                using (StreamReader newFile = File.OpenText(scriptFilePayload.ScriptFileName))
                {
                    scriptFilePayload.ScriptBuffer = newFile.ReadToEnd();
                    e.Result = scriptFilePayload;
                }
            }
            else if (targetFilePayload != null)
            {
                using (FileStream fileStream = File.OpenRead(targetFilePayload.TargetFileName))
                {
                    targetFilePayload.TargetFileBuffer = new byte[fileStream.Length];
                    fileStream.Read(targetFilePayload.TargetFileBuffer, 0, targetFilePayload.TargetFileBuffer.Length);
                    e.Result = targetFilePayload;
                }
            }
            
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            ParserPayload parserPayload = e.Result as ParserPayload;
            ScriptFilePayLoad scriptFilePayload = e.Result as ScriptFilePayLoad;
            TargetFileLoadPayLoad targetFilePayload = e.Result as TargetFileLoadPayLoad;

            if (parserPayload != null)
            {
                // output the results of the script!
                m_fileSpec = parserPayload.FileSpecToPopulate;
                outputWindowTextBox.Text = parserPayload.FileSpecToPopulate.PrintFile();
                fileDisplayPropertyGrid.SelectedObject = parserPayload.FileSpecToPopulate;
                scriptViewTextBox.Text = m_scriptBuffer;

                WriteMessageToErrorOutputWindow(parserPayload.ScriptConsumerToUse.ErrorOutput);
            }
            else if (scriptFilePayload != null)
            {
                m_scriptBuffer = scriptFilePayload.ScriptBuffer;

                WriteMessageToErrorOutputWindow($"Opened script file: {scriptFilePayload.ScriptFileName}");

                FinaliseLoad();
            }
            else if (targetFilePayload != null)
            {
                m_targetFileBuffer = targetFilePayload.TargetFileBuffer;

                WriteMessageToErrorOutputWindow($"Opened target file: {targetFilePayload.TargetFileName}");
                FinaliseLoad();
            }
            else
            {
                m_fileSpec = null;
                outputWindowTextBox.Text = string.Empty;
                fileDisplayPropertyGrid.SelectedObject = null;
                scriptViewTextBox.Text = string.Empty;

                WriteMessageToErrorOutputWindow("It went really wrong!");
            }

            HideLoadingDialogue();
        }

        private void ShowLoadingDialogue(string loadingMessage)
        {
            if (m_newLoadingDialogue != null)
            {
                m_newLoadingDialogue.CancelCloseLoading();
                m_newLoadingDialogue.LoadingText = loadingMessage;
            }
            else
            {
                m_newLoadingDialogue = new LoadingDialogue();
                m_newLoadingDialogue.FormClosed += newLoadingDialogue_FormClosed;

                m_newLoadingDialogue.LoadingText = loadingMessage;

                MainMenuStrip.Enabled = false;

                m_newLoadingDialogue.Show(this);
            }
        }

        private void HideLoadingDialogue()
        {
            if (m_newLoadingDialogue != null)
            {
                m_newLoadingDialogue.TriggerCloseLoading();
            }
        }
    }
}
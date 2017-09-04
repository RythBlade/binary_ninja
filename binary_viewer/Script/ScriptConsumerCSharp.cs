using binary_viewer.Spec;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;

namespace binary_viewer.Script
{
    public class ScriptConsumerCSharp : IScriptConsumer
    {
        private string scriptText = string.Empty;
        private FileSpec fileSpec = null;
        private string errorText = string.Empty;
        private List<string> errorsFound = new List<string>();

        string IScriptConsumer.ErrorOutput
        {
            get { return errorText; }
            set { errorText = value; }
        }

        public List<string> Errors
        {
            get { return errorsFound; }
        }
        
        public ScriptConsumerCSharp(string scriptToConsume, FileSpec fileSpecToPopulate)
        {
            scriptText = scriptToConsume;
            fileSpec = fileSpecToPopulate;
        }

        void IScriptConsumer.ParseScript()
        {
            // clear up error output
            errorsFound.Clear();
            errorText = string.Empty;

            // create our compiler + includes and compile
            CodeDomProvider csc = CodeDomProvider.CreateProvider("CSharp");
            CompilerParameters parameters = new CompilerParameters(new[] { "System.Core.dll" }, "", true);

            parameters.GenerateInMemory = true;
            parameters.GenerateExecutable = false;

            CompilerResults results = csc.CompileAssemblyFromSource(parameters, scriptText);

            // process result - output errors or populate the file spec
            if (results.Errors.HasErrors)
            {
                errorText = string.Empty;

                IEnumerable<CompilerError> foundErrors = results.Errors.Cast<CompilerError>();

                foreach(CompilerError error in foundErrors)
                {
                    errorText += $"{error.ErrorText}\r\n";
                    errorsFound.Add(error.ErrorText);
                }
            }
            else
            {
                

                /*foreach (Type foundType in results.CompiledAssembly.ExportedTypes)
                {
                    statusTextBox.Text += $"class {foundType.Name}\r\n";

                    foreach (FieldInfo foundField in foundType.GetFields())
                    {
                        statusTextBox.Text += "Found parameter: " + foundField.FieldType.Name + " - " + foundField.Name + "\r\n";

                    }
                }*/
            }
        }
    }
}

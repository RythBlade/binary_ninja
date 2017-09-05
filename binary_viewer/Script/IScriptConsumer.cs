using System.Collections.Generic;

namespace binary_viewer.Script
{
    public interface IScriptConsumer
    {
        string ErrorOutput { get; set; }

        List<string> FoundErrors { get; }

        void ParseScript();
    }
}
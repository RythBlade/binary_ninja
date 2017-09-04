namespace binary_viewer.Script
{
    public interface IScriptConsumer
    {
        string ErrorOutput { get; set; }

        void ParseScript();
    }
}
using binary_viewer.Script;
using binary_viewer.Spec;

namespace binary_viewer.Threading
{
    public class ParserPayload
    {
        public FileSpec FileSpecToPopulate { get; set; }
        public ScriptConsumerManual ScriptConsumerToUse { get; set; }

        public ParserPayload(FileSpec fileSpec, ScriptConsumerManual scriptConsumer)
        {
            FileSpecToPopulate = fileSpec;
            ScriptConsumerToUse = scriptConsumer;
        }
    }
}

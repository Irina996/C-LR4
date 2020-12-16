namespace ConfigurationManager
{
    public class Options
    {
        public string Target { get; set; }

        public string Source { get; set; }

        public string Archive { get; set; }

        public Options()
        {
            Target = @"G:\SourceDirectory";

            Source = @"G:\TargetDirectory";

            Archive = @"G:\TargetDirectory\Archive";
        }

    }
}

using System.ComponentModel;
using FubuCore;
using StoryTeller.Workspace;
using FileSystem = FubuCore.FileSystem;

namespace StoryTeller.CommandLine
{
    public class RunInput
    {
        [Description("Path to the StoryTeller project file or the project directory")]
        public string Path { get; set; }

        [Description("Path to write out the results")]
        public string ResultsPath { get; set; }

        [Description("Optional.  Runs only one workspace")]
        public string WorkspaceFlag { get; set; }

        [Description("Specify a compile target")]
        public string CompileFlag { get; set; }

        [Description("Storyteller test mode profile for systems like Serenity that use this")]
        public string ProfileFlag { get; set; }

        public Project LoadProject()
        {
            Project project = null;

            // The IsFile check blows up if it doesn't exist at all
            if (new FileSystem().IsFile(Path))
            {
                project = Project.LoadFromFile(Path);
            }
            else
            {
                project = Project.ForDirectory(Path);
            }

            if (ProfileFlag.IsNotEmpty())
            {
                project.Profile = ProfileFlag;
            }

            if (CompileFlag.IsNotEmpty())
            {
                project.CompileTarget = CompileFlag;
            }

            return project;
        }
    }
}
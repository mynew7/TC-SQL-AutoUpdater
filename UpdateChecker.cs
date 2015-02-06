using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibGit2Sharp;
using System.Diagnostics;

namespace Autoupdater
{
    public class UpdateChecker
    {
        string RepoPath;

        public UpdateChecker(string repoPath)
        {
            this.RepoPath = repoPath;
        }

        public List<string> GetNewUpdates(string currentSHA)
        {
            var result = new List<string>();
            Log("Opening repo at \"" + RepoPath + "\"");
            using (var repo = new Repository(RepoPath))
            {
                var repoCommit = repo.Commits.First();
                Log("Current repo commit: " + repoCommit.Sha.Substring(0, 7) + ": " + repoCommit.MessageShort);
                if (repoCommit.Sha == currentSHA)
                {
                    Log("Repo is on same commit as passed argument, no updates found");
                    return result;
                }

                var currentCommit = repo.Lookup<Commit>(currentSHA);
                Log("Passed commit: " + currentCommit.Sha.Substring(0, 7) + ": " + currentCommit.MessageShort);

                var changes = repo.Diff.Compare<TreeChanges>(currentCommit.Tree, repoCommit.Tree, new []{"sql/updates"});
                Log("Found " + changes.Count() + " changes");
                foreach (var added in changes.Added)
                {
                    Log("Added file: " + added.Path);
                    result.Add(added.Path);
                }
            }

            return result;
        }

        [Conditional("DEBUG")]
        void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}

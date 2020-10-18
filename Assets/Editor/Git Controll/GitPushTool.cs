using UnityEditor;
using UnityEngine;
using UnityEditor.Build.Reporting;

public class GitPushTool : MonoBehaviour
{
    /// <summary>
    /// Pushes changes to git. Massage of commit is promted.
    /// </summary>
    [MenuItem("Git/Push")]
    public static void Push()
    {
        if (Git.Diff != "")
        {
            Debug.Log(Git.Add);
            Debug.Log(Git.Commit);
            Debug.Log(Git.Push);

            // update version
            PlayerSettings.bundleVersion = Git.BuildVersion;
        }
    }

    /// <summary>
    /// Pushes changes to git. Commit message is "QC of vX.Y.Z"
    /// </summary>
    [MenuItem("Git/QuickPush")]
    public static void QuickPush()
    {
        if (Git.Diff != "")
        {
            Debug.Log(Git.Add);
            Debug.Log(Git.QuickCommit);
            Debug.Log(Git.Push);

            // update version
            PlayerSettings.bundleVersion = Git.BuildVersion;
        }
    }

    /// <summary>
    /// Pushes and changing minor version value.
    /// </summary>
    [MenuItem("Git/Tag New Major Version")]
    public static void MajorTag()
    {
        Push();

        // creating current version
        string[] version = PlayerSettings.bundleVersion.Split('.');
        int majorVersion = int.Parse(version[0]);
        int minorVersion = int.Parse(version[1]);
        string tag = "v" + (majorVersion+1) + "." + 0;

        // new tag
        Debug.Log(Git.Tag(tag, PlayerSettings.bundleVersion));
        Debug.Log(Git.PushTags);

        // update version
        PlayerSettings.bundleVersion = Git.BuildVersion;
    }

    /// <summary>
    /// Pushes and changing minor version value.
    /// </summary>
    [MenuItem("Git/Tag New Minor Version")]
    public static void MinorTag()
    {
        Push();

        // creating current version
        string[] version = PlayerSettings.bundleVersion.Split('.');
        int majorVersion = int.Parse(version[0]);
        int minorVersion = int.Parse(version[1]);
        string tag = "v" + majorVersion + "." + (minorVersion + 1);

        // new tag
        Debug.Log(Git.Tag(tag, PlayerSettings.bundleVersion));
        Debug.Log(Git.PushTags);

        // update version
        PlayerSettings.bundleVersion = Git.BuildVersion;
    }

    /*[MenuItem("Build/Build with Version")]
    public static void MyBuild()
    {
        // This gets the Build Version from Git via the `git describe` command
        PlayerSettings.bundleVersion = Git.BuildVersion;

        // ===== This sample is taken from the Unity scripting API here:
        // https://docs.unity3d.com/ScriptReference/BuildPipeline.BuildPlayer.html
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = new[] { "Assets/Scene1.unity", "Assets/Scene2.unity" };
        buildPlayerOptions.locationPathName = "Builds";
        buildPlayerOptions.target = BuildTarget.StandaloneWindows;
        buildPlayerOptions.options = BuildOptions.None;

        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Build succeeded: " + summary.totalSize + " bytes");
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Build failed");
        }
    }*/
}
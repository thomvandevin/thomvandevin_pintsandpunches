using System.Diagnostics;
using UnityEngine;
using UnityEditor;

/// <summary>
/// This class provides context menus for running tortoiseSVN commands on the project without leaving the editor. there is a public static command to run tortoiseSVN commands anywhere you need too.
/// </summary>
public class TortoiseSvn
{

    /// <summary>
    /// Executes a tortoiseSVN command.
    /// </summary>
    /// <param name="command">the command to execute. this command is one of the commands of the tortoiseproc.exe automation commands that you add after /command:. a list of them can be found in tortoiseSVN's help file in automating tortoiseSVN topic</param>
    /// <param name="path">the path that your command should be executed on. pass "" (i.e empty string) for pathing nothing</param>
    public static void SVNCommand(string command, string path)
    {
        string c = "/c tortoiseproc.exe /command:{0} /path:\"{1}\"";
        c = string.Format(c, command, path);
        ProcessStartInfo info = new ProcessStartInfo("cmd.exe", c);
        info.WindowStyle = ProcessWindowStyle.Hidden;
        Process.Start(info);
    }

    //adding functions for different items.

    /// <summary>
    /// commits the project root
    /// </summary
    [MenuItem("Assets/SVN/Commit All...")]
    static void CommitRoot()
    {
        SVNCommand("commit", Application.dataPath + "/../..");
    }

    /// <summary>
    /// commits the selection.
    /// </summary>
    [MenuItem("Assets/SVN/Commit...")]
    static void CommitSelection()
    {
        SVNCommand("commit", GetPathFromSelectionWithMetas());
    }

    [MenuItem("Assets/SVN/Commit...",true)]
    static bool CommitSelectionValidation()
    {
        return IsAnythingSelected();
    }

    /// <summary>
    /// shows the Repo Browser
    /// </summary>
    [MenuItem("Assets/SVN/Repo Browser")]
    static void ShowRepoBrowser()
    {
        SVNCommand("repobrowser", Application.dataPath + "/../..");
    }

    /// <summary>
    /// Updates the whole project
    /// </summary>
    [MenuItem("Assets/SVN/Update All...")]
    static void UpdateAll()
    {
        SVNCommand("update", Application.dataPath + "/../..");
    }

	/// <summary>
    /// Updates the selected file/folder
    /// </summary>
    [MenuItem("Assets/SVN/Update...")]
    static void UpdateSelection()
    {
        SVNCommand("update", GetPathFromSelectionWithMetas());
    }

    [MenuItem("Assets/SVN/Update...",true)]
    static bool UpdateSelectionValidation()
    {
        return IsAnythingSelected();
    }
	
	/// <summary>
    /// Locks the selected file/directory
    /// </summary>
    [MenuItem("Assets/SVN/Lock...")]
    static void LockSelection()
    {
        SVNCommand("lock", GetPathFromSelectionWithMetas());
    }

    [MenuItem("Assets/SVN/Lock...",true)]
    static bool LockSelectionValidation()
    {
        return IsAnythingSelected();
    }
	
	/// <summary>
    /// Unlocks the selected file/directory
    /// </summary>
    [MenuItem("Assets/SVN/Unlock...")]
    static void UnlockSelection()
    {
        SVNCommand("unlock", GetPathFromSelectionWithMetas());
    }

    [MenuItem("Assets/SVN/Unlock...",true)]
    static bool UnlockSelectionValidation()
    {
        return IsAnythingSelected();
    }
	
    /// <summary>
    /// execute add on the root.
    /// </summary>
    [MenuItem("Assets/SVN/Add All...")]
    static void AddAll()
    {
        SVNCommand("add", Application.dataPath + "/../..");
    }

    /// <summary>
    /// adds the selected item to repo.
    /// </summary>
    [MenuItem("Assets/SVN/Add...")]
    static void AddSelection()
    {
        SVNCommand("add", GetPathFromSelectionWithMetas());
    }

    [MenuItem("Assets/SVN/Add...", true)]
    static bool AddSelectionValidation()
    {
        return IsAnythingSelected();
    }

    /// <summary>
    /// Renames selected items using TortoiseSVN>Rename
    /// </summary>
    [MenuItem("Assets/SVN/Rename...")]
    static void RenameSelection()
    {
        SVNCommand("rename /noquestion", GetPathFromSelectionWithMetas());
    }

    [MenuItem("Assets/SVN/Rename...",true)]
    static bool RenameSelectionValidation()
    {
        return IsAnythingSelected();
    }
    
    /// <summary>
    /// Deletes selected items.
    /// </summary>
    [MenuItem("Assets/SVN/Delete...")]
    static void DeleteSelection()
    {
        SVNCommand("remove", GetPathFromSelectionWithMetas());
    }

    [MenuItem("Assets/SVN/Delete...", true)]
    static bool DeleteSelectionValidation()
    {
        return IsAnythingSelected();
    }

    /// <summary>
    /// opens the project folder.
    /// </summary>
    [MenuItem("Assets/SVN/Open Project Folder")]
    static void OpenProjectFolder()
    {
        ProcessStartInfo info = new ProcessStartInfo(Application.dataPath + "/../");
        info.UseShellExecute = true;
        Process.Start(info);
    }


    /// <summary>
    /// gets the path of the selected object in unity editor if applicable. only objects in projectview have physical locations
    /// </summary>
    /// <returns>path of the selected object if something from projectview is selected, otherwise null or empty string</returns>
    static string GetPathFromSelection()
    {
        string p = "";
        foreach (UnityEngine.Object o in Selection.objects)
        {
            if (string.IsNullOrEmpty(AssetDatabase.GetAssetPath(o)) == false)
            {
                if (p != "")
                    p += "*";
                p += Application.dataPath + "/../" + AssetDatabase.GetAssetPath(o);
            }
        }
        return p;
    }

    /// <summary>
    /// Gets the path of the selected objects in unity editor with their meta files if applicable. only objects in projectview have physical locations
    /// </summary>
    /// <returns>path of the selected object if something from projectview is selected, otherwise null or empty string</returns>
    static string GetPathFromSelectionWithMetas()
    {
        string p = "";
        foreach (UnityEngine.Object o in Selection.objects)
        {
            if (string.IsNullOrEmpty(AssetDatabase.GetAssetPath(o)) == false)
            {
                if (p != "")
                    p += "*";
                p += Application.dataPath + "/../" + AssetDatabase.GetAssetPath(o);
                p += "*" + Application.dataPath + "/../" + AssetDatabase.GetAssetPath(o) + ".meta";
            }
        }
        return p;
    }

    /// <summary>
    /// returns if anything is selected that has a physical location or not.
    /// </summary>
    /// <returns>if an item in project view is selected in editor, returns true. otherwise false.</returns>
    static bool IsAnythingSelected()
    {
        if (GetPathFromSelection() != "")
            return true;
        return false;
    }
}
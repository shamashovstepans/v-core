using System.IO;
using UnityEditor;
using UnityEngine;

namespace Core.Scopes.Cheats.Editor
{
    internal static class OpenStateDirMenuItem
    {
        [MenuItem("Window/Open State Dir")]
        private static void OpenStateDir()
        {
            var path = Path.Combine(Application.persistentDataPath, "State");
            Directory.CreateDirectory(path);
            EditorUtility.RevealInFinder(path);
        }
    }
}

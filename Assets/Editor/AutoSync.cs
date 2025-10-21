using UnityEngine;
using UnityEditor;
using System.Diagnostics;
using System.IO;

[InitializeOnLoad]
public class AutoSync : AssetPostprocessor
{
    static AutoSync()
    {
        EditorApplication.update += OnEditorUpdate;
    }

    static void OnEditorUpdate()
    {
        // 每5秒检查一次是否有未提交的更改
        if (EditorApplication.timeSinceStartup % 5 < 0.1f)
        {
            CheckForChanges();
        }
    }

    static void CheckForChanges()
    {
        // 检查Git状态
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = "git",
            Arguments = "status --porcelain",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            CreateNoWindow = true,
            WorkingDirectory = Application.dataPath + "/.."
        };

        try
        {
            using (Process process = Process.Start(startInfo))
            {
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                if (!string.IsNullOrEmpty(output.Trim()))
                {
                    UnityEngine.Debug.Log("🔄 检测到文件变化，准备自动同步...");
                    AutoSyncToGitHub();
                }
            }
        }
        catch (System.Exception e)
        {
            UnityEngine.Debug.LogWarning($"自动同步检查失败: {e.Message}");
        }
    }

    static void AutoSyncToGitHub()
    {
        string projectPath = Application.dataPath + "/..";
        
        try
        {
            // 执行自动同步脚本
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "bash",
                Arguments = "./auto-sync.sh",
                UseShellExecute = false,
                CreateNoWindow = true,
                WorkingDirectory = projectPath
            };

            using (Process process = Process.Start(startInfo))
            {
                process.WaitForExit();
                
                if (process.ExitCode == 0)
                {
                    UnityEngine.Debug.Log("✅ 自动同步成功！");
                }
                else
                {
                    UnityEngine.Debug.LogWarning("⚠️ 自动同步失败，请手动检查");
                }
            }
        }
        catch (System.Exception e)
        {
            UnityEngine.Debug.LogError($"自动同步执行失败: {e.Message}");
        }
    }

    // 当资源保存时触发
    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        if (importedAssets.Length > 0 || deletedAssets.Length > 0 || movedAssets.Length > 0)
        {
            UnityEngine.Debug.Log("📁 资源文件发生变化，准备同步...");
            
            // 延迟2秒后同步，避免频繁操作
            EditorApplication.delayCall += () => {
                System.Threading.Thread.Sleep(2000);
                AutoSyncToGitHub();
            };
        }
    }
}

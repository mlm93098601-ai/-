using UnityEngine;
using UnityEditor;
using System.Diagnostics;
using System.IO;
using System.Threading;

[InitializeOnLoad]
public class TeamSync : AssetPostprocessor
{
    private static bool isMonitoring = false;
    private static Thread monitoringThread;
    
    static TeamSync()
    {
        EditorApplication.update += OnEditorUpdate;
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    static void OnEditorUpdate()
    {
        // 每10秒检查一次团队更新
        if (EditorApplication.timeSinceStartup % 10 < 0.1f)
        {
            CheckTeamUpdates();
        }
    }

    static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.EnteredEditMode)
        {
            // 进入编辑模式时检查团队更新
            CheckTeamUpdates();
        }
    }

    static void CheckTeamUpdates()
    {
        if (isMonitoring) return;
        
        isMonitoring = true;
        
        try
        {
            // 检查是否有远程更新
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "git",
                Arguments = "fetch origin",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                WorkingDirectory = Application.dataPath + "/.."
            };

            using (Process process = Process.Start(startInfo))
            {
                process.WaitForExit();
            }

            // 检查本地和远程的差异
            ProcessStartInfo diffInfo = new ProcessStartInfo
            {
                FileName = "git",
                Arguments = "rev-list --count HEAD..origin/main",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                WorkingDirectory = Application.dataPath + "/.."
            };

            using (Process process = Process.Start(diffInfo))
            {
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                if (!string.IsNullOrEmpty(output.Trim()) && output.Trim() != "0")
                {
                    UnityEngine.Debug.Log("👥 检测到团队更新！");
                    ShowTeamUpdateNotification();
                }
            }
        }
        catch (System.Exception e)
        {
            UnityEngine.Debug.LogWarning($"团队同步检查失败: {e.Message}");
        }
        finally
        {
            isMonitoring = false;
        }
    }

    static void ShowTeamUpdateNotification()
    {
        if (EditorUtility.DisplayDialog(
            "团队更新通知", 
            "检测到团队成员的新更改！\n\n是否立即同步？", 
            "立即同步", 
            "稍后同步"))
        {
            PullTeamUpdates();
        }
    }

    static void PullTeamUpdates()
    {
        string projectPath = Application.dataPath + "/..";
        
        try
        {
            // 执行团队拉取脚本
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "bash",
                Arguments = "./team-pull.sh",
                UseShellExecute = false,
                CreateNoWindow = true,
                WorkingDirectory = projectPath
            };

            using (Process process = Process.Start(startInfo))
            {
                process.WaitForExit();
                
                if (process.ExitCode == 0)
                {
                    UnityEngine.Debug.Log("✅ 团队更新同步成功！");
                    
                    // 刷新Unity项目
                    AssetDatabase.Refresh();
                    EditorUtility.DisplayDialog("同步完成", "团队更新已同步！\n请检查项目变化。", "确定");
                }
                else
                {
                    UnityEngine.Debug.LogWarning("⚠️ 团队同步失败，请手动检查");
                    EditorUtility.DisplayDialog("同步失败", "团队同步失败，请手动运行 ./team-pull.sh", "确定");
                }
            }
        }
        catch (System.Exception e)
        {
            UnityEngine.Debug.LogError($"团队同步执行失败: {e.Message}");
        }
    }

    // 当资源保存时触发
    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        if (importedAssets.Length > 0 || deletedAssets.Length > 0 || movedAssets.Length > 0)
        {
            UnityEngine.Debug.Log("📁 本地文件发生变化，准备同步到团队...");
            
            // 延迟3秒后同步，避免频繁操作
            EditorApplication.delayCall += () => {
                System.Threading.Thread.Sleep(3000);
                SyncToTeam();
            };
        }
    }

    static void SyncToTeam()
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
                    UnityEngine.Debug.Log("✅ 本地更改已同步到团队！");
                }
                else
                {
                    UnityEngine.Debug.LogWarning("⚠️ 同步到团队失败，请手动检查");
                }
            }
        }
        catch (System.Exception e)
        {
            UnityEngine.Debug.LogError($"同步到团队失败: {e.Message}");
        }
    }
}

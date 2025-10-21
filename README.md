# Unity 打地鼠游戏项目

一个完整的 Unity 打地鼠游戏项目，支持团队协作开发。

## 🎮 游戏特性

- **经典打地鼠玩法**：点击出现的地鼠获得分数
- **多场景支持**：主菜单和游戏场景
- **完整音效系统**：背景音乐和音效
- **动画系统**：地鼠动画和UI动画
- **团队协作**：支持多人实时协作开发

## 🚀 快速开始

### 环境要求
- Unity 2022.3.10f1 或更高版本
- Visual Studio Code 或 Visual Studio
- Git 版本控制

### 安装步骤

1. **克隆项目**
   ```bash
   git clone https://github.com/mlm93098601-ai/-.git
   cd 打地鼠Unity工程源码
   ```

2. **打开 Unity 项目**
   - 启动 Unity Hub
   - 点击 "添加" → 选择项目文件夹
   - 等待 Unity 导入完成

3. **运行游戏**
   - 打开 `Assets/Scenes/MainMenu.unity` (主菜单)
   - 或打开 `Assets/Scenes/Game.unity` (游戏场景)
   - 点击 Play 按钮开始游戏

## 📁 项目结构

```
Assets/
├── Scripts/           # 游戏脚本
│   ├── Cat.cs         # 地鼠控制器
│   ├── Game.cs        # 游戏管理器
│   ├── Hole.cs        # 地洞控制器
│   └── ...
├── Scenes/           # 游戏场景
│   ├── MainMenu.unity # 主菜单场景
│   └── Game.unity    # 游戏场景
├── Prefabs/          # 预制体
│   ├── Cat.prefab    # 地鼠预制体
│   └── Hole.prefab   # 地洞预制体
├── Arts/             # 美术资源
│   ├── Background/   # 背景图片
│   ├── Cat/          # 地鼠图片
│   ├── UI/           # UI图片
│   └── Sound/        # 音频文件
└── Animations/       # 动画文件
```

## 👥 团队协作

### Git 工作流
1. **拉取最新代码**：`git pull origin main`
2. **创建功能分支**：`git checkout -b feature/新功能`
3. **提交更改**：`git add . && git commit -m "描述"`
4. **推送分支**：`git push origin feature/新功能`
5. **创建 Pull Request**

### 自动同步脚本
- `auto-sync.sh`：手动同步到 GitHub
- `watch-sync.sh`：文件监控自动同步
- `team-sync.sh`：团队协作同步

## 🎯 开发指南

### 添加新功能
1. 在 `Assets/Scripts/` 中创建新脚本
2. 遵循 Unity 命名规范
3. 添加适当的注释和文档
4. 测试功能完整性

### 场景管理
1. 主菜单场景：`Assets/Scenes/MainMenu.unity`
2. 游戏场景：`Assets/Scenes/Game.unity`
3. 添加新场景到 Build Settings

### 预制体管理
1. 地鼠预制体：`Assets/Prefabs/Cat.prefab`
2. 地洞预制体：`Assets/Prefabs/Hole.prefab`
3. 创建新预制体时遵循命名规范

## 📝 更新日志

### v1.0.0 (2024-12-25)
- ✅ 完整的打地鼠游戏逻辑
- ✅ 多场景支持
- ✅ 音效和动画系统
- ✅ 团队协作功能
- ✅ 自动同步系统

## 🤝 贡献指南

1. Fork 项目
2. 创建功能分支
3. 提交更改
4. 发起 Pull Request

## 📄 许可证

MIT License - 详见 LICENSE 文件

## 📞 联系方式

- 项目维护者：mlm93098601-ai
- GitHub：https://github.com/mlm93098601-ai/-

---

**注意**：这是一个完整的 Unity 项目，请确保使用正确的 Unity 版本打开。

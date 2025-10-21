#!/bin/bash

# 快速连接项目脚本
# 使用方法: ./快速连接脚本.sh

echo "🚀 开始连接 Unity 打地鼠游戏项目..."

# 检查是否已存在项目
if [ -d "打地鼠Unity工程源码" ]; then
    echo "⚠️ 项目已存在，是否重新克隆？"
    read -p "输入 y 重新克隆，输入 n 使用现有项目: " choice
    if [ "$choice" = "y" ]; then
        echo "🗑️ 删除现有项目..."
        rm -rf "打地鼠Unity工程源码"
    else
        echo "✅ 使用现有项目"
        cd "打地鼠Unity工程源码"
        ./team-pull.sh
        exit 0
    fi
fi

# 克隆项目
echo "📥 克隆项目..."
git clone https://github.com/mlm93098601-ai/-.git

# 进入项目目录
cd "打地鼠Unity工程源码"

# 检查项目结构
echo "📋 检查项目结构..."
if [ ! -d "Assets" ]; then
    echo "❌ 项目结构不完整，请检查网络连接"
    exit 1
fi

# 设置权限
echo "🔧 设置脚本权限..."
chmod +x auto-sync.sh
chmod +x team-pull.sh
chmod +x watch-team.sh
chmod +x watch-sync.sh

# 拉取最新代码
echo "📥 拉取最新代码..."
./team-pull.sh

echo "✅ 项目连接完成！"
echo ""
echo "🎮 下一步操作："
echo "1. 打开 Unity Hub"
echo "2. 点击 '添加' 按钮"
echo "3. 选择当前文件夹: $(pwd)"
echo "4. 等待 Unity 导入完成"
echo "5. 开始协作开发！"
echo ""
echo "📞 如有问题，请查看 '同伴连接指南.md' 文件"

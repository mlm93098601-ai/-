#!/bin/bash

# 团队实时拉取脚本
# 使用方法: ./team-pull.sh

echo "👥 开始拉取团队最新更改..."

# 检查网络连接
echo "🌐 检查网络连接..."
if ! ping -c 1 github.com &> /dev/null; then
    echo "❌ 网络连接失败，请检查网络设置"
    exit 1
fi

# 拉取最新代码
echo "📥 拉取最新代码..."
git fetch origin

# 检查是否有远程更新
LOCAL=$(git rev-parse HEAD)
REMOTE=$(git rev-parse origin/main)

if [ "$LOCAL" = "$REMOTE" ]; then
    echo "✅ 本地代码已是最新版本"
else
    echo "🔄 发现远程更新，正在拉取..."
    
    # 保存本地更改
    echo "💾 保存本地更改..."
    git stash push -m "本地更改备份 - $(date '+%Y-%m-%d %H:%M:%S')"
    
    # 拉取远程更改
    echo "📥 拉取远程更改..."
    git pull origin main
    
    # 恢复本地更改
    echo "🔄 恢复本地更改..."
    git stash pop
    
    echo "✅ 团队代码同步完成！"
    echo "📋 更新内容："
    git log --oneline -5
fi

echo "🎮 请在Unity中刷新项目以查看最新更改"

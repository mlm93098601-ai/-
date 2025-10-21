#!/bin/bash

# Unity打地鼠游戏自动同步脚本
# 使用方法: ./auto-sync.sh

echo "🚀 开始同步Unity打地鼠游戏项目到GitHub..."

# 检查Git状态
echo "📋 检查Git状态..."
git status

# 添加所有更改
echo "📁 添加所有更改..."
git add .

# 提交更改
echo "💾 提交更改..."
git commit -m "feat: 自动同步 - $(date '+%Y-%m-%d %H:%M:%S')"

# 推送到GitHub
echo "🌐 推送到GitHub..."
git push origin main

if [ $? -eq 0 ]; then
    echo "✅ 同步成功！项目已更新到GitHub"
    echo "🔗 仓库地址: https://github.com/mlm93098601-ai/-"
else
    echo "❌ 同步失败，请检查网络连接或手动推送"
    echo "💡 手动推送命令: git push origin main --force"
fi

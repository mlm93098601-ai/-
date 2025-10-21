#!/bin/bash

# 团队实时监控脚本
# 使用方法: ./watch-team.sh

echo "👥 开始监控团队实时更改..."
echo "🔄 每30秒检查一次团队更新"
echo "按 Ctrl+C 停止监控"

# 检查是否安装了fswatch
if ! command -v fswatch &> /dev/null; then
    echo "❌ 未安装fswatch，正在安装..."
    if command -v brew &> /dev/null; then
        brew install fswatch
    else
        echo "请手动安装fswatch: brew install fswatch"
        exit 1
    fi
fi

# 持续监控团队更改
while true; do
    echo "🔍 检查团队更新... $(date '+%H:%M:%S')"
    
    # 拉取团队更改
    ./team-pull.sh
    
    # 等待30秒
    echo "⏰ 等待30秒后再次检查..."
    sleep 30
done

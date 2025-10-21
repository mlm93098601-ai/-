#!/bin/bash

# 文件监控自动同步脚本
# 使用方法: ./watch-sync.sh

echo "👀 开始监控文件变化，自动同步到GitHub..."

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

# 监控文件变化并自动同步
fswatch -o . | while read f; do
    echo "📁 检测到文件变化: $(date '+%Y-%m-%d %H:%M:%S')"
    
    # 等待2秒，避免频繁同步
    sleep 2
    
    # 执行自动同步
    ./auto-sync.sh
    
    echo "⏰ 等待下次变化..."
done

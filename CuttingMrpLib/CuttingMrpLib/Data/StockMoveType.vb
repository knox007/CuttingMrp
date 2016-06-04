﻿Imports System.ComponentModel
Public Enum StockMoveType
    ''' <summary>
    ''' 系统入库
    ''' </summary>
    <Description("SystemEntry")>
    SystemEntry = 0
    ''' <summary>
    ''' 移库
    ''' </summary>
    <Description("Move")>
    Move = 1
    ''' <summary>
    ''' 倒冲减少
    ''' </summary>
    <Description("Backflush")>
    Backflush = 2
    ''' <summary>
    ''' 取消倒冲后的入库
    ''' </summary>
    <Description("BackflushCancel")>
    BackflushCancel = 3
    ''' <summary>
    ''' 手工入库
    ''' </summary>
    <Description("ManualEntry")>
    ManualEntry = 4
    ''' <summary>
    ''' 上传文件入库
    ''' </summary>
    <Description("UploadEntry")>
    UploadEntry = 5
    ''' <summary>
    ''' 手工盘点
    ''' </summary>
    <Description("InventoryManual")>
    InventoryManual = 6
    ''' <summary>
    ''' 系统盘点调整
    ''' </summary>
    <Description("InventorySystem")>
    InventorySystem = 7
End Enum

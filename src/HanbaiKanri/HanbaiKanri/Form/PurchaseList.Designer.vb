﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PurchaseList
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.BtnPurchaseCancel = New System.Windows.Forms.Button()
        Me.BtnPurchaseView = New System.Windows.Forms.Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TxtSales = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TxtPurchaseSince = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TxtSupplierCode = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TxtTel = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TxtAddress = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LblConditions = New System.Windows.Forms.Label()
        Me.TxtSupplierName = New System.Windows.Forms.TextBox()
        Me.BtnPurchaseSearch = New System.Windows.Forms.Button()
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.DgvHtyhd = New System.Windows.Forms.DataGridView()
        Me.LblMode = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.TxtCustomerPO = New System.Windows.Forms.TextBox()
        Me.dtPurchaseDateSince = New System.Windows.Forms.DateTimePicker()
        Me.dtPurchaseDateUntil = New System.Windows.Forms.DateTimePicker()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.RbtnSlip = New System.Windows.Forms.RadioButton()
        Me.RbtnDetails = New System.Windows.Forms.RadioButton()
        Me.ChkCancelData = New System.Windows.Forms.CheckBox()
        Me.TxtSpec = New System.Windows.Forms.TextBox()
        Me.LblSpec = New System.Windows.Forms.Label()
        Me.TxtItemName = New System.Windows.Forms.TextBox()
        Me.LblItemName = New System.Windows.Forms.Label()
        CType(Me.DgvHtyhd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'BtnPurchaseCancel
        '
        Me.BtnPurchaseCancel.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnPurchaseCancel.Location = New System.Drawing.Point(832, 509)
        Me.BtnPurchaseCancel.Name = "BtnPurchaseCancel"
        Me.BtnPurchaseCancel.Size = New System.Drawing.Size(165, 40)
        Me.BtnPurchaseCancel.TabIndex = 16
        Me.BtnPurchaseCancel.Text = "仕入取消"
        Me.BtnPurchaseCancel.UseVisualStyleBackColor = True
        Me.BtnPurchaseCancel.Visible = False
        '
        'BtnPurchaseView
        '
        Me.BtnPurchaseView.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnPurchaseView.Location = New System.Drawing.Point(1003, 509)
        Me.BtnPurchaseView.Name = "BtnPurchaseView"
        Me.BtnPurchaseView.Size = New System.Drawing.Size(165, 40)
        Me.BtnPurchaseView.TabIndex = 17
        Me.BtnPurchaseView.Text = "仕入参照"
        Me.BtnPurchaseView.UseVisualStyleBackColor = True
        Me.BtnPurchaseView.Visible = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label10.Location = New System.Drawing.Point(9, 179)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(163, 30)
        Me.Label10.TabIndex = 115
        Me.Label10.Text = "■表示形式"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(936, 42)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(34, 24)
        Me.Label5.TabIndex = 113
        Me.Label5.Text = "～"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label6.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.Location = New System.Drawing.Point(14, 149)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(170, 22)
        Me.Label6.TabIndex = 110
        Me.Label6.Text = "営業担当者"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtSales
        '
        Me.TxtSales.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtSales.Location = New System.Drawing.Point(190, 149)
        Me.TxtSales.Name = "TxtSales"
        Me.TxtSales.Size = New System.Drawing.Size(350, 37)
        Me.TxtSales.TabIndex = 9
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label7.Location = New System.Drawing.Point(584, 63)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(170, 22)
        Me.Label7.TabIndex = 108
        Me.Label7.Text = "仕入番号"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtPurchaseSince
        '
        Me.TxtPurchaseSince.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtPurchaseSince.Location = New System.Drawing.Point(760, 63)
        Me.TxtPurchaseSince.Name = "TxtPurchaseSince"
        Me.TxtPurchaseSince.Size = New System.Drawing.Size(369, 37)
        Me.TxtPurchaseSince.TabIndex = 7
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label8.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label8.Location = New System.Drawing.Point(584, 35)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(170, 22)
        Me.Label8.TabIndex = 106
        Me.Label8.Text = "仕入登録日"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(14, 120)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(170, 22)
        Me.Label4.TabIndex = 104
        Me.Label4.Text = "仕入先コード"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtSupplierCode
        '
        Me.TxtSupplierCode.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtSupplierCode.Location = New System.Drawing.Point(190, 120)
        Me.TxtSupplierCode.Name = "TxtSupplierCode"
        Me.TxtSupplierCode.Size = New System.Drawing.Size(350, 37)
        Me.TxtSupplierCode.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(14, 92)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(170, 22)
        Me.Label3.TabIndex = 102
        Me.Label3.Text = "電話番号"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtTel
        '
        Me.TxtTel.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtTel.Location = New System.Drawing.Point(190, 92)
        Me.TxtTel.Name = "TxtTel"
        Me.TxtTel.Size = New System.Drawing.Size(350, 37)
        Me.TxtTel.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(14, 64)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(170, 22)
        Me.Label2.TabIndex = 100
        Me.Label2.Text = "住所"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtAddress
        '
        Me.TxtAddress.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtAddress.Location = New System.Drawing.Point(190, 64)
        Me.TxtAddress.Name = "TxtAddress"
        Me.TxtAddress.Size = New System.Drawing.Size(350, 37)
        Me.TxtAddress.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(14, 36)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(170, 22)
        Me.Label1.TabIndex = 98
        Me.Label1.Text = "仕入先名"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblConditions
        '
        Me.LblConditions.AutoSize = True
        Me.LblConditions.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblConditions.Location = New System.Drawing.Point(11, 13)
        Me.LblConditions.Name = "LblConditions"
        Me.LblConditions.Size = New System.Drawing.Size(163, 30)
        Me.LblConditions.TabIndex = 97
        Me.LblConditions.Text = "■抽出条件"
        '
        'TxtSupplierName
        '
        Me.TxtSupplierName.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtSupplierName.Location = New System.Drawing.Point(190, 36)
        Me.TxtSupplierName.Name = "TxtSupplierName"
        Me.TxtSupplierName.Size = New System.Drawing.Size(350, 37)
        Me.TxtSupplierName.TabIndex = 1
        '
        'BtnPurchaseSearch
        '
        Me.BtnPurchaseSearch.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnPurchaseSearch.Location = New System.Drawing.Point(1174, 41)
        Me.BtnPurchaseSearch.Name = "BtnPurchaseSearch"
        Me.BtnPurchaseSearch.Size = New System.Drawing.Size(165, 40)
        Me.BtnPurchaseSearch.TabIndex = 11
        Me.BtnPurchaseSearch.Text = "検索"
        Me.BtnPurchaseSearch.UseVisualStyleBackColor = True
        '
        'BtnBack
        '
        Me.BtnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnBack.Location = New System.Drawing.Point(1174, 509)
        Me.BtnBack.Name = "BtnBack"
        Me.BtnBack.Size = New System.Drawing.Size(165, 40)
        Me.BtnBack.TabIndex = 18
        Me.BtnBack.Text = "戻る"
        Me.BtnBack.UseVisualStyleBackColor = True
        '
        'DgvHtyhd
        '
        Me.DgvHtyhd.AllowUserToAddRows = False
        Me.DgvHtyhd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DgvHtyhd.DefaultCellStyle = DataGridViewCellStyle1
        Me.DgvHtyhd.Location = New System.Drawing.Point(13, 236)
        Me.DgvHtyhd.Name = "DgvHtyhd"
        Me.DgvHtyhd.ReadOnly = True
        Me.DgvHtyhd.RowHeadersVisible = False
        Me.DgvHtyhd.RowTemplate.Height = 21
        Me.DgvHtyhd.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvHtyhd.Size = New System.Drawing.Size(1326, 267)
        Me.DgvHtyhd.TabIndex = 15
        '
        'LblMode
        '
        Me.LblMode.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblMode.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMode.Location = New System.Drawing.Point(1174, 9)
        Me.LblMode.Name = "LblMode"
        Me.LblMode.Size = New System.Drawing.Size(165, 22)
        Me.LblMode.TabIndex = 304
        Me.LblMode.Text = "モード"
        Me.LblMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label11.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label11.Location = New System.Drawing.Point(584, 91)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(170, 22)
        Me.Label11.TabIndex = 311
        Me.Label11.Text = "客先番号"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtCustomerPO
        '
        Me.TxtCustomerPO.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtCustomerPO.Location = New System.Drawing.Point(760, 91)
        Me.TxtCustomerPO.Name = "TxtCustomerPO"
        Me.TxtCustomerPO.Size = New System.Drawing.Size(369, 37)
        Me.TxtCustomerPO.TabIndex = 10
        '
        'dtPurchaseDateSince
        '
        Me.dtPurchaseDateSince.CalendarFont = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dtPurchaseDateSince.CustomFormat = ""
        Me.dtPurchaseDateSince.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dtPurchaseDateSince.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtPurchaseDateSince.Location = New System.Drawing.Point(760, 35)
        Me.dtPurchaseDateSince.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.dtPurchaseDateSince.Name = "dtPurchaseDateSince"
        Me.dtPurchaseDateSince.Size = New System.Drawing.Size(170, 37)
        Me.dtPurchaseDateSince.TabIndex = 332
        Me.dtPurchaseDateSince.Value = New Date(2019, 1, 1, 0, 0, 0, 0)
        '
        'dtPurchaseDateUntil
        '
        Me.dtPurchaseDateUntil.CalendarFont = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dtPurchaseDateUntil.CustomFormat = ""
        Me.dtPurchaseDateUntil.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dtPurchaseDateUntil.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtPurchaseDateUntil.Location = New System.Drawing.Point(959, 35)
        Me.dtPurchaseDateUntil.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.dtPurchaseDateUntil.Name = "dtPurchaseDateUntil"
        Me.dtPurchaseDateUntil.Size = New System.Drawing.Size(170, 37)
        Me.dtPurchaseDateUntil.TabIndex = 333
        Me.dtPurchaseDateUntil.Value = New Date(2019, 1, 1, 0, 0, 0, 0)
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.BackColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel1.ColumnCount = 5
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.Controls.Add(Me.RbtnSlip, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.RbtnDetails, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.ChkCancelData, 4, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(12, 201)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(834, 29)
        Me.TableLayoutPanel1.TabIndex = 336
        '
        'RbtnSlip
        '
        Me.RbtnSlip.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.RbtnSlip.AutoSize = True
        Me.RbtnSlip.Checked = True
        Me.RbtnSlip.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.RbtnSlip.Location = New System.Drawing.Point(3, 3)
        Me.RbtnSlip.Name = "RbtnSlip"
        Me.RbtnSlip.Size = New System.Drawing.Size(164, 23)
        Me.RbtnSlip.TabIndex = 11
        Me.RbtnSlip.TabStop = True
        Me.RbtnSlip.Text = "伝票単位"
        Me.RbtnSlip.UseVisualStyleBackColor = True
        '
        'RbtnDetails
        '
        Me.RbtnDetails.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.RbtnDetails.AutoSize = True
        Me.RbtnDetails.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.RbtnDetails.Location = New System.Drawing.Point(173, 3)
        Me.RbtnDetails.Name = "RbtnDetails"
        Me.RbtnDetails.Size = New System.Drawing.Size(164, 23)
        Me.RbtnDetails.TabIndex = 12
        Me.RbtnDetails.Text = "明細単位"
        Me.RbtnDetails.UseVisualStyleBackColor = True
        '
        'ChkCancelData
        '
        Me.ChkCancelData.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.ChkCancelData.AutoSize = True
        Me.ChkCancelData.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ChkCancelData.Location = New System.Drawing.Point(365, 3)
        Me.ChkCancelData.Name = "ChkCancelData"
        Me.ChkCancelData.Size = New System.Drawing.Size(274, 23)
        Me.ChkCancelData.TabIndex = 14
        Me.ChkCancelData.Text = "取消データを含める"
        Me.ChkCancelData.UseVisualStyleBackColor = True
        '
        'TxtSpec
        '
        Me.TxtSpec.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtSpec.Location = New System.Drawing.Point(760, 148)
        Me.TxtSpec.Name = "TxtSpec"
        Me.TxtSpec.Size = New System.Drawing.Size(369, 37)
        Me.TxtSpec.TabIndex = 345
        '
        'LblSpec
        '
        Me.LblSpec.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblSpec.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblSpec.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblSpec.Location = New System.Drawing.Point(584, 148)
        Me.LblSpec.Name = "LblSpec"
        Me.LblSpec.Size = New System.Drawing.Size(170, 22)
        Me.LblSpec.TabIndex = 344
        Me.LblSpec.Text = "型式"
        Me.LblSpec.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtItemName
        '
        Me.TxtItemName.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtItemName.Location = New System.Drawing.Point(760, 120)
        Me.TxtItemName.Name = "TxtItemName"
        Me.TxtItemName.Size = New System.Drawing.Size(369, 37)
        Me.TxtItemName.TabIndex = 343
        '
        'LblItemName
        '
        Me.LblItemName.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblItemName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblItemName.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblItemName.Location = New System.Drawing.Point(584, 120)
        Me.LblItemName.Name = "LblItemName"
        Me.LblItemName.Size = New System.Drawing.Size(170, 22)
        Me.LblItemName.TabIndex = 342
        Me.LblItemName.Text = "品名"
        Me.LblItemName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PurchaseList
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(1350, 561)
        Me.Controls.Add(Me.TxtSpec)
        Me.Controls.Add(Me.LblSpec)
        Me.Controls.Add(Me.TxtItemName)
        Me.Controls.Add(Me.LblItemName)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.dtPurchaseDateUntil)
        Me.Controls.Add(Me.dtPurchaseDateSince)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.TxtCustomerPO)
        Me.Controls.Add(Me.LblMode)
        Me.Controls.Add(Me.BtnPurchaseCancel)
        Me.Controls.Add(Me.BtnPurchaseView)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TxtSales)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TxtPurchaseSince)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TxtSupplierCode)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TxtTel)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtAddress)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LblConditions)
        Me.Controls.Add(Me.TxtSupplierName)
        Me.Controls.Add(Me.BtnPurchaseSearch)
        Me.Controls.Add(Me.BtnBack)
        Me.Controls.Add(Me.DgvHtyhd)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "PurchaseList"
        Me.Text = "PurchaseList"
        CType(Me.DgvHtyhd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnPurchaseCancel As Button
    Friend WithEvents BtnPurchaseView As Button
    Friend WithEvents Label10 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents TxtSales As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents TxtPurchaseSince As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents TxtSupplierCode As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents TxtTel As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents TxtAddress As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents LblConditions As Label
    Friend WithEvents TxtSupplierName As TextBox
    Friend WithEvents BtnPurchaseSearch As Button
    Friend WithEvents BtnBack As Button
    Friend WithEvents DgvHtyhd As DataGridView
    Friend WithEvents LblMode As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents TxtCustomerPO As TextBox
    Friend WithEvents dtPurchaseDateSince As DateTimePicker
    Friend WithEvents dtPurchaseDateUntil As DateTimePicker
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents RbtnSlip As RadioButton
    Friend WithEvents RbtnDetails As RadioButton
    Friend WithEvents ChkCancelData As CheckBox
    Friend WithEvents TxtSpec As TextBox
    Friend WithEvents LblSpec As Label
    Friend WithEvents TxtItemName As TextBox
    Friend WithEvents LblItemName As Label
End Class

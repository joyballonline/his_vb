﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DepositManagement
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
        Me.BtnRegist = New System.Windows.Forms.Button()
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.LblNo1 = New System.Windows.Forms.Label()
        Me.TxtCount1 = New System.Windows.Forms.TextBox()
        Me.DtpDepositDate = New System.Windows.Forms.DateTimePicker()
        Me.LblRemarks = New System.Windows.Forms.Label()
        Me.TxtRemarks = New System.Windows.Forms.TextBox()
        Me.LblDepositDate = New System.Windows.Forms.Label()
        Me.LblNo2 = New System.Windows.Forms.Label()
        Me.TxtCount2 = New System.Windows.Forms.TextBox()
        Me.DgvCustomer = New System.Windows.Forms.DataGridView()
        Me.請求先 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.請求残高 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LblBillingInfo = New System.Windows.Forms.Label()
        Me.LblHistory = New System.Windows.Forms.Label()
        Me.LblDeposit = New System.Windows.Forms.Label()
        Me.DgvBillingInfo = New System.Windows.Forms.DataGridView()
        Me.AddNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.請求情報請求番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.請求金額 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.請求情報入金額計 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.請求情報請求残高 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.入金額 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DgvHistory = New System.Windows.Forms.DataGridView()
        Me.DgvDeposit = New System.Windows.Forms.DataGridView()
        Me.振込入金 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.振込手数料 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.現金入金 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.手形受入 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.電子債権 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.売上割引 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.売上値引 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.リベート = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.相殺 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.諸口 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.入金額計 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.No = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.請求番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.入金済請求先 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.入金番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.入金日 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.入金種別 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.入金済入金額計 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.備考 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DgvCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvBillingInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvDeposit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnRegist
        '
        Me.BtnRegist.Location = New System.Drawing.Point(1002, 669)
        Me.BtnRegist.Name = "BtnRegist"
        Me.BtnRegist.Size = New System.Drawing.Size(165, 40)
        Me.BtnRegist.TabIndex = 293
        Me.BtnRegist.Text = "登録"
        Me.BtnRegist.UseVisualStyleBackColor = True
        '
        'BtnBack
        '
        Me.BtnBack.Location = New System.Drawing.Point(1173, 669)
        Me.BtnBack.Name = "BtnBack"
        Me.BtnBack.Size = New System.Drawing.Size(165, 40)
        Me.BtnBack.TabIndex = 292
        Me.BtnBack.Text = "戻る"
        Me.BtnBack.UseVisualStyleBackColor = True
        '
        'LblNo1
        '
        Me.LblNo1.BackColor = System.Drawing.Color.Transparent
        Me.LblNo1.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblNo1.Location = New System.Drawing.Point(1316, 65)
        Me.LblNo1.Name = "LblNo1"
        Me.LblNo1.Size = New System.Drawing.Size(22, 22)
        Me.LblNo1.TabIndex = 291
        Me.LblNo1.Text = "件"
        Me.LblNo1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtCount1
        '
        Me.TxtCount1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtCount1.Enabled = False
        Me.TxtCount1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtCount1.Location = New System.Drawing.Point(1272, 65)
        Me.TxtCount1.Name = "TxtCount1"
        Me.TxtCount1.Size = New System.Drawing.Size(38, 22)
        Me.TxtCount1.TabIndex = 290
        '
        'DtpDepositDate
        '
        Me.DtpDepositDate.CalendarFont = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpDepositDate.CustomFormat = "yyyy/MM/dd"
        Me.DtpDepositDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpDepositDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpDepositDate.Location = New System.Drawing.Point(291, 456)
        Me.DtpDepositDate.Name = "DtpDepositDate"
        Me.DtpDepositDate.Size = New System.Drawing.Size(148, 22)
        Me.DtpDepositDate.TabIndex = 285
        Me.DtpDepositDate.TabStop = False
        Me.DtpDepositDate.Value = New Date(2018, 7, 25, 13, 29, 25, 0)
        '
        'LblRemarks
        '
        Me.LblRemarks.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblRemarks.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblRemarks.Location = New System.Drawing.Point(445, 456)
        Me.LblRemarks.Name = "LblRemarks"
        Me.LblRemarks.Size = New System.Drawing.Size(170, 22)
        Me.LblRemarks.TabIndex = 284
        Me.LblRemarks.Text = "備考"
        Me.LblRemarks.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtRemarks
        '
        Me.TxtRemarks.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtRemarks.Location = New System.Drawing.Point(621, 456)
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(644, 22)
        Me.TxtRemarks.TabIndex = 283
        '
        'LblDepositDate
        '
        Me.LblDepositDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblDepositDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblDepositDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblDepositDate.Location = New System.Drawing.Point(187, 456)
        Me.LblDepositDate.Name = "LblDepositDate"
        Me.LblDepositDate.Size = New System.Drawing.Size(98, 22)
        Me.LblDepositDate.TabIndex = 282
        Me.LblDepositDate.Text = "入金日"
        Me.LblDepositDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblNo2
        '
        Me.LblNo2.BackColor = System.Drawing.Color.Transparent
        Me.LblNo2.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblNo2.Location = New System.Drawing.Point(1316, 456)
        Me.LblNo2.Name = "LblNo2"
        Me.LblNo2.Size = New System.Drawing.Size(22, 22)
        Me.LblNo2.TabIndex = 281
        Me.LblNo2.Text = "件"
        Me.LblNo2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtCount2
        '
        Me.TxtCount2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtCount2.Enabled = False
        Me.TxtCount2.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtCount2.Location = New System.Drawing.Point(1272, 456)
        Me.TxtCount2.Name = "TxtCount2"
        Me.TxtCount2.Size = New System.Drawing.Size(38, 22)
        Me.TxtCount2.TabIndex = 280
        '
        'DgvCustomer
        '
        Me.DgvCustomer.AllowUserToAddRows = False
        Me.DgvCustomer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvCustomer.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.請求先, Me.請求残高})
        Me.DgvCustomer.Location = New System.Drawing.Point(12, 19)
        Me.DgvCustomer.Name = "DgvCustomer"
        Me.DgvCustomer.RowHeadersVisible = False
        Me.DgvCustomer.RowTemplate.Height = 21
        Me.DgvCustomer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvCustomer.Size = New System.Drawing.Size(504, 40)
        Me.DgvCustomer.TabIndex = 273
        '
        '請求先
        '
        Me.請求先.HeaderText = "請求先"
        Me.請求先.Name = "請求先"
        Me.請求先.Width = 300
        '
        '請求残高
        '
        Me.請求残高.HeaderText = "請求残高"
        Me.請求残高.Name = "請求残高"
        Me.請求残高.Width = 200
        '
        'LblBillingInfo
        '
        Me.LblBillingInfo.AutoSize = True
        Me.LblBillingInfo.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblBillingInfo.Location = New System.Drawing.Point(13, 460)
        Me.LblBillingInfo.Name = "LblBillingInfo"
        Me.LblBillingInfo.Size = New System.Drawing.Size(82, 15)
        Me.LblBillingInfo.TabIndex = 279
        Me.LblBillingInfo.Text = "■請求情報"
        '
        'LblHistory
        '
        Me.LblHistory.AutoSize = True
        Me.LblHistory.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblHistory.Location = New System.Drawing.Point(12, 68)
        Me.LblHistory.Name = "LblHistory"
        Me.LblHistory.Size = New System.Drawing.Size(80, 15)
        Me.LblHistory.TabIndex = 278
        Me.LblHistory.Text = "■入金済み"
        '
        'LblDeposit
        '
        Me.LblDeposit.AutoSize = True
        Me.LblDeposit.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblDeposit.Location = New System.Drawing.Point(13, 263)
        Me.LblDeposit.Name = "LblDeposit"
        Me.LblDeposit.Size = New System.Drawing.Size(82, 15)
        Me.LblDeposit.TabIndex = 277
        Me.LblDeposit.Text = "■入金入力"
        '
        'DgvBillingInfo
        '
        Me.DgvBillingInfo.AllowUserToAddRows = False
        Me.DgvBillingInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvBillingInfo.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.AddNo, Me.請求情報請求番号, Me.請求金額, Me.請求情報入金額計, Me.請求情報請求残高, Me.入金額})
        Me.DgvBillingInfo.Location = New System.Drawing.Point(12, 484)
        Me.DgvBillingInfo.Name = "DgvBillingInfo"
        Me.DgvBillingInfo.RowHeadersVisible = False
        Me.DgvBillingInfo.RowTemplate.Height = 21
        Me.DgvBillingInfo.Size = New System.Drawing.Size(1327, 160)
        Me.DgvBillingInfo.TabIndex = 276
        '
        'AddNo
        '
        Me.AddNo.HeaderText = "No"
        Me.AddNo.Name = "AddNo"
        Me.AddNo.Width = 70
        '
        '請求情報請求番号
        '
        Me.請求情報請求番号.HeaderText = "請求番号"
        Me.請求情報請求番号.Name = "請求情報請求番号"
        Me.請求情報請求番号.Width = 150
        '
        '請求金額
        '
        Me.請求金額.HeaderText = "請求金額"
        Me.請求金額.Name = "請求金額"
        Me.請求金額.Width = 150
        '
        '請求情報入金額計
        '
        Me.請求情報入金額計.HeaderText = "入金額計"
        Me.請求情報入金額計.Name = "請求情報入金額計"
        Me.請求情報入金額計.Width = 150
        '
        '請求情報請求残高
        '
        Me.請求情報請求残高.HeaderText = "請求残高"
        Me.請求情報請求残高.Name = "請求情報請求残高"
        Me.請求情報請求残高.Width = 150
        '
        '入金額
        '
        Me.入金額.HeaderText = "入金額"
        Me.入金額.Name = "入金額"
        Me.入金額.Width = 150
        '
        'DgvHistory
        '
        Me.DgvHistory.AllowUserToAddRows = False
        Me.DgvHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvHistory.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.No, Me.請求番号, Me.入金済請求先, Me.入金番号, Me.入金日, Me.入金種別, Me.入金済入金額計, Me.備考})
        Me.DgvHistory.Location = New System.Drawing.Point(10, 92)
        Me.DgvHistory.Name = "DgvHistory"
        Me.DgvHistory.RowHeadersVisible = False
        Me.DgvHistory.RowTemplate.Height = 21
        Me.DgvHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvHistory.Size = New System.Drawing.Size(1326, 160)
        Me.DgvHistory.TabIndex = 275
        '
        'DgvDeposit
        '
        Me.DgvDeposit.AllowUserToAddRows = False
        Me.DgvDeposit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvDeposit.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.振込入金, Me.振込手数料, Me.現金入金, Me.手形受入, Me.電子債権, Me.売上割引, Me.売上値引, Me.リベート, Me.相殺, Me.諸口, Me.入金額計})
        Me.DgvDeposit.Location = New System.Drawing.Point(11, 289)
        Me.DgvDeposit.Name = "DgvDeposit"
        Me.DgvDeposit.RowHeadersVisible = False
        Me.DgvDeposit.RowTemplate.Height = 21
        Me.DgvDeposit.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvDeposit.Size = New System.Drawing.Size(1326, 160)
        Me.DgvDeposit.TabIndex = 274
        '
        '振込入金
        '
        Me.振込入金.HeaderText = "振込入金"
        Me.振込入金.Name = "振込入金"
        '
        '振込手数料
        '
        Me.振込手数料.HeaderText = "振込手数料"
        Me.振込手数料.Name = "振込手数料"
        '
        '現金入金
        '
        Me.現金入金.HeaderText = "現金入金"
        Me.現金入金.Name = "現金入金"
        '
        '手形受入
        '
        Me.手形受入.HeaderText = "手形受入"
        Me.手形受入.Name = "手形受入"
        '
        '電子債権
        '
        Me.電子債権.HeaderText = "電子債権"
        Me.電子債権.Name = "電子債権"
        '
        '売上割引
        '
        Me.売上割引.HeaderText = "売上割引"
        Me.売上割引.Name = "売上割引"
        '
        '売上値引
        '
        Me.売上値引.HeaderText = "売上値引"
        Me.売上値引.Name = "売上値引"
        '
        'リベート
        '
        Me.リベート.HeaderText = "リベート"
        Me.リベート.Name = "リベート"
        '
        '相殺
        '
        Me.相殺.HeaderText = "相殺"
        Me.相殺.Name = "相殺"
        '
        '諸口
        '
        Me.諸口.HeaderText = "諸口"
        Me.諸口.Name = "諸口"
        '
        '入金額計
        '
        Me.入金額計.HeaderText = "入金額計"
        Me.入金額計.Name = "入金額計"
        '
        'No
        '
        Me.No.HeaderText = "No"
        Me.No.Name = "No"
        Me.No.Width = 70
        '
        '請求番号
        '
        Me.請求番号.HeaderText = "請求番号"
        Me.請求番号.Name = "請求番号"
        Me.請求番号.Visible = False
        Me.請求番号.Width = 150
        '
        '入金済請求先
        '
        Me.入金済請求先.HeaderText = "請求先"
        Me.入金済請求先.Name = "入金済請求先"
        Me.入金済請求先.Width = 200
        '
        '入金番号
        '
        Me.入金番号.HeaderText = "入金番号"
        Me.入金番号.Name = "入金番号"
        Me.入金番号.Width = 150
        '
        '入金日
        '
        Me.入金日.HeaderText = "入金日"
        Me.入金日.Name = "入金日"
        Me.入金日.Width = 150
        '
        '入金種別
        '
        Me.入金種別.HeaderText = "入金種別"
        Me.入金種別.Name = "入金種別"
        Me.入金種別.Width = 150
        '
        '入金済入金額計
        '
        Me.入金済入金額計.HeaderText = "入金額計"
        Me.入金済入金額計.Name = "入金済入金額計"
        Me.入金済入金額計.Width = 200
        '
        '備考
        '
        Me.備考.HeaderText = "備考"
        Me.備考.Name = "備考"
        Me.備考.Width = 200
        '
        'DepositManagement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1350, 729)
        Me.Controls.Add(Me.BtnRegist)
        Me.Controls.Add(Me.BtnBack)
        Me.Controls.Add(Me.LblNo1)
        Me.Controls.Add(Me.TxtCount1)
        Me.Controls.Add(Me.DtpDepositDate)
        Me.Controls.Add(Me.LblRemarks)
        Me.Controls.Add(Me.TxtRemarks)
        Me.Controls.Add(Me.LblDepositDate)
        Me.Controls.Add(Me.LblNo2)
        Me.Controls.Add(Me.TxtCount2)
        Me.Controls.Add(Me.DgvCustomer)
        Me.Controls.Add(Me.LblBillingInfo)
        Me.Controls.Add(Me.LblDeposit)
        Me.Controls.Add(Me.DgvBillingInfo)
        Me.Controls.Add(Me.DgvDeposit)
        Me.Controls.Add(Me.LblHistory)
        Me.Controls.Add(Me.DgvHistory)
        Me.Name = "DepositManagement"
        Me.Text = "DepositManagement"
        CType(Me.DgvCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvBillingInfo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvDeposit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BtnRegist As Button
    Friend WithEvents BtnBack As Button
    Friend WithEvents LblNo1 As Label
    Friend WithEvents TxtCount1 As TextBox
    Friend WithEvents DtpDepositDate As DateTimePicker
    Friend WithEvents LblRemarks As Label
    Friend WithEvents TxtRemarks As TextBox
    Friend WithEvents LblDepositDate As Label
    Friend WithEvents LblNo2 As Label
    Friend WithEvents TxtCount2 As TextBox
    Friend WithEvents DgvCustomer As DataGridView
    Friend WithEvents LblBillingInfo As Label
    Friend WithEvents LblHistory As Label
    Friend WithEvents LblDeposit As Label
    Friend WithEvents DgvBillingInfo As DataGridView
    Friend WithEvents DgvHistory As DataGridView
    Friend WithEvents DgvDeposit As DataGridView
    Friend WithEvents 請求先 As DataGridViewTextBoxColumn
    Friend WithEvents 請求残高 As DataGridViewTextBoxColumn
    Friend WithEvents 振込入金 As DataGridViewTextBoxColumn
    Friend WithEvents 振込手数料 As DataGridViewTextBoxColumn
    Friend WithEvents 現金入金 As DataGridViewTextBoxColumn
    Friend WithEvents 手形受入 As DataGridViewTextBoxColumn
    Friend WithEvents 電子債権 As DataGridViewTextBoxColumn
    Friend WithEvents 売上割引 As DataGridViewTextBoxColumn
    Friend WithEvents 売上値引 As DataGridViewTextBoxColumn
    Friend WithEvents リベート As DataGridViewTextBoxColumn
    Friend WithEvents 相殺 As DataGridViewTextBoxColumn
    Friend WithEvents 諸口 As DataGridViewTextBoxColumn
    Friend WithEvents 入金額計 As DataGridViewTextBoxColumn
    Friend WithEvents AddNo As DataGridViewTextBoxColumn
    Friend WithEvents 請求情報請求番号 As DataGridViewTextBoxColumn
    Friend WithEvents 請求金額 As DataGridViewTextBoxColumn
    Friend WithEvents 請求情報入金額計 As DataGridViewTextBoxColumn
    Friend WithEvents 請求情報請求残高 As DataGridViewTextBoxColumn
    Friend WithEvents 入金額 As DataGridViewTextBoxColumn
    Friend WithEvents No As DataGridViewTextBoxColumn
    Friend WithEvents 請求番号 As DataGridViewTextBoxColumn
    Friend WithEvents 入金済請求先 As DataGridViewTextBoxColumn
    Friend WithEvents 入金番号 As DataGridViewTextBoxColumn
    Friend WithEvents 入金日 As DataGridViewTextBoxColumn
    Friend WithEvents 入金種別 As DataGridViewTextBoxColumn
    Friend WithEvents 入金済入金額計 As DataGridViewTextBoxColumn
    Friend WithEvents 備考 As DataGridViewTextBoxColumn
End Class

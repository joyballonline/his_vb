﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MstCompany
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Dgv_Company = New System.Windows.Forms.DataGridView()
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.btnCompanyAdd = New System.Windows.Forms.Button()
        Me.btnCompanyEdit = New System.Windows.Forms.Button()
        Me.TxtSearch = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BtnSearch = New System.Windows.Forms.Button()
        Me.会社コード = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.会社名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.会社略称 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.郵便番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.住所１ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.住所２ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.住所３ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.電話番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FAX番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.代表者役職 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.代表者名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.表示順 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.備考 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.銀行名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.銀行コード = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.支店名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.支店コード = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.預金種目 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.口座番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.口座名義 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.前回締日 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.今回締日 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.次回締日 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.在庫単価評価法 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.前払法人税率 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.会計用コード = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.更新者 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.更新日 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.Dgv_Company, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Dgv_Company
        '
        Me.Dgv_Company.AllowUserToAddRows = False
        Me.Dgv_Company.AllowUserToDeleteRows = False
        Me.Dgv_Company.AllowUserToResizeRows = False
        Me.Dgv_Company.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Dgv_Company.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.会社コード, Me.会社名, Me.会社略称, Me.郵便番号, Me.住所１, Me.住所２, Me.住所３, Me.電話番号, Me.FAX番号, Me.代表者役職, Me.代表者名, Me.表示順, Me.備考, Me.銀行名, Me.銀行コード, Me.支店名, Me.支店コード, Me.預金種目, Me.口座番号, Me.口座名義, Me.前回締日, Me.今回締日, Me.次回締日, Me.在庫単価評価法, Me.前払法人税率, Me.会計用コード, Me.更新者, Me.更新日})
        Me.Dgv_Company.Location = New System.Drawing.Point(12, 33)
        Me.Dgv_Company.Name = "Dgv_Company"
        Me.Dgv_Company.ReadOnly = True
        Me.Dgv_Company.RowHeadersVisible = False
        Me.Dgv_Company.RowTemplate.Height = 21
        Me.Dgv_Company.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Dgv_Company.Size = New System.Drawing.Size(1326, 470)
        Me.Dgv_Company.TabIndex = 3
        '
        'BtnBack
        '
        Me.BtnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnBack.Location = New System.Drawing.Point(1173, 509)
        Me.BtnBack.Name = "BtnBack"
        Me.BtnBack.Size = New System.Drawing.Size(165, 40)
        Me.BtnBack.TabIndex = 6
        Me.BtnBack.Text = "戻る"
        Me.BtnBack.UseVisualStyleBackColor = True
        '
        'btnCompanyAdd
        '
        Me.btnCompanyAdd.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnCompanyAdd.Location = New System.Drawing.Point(831, 509)
        Me.btnCompanyAdd.Name = "btnCompanyAdd"
        Me.btnCompanyAdd.Size = New System.Drawing.Size(165, 40)
        Me.btnCompanyAdd.TabIndex = 4
        Me.btnCompanyAdd.Text = "会社情報追加"
        Me.btnCompanyAdd.UseVisualStyleBackColor = True
        '
        'btnCompanyEdit
        '
        Me.btnCompanyEdit.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnCompanyEdit.Location = New System.Drawing.Point(1002, 509)
        Me.btnCompanyEdit.Name = "btnCompanyEdit"
        Me.btnCompanyEdit.Size = New System.Drawing.Size(165, 40)
        Me.btnCompanyEdit.TabIndex = 5
        Me.btnCompanyEdit.Text = "会社情報編集"
        Me.btnCompanyEdit.UseVisualStyleBackColor = True
        '
        'TxtSearch
        '
        Me.TxtSearch.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtSearch.Location = New System.Drawing.Point(70, 6)
        Me.TxtSearch.Name = "TxtSearch"
        Me.TxtSearch.Size = New System.Drawing.Size(100, 22)
        Me.TxtSearch.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 15)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "会社名"
        '
        'BtnSearch
        '
        Me.BtnSearch.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnSearch.Location = New System.Drawing.Point(176, 5)
        Me.BtnSearch.Name = "BtnSearch"
        Me.BtnSearch.Size = New System.Drawing.Size(75, 23)
        Me.BtnSearch.TabIndex = 2
        Me.BtnSearch.Text = "検索"
        Me.BtnSearch.UseVisualStyleBackColor = True
        '
        '会社コード
        '
        Me.会社コード.HeaderText = "会社コード"
        Me.会社コード.Name = "会社コード"
        Me.会社コード.ReadOnly = True
        Me.会社コード.Visible = False
        '
        '会社名
        '
        Me.会社名.HeaderText = "会社名"
        Me.会社名.Name = "会社名"
        Me.会社名.ReadOnly = True
        '
        '会社略称
        '
        Me.会社略称.HeaderText = "会社略称"
        Me.会社略称.Name = "会社略称"
        Me.会社略称.ReadOnly = True
        '
        '郵便番号
        '
        Me.郵便番号.HeaderText = "郵便番号"
        Me.郵便番号.Name = "郵便番号"
        Me.郵便番号.ReadOnly = True
        '
        '住所１
        '
        Me.住所１.HeaderText = "住所１"
        Me.住所１.Name = "住所１"
        Me.住所１.ReadOnly = True
        '
        '住所２
        '
        Me.住所２.HeaderText = "住所２"
        Me.住所２.Name = "住所２"
        Me.住所２.ReadOnly = True
        '
        '住所３
        '
        Me.住所３.HeaderText = "住所３"
        Me.住所３.Name = "住所３"
        Me.住所３.ReadOnly = True
        '
        '電話番号
        '
        Me.電話番号.HeaderText = "電話番号"
        Me.電話番号.Name = "電話番号"
        Me.電話番号.ReadOnly = True
        '
        'FAX番号
        '
        Me.FAX番号.HeaderText = "FAX番号"
        Me.FAX番号.Name = "FAX番号"
        Me.FAX番号.ReadOnly = True
        '
        '代表者役職
        '
        Me.代表者役職.HeaderText = "代表者役職"
        Me.代表者役職.Name = "代表者役職"
        Me.代表者役職.ReadOnly = True
        '
        '代表者名
        '
        Me.代表者名.HeaderText = "代表者名"
        Me.代表者名.Name = "代表者名"
        Me.代表者名.ReadOnly = True
        '
        '表示順
        '
        Me.表示順.HeaderText = "表示順"
        Me.表示順.Name = "表示順"
        Me.表示順.ReadOnly = True
        '
        '備考
        '
        Me.備考.HeaderText = "備考"
        Me.備考.Name = "備考"
        Me.備考.ReadOnly = True
        '
        '銀行名
        '
        Me.銀行名.HeaderText = "銀行名"
        Me.銀行名.Name = "銀行名"
        Me.銀行名.ReadOnly = True
        '
        '銀行コード
        '
        Me.銀行コード.HeaderText = "銀行コード"
        Me.銀行コード.Name = "銀行コード"
        Me.銀行コード.ReadOnly = True
        '
        '支店名
        '
        Me.支店名.HeaderText = "支店名"
        Me.支店名.Name = "支店名"
        Me.支店名.ReadOnly = True
        '
        '支店コード
        '
        Me.支店コード.HeaderText = "支店コード"
        Me.支店コード.Name = "支店コード"
        Me.支店コード.ReadOnly = True
        '
        '預金種目
        '
        Me.預金種目.HeaderText = "預金種目"
        Me.預金種目.Name = "預金種目"
        Me.預金種目.ReadOnly = True
        '
        '口座番号
        '
        Me.口座番号.HeaderText = "口座番号"
        Me.口座番号.Name = "口座番号"
        Me.口座番号.ReadOnly = True
        '
        '口座名義
        '
        Me.口座名義.HeaderText = "口座名義"
        Me.口座名義.Name = "口座名義"
        Me.口座名義.ReadOnly = True
        '
        '前回締日
        '
        Me.前回締日.HeaderText = "前回締日"
        Me.前回締日.Name = "前回締日"
        Me.前回締日.ReadOnly = True
        '
        '今回締日
        '
        Me.今回締日.HeaderText = "今回締日"
        Me.今回締日.Name = "今回締日"
        Me.今回締日.ReadOnly = True
        '
        '次回締日
        '
        Me.次回締日.HeaderText = "次回締日"
        Me.次回締日.Name = "次回締日"
        Me.次回締日.ReadOnly = True
        '
        '在庫単価評価法
        '
        Me.在庫単価評価法.HeaderText = "在庫単価評価法"
        Me.在庫単価評価法.Name = "在庫単価評価法"
        Me.在庫単価評価法.ReadOnly = True
        '
        '前払法人税率
        '
        Me.前払法人税率.HeaderText = "前払法人税率"
        Me.前払法人税率.Name = "前払法人税率"
        Me.前払法人税率.ReadOnly = True
        '
        '会計用コード
        '
        Me.会計用コード.HeaderText = "会計用コード"
        Me.会計用コード.Name = "会計用コード"
        Me.会計用コード.ReadOnly = True
        '
        '更新者
        '
        Me.更新者.HeaderText = "更新者"
        Me.更新者.Name = "更新者"
        Me.更新者.ReadOnly = True
        '
        '更新日
        '
        Me.更新日.HeaderText = "更新日"
        Me.更新日.Name = "更新日"
        Me.更新日.ReadOnly = True
        '
        'MstCompany
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(1350, 561)
        Me.Controls.Add(Me.BtnSearch)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtSearch)
        Me.Controls.Add(Me.BtnBack)
        Me.Controls.Add(Me.btnCompanyAdd)
        Me.Controls.Add(Me.btnCompanyEdit)
        Me.Controls.Add(Me.Dgv_Company)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "MstCompany"
        Me.Text = "MstCompany"
        CType(Me.Dgv_Company, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Dgv_Company As DataGridView
    Friend WithEvents BtnBack As Button
    Friend WithEvents btnCompanyAdd As Button
    Friend WithEvents btnCompanyEdit As Button
    Friend WithEvents TxtSearch As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents BtnSearch As Button
    Friend WithEvents 会社コード As DataGridViewTextBoxColumn
    Friend WithEvents 会社名 As DataGridViewTextBoxColumn
    Friend WithEvents 会社略称 As DataGridViewTextBoxColumn
    Friend WithEvents 郵便番号 As DataGridViewTextBoxColumn
    Friend WithEvents 住所１ As DataGridViewTextBoxColumn
    Friend WithEvents 住所２ As DataGridViewTextBoxColumn
    Friend WithEvents 住所３ As DataGridViewTextBoxColumn
    Friend WithEvents 電話番号 As DataGridViewTextBoxColumn
    Friend WithEvents FAX番号 As DataGridViewTextBoxColumn
    Friend WithEvents 代表者役職 As DataGridViewTextBoxColumn
    Friend WithEvents 代表者名 As DataGridViewTextBoxColumn
    Friend WithEvents 表示順 As DataGridViewTextBoxColumn
    Friend WithEvents 備考 As DataGridViewTextBoxColumn
    Friend WithEvents 銀行名 As DataGridViewTextBoxColumn
    Friend WithEvents 銀行コード As DataGridViewTextBoxColumn
    Friend WithEvents 支店名 As DataGridViewTextBoxColumn
    Friend WithEvents 支店コード As DataGridViewTextBoxColumn
    Friend WithEvents 預金種目 As DataGridViewTextBoxColumn
    Friend WithEvents 口座番号 As DataGridViewTextBoxColumn
    Friend WithEvents 口座名義 As DataGridViewTextBoxColumn
    Friend WithEvents 前回締日 As DataGridViewTextBoxColumn
    Friend WithEvents 今回締日 As DataGridViewTextBoxColumn
    Friend WithEvents 次回締日 As DataGridViewTextBoxColumn
    Friend WithEvents 在庫単価評価法 As DataGridViewTextBoxColumn
    Friend WithEvents 前払法人税率 As DataGridViewTextBoxColumn
    Friend WithEvents 会計用コード As DataGridViewTextBoxColumn
    Friend WithEvents 更新者 As DataGridViewTextBoxColumn
    Friend WithEvents 更新日 As DataGridViewTextBoxColumn
End Class

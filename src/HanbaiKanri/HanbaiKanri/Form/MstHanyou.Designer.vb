<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MstHanyou
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
        Me.Dgv_Hanyo = New System.Windows.Forms.DataGridView()
        Me.会社コード = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.固定キー = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.可変キー = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.表示順 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.文字１ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.文字２ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.文字３ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.文字４ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.文字５ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.文字６ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.数値１ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.数値２ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.数値３ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.数値４ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.数値５ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.数値６ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.メモ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.更新者 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.更新日 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BtnEdit = New System.Windows.Forms.Button()
        Me.BtnAdd = New System.Windows.Forms.Button()
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.BtnSearch = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TxtFixedkey = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TxtText1 = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TxtNumber1 = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TxtNumber2 = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TxtText2 = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TxtNumber3 = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TxtText3 = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TxtNumber4 = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TxtText4 = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.TxtNumber5 = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.TxtText5 = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.TxtNumber6 = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.TxtText6 = New System.Windows.Forms.TextBox()
        CType(Me.Dgv_Hanyo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Dgv_Hanyo
        '
        Me.Dgv_Hanyo.AllowUserToAddRows = False
        Me.Dgv_Hanyo.AllowUserToDeleteRows = False
        Me.Dgv_Hanyo.AllowUserToResizeColumns = False
        Me.Dgv_Hanyo.AllowUserToResizeRows = False
        Me.Dgv_Hanyo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Dgv_Hanyo.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.会社コード, Me.固定キー, Me.可変キー, Me.表示順, Me.文字１, Me.文字２, Me.文字３, Me.文字４, Me.文字５, Me.文字６, Me.数値１, Me.数値２, Me.数値３, Me.数値４, Me.数値５, Me.数値６, Me.メモ, Me.更新者, Me.更新日})
        Me.Dgv_Hanyo.Location = New System.Drawing.Point(12, 96)
        Me.Dgv_Hanyo.Name = "Dgv_Hanyo"
        Me.Dgv_Hanyo.ReadOnly = True
        Me.Dgv_Hanyo.RowHeadersVisible = False
        Me.Dgv_Hanyo.RowTemplate.Height = 21
        Me.Dgv_Hanyo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Dgv_Hanyo.Size = New System.Drawing.Size(1326, 407)
        Me.Dgv_Hanyo.TabIndex = 3
        '
        '会社コード
        '
        Me.会社コード.HeaderText = "会社コード"
        Me.会社コード.Name = "会社コード"
        Me.会社コード.ReadOnly = True
        '
        '固定キー
        '
        Me.固定キー.HeaderText = "固定キー"
        Me.固定キー.Name = "固定キー"
        Me.固定キー.ReadOnly = True
        '
        '可変キー
        '
        Me.可変キー.HeaderText = "可変キー"
        Me.可変キー.Name = "可変キー"
        Me.可変キー.ReadOnly = True
        '
        '表示順
        '
        Me.表示順.HeaderText = "表示順"
        Me.表示順.Name = "表示順"
        Me.表示順.ReadOnly = True
        '
        '文字１
        '
        Me.文字１.HeaderText = "文字１"
        Me.文字１.Name = "文字１"
        Me.文字１.ReadOnly = True
        '
        '文字２
        '
        Me.文字２.HeaderText = "文字２"
        Me.文字２.Name = "文字２"
        Me.文字２.ReadOnly = True
        '
        '文字３
        '
        Me.文字３.HeaderText = "文字３"
        Me.文字３.Name = "文字３"
        Me.文字３.ReadOnly = True
        '
        '文字４
        '
        Me.文字４.HeaderText = "文字４"
        Me.文字４.Name = "文字４"
        Me.文字４.ReadOnly = True
        '
        '文字５
        '
        Me.文字５.HeaderText = "文字５"
        Me.文字５.Name = "文字５"
        Me.文字５.ReadOnly = True
        '
        '文字６
        '
        Me.文字６.HeaderText = "文字６"
        Me.文字６.Name = "文字６"
        Me.文字６.ReadOnly = True
        '
        '数値１
        '
        Me.数値１.HeaderText = "数値１"
        Me.数値１.Name = "数値１"
        Me.数値１.ReadOnly = True
        '
        '数値２
        '
        Me.数値２.HeaderText = "数値２"
        Me.数値２.Name = "数値２"
        Me.数値２.ReadOnly = True
        '
        '数値３
        '
        Me.数値３.HeaderText = "数値３"
        Me.数値３.Name = "数値３"
        Me.数値３.ReadOnly = True
        '
        '数値４
        '
        Me.数値４.HeaderText = "数値４"
        Me.数値４.Name = "数値４"
        Me.数値４.ReadOnly = True
        '
        '数値５
        '
        Me.数値５.HeaderText = "数値５"
        Me.数値５.Name = "数値５"
        Me.数値５.ReadOnly = True
        '
        '数値６
        '
        Me.数値６.HeaderText = "数値６"
        Me.数値６.Name = "数値６"
        Me.数値６.ReadOnly = True
        '
        'メモ
        '
        Me.メモ.HeaderText = "メモ"
        Me.メモ.Name = "メモ"
        Me.メモ.ReadOnly = True
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
        'BtnEdit
        '
        Me.BtnEdit.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnEdit.Location = New System.Drawing.Point(1002, 509)
        Me.BtnEdit.Name = "BtnEdit"
        Me.BtnEdit.Size = New System.Drawing.Size(165, 40)
        Me.BtnEdit.TabIndex = 5
        Me.BtnEdit.Text = "編集"
        Me.BtnEdit.UseVisualStyleBackColor = True
        '
        'BtnAdd
        '
        Me.BtnAdd.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnAdd.Location = New System.Drawing.Point(831, 509)
        Me.BtnAdd.Name = "BtnAdd"
        Me.BtnAdd.Size = New System.Drawing.Size(165, 40)
        Me.BtnAdd.TabIndex = 4
        Me.BtnAdd.Text = "追加"
        Me.BtnAdd.UseVisualStyleBackColor = True
        Me.BtnAdd.Visible = False
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
        'BtnSearch
        '
        Me.BtnSearch.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnSearch.Location = New System.Drawing.Point(1182, 12)
        Me.BtnSearch.Name = "BtnSearch"
        Me.BtnSearch.Size = New System.Drawing.Size(156, 40)
        Me.BtnSearch.TabIndex = 2
        Me.BtnSearch.Text = "検索"
        Me.BtnSearch.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(61, 15)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "固定キー"
        '
        'TxtFixedkey
        '
        Me.TxtFixedkey.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtFixedkey.Location = New System.Drawing.Point(79, 12)
        Me.TxtFixedkey.Name = "TxtFixedkey"
        Me.TxtFixedkey.Size = New System.Drawing.Size(100, 22)
        Me.TxtFixedkey.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(28, 43)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(45, 15)
        Me.Label2.TabIndex = 16
        Me.Label2.Text = "文字1"
        '
        'TxtText1
        '
        Me.TxtText1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtText1.Location = New System.Drawing.Point(79, 40)
        Me.TxtText1.Name = "TxtText1"
        Me.TxtText1.Size = New System.Drawing.Size(100, 22)
        Me.TxtText1.TabIndex = 15
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(28, 71)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(45, 15)
        Me.Label3.TabIndex = 18
        Me.Label3.Text = "数字1"
        '
        'TxtNumber1
        '
        Me.TxtNumber1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtNumber1.Location = New System.Drawing.Point(79, 68)
        Me.TxtNumber1.Name = "TxtNumber1"
        Me.TxtNumber1.Size = New System.Drawing.Size(100, 22)
        Me.TxtNumber1.TabIndex = 17
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(185, 71)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(45, 15)
        Me.Label4.TabIndex = 22
        Me.Label4.Text = "数字2"
        '
        'TxtNumber2
        '
        Me.TxtNumber2.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtNumber2.Location = New System.Drawing.Point(236, 68)
        Me.TxtNumber2.Name = "TxtNumber2"
        Me.TxtNumber2.Size = New System.Drawing.Size(100, 22)
        Me.TxtNumber2.TabIndex = 21
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.Location = New System.Drawing.Point(185, 43)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(45, 15)
        Me.Label5.TabIndex = 20
        Me.Label5.Text = "文字2"
        '
        'TxtText2
        '
        Me.TxtText2.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtText2.Location = New System.Drawing.Point(236, 40)
        Me.TxtText2.Name = "TxtText2"
        Me.TxtText2.Size = New System.Drawing.Size(100, 22)
        Me.TxtText2.TabIndex = 19
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.Location = New System.Drawing.Point(342, 71)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(45, 15)
        Me.Label6.TabIndex = 26
        Me.Label6.Text = "数字3"
        '
        'TxtNumber3
        '
        Me.TxtNumber3.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtNumber3.Location = New System.Drawing.Point(393, 68)
        Me.TxtNumber3.Name = "TxtNumber3"
        Me.TxtNumber3.Size = New System.Drawing.Size(100, 22)
        Me.TxtNumber3.TabIndex = 25
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label7.Location = New System.Drawing.Point(342, 43)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(45, 15)
        Me.Label7.TabIndex = 24
        Me.Label7.Text = "文字3"
        '
        'TxtText3
        '
        Me.TxtText3.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtText3.Location = New System.Drawing.Point(393, 40)
        Me.TxtText3.Name = "TxtText3"
        Me.TxtText3.Size = New System.Drawing.Size(100, 22)
        Me.TxtText3.TabIndex = 23
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label8.Location = New System.Drawing.Point(499, 71)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(45, 15)
        Me.Label8.TabIndex = 30
        Me.Label8.Text = "数字4"
        '
        'TxtNumber4
        '
        Me.TxtNumber4.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtNumber4.Location = New System.Drawing.Point(550, 68)
        Me.TxtNumber4.Name = "TxtNumber4"
        Me.TxtNumber4.Size = New System.Drawing.Size(100, 22)
        Me.TxtNumber4.TabIndex = 29
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label9.Location = New System.Drawing.Point(499, 43)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(45, 15)
        Me.Label9.TabIndex = 28
        Me.Label9.Text = "文字4"
        '
        'TxtText4
        '
        Me.TxtText4.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtText4.Location = New System.Drawing.Point(550, 40)
        Me.TxtText4.Name = "TxtText4"
        Me.TxtText4.Size = New System.Drawing.Size(100, 22)
        Me.TxtText4.TabIndex = 27
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label10.Location = New System.Drawing.Point(656, 71)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(45, 15)
        Me.Label10.TabIndex = 34
        Me.Label10.Text = "数字5"
        '
        'TxtNumber5
        '
        Me.TxtNumber5.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtNumber5.Location = New System.Drawing.Point(707, 68)
        Me.TxtNumber5.Name = "TxtNumber5"
        Me.TxtNumber5.Size = New System.Drawing.Size(100, 22)
        Me.TxtNumber5.TabIndex = 33
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label11.Location = New System.Drawing.Point(656, 43)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(45, 15)
        Me.Label11.TabIndex = 32
        Me.Label11.Text = "文字5"
        '
        'TxtText5
        '
        Me.TxtText5.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtText5.Location = New System.Drawing.Point(707, 40)
        Me.TxtText5.Name = "TxtText5"
        Me.TxtText5.Size = New System.Drawing.Size(100, 22)
        Me.TxtText5.TabIndex = 31
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label12.Location = New System.Drawing.Point(813, 71)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(45, 15)
        Me.Label12.TabIndex = 38
        Me.Label12.Text = "数字6"
        '
        'TxtNumber6
        '
        Me.TxtNumber6.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtNumber6.Location = New System.Drawing.Point(864, 68)
        Me.TxtNumber6.Name = "TxtNumber6"
        Me.TxtNumber6.Size = New System.Drawing.Size(100, 22)
        Me.TxtNumber6.TabIndex = 37
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label13.Location = New System.Drawing.Point(813, 43)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(45, 15)
        Me.Label13.TabIndex = 36
        Me.Label13.Text = "文字6"
        '
        'TxtText6
        '
        Me.TxtText6.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtText6.Location = New System.Drawing.Point(864, 40)
        Me.TxtText6.Name = "TxtText6"
        Me.TxtText6.Size = New System.Drawing.Size(100, 22)
        Me.TxtText6.TabIndex = 35
        '
        'MstHanyou
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(1350, 561)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.TxtNumber6)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.TxtText6)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.TxtNumber5)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.TxtText5)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.TxtNumber4)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.TxtText4)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TxtNumber3)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TxtText3)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TxtNumber2)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TxtText2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TxtNumber1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtText1)
        Me.Controls.Add(Me.BtnSearch)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtFixedkey)
        Me.Controls.Add(Me.BtnBack)
        Me.Controls.Add(Me.BtnAdd)
        Me.Controls.Add(Me.BtnEdit)
        Me.Controls.Add(Me.Dgv_Hanyo)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "MstHanyou"
        Me.Text = "MstHanyou"
        CType(Me.Dgv_Hanyo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Dgv_Hanyo As DataGridView
    Friend WithEvents 会社コード As DataGridViewTextBoxColumn
    Friend WithEvents 固定キー As DataGridViewTextBoxColumn
    Friend WithEvents 可変キー As DataGridViewTextBoxColumn
    Friend WithEvents 表示順 As DataGridViewTextBoxColumn
    Friend WithEvents 文字１ As DataGridViewTextBoxColumn
    Friend WithEvents 文字２ As DataGridViewTextBoxColumn
    Friend WithEvents 文字３ As DataGridViewTextBoxColumn
    Friend WithEvents 文字４ As DataGridViewTextBoxColumn
    Friend WithEvents 文字５ As DataGridViewTextBoxColumn
    Friend WithEvents 文字６ As DataGridViewTextBoxColumn
    Friend WithEvents 数値１ As DataGridViewTextBoxColumn
    Friend WithEvents 数値２ As DataGridViewTextBoxColumn
    Friend WithEvents 数値３ As DataGridViewTextBoxColumn
    Friend WithEvents 数値４ As DataGridViewTextBoxColumn
    Friend WithEvents 数値５ As DataGridViewTextBoxColumn
    Friend WithEvents 数値６ As DataGridViewTextBoxColumn
    Friend WithEvents メモ As DataGridViewTextBoxColumn
    Friend WithEvents 更新者 As DataGridViewTextBoxColumn
    Friend WithEvents 更新日 As DataGridViewTextBoxColumn
    Friend WithEvents BtnEdit As Button
    Friend WithEvents BtnAdd As Button
    Friend WithEvents BtnBack As Button
    Friend WithEvents BtnSearch As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents TxtFixedkey As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents TxtText1 As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents TxtNumber1 As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents TxtNumber2 As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents TxtText2 As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents TxtNumber3 As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents TxtText3 As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents TxtNumber4 As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents TxtText4 As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents TxtNumber5 As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents TxtText5 As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents TxtNumber6 As TextBox
    Friend WithEvents Label13 As Label
    Friend WithEvents TxtText6 As TextBox
End Class

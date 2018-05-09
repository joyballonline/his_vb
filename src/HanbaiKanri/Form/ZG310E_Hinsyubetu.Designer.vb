<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ZG310E_Hinsyubetu
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Windows フォーム デザイナで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使用して変更できます。  
    'コード エディタを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ZG310E_Hinsyubetu))
        Me.dgvHinsyu = New CustomTabStopDataGridView.TabStop.UtilTabStopDataGridView
        Me.cnJuyousakiCD = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
        Me.cnJuyousaki = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
        Me.cnHinsyuKbn = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
        Me.cnHinsyuKbnNm = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
        Me.cnTougetu = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
        Me.cnYokugetu = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
        Me.cnYyokugetu = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
        Me.cnJuyoSort = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
        Me.cnUpdNm = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
        Me.Label11 = New System.Windows.Forms.Label
        Me.lblKensu = New System.Windows.Forms.Label
        Me.btnModoru = New System.Windows.Forms.Button
        Me.btnTouroku = New System.Windows.Forms.Button
        Me.lblKeikaku = New System.Windows.Forms.Label
        Me.lblJuyo = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.lblHinsyu = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.lblTHanbai = New System.Windows.Forms.Label
        Me.lblYHanbai = New System.Windows.Forms.Label
        Me.lblYYHanbai = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.lblSyori = New System.Windows.Forms.Label
        Me.btnExcel = New System.Windows.Forms.Button
        CType(Me.dgvHinsyu, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvHinsyu
        '
        Me.dgvHinsyu.AllowUserToAddRows = False
        Me.dgvHinsyu.AllowUserToDeleteRows = False
        Me.dgvHinsyu.AllowUserToResizeColumns = False
        Me.dgvHinsyu.AllowUserToResizeRows = False
        Me.dgvHinsyu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvHinsyu.ColumnHeadersVisible = False
        Me.dgvHinsyu.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cnJuyousakiCD, Me.cnJuyousaki, Me.cnHinsyuKbn, Me.cnHinsyuKbnNm, Me.cnTougetu, Me.cnYokugetu, Me.cnYyokugetu, Me.cnJuyoSort, Me.cnUpdNm})
        Me.dgvHinsyu.Location = New System.Drawing.Point(26, 123)
        Me.dgvHinsyu.MultiSelect = False
        Me.dgvHinsyu.Name = "dgvHinsyu"
        Me.dgvHinsyu.RowHeadersVisible = False
        Me.dgvHinsyu.RowTemplate.Height = 21
        Me.dgvHinsyu.Size = New System.Drawing.Size(760, 481)
        Me.dgvHinsyu.TabIndex = 0
        '
        'cnJuyousakiCD
        '
        Me.cnJuyousakiCD.DataPropertyName = "dtJuyousakiCD"
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black
        Me.cnJuyousakiCD.DefaultCellStyle = DataGridViewCellStyle1
        Me.cnJuyousakiCD.HeaderText = "　　"
        Me.cnJuyousakiCD.Name = "cnJuyousakiCD"
        Me.cnJuyousakiCD.ReadOnly = True
        Me.cnJuyousakiCD.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnJuyousakiCD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnJuyousakiCD.TabStop = False
        Me.cnJuyousakiCD.Width = 50
        '
        'cnJuyousaki
        '
        Me.cnJuyousaki.DataPropertyName = "dtJuyousaki"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
        Me.cnJuyousaki.DefaultCellStyle = DataGridViewCellStyle2
        Me.cnJuyousaki.HeaderText = ""
        Me.cnJuyousaki.Name = "cnJuyousaki"
        Me.cnJuyousaki.ReadOnly = True
        Me.cnJuyousaki.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnJuyousaki.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnJuyousaki.TabStop = False
        Me.cnJuyousaki.Width = 130
        '
        'cnHinsyuKbn
        '
        Me.cnHinsyuKbn.DataPropertyName = "dtHinsyuKbn"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black
        Me.cnHinsyuKbn.DefaultCellStyle = DataGridViewCellStyle3
        Me.cnHinsyuKbn.HeaderText = ""
        Me.cnHinsyuKbn.Name = "cnHinsyuKbn"
        Me.cnHinsyuKbn.ReadOnly = True
        Me.cnHinsyuKbn.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnHinsyuKbn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnHinsyuKbn.TabStop = False
        Me.cnHinsyuKbn.Width = 50
        '
        'cnHinsyuKbnNm
        '
        Me.cnHinsyuKbnNm.DataPropertyName = "dtHinsyuKbnNm"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle4.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black
        Me.cnHinsyuKbnNm.DefaultCellStyle = DataGridViewCellStyle4
        Me.cnHinsyuKbnNm.HeaderText = ""
        Me.cnHinsyuKbnNm.Name = "cnHinsyuKbnNm"
        Me.cnHinsyuKbnNm.ReadOnly = True
        Me.cnHinsyuKbnNm.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnHinsyuKbnNm.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnHinsyuKbnNm.TabStop = False
        Me.cnHinsyuKbnNm.Width = 210
        '
        'cnTougetu
        '
        Me.cnTougetu.DataPropertyName = "dtTougetu"
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle5.Format = "#,##0.0"
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Navy
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.White
        Me.cnTougetu.DefaultCellStyle = DataGridViewCellStyle5
        Me.cnTougetu.HeaderText = ""
        Me.cnTougetu.MaxInputLength = 6
        Me.cnTougetu.Name = "cnTougetu"
        Me.cnTougetu.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnTougetu.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnTougetu.TabStop = True
        '
        'cnYokugetu
        '
        Me.cnYokugetu.DataPropertyName = "dtYokugetu"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle6.Format = "#,##0.0"
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Navy
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.White
        Me.cnYokugetu.DefaultCellStyle = DataGridViewCellStyle6
        Me.cnYokugetu.HeaderText = ""
        Me.cnYokugetu.MaxInputLength = 6
        Me.cnYokugetu.Name = "cnYokugetu"
        Me.cnYokugetu.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnYokugetu.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnYokugetu.TabStop = True
        '
        'cnYyokugetu
        '
        Me.cnYyokugetu.DataPropertyName = "dtYyokugetu"
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle7.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle7.Format = "#,##0.0"
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.Navy
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.White
        Me.cnYyokugetu.DefaultCellStyle = DataGridViewCellStyle7
        Me.cnYyokugetu.HeaderText = ""
        Me.cnYyokugetu.MaxInputLength = 6
        Me.cnYyokugetu.Name = "cnYyokugetu"
        Me.cnYyokugetu.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnYyokugetu.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnYyokugetu.TabStop = True
        '
        'cnJuyoSort
        '
        Me.cnJuyoSort.DataPropertyName = "dtJuyoSort"
        Me.cnJuyoSort.HeaderText = ""
        Me.cnJuyoSort.Name = "cnJuyoSort"
        Me.cnJuyoSort.TabStop = True
        Me.cnJuyoSort.Visible = False
        '
        'cnUpdNm
        '
        Me.cnUpdNm.DataPropertyName = "dtUpdNm"
        Me.cnUpdNm.HeaderText = ""
        Me.cnUpdNm.Name = "cnUpdNm"
        Me.cnUpdNm.TabStop = True
        Me.cnUpdNm.Visible = False
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label11.Location = New System.Drawing.Point(607, 24)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(73, 22)
        Me.Label11.TabIndex = 1319
        Me.Label11.Text = "計画年月"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblKensu
        '
        Me.lblKensu.BackColor = System.Drawing.SystemColors.Control
        Me.lblKensu.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKensu.Location = New System.Drawing.Point(729, 46)
        Me.lblKensu.Name = "lblKensu"
        Me.lblKensu.Size = New System.Drawing.Size(57, 22)
        Me.lblKensu.TabIndex = 1321
        Me.lblKensu.Text = "9999件"
        Me.lblKensu.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnModoru
        '
        Me.btnModoru.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnModoru.Location = New System.Drawing.Point(656, 630)
        Me.btnModoru.Name = "btnModoru"
        Me.btnModoru.Size = New System.Drawing.Size(130, 45)
        Me.btnModoru.TabIndex = 3
        Me.btnModoru.Text = "戻る(&B)"
        Me.btnModoru.UseVisualStyleBackColor = True
        '
        'btnTouroku
        '
        Me.btnTouroku.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnTouroku.Location = New System.Drawing.Point(520, 630)
        Me.btnTouroku.Name = "btnTouroku"
        Me.btnTouroku.Size = New System.Drawing.Size(130, 45)
        Me.btnTouroku.TabIndex = 2
        Me.btnTouroku.Text = "登録(&E)"
        Me.btnTouroku.UseVisualStyleBackColor = True
        '
        'lblKeikaku
        '
        Me.lblKeikaku.AccessibleDescription = ""
        Me.lblKeikaku.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblKeikaku.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblKeikaku.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKeikaku.Location = New System.Drawing.Point(686, 24)
        Me.lblKeikaku.Name = "lblKeikaku"
        Me.lblKeikaku.Size = New System.Drawing.Size(100, 22)
        Me.lblKeikaku.TabIndex = 1325
        Me.lblKeikaku.Text = "2010/02"
        Me.lblKeikaku.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblJuyo
        '
        Me.lblJuyo.BackColor = System.Drawing.SystemColors.Control
        Me.lblJuyo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblJuyo.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblJuyo.Location = New System.Drawing.Point(26, 71)
        Me.lblJuyo.Name = "lblJuyo"
        Me.lblJuyo.Size = New System.Drawing.Size(54, 53)
        Me.lblJuyo.TabIndex = 1326
        Me.lblJuyo.Text = "需要先"
        Me.lblJuyo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(78, 71)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(130, 53)
        Me.Label3.TabIndex = 1327
        Me.Label3.Text = "需要先名"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblHinsyu
        '
        Me.lblHinsyu.BackColor = System.Drawing.SystemColors.Control
        Me.lblHinsyu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblHinsyu.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblHinsyu.Location = New System.Drawing.Point(207, 71)
        Me.lblHinsyu.Name = "lblHinsyu"
        Me.lblHinsyu.Size = New System.Drawing.Size(50, 53)
        Me.lblHinsyu.TabIndex = 1328
        Me.lblHinsyu.Text = "品種区分"
        Me.lblHinsyu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label5.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.Location = New System.Drawing.Point(256, 71)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(212, 53)
        Me.Label5.TabIndex = 1329
        Me.Label5.Text = "品種区分名"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label7.Location = New System.Drawing.Point(467, 71)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(301, 31)
        Me.Label7.TabIndex = 1330
        Me.Label7.Text = "販売計画(単位:t)"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTHanbai
        '
        Me.lblTHanbai.BackColor = System.Drawing.SystemColors.Control
        Me.lblTHanbai.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTHanbai.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTHanbai.Location = New System.Drawing.Point(467, 101)
        Me.lblTHanbai.Name = "lblTHanbai"
        Me.lblTHanbai.Size = New System.Drawing.Size(101, 23)
        Me.lblTHanbai.TabIndex = 1331
        Me.lblTHanbai.Text = "2010/01"
        Me.lblTHanbai.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblYHanbai
        '
        Me.lblYHanbai.BackColor = System.Drawing.SystemColors.Control
        Me.lblYHanbai.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblYHanbai.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblYHanbai.Location = New System.Drawing.Point(567, 101)
        Me.lblYHanbai.Name = "lblYHanbai"
        Me.lblYHanbai.Size = New System.Drawing.Size(101, 23)
        Me.lblYHanbai.TabIndex = 1332
        Me.lblYHanbai.Text = "2010/02"
        Me.lblYHanbai.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblYYHanbai
        '
        Me.lblYYHanbai.BackColor = System.Drawing.SystemColors.Control
        Me.lblYYHanbai.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblYYHanbai.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblYYHanbai.Location = New System.Drawing.Point(667, 101)
        Me.lblYYHanbai.Name = "lblYYHanbai"
        Me.lblYYHanbai.Size = New System.Drawing.Size(101, 23)
        Me.lblYYHanbai.TabIndex = 1333
        Me.lblYYHanbai.Text = "2010/03"
        Me.lblYYHanbai.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label13.Location = New System.Drawing.Point(422, 24)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(73, 22)
        Me.Label13.TabIndex = 1334
        Me.Label13.Text = "処理年月"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblSyori
        '
        Me.lblSyori.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblSyori.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSyori.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSyori.Location = New System.Drawing.Point(501, 24)
        Me.lblSyori.Name = "lblSyori"
        Me.lblSyori.Size = New System.Drawing.Size(100, 22)
        Me.lblSyori.TabIndex = 1335
        Me.lblSyori.Text = "2010/01"
        Me.lblSyori.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnExcel
        '
        Me.btnExcel.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnExcel.Location = New System.Drawing.Point(26, 629)
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Size = New System.Drawing.Size(130, 45)
        Me.btnExcel.TabIndex = 1
        Me.btnExcel.Text = "EXCEL(&K)"
        Me.btnExcel.UseVisualStyleBackColor = True
        '
        'ZG310E_Hinsyubetu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(812, 686)
        Me.Controls.Add(Me.btnExcel)
        Me.Controls.Add(Me.lblSyori)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.lblYYHanbai)
        Me.Controls.Add(Me.lblYHanbai)
        Me.Controls.Add(Me.lblTHanbai)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.lblHinsyu)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lblJuyo)
        Me.Controls.Add(Me.lblKeikaku)
        Me.Controls.Add(Me.btnModoru)
        Me.Controls.Add(Me.btnTouroku)
        Me.Controls.Add(Me.lblKensu)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.dgvHinsyu)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "ZG310E_Hinsyubetu"
        Me.Text = "品種別販売計画入力"
        CType(Me.dgvHinsyu, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgvHinsyu As CustomTabStopDataGridView.TabStop.UtilTabStopDataGridView
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents lblKensu As System.Windows.Forms.Label
    Friend WithEvents btnModoru As System.Windows.Forms.Button
    Friend WithEvents btnTouroku As System.Windows.Forms.Button
    Friend WithEvents lblKeikaku As System.Windows.Forms.Label
    Friend WithEvents lblJuyo As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblHinsyu As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblTHanbai As System.Windows.Forms.Label
    Friend WithEvents lblYHanbai As System.Windows.Forms.Label
    Friend WithEvents lblYYHanbai As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents lblSyori As System.Windows.Forms.Label
    Friend WithEvents btnExcel As System.Windows.Forms.Button
    Friend WithEvents cnJuyousakiCD As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnJuyousaki As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnHinsyuKbn As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnHinsyuKbnNm As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnTougetu As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnYokugetu As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnYyokugetu As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnJuyoSort As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnUpdNm As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
End Class

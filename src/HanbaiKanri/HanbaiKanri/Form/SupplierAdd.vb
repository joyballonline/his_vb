﻿Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls


Public Class SupplierAdd
    Inherits System.Windows.Forms.Form

    '------------------------------------------------------------------------------------------------------
    'メンバー定数宣言
    '------------------------------------------------------------------------------------------------------
    'PG制御文字 
    Private Const N As String = ControlChars.NewLine            '改行文字
    Private Const RS As String = "RecSet"                               'レコードセットテーブル
    Private Const HAIFUN_ID As String = "H@@@@@"
    Private Const HAIFUN_GYOMU1 As String = "-----------"
    Private Const HAIFUN_SHORI As String = "----------------"
    Private Const HAIFUN_SETUMEI As String = "-------------------------------------------"
    Private Const HAIFUN_MYSOUSANICHIJI As String = "---------------"
    Private Const HAIFUN_SOUSA As String = "----------"
    Private Const HAIFUN_ZENKAI As String = "---------------"

    '-------------------------------------------------------------------------------
    '   変数定義
    '-------------------------------------------------------------------------------
    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    'Private _gh As UtilDataGridViewHandler
    Private _init As Boolean                             '初期処理済フラグ

    '-------------------------------------------------------------------------------
    'デフォルトコンストラクタ（隠蔽）
    '-------------------------------------------------------------------------------
    Private Sub New()
        ' この呼び出しは、Windows フォーム デザイナで必要です。
        InitializeComponent()
    End Sub

    '-------------------------------------------------------------------------------
    'コンストラクタ　メニューから呼ばれる
    '-------------------------------------------------------------------------------
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefDbHd As UtilDBIf)
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示

        _init = True

    End Sub

    Private Sub btnAddSupplier_Click(sender As Object, e As EventArgs) Handles btnAddSupplier.Click
        Dim dtToday As DateTime = DateTime.Now
        Try
            Dim Sql As String = ""

            Sql = ""
            Sql += "INSERT INTO "
            Sql += "Public."
            Sql += "m11_supplier("
            Sql += "会社コード, 仕入先コード, 仕入先名, 仕入先名略称, 郵便番号, 住所１, 住所２, 住所３, 電話番号, 電話番号検索用, ＦＡＸ番号, 担当者名, 既定間接費率, メモ, 銀行コード, 支店コード, 預金種目, 口座番号, 口座名義,  更新者, 更新日)"
            Sql += " VALUES('"
            Sql += CompanyCode.Text
            Sql += "', '"
            Sql += SupplierCode.Text
            Sql += "', '"
            Sql += SupplierName.Text
            Sql += "', '"
            Sql += SupplierShortName.Text
            Sql += "', '"
            Sql += PostalCode.Text
            Sql += "', '"
            Sql += Address1.Text
            Sql += "', '"
            Sql += Address2.Text
            Sql += "', '"
            Sql += Address3.Text
            Sql += "', '"
            Sql += Tel.Text
            Sql += "', '"
            Sql += TelSearch.Text
            Sql += "', '"
            Sql += Fax.Text
            Sql += "', '"
            Sql += Person.Text
            Sql += "', '"
            Sql += Costs.Text
            Sql += "', '"
            Sql += Memo.Text
            Sql += "', '"
            Sql += BankCode.Text
            Sql += "', '"
            Sql += BranchOfficeCode.Text
            Sql += "', '"
            Sql += DepositCategory.Text
            Sql += "', '"
            Sql += AccountNumber.Text
            Sql += "', '"
            Sql += AccountName.Text
            Sql += "', '"
            Sql += "Admin"
            Sql += "', '"
            Sql += dtToday
            Sql += " ')"
            Sql += "RETURNING 会社コード"
            Sql += ", "
            Sql += "仕入先コード"
            Sql += ", "
            Sql += "仕入先名"
            Sql += ", "
            Sql += "仕入先名略称"
            Sql += ", "
            Sql += "郵便番号"
            Sql += ", "
            Sql += "住所１"
            Sql += ", "
            Sql += "住所２"
            Sql += ", "
            Sql += "住所３"
            Sql += ", "
            Sql += "電話番号"
            Sql += ", "
            Sql += "電話番号検索用"
            Sql += ", "
            Sql += "ＦＡＸ番号"
            Sql += ", "
            Sql += "担当者名"
            Sql += ", "
            Sql += "既定間接費率"
            Sql += ", "
            Sql += "メモ"
            Sql += ", "
            Sql += "銀行コード"
            Sql += ", "
            Sql += "支店コード"
            Sql += ", "
            Sql += "預金種目"
            Sql += ", "
            Sql += "口座番号"
            Sql += ", "
            Sql += "口座名義"
            Sql += ", "
            Sql += "更新者"
            Sql += ", "
            Sql += "更新日"

            _db.executeDB(Sql)

            Dim frmMC As MstSupplier
            frmMC = New MstSupplier(_msgHd, _db)
            frmMC.Show()
            Me.Close()

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        Dim MstSupplier As MstSupplier
        MstSupplier = New MstSupplier(_msgHd, _db)
        MstSupplier.Show()
        Me.Close()
    End Sub
End Class
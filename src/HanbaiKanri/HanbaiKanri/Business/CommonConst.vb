'===============================================================================
'
'　株式会社 全備様
'　　（システム名）販売管理
'　　（処理機能名）共通定数
'    （機能ID）    CommonConst
'
'===============================================================================
'　履歴　名前　　　　　日　付       マーク      内容
'-------------------------------------------------------------------------------
'　(1)   狩野        2018/02/22                 新規              
'-------------------------------------------------------------------------------
Public Class CommonConst

    '汎用マスタ固定KEY
    Public Const HANYO_KOTEI_SKBUNRUI As String = "1001出荷分類"        '1001出荷分類
    Public Const HANYO_KOTEI_CMNNO As String = "1002注文帳番号"         '1002注文帳番号
    Public Const HANYO_KOTEI_UNSOUBIN As String = "1003運送便"          '1003運送便
    Public Const HANYO_REITOU_KBN As String = "1004冷凍区分"            '1004冷凍区分
    Public Const HANYO_KAZEI_KBN As String = "1005課税区分"             '1005税区分
    Public Const HANYO_TANI As String = "1006単位"                      '1006単位
    Public Const HANYO_KONPOU_KBN As String = "1007梱包区分"            '1007梱包区分
    Public Const HANYO_SHOHIN_BUNRUI As String = "1008大分類"           '1008大分類
    Public Const HANYO_HANBAISIIRE_KBN As String = "1009販売仕入"       '1009販売仕入
    Public Const HANYO_NYUKINSYUBETU As String = "1010入金種別"         '1010入金種別
    Public Const HANYO_CHOHYO_BIKOU As String = "1012伝票帳票備考"      '1012伝票帳票備考
    Public Const HANYO_SHUKKASU_ICHIRAN As String = "1013出荷数一覧表"  '1013出荷数一覧表
    Public Const HANYO_KEY_HANYOMST As String = "00"                    '汎用マスタ用処理モード
    Public Const HANYO_KEY_SYORIFLG As String = "10"                    '汎用マスタ用処理モード判定フラグ

    '出荷分類
    Public Const SKBUNRUI_ITAKU As String = "1"                         '委託
    Public Const SKBUNRUI_URIAGE As String = "2"                        '売上

    '消費税区分
    Public Const SHOUHIZEI_KBN As String = "CZ"

    '伝票接続文字
    Public Const CONNECT_DEN_ITAKU As String = "T"                      '委託
    Public Const CONNECT_DEN_URIAGE As String = "U"                     '売上
    Public Const CONNECT_DEN_NYUKIN As String = "N"                     '入金
    Public Const CONNECT_DEN_SHIIRE As String = "R"                     '仕入
    Public Const CONNECT_DEN_SHIHARAI As String = "H"                   '支払

    '伝票番号 連番桁数
    Public Const KETA_DEN_ITAKU As Integer = 6                          '委託
    Public Const KETA_DEN_URIAGE As Integer = 6                         '売上
    Public Const KETA_DEN_NYUKIN As Integer = 6                         '入金
    Public Const KETA_DEN_SHIIRE As Integer = 6                         '仕入
    Public Const KETA_DEN_SHIHARAI As Integer = 6                       '支払

    '操作履歴ログ用
    Public Const PROGRAM_START As String = "1"                          '初期処理時
    Public Const PROGRAM_UPDATE As String = "2"                         '入力画面起動時
    Public Const PROGRAM_REPORT As String = "9"                         '更新印刷時
    Public Const STATUS_NORMAL As String = "0"                          '正常

    '1009販売仕入
    Public Const HAN_HANBAISIIRE_KBN_HANBAI As String = "1"    '1009販売仕入　販売
    Public Const HAN_HANBAISIIRE_KBN_SHIIRE As String = "2"    '1009販売仕入　仕入

    '特売区分
    Public Const TOKUBAI_KBN_NORMAL As String = "0"             '通常
    Public Const TOKUBAI_KBN_BARGAIN As String = "1"            '特売

    '販売単価パターン
    Public Const TANKA_PTN_BARGAIN As String = "1"              '特売単価
    Public Const TANKA_PTN_CUSTOMER As String = "2"             '出荷先別単価
    Public Const TANKA_PTN_GOODS As String = "3"                '商品別単価
    Public Const TANKA_PTN_NONE As String = "9"                 '単価なし

    '仕入単価パターン
    Public Const SHIIRE_TANKA_PTN_CUSTOMER As String = "2"      '出荷先別単価
    Public Const SHIIRE_TANKA_PTN_GOODS As String = "3"         '商品別単価
    Public Const SHIIRE_TANKA_PTN_NONE As String = "9"          '単価なし

    '取引先コード指定
    Public Const CUSTOMER_CODE_ALL As String = "ALL"            '指定なし

    '仕入区分名称
    Public Const SHIIRE_KBN_NM_SHIIRE As String = "仕入"        '仕入

    '印刷区分
    Public Const REPORT_PREVIEW As String = "プレビュー"        'プレビュー
    Public Const REPORT_DIRECT As String = "印刷"               '直接印刷

    '納品書／請求書区分
    Public Const REPORT_NOHINSHO As String = "納品書"          '納品書
    Public Const REPORT_SEIKYUSHO As String = "請求書"         '請求書

    '採番区分
    Public Const SAIBAN_ITAKU As String = "10"                  '委託注文番号
    Public Const SAIBAN_URIAGE As String = "20"                 '売上注文番号
    Public Const SAIBAN_NYUKIN As String = "30"                 '入金伝番
    Public Const SAIBAN_SHIIRE As String = "40"                 '仕入伝番
    Public Const SAIBAN_SHIHARAI As String = "50"               '支払伝番

    '税区分
    Public Const TAXKBN_External As String = "1"                '外税
    Public Const TAXKBN_Included As String = "2"                '内税
    Public Const TAXKBN_Exempt As String = "3"                  '非課税

    '税算出区分
    Public Const TAXSANSHUTSUKBN_DENPYO As String = "1"         '伝票単位
    Public Const TAXSANSHUTSUKBN_MEISAI As String = "2"         '明細単位

    '端数区分
    Public Const HASUKBN_ROUNDDOWN As String = "1"              '切り捨て
    Public Const HASUKBN_ROUNDOFF As String = "2"               '四捨五入
    Public Const HASUKBN_ROUNDUP As String = "3"                '切り上げ

    '冷凍区分
    Public Const REITOU_KBN_REITOU As String = "1"              '冷凍
    Public Const REITOU_KBN_CHILLED As String = "2"             'チルド

    '支払有無名称
    Public Const SHIHARAIUMUNM_MI As String = "未"              '未払い
    Public Const SHIHARAIUMUNM_SUMI As String = "済"            '支払済
    Public Const SHIHARAIUMUNM_ICHIBU As String = "一部"        '一部支払済

    '取引先選択画面 対象区分
    Public Const TORIHIKISAKI_TARGET_KBN_SEIKYU As String = "1"     '請求先
    Public Const TORIHIKISAKI_TARGET_KBN_SHUKKA As String = "2"     '出荷先
    Public Const TORIHIKISAKI_TARGET_KBN_SHUKKAG As String = "3"    '出荷先Ｇ
    Public Const TORIHIKISAKI_TARGET_KBN_SHIIRE As String = "4"     '仕入先
    Public Const TORIHIKISAKI_TARGET_KBN_SHIHARAI As String = "5"   '支払先

    '取引先 分類名
    Public Const TORIHIKISAKI_BUNRUINM_SEIKYU As String = "請求"    '請求先
    Public Const TORIHIKISAKI_BUNRUINM_SHUKKAG As String = "出荷Ｇ" '出荷先Ｇ
    Public Const TORIHIKISAKI_BUNRUINM_SHIIRE As String = "仕入"    '仕入先
    Public Const TORIHIKISAKI_BUNRUINM_SHIHARAI As String = "支払"  '支払先

    '起動元処理ID
    Public Const STARTUPID_MENU As String = "C0130"             'メニュー（C01F30）

    '処理ＩＤ
    Public Const MENU_H0110 As String = "H0110"           '注文登録
    Public Const MENU_H0120 As String = "H0120"           '注文変更
    Public Const MENU_H0130 As String = "H0130"           '注文取消
    Public Const MENU_H0140 As String = "H0140"           '注文照会
    Public Const MENU_H0210 As String = "H0210"           '注文帳照会
    Public Const MENU_H0310 As String = "H0310"           '委託売上登録
    Public Const MENU_H0320 As String = "H0320"           '委託売上変更
    Public Const MENU_H0330 As String = "H0330"           '委託売上取消
    Public Const MENU_H0340 As String = "H0340"           '委託売上照会
    Public Const MENU_H0410 As String = "H0410"           '請求書発行
    Public Const MENU_H0510 As String = "H0510"           '入金登録
    Public Const MENU_H0520 As String = "H0520"           '入金変更
    Public Const MENU_H0530 As String = "H0530"           '入金取消
    Public Const MENU_H0540 As String = "H0540"           '入金照会
    Public Const MENU_H0610 As String = "H0610"           '仕入登録
    Public Const MENU_H0620 As String = "H0620"           '仕入変更
    Public Const MENU_H0630 As String = "H0630"           '仕入取消
    Public Const MENU_H0640 As String = "H0640"           '仕入照会
    Public Const MENU_H0710 As String = "H0710"           '支払登録
    Public Const MENU_H0720 As String = "H0720"           '支払変更
    Public Const MENU_H0730 As String = "H0730"           '支払取消
    Public Const MENU_H0740 As String = "H0740"           '支払照会
    Public Const MENU_H1001 As String = "H1001"           '注文明細表
    Public Const MENU_H1002 As String = "H1002"           '売上未計上一覧表
    Public Const MENU_H1003 As String = "H1003"           '売掛金一覧表
    Public Const MENU_H1004 As String = "H1004"           '得意先元帳
    Public Const MENU_H1005 As String = "H1005"           '出荷数一覧表
    Public Const MENU_H1101 As String = "H1101"           '仕入明細表
    Public Const MENU_H1102 As String = "H1102"           '買掛金一覧表
    Public Const MENU_H1103 As String = "H1103"           '仕入先元帳
    Public Const MENU_H1121 As String = "H1121"           '仕入総括表
    Public Const MENU_M0170 As String = "M0170"           '汎用マスタ一覧
    Public Const MENU_M7002 As String = "M7002"           '汎用マスタ保守
    Public Const MENU_M0120 As String = "M0120"           '取引先マスタ一覧
    Public Const MENU_M2002 As String = "M2002"           '取引先マスタ保守
    Public Const MENU_M0130 As String = "M0130"           '商品マスタ一覧
    Public Const MENU_M3002 As String = "M3002"           '商品マスタ保守
    Public Const MENU_M0110 As String = "M0110"           'ユーザマスタ一覧
    Public Const MENU_M1002 As String = "M1002"           'ユーザマスタ保守
    Public Const MENU_M0140 As String = "M0140"           '販売単価マスタ一覧
    Public Const MENU_M4002 As String = "M4002"           '販売単価マスタ保守
    Public Const MENU_M0150 As String = "M0150"           '仕入単価マスタ一覧
    Public Const MENU_M5002 As String = "M5002"           '仕入単価マスタ保守
    Public Const MENU_M0180 As String = "M0180"           '消費税マスタ一覧
    Public Const MENU_M8002 As String = "M8002"           '消費税マスタ保守

    '各フォームに引き渡す編集モードの値
    Public Const MODE_ADDNEW = 1                                 '登録
    Public Const MODE_EditStatus = 2                             '変更
    Public Const MODE_CancelStatus = 3                           '取消
    Public Const MODE_InquiryStatus = 4                          '照会
    Public Const MODE_ADDNEWCOPY = 5                             '複写新規
    Public Const MODE_DELETE = 6                                 '照会
    '各フォームに引き渡す編集モードの名称
    Public Const MODE_ADDNEW_NAME = "新規"                                 '新規
    Public Const MODE_ADDNEWCOPY_NAME = "複写新規"                         '複写新規
    Public Const MODE_EditStatus_NAME = "変更"                             '変更
    Public Const MODE_CancelStatus_NAME = "取消"                           '取消
    Public Const MODE_InquiryStatus_NAME = "参照"                          '参照
    Public Const MODE_DELETE_NAME = "削除"                                 '削除

    '売上区分
    Public Const URI_KBN_ITAKU = "委託"

    'バックアップサーバ接続時の文言
    Public Const BACKUPSERVER = "★バックアップサーバ接続中★"

    'データ選択
    Public Const SELECT_DATA_NOT_SHITEI = "（指定なし）"                   '指定なし

End Class

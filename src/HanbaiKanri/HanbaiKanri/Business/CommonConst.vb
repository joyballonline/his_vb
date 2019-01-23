'===============================================================================
'
'　カネキ吉田商店様
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
    Public Const MENU_H0110 As String = "H0110"           '見積登録
    Public Const MENU_H0120 As String = "H0120"           '仕入単価入力
    Public Const MENU_H0130 As String = "H0130"           '見積修正
    Public Const MENU_H0140 As String = "H0140"           '見積複写
    Public Const MENU_H0150 As String = "H0150"           '見積参照
    Public Const MENU_H0160 As String = "H0160"           '見積取消
    Public Const MENU_H0170 As String = "H0170"           '受発注登録

    Public Const MENU_H0210 As String = "H0210"           '受注登録
    Public Const MENU_H0220 As String = "H0220"           '受注編集
    Public Const MENU_H0230 As String = "H0230"           '受注複写
    Public Const MENU_H0240 As String = "H0240"           '受注取消
    Public Const MENU_H0250 As String = "H0250"           '受注参照

    Public Const MENU_H0310 As String = "H0310"           '売上登録
    Public Const MENU_H0320 As String = "H0320"           '売上編集
    Public Const MENU_H0330 As String = "H0330"           '売上取消
    Public Const MENU_H0340 As String = "H0340"           '売上参照

    Public Const MENU_H0410 As String = "H0410"           '出庫登録
    Public Const MENU_H0420 As String = "H0420"           '出庫編集
    Public Const MENU_H0430 As String = "H0430"           '出庫取消
    Public Const MENU_H0440 As String = "H0440"           '出庫参照

    Public Const MENU_H0510 As String = "H0510"           '発注登録
    Public Const MENU_H0520 As String = "H0520"           '発注編集
    Public Const MENU_H0530 As String = "H0530"           '発注複写
    Public Const MENU_H0540 As String = "H0540"           '発注取消
    Public Const MENU_H0550 As String = "H0550"           '発注参照

    Public Const MENU_H0610 As String = "H0610"           '仕入登録
    Public Const MENU_H0620 As String = "H0620"           '仕入編集
    Public Const MENU_H0630 As String = "H0630"           '仕入複写
    Public Const MENU_H0640 As String = "H0640"           '仕入取消

    Public Const MENU_H0710 As String = "H0710"           '入庫登録
    Public Const MENU_H0720 As String = "H0720"           '入庫編集
    Public Const MENU_H0730 As String = "H0730"           '入庫複写
    Public Const MENU_H0740 As String = "H0740"           '入庫取消

    Public Const MENU_H0810 As String = "H0810"           '受発注登録

    Public Const MENU_H0910 As String = "H0910"           '請求登録
    Public Const MENU_H0920 As String = "H0920"           '請求編集
    Public Const MENU_H0930 As String = "H0930"           '請求取消
    Public Const MENU_H0940 As String = "H0940"           '請求参照
    Public Const MENU_H0950 As String = "H0950"           '請求計算

    Public Const MENU_H1010 As String = "H1010"           '入金登録
    Public Const MENU_H1020 As String = "H1020"           '入金取消
    Public Const MENU_H1030 As String = "H1030"           '入金参照

    Public Const MENU_H1110 As String = "H1110"           '売掛登録
    Public Const MENU_H1120 As String = "H1120"           '売掛編集
    Public Const MENU_H1130 As String = "H1130"           '売掛取消
    Public Const MENU_H1140 As String = "H1140"           '売掛参照

    Public Const MENU_H1210 As String = "H1210"           '支払登録
    Public Const MENU_H1220 As String = "H1220"           '支払取消
    Public Const MENU_H1230 As String = "H1230"           '支払参照

    Public Const MENU_H1310 As String = "H1310"           '締処理ログ参照
    Public Const MENU_H1320 As String = "H1320"           '仕訳処理

    Public Const MENU_M0110 As String = "M0110"           '汎用マスタ
    Public Const MENU_M0120 As String = "M0120"           '得意先マスタ
    Public Const MENU_M0130 As String = "M0130"           '仕入先マスタ
    Public Const MENU_M0140 As String = "M0140"           '会社マスタ
    Public Const MENU_M0150 As String = "M0150"           'ユーザマスタ
    Public Const MENU_M0160 As String = "M0160"           '言語マスタ
    Public Const MENU_M0170 As String = "M0170"           '在庫マスタ

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

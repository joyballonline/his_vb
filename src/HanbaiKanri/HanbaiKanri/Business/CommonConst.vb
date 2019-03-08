'===============================================================================
'
'　SPIN
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
    Public Const MENU_H0260 As String = "H0260"           '受注残一覧
    Public Const MENU_H0270 As String = "H0270"           '受注一覧

    Public Const MENU_H0310 As String = "H0310"           '売上登録
    Public Const MENU_H0320 As String = "H0320"           '売上編集
    Public Const MENU_H0330 As String = "H0330"           '売上取消
    Public Const MENU_H0340 As String = "H0340"           '売上参照
    Public Const MENU_H0350 As String = "H0350"           '売上利益一覧
    Public Const MENU_H0360 As String = "H0360"           '売上金・ＶＡＴ一覧

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
    Public Const MENU_H0750 As String = "H0750"           '当月購入在庫金額・VAT一覧

    Public Const MENU_H0810 As String = "H0810"           '受発注登録

    Public Const MENU_H0910 As String = "H0910"           '請求登録
    Public Const MENU_H0920 As String = "H0920"           '請求編集
    Public Const MENU_H0930 As String = "H0930"           '請求取消
    Public Const MENU_H0940 As String = "H0940"           '請求参照
    Public Const MENU_H0950 As String = "H0950"           '請求計算
    Public Const MENU_H0960 As String = "H0960"           '得意先別売掛金一覧
    Public Const MENU_H0970 As String = "H0970"           '回収予定期日別売掛金一覧

    Public Const MENU_H1010 As String = "H1010"           '入金登録
    Public Const MENU_H1020 As String = "H1020"           '入金取消
    Public Const MENU_H1030 As String = "H1030"           '入金参照

    Public Const MENU_H1110 As String = "H1110"           '買掛登録
    Public Const MENU_H1120 As String = "H1120"           '買掛編集
    Public Const MENU_H1130 As String = "H1130"           '買掛取消
    Public Const MENU_H1140 As String = "H1140"           '買掛参照
    Public Const MENU_H1150 As String = "H1150"           '仕入先別買掛金一覧
    Public Const MENU_H1160 As String = "H1160"           '支払予定期日別買掛金一覧

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
    Public Const MENU_M0180 As String = "M0180"           '勘定科目マスタ

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

    'フラグ等

    '無効フラグ
    Public Const FLAG_ENABLED As Integer = 0                  '有効
    Public Const FLAG_DISABLED As Integer = 1                 '無効

    '無効フラグ テキスト
    Public Const FLAG_ENABLED_TXT As String = "有効"         '有効
    Public Const FLAG_DISABLED_TXT As String = "無効"        '無効


    '請求区分
    Public Const BILLING_KBN_DEPOSIT As Integer = 1                         '前受請求
    Public Const BILLING_KBN_NORMAL As Integer = 2                          '通常請求

    '請求区分テキスト
    Public Const BILLING_KBN_DEPOSIT_TXT As String = "前受金請求"                         '前受請求
    Public Const BILLING_KBN_NORMAL_TXT As String = "通常請求"                          '通常請求

    '買掛区分
    Public Const APC_KBN_DEPOSIT As Integer = 1                         '前払買掛
    Public Const APC_KBN_NORMAL As Integer = 2                          '通常買掛

    '買掛区分テキスト
    Public Const APC_KBN_DEPOSIT_TXT As String = "前払金買掛"                         '前払買掛
    Public Const APC_KBN_NORMAL_TXT As String = "通常買掛"                          '通常買掛
    Public Const APC_KBN_DEPOSIT_TXT_E As String = "Prepaid"                         '前払買掛（英語表記）
    Public Const APC_KBN_NORMAL_TXT_E As String = "Normal"                          '通常買掛（英語表記）

    '仕入区分
    Public Const Sire_KBN_Sire As Integer = 1                           '仕入
    Public Const Sire_KBN_Zaiko As Integer = 2                          '在庫
    Public Const Sire_KBN_SERVICE As Integer = 3                        'サービス

    '仕入区分 テキスト
    Public Const Sire_KBN_Sire_TXT As String = "仕入"                           '仕入
    Public Const Sire_KBN_Zaiko_TXT As String = "在庫"                          '在庫
    Public Const Sire_KBN_SERVICE_TXT As String = "サービス"                    'サービス


    '取消区分
    Public Const CANCEL_KBN_ENABLED As Integer = 0                      '未取消
    Public Const CANCEL_KBN_DISABLED As Integer = 1                 '取消済

    '取消 テキスト
    Public Const CANCEL_KBN_DISABLED_TXT As String = "済"                '取消

    '言語
    Public Const LANG_KBN_JPN As String = "JPN"                         '日本語
    Public Const LANG_KBN_ENG As String = "ENG"                         '英語

    Public Const CANCEL_KBN_JPN_TXT As String = "済"                         '済
    Public Const CANCEL_KBN_ENG_TXT As String = "Del"                         'Del

    '検索系 開始日のデフォルト値
    Public Const SINCE_DEFAULT_DAY As Integer = -14                          '二週間

    Public Const DEADLINE_DEFAULT_DAY As Integer = 7                          '二週間

    'カルチャー JP
    Public Const CI_JP As String = "ja-JP"               'カルチャー 日本

    '固定キー
    Public Const FIXED_KEY_READTIME As String = "4"          'リードタイム単位
    Public Const FIXED_KEY_TRADE_TERMS As String = "5"          '貿易条件
    Public Const FIXED_KEY_PURCHASING_CLASS As String = "1002"       '仕入区分

    'メニュー ステータス
    Public Const STATUS_ADD As String = "ADD"          '登録モード
    Public Const STATUS_PRICE As String = "PRICE"          '仕入単価入力
    Public Const STATUS_EDIT As String = "EDIT"          '修正モード
    Public Const STATUS_CLONE As String = "CLONE"          '複写モード
    Public Const STATUS_VIEW As String = "VIEW"          '参照モード
    Public Const STATUS_CANCEL As String = "CANCEL"          '取消モード
    Public Const STATUS_ORDER_PURCHASE As String = "ORDER_PURCHASE"          '受発注登録モード
    Public Const STATUS_ORDER_NEW As String = "ORDER_NEW"          '受注登録モード
    Public Const STATUS_SALES As String = "SALES"          '売上登録・編集モード
    Public Const STATUS_GOODS_ISSUE As String = "GOODS_ISSUE"          '出庫登録・編集モード
    Public Const STATUS_ORDING As String = "ORDING"          '仕入登録・編集モード
    Public Const STATUS_RECEIPT As String = "RECEIPT"          '入庫登録・編集モード
    Public Const STATUS_BILL As String = "BILL"          '請求登録・編集モード
    Public Const STATUS_AP As String = "AP"          '売掛登録・編集モード

    '検索時のスタート初期値（年）
    Public Const SINCE_DEFAULT_YEAR As Integer = 2019                          '二週間

End Class

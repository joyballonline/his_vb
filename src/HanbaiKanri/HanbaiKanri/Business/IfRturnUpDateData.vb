'===================================================================================
'　 （システム名）      在庫計画システム
'
'   （機能名）          共通インターフェース（付属品）
'   （クラス名）        IfRturnUpDateData
'   （処理機能名）      
'   （備考）            親画面と子画面間の値渡しのためのインターフェース
'
'===================================================================================
' 履歴  名前                日付        マーク      内容
'-----------------------------------------------------------------------------------
'  (1)  橋本     　　       2010/10/22              新規
'　(2)  中澤                2010/11/17              変更(ZG620E及びZG621Eの項目「納期」削除対応)    
'  (3)  中澤                2010/12/02              変更(対象外が親画面に反映されないバグ修正)    
'-----------------------------------------------------------------------------------

Public Interface IfRturnUpDateData

    '修正した手配データを渡す
    '2010/12/02 update start Nakazawa---
    '2010/11/17 update start Nakazawa
    'Sub setUpDateData(ByVal prmKibou As String, ByVal prmNouki As String, ByVal prmTehaiSuuryou As String, _
    '                 ByVal prmTantyou As String, ByVal prmJousuu As String, ByVal prmSiyousyoNo As String)
    'Sub setUpDateData(ByVal prmKibou As String, ByVal prmTehaiSuuryou As String, ByVal prmTantyou As String, _
    '                                                    ByVal prmJousuu As String, ByVal prmSiyousyoNo As String)
    '2010/11/17 update end Nakazawa
    Sub setUpDateData(ByVal prmKibou As String, ByVal prmTehaiSuuryou As String, ByVal prmTantyou As String, _
                        ByVal prmJousuu As String, ByVal prmSiyousyoNo As String, ByVal prmTaisyogaiFlg As Boolean)
    '2010/12/02 update end Nakazawa---

    Sub myActivate()

    Sub myShow()

End Interface

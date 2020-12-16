CREATE OR REPLACE VIEW public.v_t31_urigdt_1
AS SELECT 
t31.会社コード,t30.売上番号,t30.売上番号枝番,
t31.受注番号,t31.受注番号枝番,
t31.行番号, t31.メーカー, t31.品名, t31.型式, t31.受注数量, t31.単位, 
t31.売上数量,
t11_get_pr(t31.会社コード,t31.受注番号,t31.受注番号枝番,t31.行番号) as 見積単価_外貨,
t31.仕入区分,
t31.入金番号
FROM t30_urighd t30, t31_urigdt t31
WHERE 
t30.会社コード=t31.会社コード
and t30.売上番号=t31.売上番号
and t30.売上番号枝番=t31.売上番号枝番
and t30.取消区分=0;

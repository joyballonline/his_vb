CREATE OR REPLACE VIEW public.v_t23_skyuhd_1
AS SELECT t23.会社コード,t23.受注番号,t23.受注番号枝番,t23.請求番号,t23.請求日,t23.得意先名,
t23.通貨,t23.客先番号,t23.請求金額計_外貨,t23.請求消費税計,t23.請求金額計,
t31.行番号 as 売上行番号,t31.仕入区分,t31.売上数量
FROM t23_skyuhd t23, t30_urighd t30, t31_urigdt t31 
WHERE t23.取消区分=0 
and t23.会社コード=t30.会社コード
and t23.受注番号=t30.受注番号
and t23.受注番号枝番=t30.受注番号枝番
and t30.会社コード=t31.会社コード
and t30.売上番号=t31.売上番号
and t30.売上番号枝番=t31.売上番号枝番
and t30.取消区分=0

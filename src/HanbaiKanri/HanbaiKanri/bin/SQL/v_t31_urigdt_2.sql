CREATE OR REPLACE VIEW public.v_t31_urigdt_2
AS SELECT 
t31.*,
t21_get_po(t31.会社コード,t31.受注番号,t31.受注番号枝番,t31.行番号) as 発注番号Z,
t10_get_salesman(t31.会社コード,t31.受注番号,t31.受注番号枝番) as 営業担当者,
t10_get_qt(t31.会社コード,t31.受注番号,t31.受注番号枝番) as 見積番号
FROM t31_urigdt t31;

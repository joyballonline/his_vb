CREATE OR REPLACE VIEW public.v_t70_inout
AS SELECT t70.*,
m90_get_inout_class(t70."会社コード", '1003'::character varying, t70."入出庫種別") AS "入出庫種別EN", 
m20_get_wh_name(t70.会社コード, t70.倉庫コード) AS 倉庫,
t43_get_supplier_name(t70.会社コード, t70.伝票番号, t70.行番号) as sup,
t44_get_customer_name(t70.会社コード, t70.伝票番号) as cust,
t43_get_rec_price_idr(t70.会社コード, t70.伝票番号, t70.行番号) as recpr,
t43_get_rec_price_idr(t70.会社コード, left(t70.出庫開始サイン,10), to_number('0'||substring(t70.出庫開始サイン,11,100), '000')) as isspr
FROM t70_inout t70 WHERE t70.取消区分='0' and coalesce(t70.引当区分,'0')='0';

-- Permissions

ALTER TABLE public.v_t70_inout OWNER TO postgres;
GRANT ALL ON TABLE public.v_t70_inout TO postgres;

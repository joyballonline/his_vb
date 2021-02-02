CREATE OR REPLACE FUNCTION public.t43_get_rec_price_idr(a_ character varying, b_ character varying, c_ numeric)
 RETURNS numeric
 LANGUAGE plpgsql
AS $function$
declare w1 varchar(100);
w2 t43_nyukodt.仕入値%type;
	begin

SELECT t43.仕入値 into w2 FROM t43_nyukodt t43 where t43.会社コード=a_ and t43.入庫番号=b_ and t43.行番号=c_;

		return w2;
	END;
$function$
;

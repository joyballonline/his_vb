CREATE OR REPLACE FUNCTION public.t10_get_cur(a_ character varying, b_ character varying, c_ character varying)
 RETURNS numeric
 LANGUAGE plpgsql
AS $function$
declare
w2 t10_cymnhd.通貨%type := 1;
	begin

SELECT t10.通貨 into w2 FROM t10_cymnhd t10 where t10.会社コード=a_ and t10.受注番号=b_ and t10.受注番号枝番=c_;
if w2 is null then
	w2 := 1;
end if;
		return w2;
	END;
$function$
;
CREATE OR REPLACE FUNCTION public.t11_get_pr(a_ character varying, b_ character varying, c_ character varying, d_ numeric)
 RETURNS numeric
 LANGUAGE plpgsql
AS $function$
declare
w2 t11_cymndt.見積単価_外貨%type;
	begin

SELECT t11.見積単価_外貨 into w2 FROM t11_cymndt t11 where t11.会社コード=a_ and t11.受注番号=b_ and t11.受注番号枝番=c_ and t11.行番号=d_;

		return w2;
	END;
$function$
;

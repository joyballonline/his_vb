CREATE OR REPLACE FUNCTION public.t20_get_custpo(a_ character varying, b_ character varying, c_ character varying)
 RETURNS character varying
 LANGUAGE plpgsql
AS $function$
declare
w2 t20_hattyu.客先番号%type;
	begin

SELECT t20.客先番号 into w2 FROM t20_hattyu t20 where t20.会社コード=a_ and t20.発注番号=b_ and t20.発注番号枝番=c_;

		return w2;
	END;
$function$
;
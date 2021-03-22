CREATE OR REPLACE FUNCTION public.t21_get_po(a_ character varying, b_ character varying, c_ character varying, d_ numeric)
 RETURNS character varying
 LANGUAGE plpgsql
AS $function$
declare
w2 varchar(20);
	begin

SELECT t20.発注番号||'-'||t20.発注番号枝番 into w2 FROM t20_hattyu t20, t21_hattyu t21 
where t20.会社コード=t21.会社コード and t20.発注番号=t21.発注番号 and t20.発注番号枝番=t21.発注番号枝番 
and t20.会社コード=a_ and t20.受注番号=b_ and t20.受注番号枝番=c_ and t21.行番号=d_ limit 1;

		return w2;
	END;
$function$
;
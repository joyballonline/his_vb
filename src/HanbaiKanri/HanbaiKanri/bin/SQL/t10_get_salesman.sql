CREATE OR REPLACE FUNCTION public.t10_get_salesman(a_ character varying, b_ character varying, c_ character varying)
 RETURNS character varying
 LANGUAGE plpgsql
AS $function$
declare
w2 t10_cymnhd.営業担当者%type;
	begin

SELECT t10.営業担当者 into w2 FROM t10_cymnhd t10 where t10.会社コード=a_ and t10.受注番号=b_ and t10.受注番号枝番=c_;

		return w2;
	END;
$function$
;
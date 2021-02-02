CREATE OR REPLACE FUNCTION public.t44_get_customer_name(a_ character varying, b_ character varying)
 RETURNS character varying
 LANGUAGE plpgsql
AS $function$
declare w1 varchar(100);
w2 t44_shukohd.得意先名%type;
	begin

SELECT t44.得意先名 into w2 FROM t44_shukohd t44 where t44.会社コード=a_ and t44.出庫番号=b_;

		return w2;
	END;
$function$
;
